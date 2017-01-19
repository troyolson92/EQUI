﻿using System;
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
        //connection to maximo
        MaximoComm lMaximoComm = new MaximoComm();
        //local Asset data instance
        applData.ASSETSDataTable lASSETS = new applData.ASSETSDataTable();
        //procedure manager instance 
        StoredProcedureManger ProcMngr;
        //asset manager instance
        AssetManager AssetMngr;
        //connection manager instance
        ConnectionManger ConnMng; 
        //intance of datetimepickers;
        dtPicker StartDatePicker;
        dtPicker EndDatePicker;
        //active workbook
        Excel.Workbook activeWorkbook; 

        private void EquipmentDBRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            //Fill local dataset
            using (applDataTableAdapters.ASSETSTableAdapter adapter = new applDataTableAdapters.ASSETSTableAdapter())
            {
                adapter.Fill(lASSETS);
            }
            //populate with existing workbook connections
            dd_connections_update();
            //set active workbook
            activeWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook as Excel.Workbook;
            //fire event when sheets changes (set the active connection automaitcly)
            if (activeWorkbook != null)  activeWorkbook.SheetChange += activeWorkbook_SheetChange;
        }

        //fires on sheetchanges and sets the active connection
        void activeWorkbook_SheetChange(object Sh, Excel.Range Target)
        {
            dd_connections_update();
            string worksheetconn = activeSheet_connection();
            foreach (RibbonDropDownItem item in dd_activeConnection.Items)
            {
                if ((item.Label == worksheetconn) && (item.Label != dd_activeConnection.SelectedItem.Label))
                {
                    dd_activeConnection.SelectedItem = item;
                    sync_with_activeconnection();
                }
            }
        }
        //returns the connection name of the active sheet 
        private String activeSheet_connection()
        {
            Excel.Worksheet activeWorksheet = Globals.ThisAddIn.Application.ActiveSheet as Excel.Worksheet;
            foreach (Excel.QueryTable oTable in activeWorksheet.QueryTables)
            {
                Excel.WorkbookConnection conn = oTable.WorkbookConnection;
                return conn.Name.ToString();
            }

            foreach (Excel.ListObject oListobject in activeWorksheet.ListObjects)
            {
                Excel.QueryTable oTable = oListobject.QueryTable;
                Excel.WorkbookConnection conn = oTable.WorkbookConnection;
                return conn.Name.ToString();
            }
            return null;
        }
        /*  If the active connection changes than create a new instance of the procmngr
         *  =>Only when its a real connection (not refreshall mode)
         *  =>also load the "available" parameters from procmager back into the ribbon 
         */ 
        private void sync_with_activeconnection()
        {
            if (dd_activeConnection.SelectedItem.Label != "RefreshAll")
            {
                if (ProcMngr == null)
                {
                    ProcMngr = new StoredProcedureManger(dd_activeConnection.SelectedItem.Label);
                }
                else if (ProcMngr.activeconnection != dd_activeConnection.SelectedItem.Label)
                {
                    ProcMngr.Close();
                    ProcMngr.Dispose();
                    ProcMngr = new StoredProcedureManger(dd_activeConnection.SelectedItem.Label);
                }
                //event handeler for sheet Hide. (to trigger sync with ribbon)
                ProcMngr.Deactivate += ProcMngr_Deactivate;
            }
            //loads the available parameters back into the ribbon
            set_RibonToProcedureManager();
        }

        void ProcMngr_Deactivate(object sender, EventArgs e)
        {
            sync_with_activeconnection();
        }

        #region population of comboboxes (dynamic filtering)
        //population of dynamic boxes
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
        public void dd_connections_update()
        {
            dd_activeConnection.Items.Clear();
            RibbonDropDownItem defaultitem = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
            defaultitem.Label = "RefreshAll";
            dd_activeConnection.Items.Add(defaultitem);
            dd_activeConnection.SelectedItem.Label = "RefreshAll";
            Excel._Workbook activeWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook as Excel.Workbook;
            if (activeWorkbook == null) return; 
                foreach (var connection in activeWorkbook.Connections.Cast<Excel.WorkbookConnection>())
                {
                    switch (connection.Type)
                    {
                        case Excel.XlConnectionType.xlConnectionTypeODBC:
                            RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                            item.Label = connection.Name;
                            dd_activeConnection.Items.Add(item);
                            break;
                        default:
                            Debug.WriteLine("connection type not supported");
                            break;
                    }
                }
        }
        private void set_RibonToProcedureManager()
        {
            //set ribbon control values 
            cb_assets.Text = ProcMngr.assets.input ;
            cb_Lochierarchy.Text = ProcMngr.lochierarchy.input;
            cb_locations.Text = ProcMngr.locations.input;
            //set enabeld or disabled. 
            cb_assets.Enabled = ProcMngr.assets.active;
            cb_Lochierarchy.Enabled = ProcMngr.lochierarchy.active;
            cb_locations.Enabled = ProcMngr.locations.active;
            btn_StartDate.Enabled = ProcMngr.startDate.active;
            btn_EndDate.Enabled = ProcMngr.endDate.active;
            btn_nDays.Enabled = ProcMngr.daysBack.active;
        }
        #endregion

        #region ribbon event handeling
        /*Reloads the asset collection each time the user drops the box
         * Each time because of dynamic filtering
         * =>>>>need to look if I can inprove this Slow as ...
         */
        private void cb_Lochierarchy_itemsload(object sender, RibbonControlEventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            cb_lochierarchy_update();
            Cursor.Current = Cursors.Default;
        }
        private void cb_assets_itemsload(object sender, RibbonControlEventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            cb_assets_update();
            Cursor.Current = Cursors.Default;
        }
        private void cb_locations_itemsload(object sender, RibbonControlEventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            cb_locations_update();
            Cursor.Current = Cursors.Default;
        }
        //handel feedback from filter controls
        private void btn_StartDate_Click(object sender, RibbonControlEventArgs e)
        {
           if (StartDatePicker == null) StartDatePicker = new dtPicker(ProcMngr.startDate);
           StartDatePicker.Show();
        }
        private void btn_EndDate_Click(object sender, RibbonControlEventArgs e)
        {
            if (EndDatePicker == null) EndDatePicker = new dtPicker(ProcMngr.endDate);
            EndDatePicker.Show();
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
        //keeps the collection of connections up to date
        private void dd_activeConnection_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            sync_with_activeconnection();
        }
        //show connection manger of valid connection is selected
        private void btn_EditProcedure_Click(object sender, RibbonControlEventArgs e)
        {
            if (dd_activeConnection.SelectedItem.Label == "RefreshAll") //this is not a connection to van not be edited
            {
                MessageBox.Show("Please select an other connection. 'RefreshAll' is not a connection", "Sorry", MessageBoxButtons.OK);
            }
            else
            {
                ProcMngr.Show();
                ProcMngr.ShowOnClick();
            }
        }
        //refresh the active connection or refresh all connections if needed
        private void btn_Query_Click(object sender, RibbonControlEventArgs e)
        {
            Excel._Workbook activeWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook as Excel.Workbook;
            if (dd_activeConnection.SelectedItem.Label == "RefreshAll")
            {
                activeWorkbook.RefreshAll();
            }
            else
            {
                ProcMngr.GADATA_ProcMngrToActiveConnection();
                foreach (var connection in activeWorkbook.Connections.Cast<Excel.WorkbookConnection>())
                {
                    if (connection.Name == dd_activeConnection.SelectedItem.Label)
                    {

                        //connects the ribbon filter controls with the procMngr
                        if (ProcMngr.get_ODBCconnString_from_activeconnection().Like("%Max%")) //MX7connections
                        {
                            ProcMngr.MX7_ProcMngrToActiveConnection(lMaximoComm.oracle_get_QueryTemplate_from_GADATA(connection.Name, "MX7"));
                            connection.Refresh();
                        }
                        else if (ProcMngr.get_ODBCconnString_from_activeconnection().Like("%GADATA%")) //GADATAconnections
                        {
                            ProcMngr.GADATA_ProcMngrToActiveConnection();
                            connection.Refresh();
                        }
                        else
                        {
                            Debug.WriteLine("Unable to find connection");
                        }
                    }
                }
            }

        }
        //create tools instance when needed. (multible instances are allowed for now) 
        private void btn_AssetManager_Click(object sender, RibbonControlEventArgs e)
        {
            if (AssetManager == null)  AssetMngr = new AssetManager();
            AssetMngr.Show();
        }
        private void btn_ConnectionManager_Click(object sender, RibbonControlEventArgs e)
        {
            if (ConnMng != null) ConnMng.Dispose();
            ConnMng = new ConnectionManger();
            ConnMng.Show();
        }
        #endregion

        private void btn_refreshconn_Click(object sender, RibbonControlEventArgs e)
        {
            dd_connections_update();
        }
    }
}
