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

namespace ExcelAddInEquipmentDatabase
{
    public partial class AssetManager : Form
    {
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

        private void form_load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            //Fill local dataset
            using (applDataTableAdapters.ASSETSTableAdapter adapter = new applDataTableAdapters.ASSETSTableAdapter())
            {
                try {adapter.Fill(lASSETS);}
                catch (Exception ex) { Debug.WriteLine("Failed to fill assets table: " + ex.Message); }

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
     SELECT DISTINCT
                 l1.SYSTEMID
                ,l1.LOCATION LOCATION
                ,LOCATIONS.DESCRIPTION LocationDescription
                ,ASSET.ASSETNUM 
                ,ASSET.DESCRIPTION AssetDescription
                ,(
                 NVL2(l10.PARENT , l10.PARENT ||' -> ' ,'')||
                 NVL2(l9.PARENT , l9.PARENT ||' -> ','') ||
                 NVL2(l8.PARENT , l8.PARENT ||' -> ','') ||
                 NVL2(l7.PARENT , l7.PARENT ||' -> ','') ||
                 NVL2(l6.PARENT , l6.PARENT ||' -> ','') ||
                 NVL2(l5.PARENT , l5.PARENT ||' -> ','') ||
                 NVL2(l4.PARENT , l4.PARENT ||' -> ','') ||
                 NVL2(l3.PARENT , l3.PARENT ||' -> ','') ||
                 NVL2(l2.PARENT , l2.PARENT ||' -> ','') ||
                 NVL2(l1.PARENT , l1.PARENT ||' -> ','') ||
                 l1.LOCATION
                 ) LocationTree
                 
                , c1.DESCRIPTION CLASSDESCRIPTION
                , c1.CLASSSTRUCTUREID
                , c1.CLASSIFICATIONID
                ,(
                 NVL2(c10.CLASSIFICATIONID , c10.CLASSIFICATIONID ||' -> ' ,'')||
                 NVL2(c9.CLASSIFICATIONID , c9.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c8.CLASSIFICATIONID , c8.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c7.CLASSIFICATIONID , c7.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c6.CLASSIFICATIONID , c6.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c5.CLASSIFICATIONID , c5.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c4.CLASSIFICATIONID , c4.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c3.CLASSIFICATIONID , c3.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c2.CLASSIFICATIONID , c2.CLASSIFICATIONID ||' -> ','') ||
                 NVL2(c1.CLASSIFICATIONID , c1.CLASSIFICATIONID ||' -> ','') ||
                 c1.CLASSIFICATIONID
                 ) ClassificationTree
                 
                FROM MAXIMO.LOCHIERARCHY l1
                JOIN MAXIMO.LOCATIONS LOCATIONS on LOCATIONS.LOCATION = l1.LOCATION -- to get equipment status and details 
                JOIN MAXIMO.ASSET ASSET on ASSET.LOCATION = l1.LOCATION --to get asset number

                --join next 10 levels to build up structure
                LEFT JOIN MAXIMO.LOCHIERARCHY l2 on l2.LOCATION = l1.PARENT AND l2.SYSTEMID = L1.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l3 on l3.LOCATION = l2.PARENT AND l3.SYSTEMID = L2.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l4 on l4.LOCATION = l3.PARENT AND l4.SYSTEMID = L3.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l5 on l5.LOCATION = l4.PARENT AND l5.SYSTEMID = L4.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l6 on l6.LOCATION = l5.PARENT AND l6.SYSTEMID = L5.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l7 on l7.LOCATION = l6.PARENT AND l7.SYSTEMID = L6.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l8 on l8.LOCATION = l7.PARENT AND l8.SYSTEMID = L7.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l9 on l9.LOCATION = l8.PARENT AND l9.SYSTEMID = L8.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l10 on l10.LOCATION = l9.PARENT AND l10.SYSTEMID = L9.SYSTEMID
                
                --join 10 levels to get classification of asset
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c1 on c1.CLASSSTRUCTUREID = ASSET.CLASSSTRUCTUREID
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c2 on c2.CLASSSTRUCTUREID = c1.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c3 on c3.CLASSSTRUCTUREID = c2.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c4 on c4.CLASSSTRUCTUREID = c3.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c5 on c5.CLASSSTRUCTUREID = c4.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c6 on c6.CLASSSTRUCTUREID = c5.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c7 on c7.CLASSSTRUCTUREID = c6.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c8 on c8.CLASSSTRUCTUREID = c7.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c9 on c9.CLASSSTRUCTUREID = c8.PARENT 
                LEFT JOIN MAXIMO.CLASSSTRUCTURE c10 on c10.CLASSSTRUCTUREID = c9.PARENT    
                
                WHERE 
                l1.SITEID = 'VCG'
                AND
                l1.SYSTEMID = 'PRODMID'
            ";
           tableFromMx7 = lMaximoComm.oracle_runQuery(strSqlGetFromMaximo);
           //clear destination table  in GADATA
           string CmdDeleteTableData = @"DELETE FROM [Equi].[ASSETS_fromMX7] FROM [Equi].[ASSETS_fromMX7]";
           lGadataComm.RunCommandGadata(CmdDeleteTableData,true);
           //send the data to the SQL GADATA SERVER 
           lGadataComm.BulkCopyToGadata("Equi", tableFromMx7, "ASSETS_fromMX7");
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
	                  ,ISNULL(r.controller_name,rr.controller_name) as 'controller_name'
	                  ,ISNULL(r.controller_type,rr.controller_type) as 'controller_type'
	                  ,ISNULL(r.id,rr.id) as 'controller_id'
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
                  REPLACE(REPLACE(assets.LOCATION,'GH','R'),'GP','R') LIKE rr.controller_name+'%'
                  )
                  OR
                  --Weld WS (func pack spot) WN (nutweld)
                  (
                  REPLACE(REPLACE(REPLACE(assets.LOCATION,'WS','R'),'xxxxxx','R'),'WN','R') LIKE rr.controller_name+'%'
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
                where assets.LocationTree like 'VCG -> A%' AND assets.ASSETNUM like 'U%'

               
                ";
           lGadataComm.RunCommandGadata(CmdUpdateAssetsControllers,true);
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
                Debug.WriteLine(e.Message);
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
                Debug.WriteLine(e.Message);
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
                Debug.WriteLine(e.Message);
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
                Debug.WriteLine(e.Message);
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
                 result = MessageBox.Show("disbaled in source code need to fix safety issue to c_controller", "OEPS", MessageBoxButtons.YesNoCancel);
                
                Cursor.Current = Cursors.AppStarting;
             //   UpdateMaximoAssetsToGadata();
                Cursor.Current = Cursors.Default;
            }

        }
        private void CB_ALL_SelectedIndexChanged(object sender, EventArgs e)
        {
          refreshGrid1();
        }
    }


}
