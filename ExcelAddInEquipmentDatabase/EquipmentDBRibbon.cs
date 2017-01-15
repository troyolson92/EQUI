using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Data.Linq.SqlClient;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace ExcelAddInEquipmentDatabase
{
    public partial class EquipmentDBRibbon
    {
        //connection to gadata
        GadataComm lGadataComm = new GadataComm();
        //local Asset data instance
        applData.ASSETSDataTable lASSETS = new applData.ASSETSDataTable();
        //procedure manager instance 
        StoredProcedureManger ProcMngr;
        //intance of datetimepickers;
        dtPicker StartDatePicker = new dtPicker("@StartDate");
        dtPicker EndDatePicker = new dtPicker("@EndDate");

        private void EquipmentDBRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            //Fill local dataset
            using (applDataTableAdapters.ASSETSTableAdapter adapter = new applDataTableAdapters.ASSETSTableAdapter())
            {
                adapter.Fill(lASSETS);
            }
            //connects the ribbon filter controls with the procMngr
            ProcMngr.assets.parameterName = "@assets";
            ProcMngr.lochierarchy.parameterName = "@lochierarchy";
            ProcMngr.locations.parameterName = "@locations";
            ProcMngr.startDate.parameterName = "@startDate";
            ProcMngr.endDate.parameterName = "@endDate";
            ProcMngr.daysBack.parameterName = "@daysBack";
            //populate the connections of the workbook
            cb_connections_update();
        }

        #region population of comboboxes (dynamic filtering)
        private void cb_lochierarchy_update()
        {
            cb_Lochierarchy.Items.Clear();
            try
            {
                var data = from a in lASSETS
                                         where a.LocationTree.Like(cb_Lochierarchy.Text)
                                         && a.LOCATION.Like(cb_locations.Text)
                                         && a.ASSETNUM.Like(cb_assets.Text)
                                         orderby a.LocationTree descending
                                         select a.LocationTree;
                data.Distinct().ToList();
                foreach (string thing in data)
                {
                    RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                    item.Label = thing;
                    cb_Lochierarchy.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                cb_Lochierarchy.Text = "%";
                Debug.WriteLine(e.Message);
            }

        }
        private void cb_locations_update()
        {
            cb_locations.Items.Clear();
            try
            {
                var data = from a in lASSETS
                                         where a.LocationTree.Like(cb_Lochierarchy.Text)
                                         && a.LOCATION.Like(cb_locations.Text)
                                         && a.ASSETNUM.Like(cb_assets.Text)
                                          orderby a.LOCATION descending
                                         select a.LOCATION;
                data.Distinct().ToList();
                foreach (string thing in data)
                {
                    RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                    item.Label = thing;
                    cb_locations.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                cb_locations.Text = "%";
                Debug.WriteLine(e.Message);
            }
        }
        private void cb_assets_update()
        {
            cb_assets.Items.Clear();
            try
            {
                var data = from a in lASSETS
                                         where a.LocationTree.Like(cb_Lochierarchy.Text)
                                         && a.LOCATION.Like(cb_locations.Text)
                                         && a.ASSETNUM.Like(cb_assets.Text)
                                         orderby a.ASSETNUM descending
                                         select a.ASSETNUM;
                data.Distinct().ToList();
                foreach (string thing in data)
                {
                    RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                    item.Label = thing;
                    cb_assets.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                cb_locations.Text = "%";
                Debug.WriteLine(e.Message);
            }
        }
        private void cb_connections_update()
        {
            cb_activeConnection.Items.Clear();
            RibbonDropDownItem defaultitem = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
            defaultitem.Label = "RefreshAll";
            cb_activeConnection.Items.Add(defaultitem);
            cb_activeConnection.Text = "RefreshAll";
            Excel._Workbook activeWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook as Excel.Workbook;
            foreach (var connection in activeWorkbook.Connections.Cast<Excel.WorkbookConnection>())
            {
                switch (connection.Type)
                {
                    case Excel.XlConnectionType.xlConnectionTypeODBC:
                        RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                        item.Label = connection.Name;
                        cb_activeConnection.Items.Add(item);
                        break;
                    default:
                        Debug.WriteLine("connection tpye not supported");
                        break;
                }
            }
        }
        private void cb_load_all_procparameters()
        {
            cb_assets.Text = ProcMngr.assets.input ;
            cb_Lochierarchy.Text = ProcMngr.lochierarchy.input;
            cb_locations.Text = ProcMngr.locations.input;
            StartDatePicker.selectedDate = Convert.ToDateTime(ProcMngr.startDate.input);
            EndDatePicker.selectedDate = Convert.ToDateTime(ProcMngr.endDate.input);
           // ProcMngr.daysBack.input;
            //set enabeld or disabled. 
            cb_assets.Enabled = ProcMngr.assets.enabeld;
            cb_Lochierarchy.Enabled = ProcMngr.lochierarchy.enabeld;
            cb_locations.Enabled = ProcMngr.locations.enabeld;
            btn_StartDate.Enabled = ProcMngr.startDate.enabeld;
            btn_EndDate.Enabled = ProcMngr.endDate.enabeld;
            btn_nDays.Enabled = ProcMngr.daysBack.enabeld;
        }
        #endregion

        #region ribbon event handeling
        private void cb_Lochierarchy_itemsload(object sender, RibbonControlEventArgs e)
        {
            cb_lochierarchy_update();
        }
        private void cb_assets_itemsload(object sender, RibbonControlEventArgs e)
        {
            cb_assets_update();
        }
        private void cb_locations_itemsload(object sender, RibbonControlEventArgs e)
        {
            cb_locations_update();
        }
        private void btn_AssetManager_Click(object sender, RibbonControlEventArgs e)
        {
            //instance of asset manger 
            AssetManager AssetMngr = new AssetManager();
            AssetMngr.Show();
        }
        private void btn_ConnectionManager_Click(object sender, RibbonControlEventArgs e)
        {
            //intance of connectionmanger 
            ConnectionManger ConnMng = new ConnectionManger();
            ConnMng.Show();
        }
        private void btn_StartDate_Click(object sender, RibbonControlEventArgs e)
        {
            StartDatePicker.Show();
        }
        private void btn_EndDate_Click(object sender, RibbonControlEventArgs e)
        {
            EndDatePicker.Show();
        }
        private void cb_activeConnection_ItemsLoading(object sender, RibbonControlEventArgs e)
        {
            cb_connections_update();
        }
        private void btn_Query_Click(object sender, RibbonControlEventArgs e)
        {
            Excel._Workbook activeWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook as Excel.Workbook;
            if (cb_activeConnection.Text == "RefreshAll")
            {
                activeWorkbook.RefreshAll();
            }
            else
            {
                foreach (var connection in activeWorkbook.Connections.Cast<Excel.WorkbookConnection>())
                {
                    if (connection.Name == cb_activeConnection.Text) connection.Refresh();
                }
            }

        }
        private void btn_EditProcedure_Click(object sender, RibbonControlEventArgs e)
        {
            if (cb_activeConnection.Text == "RefreshAll") //this is not a connection to van not be edited
            {
               MessageBox.Show("Please select an other connection. 'RefreshAll' is not a connection", "Sorry", MessageBoxButtons.OK);
            }
            else
            {
                if (ProcMngr == null)
                {
                    ProcMngr = new StoredProcedureManger(cb_activeConnection.Text);
                }
                else if (ProcMngr.activeconnection != cb_activeConnection.Text)
                {
                    ProcMngr.Close();
                    ProcMngr.Dispose();
                    ProcMngr = new StoredProcedureManger(cb_activeConnection.Text);
                }
                ProcMngr.Show();
            }
        }
        private void btn_nDays_Click(object sender, RibbonControlEventArgs e)
        {
            MessageBox.Show("not done", "OEPS", MessageBoxButtons.OK);
        }
        private void cb_Lochierarchy_TextChanged(object sender, RibbonControlEventArgs e)
        {
            ProcMngr.lochierarchy.input = cb_Lochierarchy.Text;
        }
        private void cb_locations_TextChanged(object sender, RibbonControlEventArgs e)
        {
            ProcMngr.locations.input = cb_locations.Text;
        }
        private void cb_assets_TextChanged(object sender, RibbonControlEventArgs e)
        {
            ProcMngr.assets.input = cb_assets.Text;
        }
        #endregion

    }
}
