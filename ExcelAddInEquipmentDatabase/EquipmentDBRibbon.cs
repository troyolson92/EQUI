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

namespace ExcelAddInEquipmentDatabase
{


    public partial class EquipmentDBRibbon
    {
        //connection to gadata
        GadataComm lGadataComm = new GadataComm();
        //local Asset data instance
        applData.ASSETSDataTable lASSETS = new applData.ASSETSDataTable();

        private void EquipmentDBRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            Debug.WriteLine("Ribbon1_Load");
            //Fill local dataset
            using (applDataTableAdapters.ASSETSTableAdapter adapter = new applDataTableAdapters.ASSETSTableAdapter())
            {
                adapter.Fill(lASSETS);
            }
            //Set default filters
            cb_assets.Text = "UR%";
            cb_Lochierarchy.Text = "%";
            cb_locations.Text = "%";
            //
            cb_connections_update();

        }

        //population of comboboxes (dynamic filtering)
        private void cb_Lochierarchy_update()
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
        
        //ribbon event handeling
        private void cb_Lochierarchy_itemsload(object sender, RibbonControlEventArgs e)
        {
            cb_Lochierarchy_update();
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
            AssetManager MngrForm = new AssetManager();
            MngrForm.Show();
        }
        private void btn_ConnectionManager_Click(object sender, RibbonControlEventArgs e)
        {
            ConnectionManger ConnMngForm = new ConnectionManger();
            ConnMngForm.Show();
        }
        private void btn_StartDate_Click(object sender, RibbonControlEventArgs e)
        {
            dtPicker StartDatePicker = new dtPicker();
            StartDatePicker.Show();
        }
        private void btn_EndDate_Click(object sender, RibbonControlEventArgs e)
        {
            dtPicker EndDatePicker = new dtPicker();
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
        

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
           StoredProcedureManger ProcMngr = new StoredProcedureManger();
           ProcMngr.Show();
        }
    }



}
