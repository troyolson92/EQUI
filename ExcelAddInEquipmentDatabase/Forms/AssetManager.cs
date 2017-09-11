using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.Linq.SqlClient;
using System.Text.RegularExpressions;
using EQUICommunictionLib;

namespace ExcelAddInEquipmentDatabase
{
    public partial class AssetManager : Form
    {
        //debugger
        myDebugger Debugger = new myDebugger();
        //local Asset data instance
        applData.ASSETSDataTable lASSETS = new applData.ASSETSDataTable();
        //connection to GADATA
        GadataComm lGadataComm = new GadataComm();
        //connection to MAXIMO
        MaximoComm lMaximoComm = new MaximoComm();

        public AssetManager()
        {
            InitializeComponent();
            //build running clock to see when shit craps out
            Timer timer = new Timer();
            timer.Interval = (1 * 1000); // 1 secs
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            //Fill local dataset
            using (applDataTableAdapters.ASSETSTableAdapter adapter = new applDataTableAdapters.ASSETSTableAdapter())
            {
                try { adapter.Fill(lASSETS); }
                catch (Exception ex) { Debugger.Exeption(ex); }

            }
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //link datasource for grid (all data on init)
            dataGridView1.DataSource = lASSETS;
            Cursor.Current = Cursors.Default;
        }

        private void refreshGrid1()
        {
                            var data = from a in lASSETS
                                       where a.LocationTree.Like(CB_LOCHIERARCHY.Text)
                                          && a.LOCATION.Like(CB_LOCATION.Text)
                                          && a.ASSETNUM.Like(CB_ASSET.Text)
                                          && a.SYSTEMID.Like(CB_SYSTEMID.Text)
                                          && a.LocationTree.Like(CB_UNIVERSE.Text)
                                        orderby a.ASSETNUM ascending
                                       select a;
                            dataGridView1.DataSource = data.AsDataView();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lbl_clock.Text = DateTime.Now.ToString();
        }

        //This is for sync Mx7 with the database. 
        private void UpdateMaximoAssetsToGadata()
        {
           //get asset list from maximo M7 daily copy
           DataTable tableFromMx7 = new DataTable();
           string strSqlGetFromMaximo = @"
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
WHERE ROOT.LOCATION= 'VCG' --SELECT ITEM NUM TO START BOM FROM
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
    ,NVL(LOCATIONCTE.PARENT,'NoStation') STATION
    ,NVL(AREA.PARENT ,'NoArea') AREA
    ,NVL(TEAM.PARENT,'NoTeam') TEAM
FROM LOCATIONCTE  

LEFT JOIN MAXIMO.ASSET ASSET on ASSET.LOCATION = LOCATIONCTE.LOCATION --to get asset number
LEFT JOIN MAXIMO.CLASSSTRUCTURE CLASSSTRUCTURE on CLASSSTRUCTURE.CLASSSTRUCTUREID = ASSET.CLASSSTRUCTUREID
--get AREA (one level up from station)
LEFT JOIN MAXIMO.LOCHIERARCHY AREA ON (LOCATIONCTE.PARENT = AREA.LOCATION AND LOCATIONCTE.ORGID = AREA.ORGID AND LOCATIONCTE.SYSTEMID = AREA.SYSTEMID )   
--get the team form the ORG Hierarchy
LEFT JOIN MAXIMO.LOCHIERARCHY TEAM ON (AREA.PARENT = TEAM.LOCATION AND TEAM.SYSTEMID = 'ORG' ) --ORG might be VCG specific
WHERE LOCATIONCTE.CHILDREN = 0 
)
            ";
           tableFromMx7 = lMaximoComm.oracle_runQuery(strSqlGetFromMaximo);
           //clear destination table  in GADATA
           string CmdDeleteTableData = @"DELETE FROM [Equi].[ASSETS_fromMX7] FROM [Equi].[ASSETS_fromMX7]";
           lGadataComm.RunCommandGadata(CmdDeleteTableData,true);
           //send the data to the SQL GADATA SERVER 
           lGadataComm.BulkCopyToGadata("Equi", tableFromMx7, "ASSETS_fromMX7");
           //
           dataGridView1.DataSource = tableFromMx7;
        }

        private void Assets_from_mx7_TO_Assets()
        {
            //run a command to links maximo assets with our c_controller tables
            string CmdUpdateAssetsControllers = @"
  if (OBJECT_ID('GADATA.Equi.ASSETS ') is not null)   DROP TABLE GADATA.Equi.ASSETS 
                SELECT [SYSTEMID]
                      ,assets.[LOCATION]
                      ,assets.[ASSETNUM]
                      ,assets.[AssetDescription]
                      ,assets.[LocationTree]
                      ,assets.[ClassDescription]
                      ,assets.[ClassStructureId]
                      ,assets.[CLassificationId]
                      ,assets.[ClassificationTree]
                      ,assets.[Station]
                      ,assets.[Area]
                      ,assets.[Team]
                      ,ISNULL(r.controller_name,rr.controller_name) as 'controller_name'
                      ,ISNULL(r.controller_type,rr.controller_type) as 'controller_type'
                      ,ISNULL(r.id,rr.id) as 'controller_id'
                      ,ROW_NUMBER() OVER (PARTITION BY 
                          ISNULL(r.controller_type,rr.controller_type)
                        , ISNULL(r.id,rr.id), assets.classificationid 
                        ORDER BY assets.location ASC) AS 'controller_ToolID'
                  INTO GADATA.Equi.ASSETS
                  FROM [GADATA].[Equi].[ASSETS_fromMX7] as assets
                  --join robot assets with there controller
                  left join GADATA.volvo.Robots as r on 
                  r.controller_name = assets.LOCATION
                  AND
                  (assets.ASSETNUM like 'URC%' OR assets.ASSETNUM like 'URA%') --COMAU and ABB assets
  
                  --join robot controller assets with there controller
                  left join GADATA.VOLVO.Robots as rr on 
                  --Grippers
                  (
                  REPLACE(REPLACE(REPLACE(assets.LOCATION,'GH','R'),'GP','R'),'GD','R') LIKE rr.controller_name+'%'
                  )
                  OR
                  --Weld WS (func pack spot) WN (nutweld)
                  (
                  REPLACE(REPLACE(REPLACE(assets.LOCATION,'WS','R'),'WT','R'),'WN','R') LIKE rr.controller_name+'%'
                  )
                    OR
                  -- WT (tucker) Pistool, Toevoer, Lasbron, algemene zaken
                  (
                  REPLACE(REPLACE(REPLACE(REPLACE(assets.LOCATION,'WTP','R'),'WTT','R'),'WTL','R'),'WTA','R') LIKE rr.controller_name+'%'
                  )
                  OR
                  --Dispense (and quis)
                  (
                  REPLACE(REPLACE(assets.LOCATION,'SH','R'),'QF','R') LIKE rr.controller_name+'%'
                  )
                  OR
                  --Nutrunners
                  (
                  REPLACE(assets.LOCATION,'JB','R') LIKE rr.controller_name+'%'
                  )
                where 
                (assets.LocationTree like 'VCG -> A%' AND assets.ASSETNUM like 'U%')
                OR
                (assets.LocationTree like 'VCG -> B%' AND assets.ASSETNUM like 'U%')
                ";
            lGadataComm.RunCommandGadata(CmdUpdateAssetsControllers, true);
        }

        //linq querys to update and filter comboxes

        private void cb_asset_update()
        {
            try
            {
                var assetslist = from a in lASSETS
                                 where a.LocationTree.Like(CB_LOCHIERARCHY.Text)
                                   && a.LOCATION.Like(CB_LOCATION.Text)
                                   //&& a.ASSETNUM.Like(CB_ASSET.Text)
                                   && a.SYSTEMID.Like(CB_SYSTEMID.Text)
                                   && a.LocationTree.Like(CB_UNIVERSE.Text)
                                 orderby a.ASSETNUM ascending
                                 select a.ASSETNUM;
                CB_ASSET.DataSource = assetslist.Distinct().ToList();
            }
            catch (Exception e)
            {
                CB_ASSET.Text = "%";
               Debugger.Exeption(e);
            }
        }
        private void cb_location_update()
        {
            try
            {
                var locationlist = from a in lASSETS
                                   where a.LocationTree.Like(CB_LOCHIERARCHY.Text)
                                     //&& a.LOCATION.Like(CB_LOCATION.Text)
                                     && a.ASSETNUM.Like(CB_ASSET.Text)
                                     && a.SYSTEMID.Like(CB_SYSTEMID.Text)
                                     && a.LocationTree.Like(CB_UNIVERSE.Text)
                                   orderby a.LOCATION ascending
                                   select a.LOCATION;
               CB_LOCATION.DataSource = locationlist.Distinct().ToList();
            }
            catch (Exception e)
            {
                CB_LOCATION.Text = "%";
               Debugger.Exeption(e);
            }
        }
        private void cb_Lochierarchy_update()
        {
            try
            {
                var LochierarchyList = from a in lASSETS
                                       where //a.LocationTree.Like(CB_LOCHIERARCHY.Text) &&
                                          a.LOCATION.Like(CB_LOCATION.Text)
                                         && a.ASSETNUM.Like(CB_ASSET.Text)
                                         && a.SYSTEMID.Like(CB_SYSTEMID.Text)
                                         && a.LocationTree.Like(CB_UNIVERSE.Text)
                                       orderby a.LocationTree ascending
                                       select a.LocationTree;
                CB_LOCHIERARCHY.DataSource = LochierarchyList.Distinct().ToList();
            }
            catch (Exception e)
            {
                CB_LOCHIERARCHY.Text = "%";
               Debugger.Exeption(e);
            }
        }
        private void cb_systemid_load()
        {
            try
            {
                var Systemlist = from a in lASSETS
                                       where a.LocationTree.Like(CB_LOCHIERARCHY.Text)
                                         && a.LOCATION.Like(CB_LOCATION.Text)
                                         && a.ASSETNUM.Like(CB_ASSET.Text)
                                         //&& a.SYSTEMID.Like(CB_SYSTEMID.Text)
                                         && a.LocationTree.Like(CB_UNIVERSE.Text)
                                       orderby a.SYSTEMID ascending
                                     select a.SYSTEMID;
                CB_SYSTEMID.DataSource = Systemlist.Distinct().ToList();
            }
            catch (Exception e)
            {
                CB_SYSTEMID.Text = "%";
               Debugger.Exeption(e);
            }
        }

        //event handers for form
        private void CB_LOCHIERARCHY_DropDown(object sender, EventArgs e)
        {
            cb_Lochierarchy_update();
        }
        private void CB_LOCATION_DropDown(object sender, EventArgs e)
        {
            cb_location_update();
        }
        private void CB_ASSET_DropDown(object sender, EventArgs e)
        {
            cb_asset_update();
        }
        private void btn_MX7toGADATA_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? This will change the server side tabels!!!", "Confirmation", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.AppStarting;
                UpdateMaximoAssetsToGadata();
                Cursor.Current = Cursors.Default;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {            DialogResult result = MessageBox.Show("Are you sure? This will change the server side tabels!!!", "Confirmation", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.AppStarting;
                Assets_from_mx7_TO_Assets();
                Cursor.Current = Cursors.Default;
            }
         }

        private void CB_ALL_SelectedIndexChanged(object sender, EventArgs e)
        {
          refreshGrid1();
        }

    }


}
