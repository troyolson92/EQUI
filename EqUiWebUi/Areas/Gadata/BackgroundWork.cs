using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using EqUiWebUi.Areas.Gadata.Models;
using Hangfire;
using EQUICommunictionLib;
using Hangfire.Server;
using Hangfire.Console;

namespace EqUiWebUi.Areas.Gadata
{
    public static class DataBuffer
    {
        //
        public static List<Supervisie> Supervisie { get; set; }
        public static DateTime SupervisieLastDt { get; set; }
        //
        public static List<PloegRaport_Result> Ploegreport { get; set; }
        public static DateTime PloegreportLastDt { get; set; }
    }


    public class BackgroundWork
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            public void configHangfireJobs()
            {
                //background work
                BackgroundWork backgroundwork = new BackgroundWork();
                //**********************************Ploegreport table***************************************************
                //set job to refresh every 5 minutes
                RecurringJob.AddOrUpdate("BT_PloegReport",() => backgroundwork.UpdatePloegreport(), Cron.MinuteInterval(5));
                //**********************************Supervisie table***************************************************
                //set job to refresh every minute
                RecurringJob.AddOrUpdate("BT_Supervis",() => backgroundwork.UpdateSupervisie(), Cron.Minutely);
                //**********************************Copy maximi Assets to GADATA***************************************************
                //set job to run sunday at 12
                RecurringJob.AddOrUpdate("AssetsMx-gadata", () => backgroundwork.UpdateMaximoAssetsToGadata(), "0 12 * * 0");

            //**********************************normalize***************************************************
            //set job to refresh every minute
            RecurringJob.AddOrUpdate("norm_C3G", () => backgroundwork.norm_c3g(null), Cron.Minutely);
            RecurringJob.AddOrUpdate("norm_C4G", () => backgroundwork.norm_c4g(null), Cron.Minutely);
            RecurringJob.AddOrUpdate("norm_NGAC", () => backgroundwork.norm_NGAC(null), Cron.Minutely);
            RecurringJob.AddOrUpdate("norm_1day", () => backgroundwork.norm_1day(null), Cron.Daily);
        }

            //update the local datatable with ploeg rapport called every minute #hangfire
            [Queue("gadata")]
            [AutomaticRetry(Attempts = 0)]
            public void UpdatePloegreport()
            {
                GADATAEntities2 gADATAEntities = new GADATAEntities2();
                gADATAEntities.Database.CommandTimeout = 60; // normally 30 but this one is heavy
                List<PloegRaport_Result> data = (from ploegrapport in gADATAEntities.PloegRaport
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
            public void UpdateSupervisie()
            {
                GADATAEntities2 gADATAEntities = new GADATAEntities2();
                List<Supervisie> data = (from supervis in gADATAEntities.Supervisie
                                         select supervis).ToList();

                if (data.Count != 0)
                {
                    DataBuffer.Supervisie = data;

                    DateTime maxDate = data
                                .Where(r => r != null)
                                .Select(r => r.timestamp.Value)
                                .Max();

                    DataBuffer.SupervisieLastDt = maxDate;

                    //add singal R 
                    var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<DataRefreshHub>();
                    context.Clients.Group("Supervisie").newData();
                }
                else
                {
                    log.Error("UpdateSupervisie did not return any data");
                }
            }

             //update asset table on GADATA from maximo called every sunday #hangfire
            [Queue("gadata")]
            [AutomaticRetry(Attempts = 5)]
            //This is for sync Mx7 with the database. 
            public void UpdateMaximoAssetsToGadata()
            {
                //get asset list from maximo M7 daily copy
                string strSqlGetFromMaximo = string.Format(@"
    SELECT * FROM   
    (
    --PRESELECT IN MEMORY USING COMMON TABLE EXPRESSION CTE
    --TREE WALK QUERY SEE
    --HTTPS://ORACLE-BASE.COM/ARTICLES/11G/RECURSIVE-SUBQUERY-FACTORING-11GR2
    WITH LOCATIONCTE (LOCATION, PARENT, SYSTEMID, ORGID, CHILDREN, H_LEVEL, H_PATH) AS  
    (    
    --SELECT ROOT ITEM 'ANCHOR MEMBER'
    SELECT 
      ROOT.LOCATION
    , ROOT.PARENT
    , ROOT.SYSTEMID
    , ROOT.ORGID
    , ROOT.CHILDREN
    , 1 H_LEVEL
    , LOCATION H_PATH
    FROM MAXIMO.LOCHIERARCHY ROOT 
    WHERE ROOT.LOCATION= '{0}' --SELECT ITEM NUM TO START BOM FROM
    --SELECT CHILD NODES 'RECURSIVE MEMBER.'
    UNION ALL       
    SELECT 
      LOCCHILD.LOCATION
    , LOCCHILD.PARENT
    , LOCCHILD.SYSTEMID
    , LOCCHILD.ORGID
    , LOCCHILD.CHILDREN
    , LOCATIONCTE.H_LEVEL+1
    , (LOCATIONCTE.H_PATH ||' -> '|| LOCCHILD.LOCATION) H_PATH
    FROM MAXIMO.LOCHIERARCHY LOCCHILD     
    JOIN LOCATIONCTE  ON (LOCATIONCTE.LOCATION = LOCCHILD.PARENT AND LOCATIONCTE.ORGID = LOCCHILD.ORGID AND LOCATIONCTE.SYSTEMID = LOCCHILD.SYSTEMID )     
    )  
    SEARCH DEPTH FIRST BY LOCATION SET VOLGNR          
    --***************************************************************************************--
    --SELECT OUTPUT
    SELECT 
         LOCATIONCTE.SYSTEMID
        ,LOCATIONCTE.LOCATION
        ,null LocationDescription
        ,ASSET.ASSETNUM
        ,ASSET.DESCRIPTION AssetDescription
        ,LOCATIONCTE.H_PATH LocationTree
        ,null CLASSDESCRIPTION
        ,ASSET.CLASSSTRUCTUREID
        ,CLASSSTRUCTURE.CLASSIFICATIONID
        ,null ClassificationTree
        ,AREA.LOCATION STATION
        ,AREA.PARENT AREA
        ,TEAM.PARENT TEAM
    FROM LOCATIONCTE  

    LEFT JOIN MAXIMO.ASSET ASSET on ASSET.LOCATION = LOCATIONCTE.LOCATION --to get asset number 
    LEFT JOIN MAXIMO.CLASSSTRUCTURE CLASSSTRUCTURE on CLASSSTRUCTURE.CLASSSTRUCTUREID = ASSET.CLASSSTRUCTUREID
    --get AREA (one level up from station)
    LEFT JOIN MAXIMO.LOCHIERARCHY AREA ON (LOCATIONCTE.PARENT = AREA.LOCATION AND LOCATIONCTE.ORGID = AREA.ORGID AND LOCATIONCTE.SYSTEMID = AREA.SYSTEMID AND LOCATIONCTE.CHILDREN = 0)   
    --get the team form the ORG Hierarchy
    LEFT JOIN MAXIMO.LOCHIERARCHY TEAM ON (AREA.PARENT = TEAM.LOCATION AND TEAM.SYSTEMID = '{1}' ) --ORG might be VCG specific
    --WHERE LOCATIONCTE.CHILDREN = 0 
    )"
    , System.Configuration.ConfigurationManager.AppSettings["Maximo_SiteID"].ToString() //root location to start building three.
    , System.Configuration.ConfigurationManager.AppSettings["Maximo_ORG_SYSTEMID"].ToString() //structure in maximo location that has the organisation dygram
    ); 


             DataTable tableFromMx7 = new DataTable();
                ConnectionManager connectionManager = new ConnectionManager();    
                log.Info("DataFromMaximo start");
                tableFromMx7 = connectionManager.RunQuery(strSqlGetFromMaximo,dbName: "MAXIMO7rep", enblExeptions: true, maxEXECtime: 300);
                log.Info("DataFromMaximo Rowcount: " + tableFromMx7.Rows.Count);
                //clear destination table  in GADATA
                string CmdDeleteTableData = @"DELETE FROM [Equi].[ASSETS_fromMX7] FROM [Equi].[ASSETS_fromMX7]";
                log.Info("GadataDelete Start");
                connectionManager.RunCommand(CmdDeleteTableData, enblExeptions:true, maxEXECtime:300);
                log.Info("GadataDelete Done");
                //send the data to the SQL GADATA SERVER 
                log.Info("BulkCopyToGadata Start");
                connectionManager.BulkCopy(tableFromMx7, "[EQUI].[ASSETS_fromMX7]", enblExeptions: true, maxEXECtime: 300);
                log.Info("BulkCopyToGadata End");
                //Run command to link the assets and update c_controllerTables
                LinkMaximoAssetsToGadata();
        }

        //This relinks the assets from maximo in gadata 
        public void LinkMaximoAssetsToGadata()
        {
            log.Info("sp_linkassets Start");
            string CmdLinkAssets = "EXEC [EqUi].[sp_LinkAssets]";
            ConnectionManager connectionManager = new ConnectionManager();
            connectionManager.RunCommand(CmdLinkAssets, enblExeptions: true, maxEXECtime: 300);
            log.Info("sp_linkassets Done");
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
                    ,"EXEC [Volvo].[LiveView]"
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
                    ,"EXEC [NGAC].[sp_update_Lerror_classifcation]"
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