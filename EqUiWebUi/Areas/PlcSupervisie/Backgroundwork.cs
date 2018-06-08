using EQUICommunictionLib;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.PlcSupervisie
{
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
            //check every minute for new data (hystorian)
            Hangfire.RecurringJob.AddOrUpdate("STO=>GADATA", () => backgroundwork.PushDatafromSTOtoGADATA(), Cron.MinuteInterval(5));
        }

        //main (does the jobs 1 by one
        [AutomaticRetry(Attempts = 0)]
        [Queue("sto")]
        public void PushDatafromSTOtoGADATA()
        {
            var jobId1 = BackgroundJob.Enqueue(() => HandleStoTable("ALARM_DATA_UB12"));
            var jobId2 = BackgroundJob.ContinueWith(jobId1,() => HandleStoTable("ALARM_DATA_SUBASSY"));
            var jobId3 = BackgroundJob.ContinueWith(jobId2, () => HandleStoTable("ALARM_DATA_BODY_SIDES"));
            var jobId4 = BackgroundJob.ContinueWith(jobId3, () => HandleStoTable("ALARM_DATA_PREASSY"));
            var jobId5 = BackgroundJob.ContinueWith(jobId4, () => NormalizeSTOdata());
        }

        //update new data from STO to gadata for a specifc table . 
        [AutomaticRetry(Attempts = 2)]
        [Queue("sto")]
        public void HandleStoTable(string TargetTable) 
        {
            GadataComm lGadataComm = new GadataComm();
            //get last record in GADATA 
            string gadataGetMaxTimestampQry = string.Format(@"select max(_timestamp) as 'ts' FROM GADATA.STO.h_breakdown
                                                            left join GADATA.STO.c_StoTable on c_StoTable.id = h_breakdown.c_stotable_id
                                                            where c_StoTable.StoTable = '{0}'", TargetTable);
            DataTable dtGadataMaxTS = lGadataComm.RunQueryGadata(gadataGetMaxTimestampQry);
            //handel empty table copy last 30 days
            DateTime GadataMAxTs = System.DateTime.Now.AddDays(-30);
            if (dtGadataMaxTS.Rows.Count != 0)
            {
                GadataMAxTs = dtGadataMaxTS.Rows[0].Field<DateTime>("ts");
            }
            else
            {
                log.Error(String.Format("TargetTable: {0} had no data so full refresh", TargetTable));
            }
            //get new records from STO
            StoComm lStoComm = new StoComm();
            string stoQry = string.Format(@"
                                        SELECT 
                                           {1}.*
                                        , '{1}' StoTable 
                                        FROM STO_SYS.{1}
                                        WHERE CHANGETS > TO_TIMESTAMP('{0}', 'YYYY/MM/DD HH24:MI:SS')
"
                , GadataMAxTs.ToString("yyyy-MM-dd HH:mm:ss"),TargetTable); //USE BIG HH for 24 hour format !!!!

            DataTable newStoDt = lStoComm.Oracle_runQuery(stoQry, enblExeptions: true);
            log.Debug(String.Format("TargetTable: {0}  Records: {1}", TargetTable, newStoDt.Rows.Count));
            //push to gadata
            lGadataComm.BulkCopyToGadata("STO", newStoDt, "rt_error");
        }

        [AutomaticRetry(Attempts = 2)]
        [Queue("sto")]
        public void NormalizeSTOdata()
        {
           log.Debug("Normalisation started");
           GadataComm gadataComm = new GadataComm();
           gadataComm.RunCommandGadata("EXEC GADATA.STO.[sp_update_L]", runAsAdmin: true, enblExeptions: true);
    
            //trigger classification
            //   lGadataComm.RunCommandGadata("EXEC GADATA.STO.[sp_update_Lerror_classifcation]", true);
            //fire and forget to init

        }
    }
}