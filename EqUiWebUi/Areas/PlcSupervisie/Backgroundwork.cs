using EQUICommunictionLib;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
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
            if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled("PlcSupervisie"))
            {
                Backgroundwork backgroundwork = new Backgroundwork();
                Hangfire.RecurringJob.AddOrUpdate("STO=>GADATA", () => backgroundwork.PushDatafromSTOtoGADATA(), Cron.MinuteInterval(5));
            }
            else
            {
                log.Warn("PlcSupervisie area is disabled removing hangfire jobs");
                RecurringJob.RemoveIfExists("STO=>GADATA");
            }
        }

        //main (does the jobs 1 by one
        [AutomaticRetry(Attempts = 0)]
        [Queue("sto")]
        public void PushDatafromSTOtoGADATA()
        {
            var jobId1 = BackgroundJob.Enqueue(() => HandleStoTable("ALARM_DATA_UB12",null));
            var jobId2 = BackgroundJob.ContinueWith(jobId1,() => HandleStoTable("ALARM_DATA_SUBASSY", null));
            var jobId3 = BackgroundJob.ContinueWith(jobId2, () => HandleStoTable("ALARM_DATA_BODY_SIDES", null));
            var jobId4 = BackgroundJob.ContinueWith(jobId3, () => HandleStoTable("ALARM_DATA_PREASSY", null));
            var jobId5 = BackgroundJob.ContinueWith(jobId4, () => NormalizeSTOdata());
            var jobId6 = BackgroundJob.ContinueWith(jobId5, () => ClassificationOfSTOdata());
        }

        //update new data from STO to gadata for a specifc table . 
        [AutomaticRetry(Attempts = 2)]
        [Queue("sto")]
        public void HandleStoTable(string TargetTable, PerformContext context) 
        {
            ConnectionManager connectionManager = new ConnectionManager();
            //get last record in GADATA 
            string gadataGetMaxTimestampQry = string.Format(@"select max(_timestamp) as 'ts' FROM STO.h_breakdown
                                                            left join STO.c_StoTable on c_StoTable.id = h_breakdown.c_stotable_id
                                                            where c_StoTable.StoTable = '{0}'", TargetTable);
            DataTable dtGadataMaxTS = connectionManager.RunQuery(gadataGetMaxTimestampQry);
            //handel empty table copy last 30 days
            DateTime GadataMAxTs = System.DateTime.Now.AddDays(-30);
            if (dtGadataMaxTS.Rows.Count != 0)
            {
                GadataMAxTs = dtGadataMaxTS.Rows[0].Field<DateTime>("ts");
                context.WriteLine("GadataMAxTs: " + GadataMAxTs.ToString());
            }
            else
            {
                string msg = String.Format("TargetTable: {0} had no data so full refresh", TargetTable);
                context.WriteLine(msg);
                log.Error(msg);
            }
            //get new records from STO
            string stoQry = string.Format(@"
                                        SELECT 
                                           {1}.*
                                        , '{1}' StoTable 
                                        FROM STO_SYS.{1}
                                        WHERE CHANGETS > TO_TIMESTAMP('{0}', 'YYYY/MM/DD HH24:MI:SS')
"
                , GadataMAxTs.ToString("yyyy-MM-dd HH:mm:ss"),TargetTable); //USE BIG HH for 24 hour format !!!!

            context.WriteLine("Get new data from DST started");
            DataTable newStoDt = connectionManager.RunQuery(stoQry,dbName:"DST", enblExeptions: true);
            context.WriteLine(String.Format("Done New rows: {0}", newStoDt.Rows.Count));
            //push to gadata
            context.WriteLine("Push to gadata started");
            connectionManager.BulkCopy(newStoDt, "[STO].[rt_error]");
            context.WriteLine("Done");
        }

        [AutomaticRetry(Attempts = 2)]
        [Queue("sto")]
        public void NormalizeSTOdata()
        {
            ConnectionManager connectionManager = new ConnectionManager();
           connectionManager.RunCommand("EXEC STO.[sp_update_L]", enblExeptions: true);
        }

        [AutomaticRetry(Attempts = 2)]
        [Queue("sto")]
        public void ClassificationOfSTOdata()
        {
            //stupid that I need to spin up all these classes to get it to run... (temp solution)
            EqUiWebUi.Controllers.ClassificationController classificationController = new EqUiWebUi.Controllers.ClassificationController();
            EqUiWebUi.Models.c_LogClassRules c_LogClassRule = new EqUiWebUi.Models.c_LogClassRules();
            EqUiWebUi.Models.GADATAEntitiesEQUI db = new EqUiWebUi.Models.GADATAEntitiesEQUI();
            c_LogClassRule.c_logClassSystem_id = db.c_logClassSystem.Where(c => c.Name == "DBI_STO").First().id;
            c_LogClassRule.id = 0; //this causes us to run all rules
            classificationController.RunRule(c_LogClassRule,overrideManualSet: false, Clear: false, UPDATE: false);

            //update supervisie databuffer
            //context.WriteLine("Supervisie dataC3G"); 
            Gadata.Models.GADATAEntities2 GADATAEntities2 = new Gadata.Models.GADATAEntities2();
            EqUiWebUi.Areas.Gadata.DataBuffer.dataSTO = GADATAEntities2.STO_Supervisie.Select(x => new EqUiWebUi.Areas.Gadata.SupervisieDummy() {
                 Location= x.Location
                ,logtext = x.logtext
                ,RT = x.RT
                ,DT = x.DT
                ,Classification = x.Classification
                ,Subgroup = x.Subgroup
                ,Severity = x.Severity
                ,Logcode = x.Logcode
                ,Logtype = x.Logtype
                ,refId = x.refId
                ,timestamp = x.timestamp
                ,LocationTree = x.LocationTree
                ,ClassTree = x.ClassTree
                ,Vyear = x.Vyear
                ,Vweek = x.Vweek
                ,Vday = x.Vday
                ,shift = x.shift
                ,animation = x.Logtype
            }).Where(x => x.timestamp > EqUiWebUi.Areas.Gadata.DataBuffer.EndDate).ToList();
            //context.WriteLine(DataBuffer.dataC3G.Count());
        }
    }
}