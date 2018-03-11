using System.Data;
using EQUICommunictionLib;
using System;
using System.Linq;
using Hangfire;
using EqUiWebUi.Models;

using System.Collections.Generic;
using EqUiWebUi.Areas.Gadata;

namespace EqUiWebUi
{
	public static class DataBuffer
	{
		//
		public static List<TipMonitor> Tipstatus { get; set; }
		public static DateTime TipstatusLastDt { get; set; }

    }

	public class Backgroundwork
	{
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// configure standard jobs to hangfire
        /// </summary>
        public void configHangfireJobs()
        {
            //background work
            Backgroundwork backgroundwork = new Backgroundwork();
            //**********************************TipDresData table***************************************************
            //set job to refresh every minute
            RecurringJob.AddOrUpdate(() => backgroundwork.UpdateTipstatus(), Cron.Minutely);  


            //**********************************snapshot system****************************************************
            //check every minute for new jobs 
            RecurringJob.AddOrUpdate(() => backgroundwork.HandleMaximoSnapshotWork(), Cron.Minutely);
            //**********************************STO****************************************************************
            //check every minute for new data (hystorian)
            //RecurringJob.AddOrUpdate(() => backgroundwork.PushDatafromSTOtoGADATA(), Cron.MinuteInterval(5));
            //**********************************STW040 BI rapoort****************************************************
            RecurringJob.AddOrUpdate(() => backgroundwork.PushDatafromSTW040toGADATA(), Cron.HourInterval(1));
            //**********************************MX7 *****************************************************************
            //BI rapport
            //RecurringJob.AddOrUpdate(() => backgroundwork.PushDatafromMX7toGADATA(), Cron.HourInterval(1));
            //reporting DB 
            RecurringJob.AddOrUpdate(() => backgroundwork.PushDatafromMAXIMOtoGADATA(), Cron.HourInterval(1));
            //**********************************Tableau buffers******************************************************
            RecurringJob.AddOrUpdate(() => backgroundwork.UpdateTableauBuffers(), Cron.MinuteInterval(20));
        }

     
        //update the local datatable with tipstatus called every minute #hangfire
		[AutomaticRetry(Attempts = 0)]
        [DisableConcurrentExecution(50)] //locks the job from starting multible times if other one stil running.
        public void UpdateTipstatus()
		{
			GADATAEntities gADATAEntities = new GADATAEntities();
			List<TipMonitor> data = (from tipstatus in gADATAEntities.TipMonitor
									 select tipstatus).ToList();

			if (data.Count != 0)
			{
				DataBuffer.Tipstatus = data;

				DateTime maxDate = data
							.Where(r => r != null)
							.Select(r => r.Date_Time.Value)
							.Max();

				DataBuffer.TipstatusLastDt = maxDate;
                log.Info(string.Format("UpdateTipstatus {0} records", data.Count));

                //add singal R 
                var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<DataRefreshHub>();
                context.Clients.Group("TipstatusGrid").newData();
            }
			else
			{
                log.Error("UpdateTipstatus did not return any data");
			}
		}

        //check if there are snapshots that need to be run. Called every minute #hangfire.
        //if work is needed this wil fire and forget the work.
        [AutomaticRetry(Attempts = 0)]
        [DisableConcurrentExecution(50)] //timeout
        public void HandleMaximoSnapshotWork()
		{
			GadataComm gadataComm = new GadataComm();
			//query to get work that needs to be run
			string qryGetwork =
				  @"SELECT             
				   h.[id]
				  ,h.[queryId]
				  ,h.[timestamp]
				  ,h.[hangfire]
				  ,c.query
				  ,c.name
			  FROM [GADATA].[EqUi].[h_querySnapshots] as h
			  left join gadata.equi.[c_querySnapshots] as c on c.id = h.[queryId] 
			  where h.[timestamp] < getdate() AND h.[hangfire] is null";
		
			DataTable dtWork = gadataComm.RunQueryGadata(qryGetwork);
			//loop work
			if (dtWork.Rows.Count != 0)
			{
				foreach(DataRow row in dtWork.Rows)
				{
					//hand of job to queue
					BackgroundJob.Enqueue(() => DoSnapshot(row.Field<int>("id"), row.Field<string>("query")));
					//tick of job start on gadata
					string cmd = string.Format(
						@"UPDATE [GADATA].[EqUi].[h_querySnapshots]
						SET Hangfire = 1 
						FROM [GADATA].[EqUi].[h_querySnapshots] 
						WHERE id = {0}
						", row.Field<int>("id"));
					gadataComm.RunCommandGadata(cmd,true);
				}
			}
			else
			{
                log.Debug("HandleMaximoSnapshotWork no work to do");
			}
		}

		public void DoSnapshot(int id, string query)
		{
			GadataComm gadataComm = new GadataComm();
			MaximoComm maximoComm = new MaximoComm();
			//run maximo query
			DataTable maximoResult = maximoComm.Oracle_runQuery(query);
			//check result valid
			if (maximoResult.Rows.Count < 1)
			{
				throw new System.ArgumentException("datatable result invallid.");
			}
			else
			{
				string htmlResult = ConvertDataTableToHTML(maximoResult);
				//insert in gadata
				gadataComm.InsertSnaphotGadata(id, htmlResult);
			}
		}

		public static string ConvertDataTableToHTML(DataTable dt)
		{
			string html = "<table>";
			//add header row
			html += "<tr>";
			for (int i = 0; i < dt.Columns.Count; i++)
				html += "<td>" + dt.Columns[i].ColumnName + "</td>";
			html += "</tr>";
			//add rows
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				html += "<tr>";
				for (int j = 0; j < dt.Columns.Count; j++)
					html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
				html += "</tr>";
			}
			html += "</table>";
			return html;
		}


		//update new data from STO to gadata. called every minute #hangfire
		[AutomaticRetry(Attempts = 0)]
		[DisableConcurrentExecution(60*5)] //locks the job from starting multible times if other one stil running.
		public void PushDatafromSTOtoGADATA()
		{
			GadataComm lGadataComm = new GadataComm();
			//get last record in GADATA 
			string gadataGetMaxTimestampQry = "select TOP 1 _timestamp FROM GADATA.STO.h_breakdown order by _timestamp desc ";
			DataTable dtGadataMaxTS= lGadataComm.RunQueryGadata(gadataGetMaxTimestampQry);
			//handel empty table
			DateTime GadataMAxTs = DateTime.Parse("1900-01-01 00:00:00");
			if (dtGadataMaxTS.Rows.Count != 0)
			{
				GadataMAxTs = dtGadataMaxTS.Rows[0].Field<DateTime>("_timestamp");
			}
			//get new records from STO
StoComm lStoComm = new StoComm();
			string stoQry = string.Format(@"
SELECT * FROM
(
SELECT ALARM_DATA_UB12.*, 'ALARM_DATA_UB12' stoTable FROM STO_SYS.ALARM_DATA_UB12
UNION
SELECT ALARM_DATA_SUBASSY.*,'ALARM_DATA_SUBASSY' stoTable FROM STO_SYS.ALARM_DATA_SUBASSY
UNION
SELECT ALARM_DATA_BODY_SIDES.*,'ALARM_DATA_BODY_SIDES' stoTable FROM STO_SYS.ALARM_DATA_BODY_SIDES
UNION
SELECT ALARM_DATA_SUBASSY.*,'ALARM_DATA_SUBASSY' stoTable FROM STO_SYS.ALARM_DATA_SUBASSY
) WHERE CHANGETS > TO_TIMESTAMP('{0}', 'YYYY/MM/DD HH24:MI:SS') AND ALARMSTATUS = 1
"
				, GadataMAxTs.ToString("yyyy-MM-dd HH:mm:ss")); //USE BIG HH for 24 hour format !!!!

			DataTable newStoDt = lStoComm.oracle_runQuery(stoQry);
			//push to gadata
			lGadataComm.BulkCopyToGadata("STO", newStoDt, "rt_error");
			//trigger normalisation make new gadatacom and use Admin powers 
			GadataComm gadataComm = new GadataComm();
			gadataComm.RunCommandGadata("EXEC GADATA.STO.[sp_update_L]", true);
			//trigger classification
			//   lGadataComm.RunCommandGadata("EXEC GADATA.STO.[sp_update_Lerror_classifcation]", true);
			//fire and forget to init
		}

		//update new data from STO to gadata. called every minute #hangfire
		[AutomaticRetry(Attempts = 0)]
		public void PushDatafromMAXIMOtoGADATA()
		{
			//delete data in now in maximo.
			GadataComm lGadataComm = new GadataComm();
			lGadataComm.RunCommandGadata("DELETE GADATA.MAXIMO.WORKORDERS FROM GADATA.MAXIMO.WORKORDERS",true);

			//get new records from STO
			MaximoComm maximoComm = new MaximoComm();
			string MaximoQry = string.Format(@"
select 
 WORKORDER.WONUM WONUM
,WORKORDER.STATUS
,WORKORDER.STATUSDATE
,WORKORDER.WORKTYPE
,WORKORDER.DESCRIPTION
,WORKORDER.LOCATION
,WORKORDER.REPORTEDBY
,WORKORDER.REPORTDATE
,locancestor.ANCESTOR
from MAXIMO.WORKORDER WORKORDER
join MAXIMO.locancestor locancestor on 
locancestor.LOCATION = WORKORDER.LOCATION
and 
locancestor.ORGID = 'VCCBE'

and
workorder.changedate >= sysdate - 200
and
workorder.worktype = 'CI'
and 
locancestor.ANCESTOR LIKE 'A LIJN%'

ORDER BY WORKORDER.STATUSDATE DESC
"
);

			DataTable newMaximoDt = maximoComm.Oracle_runQuery(MaximoQry);
			//push to gadata
			lGadataComm.BulkCopyToGadata("MAXIMO", newMaximoDt, "WORKORDERS");
		}

		//update new data from STW040 (BI RAPPORT) to gadata. called every minute #hangfire
		[AutomaticRetry(Attempts = 0)]
		public void PushDatafromSTW040toGADATA()
		{
			stw040Sync lstw040Sync = new stw040Sync();
			lstw040Sync.get_swt040data();
		}

		//update new data from MX7 (BI RAPPORT) to gadata. called every minute #hangfire
		[AutomaticRetry(Attempts = 0)]
		public void PushDatafromMX7toGADATA()
		{
			mx7Sync mx7Sync = new mx7Sync();
			mx7Sync.get_mx7data();
		}

		//update tableau buffers. called every 20minutes #hangfire
		[AutomaticRetry(Attempts = 0)]
		[DisableConcurrentExecution(60*2)] //locks the job from starting multible times if other one stil running.
		public void UpdateTableauBuffers()
		{
			GadataComm lGadataComm = new GadataComm();
			//
			lGadataComm.RunCommandGadata(@"EXEC [GADATA].[Tableau].[Updatebuffers]", true);
		}
	}
}