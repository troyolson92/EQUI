﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Hangfire;
using EQUICommunictionLib;
using Hangfire.Server;
using Hangfire.Console;
using System.Configuration;

namespace EqUiWebUi.Areas.Supervision
{
    //databuffer for supervisie system
    public static class DataBuffer
    {
        //
        public static List<EqUiWebUi.Areas.Supervision.SupervisieDummy> Supervisie { get; set; }
        public static List<EqUiWebUi.Areas.Supervision.SupervisieDummy> dataALERT { get; set; }
        public static List<EqUiWebUi.Areas.Supervision.SupervisieDummy> dataVASC { get; set; }
        public static bool _isRunningUpdateNGAC;
        public static List<EqUiWebUi.Areas.Supervision.SupervisieDummy> dataC3G { get; set; }
        public static List<EqUiWebUi.Areas.Supervision.SupervisieDummy> dataC4G { get; set; }
        public static List<EqUiWebUi.Areas.Supervision.SupervisieDummy> dataS4C { get; set; }
        public static List<EqUiWebUi.Areas.Supervision.SupervisieDummy> dataSTO { get; set; }
        public static bool _isRunningNormalizeSTO;
        public static bool _isRunningUpdateSTO;
        public static DateTime SupervisieLastDt { get; set; }
        //
        public static DateTime StartDate { get; set; }
        public static DateTime EndDate { get; set; }
    }

    //dummy object for supervision data
    public partial class SupervisieDummy
    {
        public string Location { get; set; }
        public string logtext { get; set; }
        public Nullable<int> RT { get; set; }
        public Nullable<int> DT { get; set; }
        public string Classification { get; set; }
        public string Subgroup { get; set; }
        public string Severity { get; set; }
        public string Logcode { get; set; }
        public string Logtype { get; set; }
        public int refId { get; set; }
        public Nullable<System.DateTime> timestamp { get; set; }
        public string LocationTree { get; set; }
        public string ClassTree { get; set; }
        public Nullable<int> Vyear { get; set; }
        public Nullable<int> Vweek { get; set; }
        public Nullable<int> Vday { get; set; }
        public string shift { get; set; }
        public string animation { get; set; }
    }

    public partial class PloegRaport_dummy
    {
        public string Location { get; set; }
        public string Logcode { get; set; }
        public string Logtype { get; set; }
        public string logtext { get; set; }
        public Nullable<int> Response_min_ { get; set; }
        public Nullable<int> Downtime_min_ { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<System.DateTime> timestamp { get; set; }
        public string Subgroup { get; set; }
        public string Classification { get; set; }
        public Nullable<int> refId { get; set; }
        public string LocationTree { get; set; }
        public string animation { get; set; }
    }

    public class BackgroundWork
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public void configHangfireJobs()
    {
        BackgroundWork backgroundwork = new BackgroundWork();
       //default setup. (VASC ABB robot)
        if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled("Supervision"))
            { 
                RecurringJob.AddOrUpdate("BT_Supervis", () => backgroundwork.UpdateSupervisie(null), Cron.Minutely);
                RecurringJob.AddOrUpdate("norm_NGAC", () => backgroundwork.norm_NGAC(null), Cron.Minutely);
        }
        else
        {
            log.Warn("Supervision area is disabled removing hangfire jobs");
            RecurringJob.RemoveIfExists("BT_Supervis");

            RecurringJob.RemoveIfExists("norm_NGAC");
        }

        //incase of Ghent (VCSC comau robots)
        if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled("VCSC"))
        {
            RecurringJob.AddOrUpdate("norm_C3G", () => backgroundwork.norm_c3g(null), Cron.Minutely);
            RecurringJob.AddOrUpdate("norm_C4G", () => backgroundwork.norm_c4g(null), Cron.Minutely);
        }
        else
        {
            log.Warn("VCSC area is disabled removing hangfire jobs");
            RecurringJob.RemoveIfExists("norm_C3G");
            RecurringJob.RemoveIfExists("norm_C4G");
        }
    }

    //update the local datatable with supervisie called every minute #hangfire
    [Queue("gadata")]
    [AutomaticRetry(Attempts = 0)]
    public void UpdateSupervisie(PerformContext context)
    {
        List<EqUiWebUi.Areas.Supervision.SupervisieDummy> data = new List<EqUiWebUi.Areas.Supervision.SupervisieDummy>();  
        //init external buffers if null
        if (DataBuffer.dataC3G == null) DataBuffer.dataC3G = new List<EqUiWebUi.Areas.Supervision.SupervisieDummy>();
        if (DataBuffer.dataC4G == null) DataBuffer.dataC4G = new List<EqUiWebUi.Areas.Supervision.SupervisieDummy>();
        if (DataBuffer.dataVASC == null) DataBuffer.dataVASC = new List<EqUiWebUi.Areas.Supervision.SupervisieDummy>();
        if (DataBuffer.dataSTO == null) DataBuffer.dataSTO = new List<EqUiWebUi.Areas.Supervision.SupervisieDummy>();

            //number of shifts to load in memory (when set to 9 NGAc started timeing out)
            int NumShifts = Convert.ToInt32(ConfigurationManager.AppSettings["DatabufferNumShifts"]);
            //number of hours to load in memory (in case there is no timeline data)
            int NumDefaultHours = Convert.ToInt32(ConfigurationManager.AppSettings["DatabufferNumDefaultHours"]) * -1;
            DateTime now = System.DateTime.Now;
            //get timeline
            Supervision.Models.SupervisionEntities SupervisionEntities = new Models.SupervisionEntities();
            data = SupervisionEntities.Timeline.Select(
                x => new EqUiWebUi.Areas.Supervision.SupervisieDummy() {
                  Location = x.Location
                , logtext = x.Logtekst
                , RT = null
                , DT = null
                , Classification = ""
                , Subgroup = x.Subgroup
                , Severity = null
                , Logcode = ""
                , Logtype = "TIMELINE"
                , refId = x.id
                , timestamp = x.Timestamp
                , LocationTree = ""
                , ClassTree = ""
                , Vyear = x.Year
                , Vweek = x.Week
                , Vday = x.day
                , shift = x.Shift
                , animation = "TIMELINE"
                }).Where(x => x.timestamp < now).OrderByDescending(x => x.timestamp).Take(NumShifts).ToList();
            if (data.Count() != 0 )
            {
                DataBuffer.StartDate = data.Select(x => x.timestamp).First() ?? System.DateTime.Now;
                DataBuffer.EndDate = data.Select(x => x.timestamp).Last() ?? System.DateTime.Now.AddHours(NumDefaultHours);
                context.WriteLine(string.Format("TIMELINE startdate:{0} => enddate:{1}", DataBuffer.StartDate, DataBuffer.EndDate));
                context.WriteLine("Span: " + (DataBuffer.StartDate - DataBuffer.EndDate));
            }
            else
            {
                DataBuffer.StartDate = System.DateTime.Now;
                DataBuffer.EndDate =  System.DateTime.Now.AddHours(NumDefaultHours);
                context.WriteLine(string.Format("NOTIMELINE DATA! startdate:{0} => enddate:{1}", DataBuffer.StartDate, DataBuffer.EndDate));
                context.WriteLine("Span: " + (DataBuffer.StartDate - DataBuffer.EndDate));
            }

         //Get Alert data
            DataBuffer.dataALERT  = SupervisionEntities.Alerts_Supervisie.Select(
                x => new EqUiWebUi.Areas.Supervision.SupervisieDummy() {
                 Location= x.Location
                ,logtext = x.Logtext
                ,RT = x.Response
                ,DT = x.Downtime
                ,Classification = x.Classification
                ,Subgroup = x.Subgroup
                ,Severity = x.Severity
                ,Logcode = x.Logcode
                ,Logtype = x.Logtype
                ,refId = x.refId
                ,timestamp = x.timestamp
                ,LocationTree = x.LocationTree
                ,ClassTree = x.ClassTree.ToString()
                ,Vyear = x.Vyear
                ,Vweek = x.Vweek
                ,Vday = x.Vday
                ,shift = x.shift
                ,animation = x.animation
            }).Where(x => x.timestamp > DataBuffer.EndDate).ToList();
            context.WriteLine("dataALERT count:" + DataBuffer.dataALERT.Count());
            data.AddRange(DataBuffer.dataALERT);


            //VASC
            context.WriteLine("dataVASC count:" + DataBuffer.dataVASC.Count());
            data.AddRange(DataBuffer.dataVASC);


            //VCSC AREA (Ghent only)
            if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled("VCSC"))
            {
                //COMAU C3G
                context.WriteLine("dataC3G count:" + DataBuffer.dataC3G.Count());
                data.AddRange(DataBuffer.dataC3G);
                //COMAU c4G
                context.WriteLine("dataC4G count:" + DataBuffer.dataC4G.Count());
                data.AddRange(DataBuffer.dataC4G);
                //TIA STO
                context.WriteLine("dataSTO count:" + DataBuffer.dataSTO.Count());
                data.AddRange(DataBuffer.dataSTO);

                //ABB s4c is here because it has no norm
                //run classiciation
                context.WriteLine("S4C runRule started");
                //stupid that I need to spin up all these classes to get it to run... (temp solution)
                EqUiWebUi.Controllers.ClassificationController classificationController = new EqUiWebUi.Controllers.ClassificationController();
                EqUiWebUi.Models.c_LogClassRules c_LogClassRule = new EqUiWebUi.Models.c_LogClassRules();
                EqUiWebUi.Models.GADATAEntitiesEQUI db = new EqUiWebUi.Models.GADATAEntitiesEQUI();
                c_LogClassRule.c_logClassSystem_id = db.c_logClassSystem.Where(c => c.Name == "VASC_S4C").First().id;
                c_LogClassRule.id = 0; //this causes us to run all rules
                classificationController.RunRule(c_LogClassRule, overrideManualSet: false, Clear: false, UPDATE: false);
                context.WriteLine("S4C runRule done");
                //get supervision data
                Supervision.Models.SupervisionEntities Supervision = new Supervision.Models.SupervisionEntities();
                    DataBuffer.dataS4C = Supervision.S4C_Supervisie.Select(x => new EqUiWebUi.Areas.Supervision.SupervisieDummy() {
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
                    }).Where(x => x.timestamp > DataBuffer.EndDate).ToList();
                context.WriteLine("dataS4C count:" + DataBuffer.dataS4C.Count());
                data.AddRange(DataBuffer.dataS4C);
            }


            //Set the main dataset and update the max ts
            context.WriteLine("--------------------------------");
            context.WriteLine("Total datacount " + data.Count());
            if (data.Count() != 0)
            {
                DataBuffer.Supervisie = data;
                DateTime maxDate = data
                            .Where(r => r != null && r.timestamp < now)
                            .Select(r => r.timestamp.Value)
                            .Max();

                DataBuffer.SupervisieLastDt = maxDate;
                context.WriteLine("maxdate: " + maxDate);
            }
            //add singal R to notify clients
            var SingnalRcontext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<DataRefreshHub>();
            SingnalRcontext.Clients.Group("Supervisie").newData();
            SingnalRcontext.Clients.Group("Ploegrapport").newData();
        }

        //run C3G normalisation steps.
        [Queue("gadata")]
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public void norm_c3g(PerformContext context)
        {
            //
            ConnectionManager connectionManager = new ConnectionManager();
            string[] cmds = { "exec C3G.sp_update_L"
                    ,"exec C3G.sp_L_breakdown"
            };

            foreach(string cmd in cmds)
            {
                context.WriteLine(cmd);
                connectionManager.RunCommand(cmd, enblExeptions: true, maxEXECtime: 300);
                context.WriteLine("Done");
            }

            context.WriteLine("runRule started");
            //stupid that I need to spin up all these classes to get it to run... (temp solution)
            EqUiWebUi.Controllers.ClassificationController classificationController = new EqUiWebUi.Controllers.ClassificationController();
            EqUiWebUi.Models.c_LogClassRules c_LogClassRule = new EqUiWebUi.Models.c_LogClassRules();
            EqUiWebUi.Models.GADATAEntitiesEQUI db = new EqUiWebUi.Models.GADATAEntitiesEQUI();
            c_LogClassRule.c_logClassSystem_id = db.c_logClassSystem.Where(c => c.Name == "VCSC_C3G").First().id;
            c_LogClassRule.id = 0; //this causes us to run all rules
            classificationController.RunRule(c_LogClassRule, overrideManualSet: false, Clear: false, UPDATE: false);
            context.WriteLine("runRule done");
            
            //update supervisie databuffer
            context.WriteLine("Supervisie dataC3G");
            Supervision.Models.SupervisionEntities SupervisionEntities = new Supervision.Models.SupervisionEntities();
            DataBuffer.dataC3G = SupervisionEntities.C3G_Supervisie.Select(x => new EqUiWebUi.Areas.Supervision.SupervisieDummy() {
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
            }).Where(x => x.timestamp > DataBuffer.EndDate).ToList();
            context.WriteLine(DataBuffer.dataC3G.Count());
        }

        //run c4g normalisation steps.
        [Queue("gadata")]
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public void norm_c4g(PerformContext context)
        {
            //
            ConnectionManager connectionManager = new ConnectionManager();
            string[] cmds = { "exec [C4G].[sp_update_L]"
                    ,"exec [C4G].sp_Update_L_breakdown"
                    ,"exec [C4G].sp_ReClass_L_breakdown"
                    ,"exec [c4G].[sp_calc_LiveView]"
            };

            foreach (string cmd in cmds)
            {
                context.WriteLine(cmd);
                connectionManager.RunCommand(cmd, enblExeptions: true, maxEXECtime: 300);
                context.WriteLine(" Done");
            }

            context.WriteLine("runRule started");
            //stupid that I need to spin up all these classes to get it to run... (temp solution)
            EqUiWebUi.Controllers.ClassificationController classificationController = new EqUiWebUi.Controllers.ClassificationController();
            EqUiWebUi.Models.c_LogClassRules c_LogClassRule = new EqUiWebUi.Models.c_LogClassRules();
            EqUiWebUi.Models.GADATAEntitiesEQUI db = new EqUiWebUi.Models.GADATAEntitiesEQUI();
            c_LogClassRule.c_logClassSystem_id = db.c_logClassSystem.Where(c => c.Name == "VCSC_C4G").First().id;
            c_LogClassRule.id = 0; //this causes us to run all rules
            classificationController.RunRule(c_LogClassRule, overrideManualSet: false, Clear: false, UPDATE: false);
            context.WriteLine("runRule done");
            //update supervisie databuffer
            context.WriteLine("Supervisie dataC4G");
            Supervision.Models.SupervisionEntities SupervisionEntities = new Supervision.Models.SupervisionEntities();
            DataBuffer.dataC4G = SupervisionEntities.C4G_Supervisie.Select(x => new EqUiWebUi.Areas.Supervision.SupervisieDummy() {
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
            }).Where(x => x.timestamp > DataBuffer.EndDate).ToList();
            context.WriteLine(DataBuffer.dataC4G.Count());
        }

        //run NGAC normalisation steps.
        [Queue("gadata")]
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public void norm_NGAC(PerformContext context)
        {
            try
            {
                if (DataBuffer._isRunningUpdateNGAC)
                {
                    log.Error("job was already running CANCEL job");
                    context.WriteLine("job was already running CANCEL job");
                    return;
                }
                DataBuffer._isRunningUpdateNGAC = true;

                // Logic...
                ConnectionManager connectionManager = new ConnectionManager();
                context.WriteLine("EXEC [NGAC].[sp_update_cleanLogteksts]");
                connectionManager.RunCommand("EXEC [NGAC].[sp_update_cleanLogteksts]", enblExeptions: true, maxEXECtime: 300);
                context.WriteLine("Done");

                context.WriteLine("runRule started");
                //stupid that I need to spin up all these classes to get it to run... (temp solution)
                EqUiWebUi.Controllers.ClassificationController classificationController = new EqUiWebUi.Controllers.ClassificationController();
                EqUiWebUi.Models.c_LogClassRules c_LogClassRule = new EqUiWebUi.Models.c_LogClassRules();
                EqUiWebUi.Models.GADATAEntitiesEQUI db = new EqUiWebUi.Models.GADATAEntitiesEQUI();
                c_LogClassRule.c_logClassSystem_id = db.c_logClassSystem.Where(c => c.Name == "VASC_NGAC").First().id;
                c_LogClassRule.id = 0; //this causes us to run all rules
                classificationController.RunRule(c_LogClassRule, overrideManualSet: false, Clear: false, UPDATE: false);
                context.WriteLine("runRule done");

                //update supervisie databuffer
                context.WriteLine("Supervisie dataVASC");
                Supervision.Models.SupervisionEntities SupervisionEntities = new Supervision.Models.SupervisionEntities();
                SupervisionEntities.Database.CommandTimeout = 60; // default 30 
                DataBuffer.dataVASC = SupervisionEntities.NGAC_Supervisie.Select(x => new EqUiWebUi.Areas.Supervision.SupervisieDummy() {
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
                }).Where(x => x.timestamp > DataBuffer.EndDate).ToList();
                context.WriteLine(DataBuffer.dataVASC.Count());

                //fire and forget TIPLIFE
                Areas.Tiplife.Backgroundwork backgroundworkTiplife = new Tiplife.Backgroundwork();
                var tiplifeJobID = BackgroundJob.Enqueue(() => backgroundworkTiplife.UpdateTipstatus(null));
                context.WriteLine(System.Configuration.ConfigurationManager.AppSettings["HangfireDetailsBasepath"].ToString() + tiplifeJobID);
            }
            finally
            {
                DataBuffer._isRunningUpdateNGAC = false;
            }
          
        }

    }
}
 