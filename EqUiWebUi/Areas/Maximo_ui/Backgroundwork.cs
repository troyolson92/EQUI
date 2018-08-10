using EQUICommunictionLib;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.Maximo_ui
{
    public class Backgroundwork
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void configHangfireJobs()
        {
            if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled("Maximo_ui"))
            {
                Backgroundwork backgroundwork = new Backgroundwork();
                RecurringJob.AddOrUpdate("MX-WO=>GADATA", () => backgroundwork.PushDatafromMAXIMOtoGADATA(null), Cron.HourInterval(1));  //set job to run every hour
                RecurringJob.AddOrUpdate("MX-ASSETS=>GADATA", () => backgroundwork.UpdateMaximoAssetsToGadata(), "0 12 * * 0");            //set job to run sunday at 12
            }
            else
            {
                log.Warn("Maximo_ui area is disabled removing hangfire jobs");
                RecurringJob.RemoveIfExists("MX-WO=>GADATA");
                RecurringJob.RemoveIfExists("MX-ASSETS=>GADATA");
            }
        }

        //update new data from STO to gadata. called every minute #hangfire
        [AutomaticRetry(Attempts = 0)]
        [Queue("gadata")]
        public void PushDatafromMAXIMOtoGADATA(PerformContext context)
        {
            //delete data in now in maximo.
            ConnectionManager connectionManager = new ConnectionManager();
            context.WriteLine(" Delete workorders in gadata started");
            connectionManager.RunCommand("DELETE GADATA.MAXIMO.WORKORDERS FROM GADATA.MAXIMO.WORKORDERS");
            context.WriteLine(" Done");

            //get new records from STO
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
locancestor.ANCESTOR LIKE 'A'
WHERE
workorder.status not in ('CLOSE','CLOSEVOID') --they changes something where they set a shitload of workorders in this state. (the changedate was updated)
and
workorder.changedate >= sysdate - 100
"
);

            context.WriteLine(" Get workorders on MAXIMOrt started");
            DataTable newMaximoDt;
            try
            {
                //try realtime connection
                newMaximoDt = connectionManager.RunQuery(MaximoQry, dbName: "MAXIMOrt", maxEXECtime: 120, enblExeptions: true);
                context.WriteLine(" Done");
            }
            catch (Exception ex)
            {
                context.WriteLine(" FAILURE for MAXIMOrt connection! " + ex.Message);
                log.Error(" FAILURE for MAXIMOrt connection", ex);
                //if fails try reporting db
                newMaximoDt = connectionManager.RunQuery(MaximoQry, dbName: "MAXIMO7rep", maxEXECtime: 120, enblExeptions: true);
                context.WriteLine(" Done (used MAXIMO7rep)");
            }
            //push to gadata
            context.WriteLine(" Push workorders to gadata started");
            connectionManager.BulkCopy(newMaximoDt, "[MAXIMO].[WORKORDERS]");
            context.WriteLine(" Done");
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
            tableFromMx7 = connectionManager.RunQuery(strSqlGetFromMaximo, dbName: "MAXIMO7rep", enblExeptions: true, maxEXECtime: 300);
            log.Info("DataFromMaximo Rowcount: " + tableFromMx7.Rows.Count);
            //clear destination table  in GADATA
            string CmdDeleteTableData = @"DELETE FROM [Equi].[ASSETS_fromMX7] FROM [Equi].[ASSETS_fromMX7]";
            log.Info("GadataDelete Start");
            connectionManager.RunCommand(CmdDeleteTableData, enblExeptions: true, maxEXECtime: 300);
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
    }
}