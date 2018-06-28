using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using EqUiWebUi.Areas.Gadata.Models;
using Hangfire;
using EQUICommunictionLib;

namespace EqUiWebUi.Areas.Gadata
{
    public static class DataBuffer
    {
        //
        public static List<Supervisie> Supervisie { get; set; }
        public static DateTime SupervisieLastDt { get; set; }
        //
        public static List<AAOSR_PloegRaport_Result> Ploegreport { get; set; }
        public static DateTime PloegreportLastDt { get; set; }
        //
        public static List<EQpluginDefaultNGAC_Result> EQpluginDefaultNGAC { get; set; }
        public static DateTime EQpluginDefaultNGAC_DT { get; set; }
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
                //**********************************Ploegreport table***************************************************
                //set job to refresh every 5 minutes
                RecurringJob.AddOrUpdate("BT_SupervisHigh",() => backgroundwork.UpdateEQpluginDefaultNGAC(), Cron.MinuteInterval(5));
                //**********************************Supervisie table***************************************************
                //set job to refresh every minute
                RecurringJob.AddOrUpdate("BT_Supervis",() => backgroundwork.UpdateSupervisie(), Cron.Minutely);
                //**********************************Copy maximi Assets to GADATA***************************************************
                //set job to run sunday at 12
                RecurringJob.AddOrUpdate("AssetsMx-gadata", () => backgroundwork.UpdateMaximoAssetsToGadata(), "0 12 * * 0");
        }

            //update the local datatable with ploeg rapport called every minute #hangfire
            [Queue("gadata")]
            [AutomaticRetry(Attempts = 0)]
            public void UpdatePloegreport()
            {
                GADATAEntities2 gADATAEntities = new GADATAEntities2();

                List<AAOSR_PloegRaport_Result> data = (from ploegrapport in gADATAEntities.AAOSR_PloegRaport
                                    (startDate: null,
                                       endDate: null,
                                       daysBack: null,
                                       assets: "%",
                                       locations: "%",
                                       lochierarchy: "%",
                                       minDowntime: 20,
                                       minCountOfDowtime: 3,
                                       minCountofWarning: 4,
                                       getAlerts: false,
                                       getShifbook: true)
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

            //update the local datatable with detail robot data called every minute #hangfire
            [Queue("gadata")]
            [AutomaticRetry(Attempts = 0)]
            public void UpdateEQpluginDefaultNGAC()
            {
                GADATAEntities2 gADATAEntities = new GADATAEntities2();

            List<EQpluginDefaultNGAC_Result> data = (from DefaultNgac in gADATAEntities.EQpluginDefaultNGAC      
                                  (startDate: null,
                                   endDate: null,
                                   daysBack: 1,
                                   assets: "%",
                                   locations: "%",
                                   lochierarchy: "%",
                                   timeline: true,
                                   controllerEventLog: false,
                                   errDispLog: true,
                                   errDispLogS4C: true,
                                   variableLog: false,
                                   deviceProperty: false,
                                   breakdown: true,
                                   breakdownStart: false,
                                   jobs: false,
                                   displayLevel: 0,
                                   displayFullLogtext: true,
                                   excludeOperational: true
                                       )
                                                         select DefaultNgac).ToList();

                if (data.Count != 0)
                {
                    DataBuffer.EQpluginDefaultNGAC = data;

                    DateTime maxDate = data
                        .Where(r => Convert.ToDateTime(r.timestamp) < System.DateTime.Now)
                        // .Where(r => r.Logtype.Contains("Ruleinfo") == false)
                        .Select(r => Convert.ToDateTime(r.timestamp))
                        .Max();

                    DataBuffer.EQpluginDefaultNGAC_DT = maxDate;

                    //add singal R 
                    var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<DataRefreshHub>();
                    context.Clients.Group("EQpluginDefaultNGAC").newData();
                }
                else
                {
                    log.Error("UpdateEQpluginDefaultNGAC did not return any data");
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
            string CmdLinkAssets = "EXEC GADATA.[EqUi].[sp_LinkAssets]";
            ConnectionManager connectionManager = new ConnectionManager();
            connectionManager.RunCommand(CmdLinkAssets, enblExeptions: true, maxEXECtime: 300);
            log.Info("sp_linkassets Done");
        }
    }
}