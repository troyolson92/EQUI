using System.Data;
using EQUICommunictionLib;
using System;
using System.Linq;
using Hangfire;

namespace EqUiWebUi
{
    public static class DataBuffer
    {
        public static DataTable Tipstatus { get; set; }
        public static DateTime TipstatusLastDt { get; set; }
    }

  
	public class Backgroundwork
	{
        //update the local datatable with tipstatus called every minute #hangfire
		public void UpdateTipstatus()
		{
			GadataComm gadataComm = new GadataComm();
			DataTable dt = gadataComm.RunQueryGadata(
                @"SELECT [controller_name] as 'Robot'
                          ,[Date Time]
                          ,[Dress_Num] as 'nDress'
                          ,[Weld_Counter] as 'nWelds'
	                      ,CASE 
		                      WHEN [Wear_Fixed] > [Wear_Move] THEN ROUND(([Wear_Fixed]  * ([Max_Wear_Fixed]/100))*100,0)
		                      ELSE ROUND(([Wear_Move] * (Max_Wear_Move/100))*100,0)
		                      END '%Wear'
                          ,CASE 
	                          WHEN [ESTremainingspotsFixed] > [ESTremainingspotsMove] THEN [ESTremainingspotsFixed]
		                      ELSE [ESTremainingspotsMove]
		                      END 'nRspots'
                          ,CASE 
	                          WHEN [ESTremainingCarsFixed] > [ESTremainingsCarsMove] THEN [ESTremainingCarsFixed]
		                      ELSE [ESTremainingsCarsMove]
		                      END 'nRcars'
                      FROM [GADATA].[NGAC].[TipwearLast]
                      where [Date Time] < getdate()
                      Order by [%Wear] DESC");
			if (dt.Rows.Count != 0 )
			{
                
                DataBuffer.Tipstatus = dt;

                DateTime maxDate = dt.AsEnumerable()
                            .Select(r => r.Field<DateTime>("Date Time"))
                            .Max();

                DataBuffer.TipstatusLastDt = maxDate;

            }
			else
			{
				// send message that something failed s
			}
		}

        //check if there are snapshots that need to be run. Called every minute #hangfire.
        //if work is needed this wil fire and forget the work.
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
                    gadataComm.RunCommandGadata(cmd);
                }
            }
            else
            {
                //nowork to do
            }
        }

        public void DoSnapshot(int id, string query)
        {
            GadataComm gadataComm = new GadataComm();
            MaximoComm maximoComm = new MaximoComm();
            //run maximo query
            DataTable maximoResult = maximoComm.oracle_runQuery(query);
            string htmlResult = ConvertDataTableToHTML(maximoResult);
            //insert in gadata
            gadataComm.InsertSnaphotGadata(id, htmlResult);
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
    }
}