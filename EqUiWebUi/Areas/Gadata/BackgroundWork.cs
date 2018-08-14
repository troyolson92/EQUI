using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Hangfire;
using EQUICommunictionLib;
using Hangfire.Server;
using Hangfire.Console;

namespace EqUiWebUi.Areas.Gadata
{
    //databuffer for supervisie system
    public static class DataBuffer
    {
        //
        public static List<EqUiWebUi.Areas.Gadata.SupervisieDummy> Supervisie { get; set; }
        public static DateTime SupervisieLastDt { get; set; }
        //
        public static List<EqUiWebUi.Areas.Gadata.Models.PloegRaport_Result> Ploegreport { get; set; }
        public static DateTime PloegreportLastDt { get; set; }
    }

    //dummy object for supervision data
    public partial class SupervisieDummy
    {
        public string Location { get; set; }
        public string logtext { get; set; }
        public Nullable<int> RT { get; set; }
        public Nullable<int> DT { get; set; }
        public string time { get; set; }
        public string Classification { get; set; }
        public string Subgroup { get; set; }
        public string Severity { get; set; }
        public string Logcode { get; set; }
        public string Logtype { get; set; }
        public int refId { get; set; }
        public System.DateTime timestamp { get; set; }
        public string LocationTree { get; set; }
        public Nullable<int> ClassTree { get; set; }
    }


    public class BackgroundWork
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            public void configHangfireJobs()
            {
                if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled("Gadata"))
                {
                    BackgroundWork backgroundwork = new BackgroundWork();
                    //**********************************Ploegreport table***************************************************
                    //set job to refresh every 5 minutes
                    RecurringJob.AddOrUpdate("BT_PloegReport", () => backgroundwork.UpdatePloegreport(), Cron.MinuteInterval(5));
                    //**********************************Supervisie table***************************************************
                    //set job to refresh every minute
                    RecurringJob.AddOrUpdate("BT_Supervis", () => backgroundwork.UpdateSupervisie(null), Cron.Minutely);
                    //**********************************normalize***************************************************
                    //set job to refresh every minute
                    RecurringJob.AddOrUpdate("norm_C3G", () => backgroundwork.norm_c3g(null), Cron.Minutely);
                    RecurringJob.AddOrUpdate("norm_C4G", () => backgroundwork.norm_c4g(null), Cron.Minutely);
                    RecurringJob.AddOrUpdate("norm_NGAC", () => backgroundwork.norm_NGAC(null), Cron.Minutely);
                    RecurringJob.AddOrUpdate("norm_1day", () => backgroundwork.norm_1day(null), Cron.Daily);
                }
                else
                {
                    log.Warn("Gadata area is disabled removing hangfire jobs");
                    RecurringJob.RemoveIfExists("BT_PloegReport");
                    RecurringJob.RemoveIfExists("BT_Supervis");
                    RecurringJob.RemoveIfExists("norm_C3G");
                    RecurringJob.RemoveIfExists("norm_C4G");
                    RecurringJob.RemoveIfExists("norm_NGAC");
                    RecurringJob.RemoveIfExists("norm_1day");
                }
            }

            //update the local datatable with ploeg rapport called every minute #hangfire
            [Queue("gadata")]
            [AutomaticRetry(Attempts = 0)]
            public void UpdatePloegreport()
            {
                EqUiWebUi.Areas.Gadata.Models.GADATAEntities2 gADATAEntities = new EqUiWebUi.Areas.Gadata.Models.GADATAEntities2();
                gADATAEntities.Database.CommandTimeout = 60; // normally 30 but this one is heavy
                List<EqUiWebUi.Areas.Gadata.Models.PloegRaport_Result> data = (from ploegrapport in gADATAEntities.PloegRaport
                                    (startDate: null,
                                       endDate: null,
                                       daysBack: null,
                                       assets: "%",
                                       locations: "%",
                                       lochierarchy: "%",
                                       minDowntime: 20,
                                       minCountOfDowtime: 3,
                                       minCountofWarning: 4)
                                       select ploegrapport).ToList();

                if (data.Count != 0)
                {
                    DataBuffer.Ploegreport = data;

                    DateTime maxDate = data
                        .Where(r => Convert.ToDateTime(r.timestamp) < System.DateTime.Now)
                        // .Where(r => r.Logtype.Contains("Ruleinfo") == false)
                        .Select(r => Convert.ToDateTime(r.timestamp))
                        .Max();

                    DataBuffer.PloegreportLastDt = maxDate;

                    //add singal R 
                    var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<DataRefreshHub>();
                    context.Clients.Group("Ploegrapport").newData();
                }
                else
                {
                    log.Error("UpdatePloegreport did not return any data");
                }
            }

            //update the local datatable with supervisie called every minute #hangfire
            [Queue("gadata")]
            [AutomaticRetry(Attempts = 0)]
            public void UpdateSupervisie(PerformContext context)
            {
                List<EqUiWebUi.Areas.Gadata.SupervisieDummy> data = new List<EqUiWebUi.Areas.Gadata.SupervisieDummy>();
            /*
                context.WriteLine("dataEQUI");
                //get data from EQUI schema (this should only contain the timeline)
                EqUiWebUi.Models.GADATAEntitiesEQUI GADATAEntitiesEQUI = new EqUiWebUi.Models.GADATAEntitiesEQUI();
                List<EqUiWebUi.Models.Supervisie> dataEQUI = (from supervis in GADATAEntitiesEQUI.Supervisie select supervis).ToList();
                context.WriteLine(dataEQUI.Count());
                data.AddRange(dataEQUI.Cast<EqUiWebUi.Areas.Gadata.SupervisieDummy>());
                */
                
                context.WriteLine("dataALERT");
                //get data from alert schema 
                Alert.Models.GADATA_AlertModel GADATA_AlertModel = new Alert.Models.GADATA_AlertModel();
                List<Alert.Models.Alerts_Supervisie> dataALERT = (from supervis in GADATA_AlertModel.Alerts_Supervisie select supervis).ToList();
                context.WriteLine(dataALERT.Count());
                data.AddRange(dataALERT.Cast<EqUiWebUi.Areas.Gadata.SupervisieDummy>());
               
                context.WriteLine("dataVASC");
                //get data from NGAC schema 
                VASC.Models.GADATAEntitiesVASC gADATAEntitiesVASC = new VASC.Models.GADATAEntitiesVASC();
                List<EqUiWebUi.Areas.VASC.Models.NGAC_Supervisie> dataVASC = (from supervis in gADATAEntitiesVASC.NGAC_Supervisie select supervis).ToList();
                context.WriteLine(dataVASC.Count());
                data.AddRange(dataVASC.Cast<EqUiWebUi.Areas.Gadata.SupervisieDummy>());

            /*
             *     
     var query = GetQuery().Select(x => new VehicleDTO(){ ID = c.ID, Number = c.Number });

    if (includeContractorVehicles)
    {
        WorkerRepository rep = new WorkerRepository();
        var contractorsVehicles = rep.GetWirkers().
            Select(x => new VehicleDTO(){ Number = x.ContractorVehicleNumber});
        query = query.Union(contractorsVehicles);
    }*/
    


            if (data.Count != 0)
                {
                    DataBuffer.Supervisie = data;

                    DateTime maxDate = data
                                .Where(r => r != null)
                                .Select(r => r.timestamp)
                                .Max();

                    DataBuffer.SupervisieLastDt = maxDate;

                    //add singal R 
                    var SingnalRcontext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<DataRefreshHub>();
                    SingnalRcontext.Clients.Group("Supervisie").newData();
                }
                else
                {
                    log.Error("UpdateSupervisie did not return any data");
                }
            }

        //run C3G normalisation steps.
        [Queue("gadata")]
        [AutomaticRetry(Attempts = 0)]
        public void norm_c3g(PerformContext context)
        {
            context.WriteLine(" runRule started");
            //stupid that I need to spin up all these classes to get it to run... (temp solution)
            EqUiWebUi.Controllers.ClassificationController classificationController = new EqUiWebUi.Controllers.ClassificationController();
            EqUiWebUi.Models.c_LogClassRules c_LogClassRule = new EqUiWebUi.Models.c_LogClassRules();
            EqUiWebUi.Models.GADATAEntitiesEQUI db = new EqUiWebUi.Models.GADATAEntitiesEQUI();
            c_LogClassRule.c_logClassSystem_id = db.c_logClassSystem.Where(c => c.Name == "VCSC_C3G").First().id;
            c_LogClassRule.id = 0; //this causes us to run all rules
            classificationController.RunRule(c_LogClassRule, overrideManualSet: false, Clear: false, UPDATE: false);
            context.WriteLine(" runRule done");
            //
            ConnectionManager connectionManager = new ConnectionManager();
            string[] cmds = { "exec C3G.sp_update_L"
                    ,"exec C3G.sp_L_breakdown"
            };

            foreach(string cmd in cmds)
            {
                context.WriteLine(" "+cmd);
                connectionManager.RunCommand(cmd, enblExeptions: true, maxEXECtime: 300);
                context.WriteLine(" Done");
            }
        }

        //run c4g normalisation steps.
        [Queue("gadata")]
        [AutomaticRetry(Attempts = 0)]
        public void norm_c4g(PerformContext context)
        {
            context.WriteLine(" runRule started");
            //stupid that I need to spin up all these classes to get it to run... (temp solution)
            EqUiWebUi.Controllers.ClassificationController classificationController = new EqUiWebUi.Controllers.ClassificationController();
            EqUiWebUi.Models.c_LogClassRules c_LogClassRule = new EqUiWebUi.Models.c_LogClassRules();
            EqUiWebUi.Models.GADATAEntitiesEQUI db = new EqUiWebUi.Models.GADATAEntitiesEQUI();
            c_LogClassRule.c_logClassSystem_id = db.c_logClassSystem.Where(c => c.Name == "VCSC_C4G").First().id;
            c_LogClassRule.id = 0; //this causes us to run all rules
            classificationController.RunRule(c_LogClassRule, overrideManualSet: false, Clear: false, UPDATE: false);
            context.WriteLine(" runRule done");
            //
            ConnectionManager connectionManager = new ConnectionManager();
            string[] cmds = { "exec [C4G].[sp_update_L]"
                    ,"exec [C4G].sp_Update_L_breakdown"
                    ,"exec [C4G].sp_ReClass_L_breakdown"
                    ,"exec [Volvo].[LiveView]"
            };

            foreach (string cmd in cmds)
            {
                context.WriteLine(" " + cmd);
                connectionManager.RunCommand(cmd, enblExeptions: true, maxEXECtime: 300);
                context.WriteLine(" Done");
            }
        }

        //run NGAC normalisation steps.
        [Queue("gadata")]
        [AutomaticRetry(Attempts = 0)]
        public void norm_NGAC(PerformContext context)
        {
            context.WriteLine(" runRule started");
            //stupid that I need to spin up all these classes to get it to run... (temp solution)
            EqUiWebUi.Controllers.ClassificationController classificationController = new EqUiWebUi.Controllers.ClassificationController();
            EqUiWebUi.Models.c_LogClassRules c_LogClassRule = new EqUiWebUi.Models.c_LogClassRules();
            EqUiWebUi.Models.GADATAEntitiesEQUI db = new EqUiWebUi.Models.GADATAEntitiesEQUI();
            c_LogClassRule.c_logClassSystem_id = db.c_logClassSystem.Where(c => c.Name == "VASC_NGAC").First().id;
            c_LogClassRule.id = 0; //this causes us to run all rules
            classificationController.RunRule(c_LogClassRule, overrideManualSet: false, Clear: false, UPDATE: false);
            context.WriteLine(" runRule done");
            //
            ConnectionManager connectionManager = new ConnectionManager();
            string[] cmds = { "EXEC [NGAC].[sp_update_cleanLogteksts]"
            };

            foreach (string cmd in cmds)
            {
                context.WriteLine(" " + cmd);
                connectionManager.RunCommand(cmd, enblExeptions: true, maxEXECtime: 300);
                context.WriteLine(" Done");
            }
        }


        //run NGAC normalisation steps.
        [Queue("gadata")]
        [AutomaticRetry(Attempts = 5)]
        public void norm_1day(PerformContext context)
        {
            ConnectionManager connectionManager = new ConnectionManager();
            string[] cmds = { "exec C3G.sp_Housekeeping"
                    ,"exec c4g.sp_Housekeeping"
                    ,"exec NGAC.sp_Housekeeping"
            };

            foreach (string cmd in cmds)
            {
                context.WriteLine(" " + cmd);
                connectionManager.RunCommand(cmd, enblExeptions: true, maxEXECtime: 300);
                context.WriteLine(" Done");
            }
        }
    }
}
 