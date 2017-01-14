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
            //Fill local dataset
            using (applDataTableAdapters.ASSETSTableAdapter adapter = new applDataTableAdapters.ASSETSTableAdapter())
            {
                adapter.Fill(lASSETS);
            }
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //link datasource for grid (all data on init)
            dataGridView1.DataSource = lASSETS;
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
           //get asset list from maximo M7 dailiy copy
           DataTable tableFromMx7 = new DataTable();
           tableFromMx7 = lGadataComm.GetAssetListFromMX7();
           //Create destination table  in GADATA
           string CmdDeleteTableData = @"DELETE FROM [Volvo].[ASSETS_fromMX7] FROM [Volvo].[ASSETS_fromMX7]";
           string CmdCreateTable = @"
            CREATE TABLE [Volvo].[ASSETS_fromMX7](
	            [SYSTEMID] [varchar](255) NULL,
	            [LOCATION] [varchar](255) NULL,
	            [LocationDescription] [varchar](255) NULL,
	            [ASSETNUM] [varchar](255) NULL,
	            [AssetDescription] [varchar](255) NULL,
	            [LocationTree] [varchar](max) NULL
            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
            ";
           lGadataComm.RunCommandGadata(CmdDeleteTableData);
           lGadataComm.RunCommandGadata(CmdCreateTable);
           //send the data to the SQL GADATA SERVER 
           lGadataComm.BulkCopyToGadata("VOLVO", tableFromMx7, "ASSETS_fromMX7");
           string CmdUpdateAssetsControllers = @"
                DROP TABLE GADATA.volvo.ASSETS 
                SELECT [SYSTEMID]
                      ,assets.[LOCATION]
                      ,assets.[ASSETNUM]
                      ,assets.[AssetDescription]
                      ,assets.[LocationTree]
	                  ,ISNULL(r.controller_name,rr.controller_name) as 'controller_name'
	                  ,ISNULL(r.controller_type,rr.controller_type) as 'controller_type'
	                  ,ISNULL(r.id,rr.id) as 'controller_id'
                  INTO GADATA.volvo.ASSETS
                  FROM [GADATA].[Volvo].[ASSETS_fromMX7] as assets
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
           lGadataComm.RunCommandGadata(CmdUpdateAssetsControllers);
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
            DialogResult result = MessageBox.Show("Are you sure? This will change the server side tabels!!!", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                UpdateMaximoAssetsToGadata();
            }
            else if (result == DialogResult.No)
            {
                //...
            }
            else
            {
                //...
            } 

        }
        private void CB_ALL_SelectedIndexChanged(object sender, EventArgs e)
        {
          refreshGrid1();
        }
    }


}
