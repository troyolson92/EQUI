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
using System.Reflection;

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
        //local ParameterSets data instance 
        applData.QUERYParametersDataTable lParameterSets = new applData.QUERYParametersDataTable();
        //local worsksheet function instance
        WorksheetFeatures lWorksheetFeatures = new WorksheetFeatures();
        //procedure manager instance 
        StoredProcedureManger ProcMngr;
        //asset manager instance
        AssetManager AssetMngr;
        //connection manager instance
        ConnectionManger ConnMng; 
        //intance of datetimepickers;
        dtPicker StartDatePicker;
        dtPicker EndDatePicker;
        
        private void EquipmentDBRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            //set build version
            Assembly _assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(_assembly.Location);
            g_config.Label = string.Format("V:{0}",fvi.ProductVersion,"");
            //Set user name and level
            dd_user_update();  
            //test 
            Application.EnableVisualStyles();
            //check here for offline mode. (disabels Querys)

            //find connections in wb
            dd_connections_update();
            //subscribe to workbook open event
            Globals.ThisAddIn.Application.WorkbookActivate += Application_WorkbookActivate;
            //subscribe to sheet change event.
            Globals.ThisAddIn.Application.SheetActivate += Application_SheetActivate;
            //subscribe to before rightclick for context menus.
            Globals.ThisAddIn.Application.SheetBeforeRightClick += lWorksheetFeatures.Application_SheetBeforeRightClick;
            //run background tick for refresh events 
            Timer timer = new Timer();
            timer.Interval = (60 * 1000); // 60 secs
            timer.Tick += new EventHandler(Refresh_Tick);
            timer.Start();
        }

        void Application_WorkbookActivate(Excel.Workbook Wb)
        {
            Set_activeconnection();
        }

        void Application_SheetActivate(object Sh)
        {
            Set_activeconnection();
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
        private void Set_activeconnection()
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

        private void sync_with_activeconnection()
        {
                if (ProcMngr != null) ProcMngr.Dispose();
                if (dd_activeConnection.SelectedItem.Label != "RefreshAll")
                {
                    ProcMngr = new StoredProcedureManger(dd_activeConnection.SelectedItem.Label);
                }
                //event handeler for sheet Hide. (to trigger sync with ribbon)
                ProcMngr.Deactivate += ProcMngr_Deactivate;
               //loads the available parameters back into the ribbon
                set_RibonToProcedureManager();
               //load parameter sets available on db 
                dd_ParameterSets_update();
        }

        void ProcMngr_Deactivate(object sender, EventArgs e)
        {
            set_RibonToProcedureManager();
        }

        #region population of comboboxes (dynamic filtering)
        //population of dynamic boxes
        private void load_assetsDataset()
        {
            if ((lASSETS == null) ||(lASSETS.Count == 0))
            {
                //Fill local dataset
                using (applDataTableAdapters.ASSETSTableAdapter adapter = new applDataTableAdapters.ASSETSTableAdapter())
                {
                    adapter.Fill(lASSETS);
                }
            }
        }
        private void cb_lochierarchy_update()
        {
            load_assetsDataset();
            cb_Lochierarchy.Items.Clear();
            try
            {
                var data = from a in lASSETS
                                         where a.LocationTree.Like(cb_Lochierarchy.Text)
                                         && a.LOCATION.Like(cb_locations.Text)
                                         && a.CLassificationId.Like(cb_assets.Text)
                                         && a.LocationTree != null
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
            load_assetsDataset();
            cb_locations.Items.Clear();
            try
            {
                var data = from a in lASSETS
                                         where a.LocationTree.Like(cb_Lochierarchy.Text)
                                         && a.LOCATION.Like(cb_locations.Text)
                                         && a.CLassificationId.Like(cb_assets.Text)
                                         && a.LOCATION != null
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
            load_assetsDataset();
            cb_assets.Items.Clear();
            try
            {
                var data = from a in lASSETS
                                         where a.LocationTree.Like(cb_Lochierarchy.Text)
                                         && a.LOCATION.Like(cb_locations.Text)
                                         && a.CLassificationId.Like(cb_assets.Text)
                                         && a.CLassificationId != null
                                         orderby a.CLassificationId descending
                                         select a.CLassificationId;
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
                    try
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
                    catch (Exception e )
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
        }
        private void dd_user_update()
        {
            RibbonDropDownItem defaultitem = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
            defaultitem.Label = Environment.UserName; // default;
            dd_User.Items.Add(defaultitem);

            //get you level 

            //get list of users and levels 

        }
        private void dd_ParameterSets_update()
        {
             dd_ParameterSets.Enabled = false;
             dd_ParameterSets.Items.Clear();
             RibbonDropDownItem item;
             item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
             item.Label = "UserDef";
             dd_ParameterSets.Items.Add(item);
             foreach (string setName in lGadataComm.Select_ParmSet_list(ProcMngr.activeSystem,ProcMngr.ProcedureName))
                {
                    if (setName == null) break;
                     item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                     item.Label = setName;
                     dd_ParameterSets.Items.Add(item);
                }
             if (dd_ParameterSets.Items.Count() > 1) dd_ParameterSets.Enabled = true;
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
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the number of days you whant to go back.", "Number of days back", "10", -1, -1);
            if (Microsoft.VisualBasic.Information.IsNumeric(input))
            {
                ProcMngr.daysBack.input = input;
                //also set Startdate = enddate - daysback to make maximo work beter.
                ProcMngr.startDate.input = DateTime.Now.AddDays(Convert.ToInt32(input) * -1);
                ProcMngr.endDate.input = DateTime.Now;
            }
            else
            {
                MessageBox.Show(string.Format("Please try it again '{0}' not a valid number ",input), "Sorry", MessageBoxButtons.OK);
                ProcMngr.daysBack.input = "10";
            }
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
                ProcMngr.ShowOnClick();
            }
        }
        //refresh the active connection or refresh all connections if needed
        private void btn_Query_Click(object sender, RibbonControlEventArgs e)
        {
            //also set Startdate = enddate - daysback to make maximo work beter.
            if (ProcMngr == null) { return; }
            ProcMngr.startDate.input = DateTime.Now.AddDays(Convert.ToInt32(ProcMngr.daysBack.input) * -1);
            ProcMngr.endDate.input = DateTime.Now;
            //
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
                        if (ProcMngr.activeSystem == lMaximoComm.DsnMX7) //MX7connections
                        {
                            ProcMngr.MX7_ProcMngrToActiveConnection(lMaximoComm.oracle_get_QueryTemplate_from_GADATA(connection.Name, lMaximoComm.SystemMX7));
                            connection.Refresh();
                        }
                        else if (ProcMngr.activeSystem == lGadataComm.DsnGADATA) //GADATAconnections
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
            if (AssetManager != null) AssetManager.Dispose();
            AssetMngr = new AssetManager();
            AssetMngr.Show();
        }
        private void btn_ConnectionManager_Click(object sender, RibbonControlEventArgs e)
        {
            if (ConnMng != null) ConnMng.Dispose();
            ConnMng = new ConnectionManger();
            ConnMng.Show();
            ConnMng.Disposed += ConnMng_Disposed;
        }

        void ConnMng_Disposed(object sender, EventArgs e)
        {
            Set_activeconnection();
        }

        private void dd_ParameterSets_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            ProcMngr.Load_ParmsSet(dd_ParameterSets.SelectedItem.Label);
            //loads the available parameters back into the ribbon
            set_RibonToProcedureManager();
        }
        #endregion

        private void btn_help_Click(object sender, RibbonControlEventArgs e)
        {
            string helpfile = @"\\gnlsnm0101.gen.volvocars.net\proj\6308-Shr-VC024800\OBJECTBEHEER GA\Robots\12. SW + Tools\RobotDatabase\VSTO\Manuals\VSTO EQdatabase.pdf";
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                process.StartInfo = startInfo;
                startInfo.FileName = helpfile;
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Was not able to open the help file.
                                 File should be on <"+helpfile+@">
                                    Exeption:"+ex.Message, "Sorry", MessageBoxButtons.OK);
            }
        }

        private void btn_testWs_Click(object sender, RibbonControlEventArgs e)
        {
//nothing
        }

        private void btn_docMngr_Click(object sender, RibbonControlEventArgs e)
        {
            Forms.DocManager lDocManager = new Forms.DocManager();
            lDocManager.Show();
        }

        private void btn_ErrorMngr_Click(object sender, RibbonControlEventArgs e)
        {
            Forms.ErrorManger lErrorManager = new Forms.ErrorManger();
            lErrorManager.Show();
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            if (tbtn_Autorefresh.Checked)
            {
                Excel._Workbook activeWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook as Excel.Workbook;
                activeWorkbook.RefreshAll();
            }
        }

        private void tbtn_StopRightClick_Click(object sender, RibbonControlEventArgs e)
        {
            if (tbtn_StopRightClick.Checked)
            {
                //UNsubscribe to before rightclick for context menus. (to play well with oter wbs
                Globals.ThisAddIn.Application.SheetBeforeRightClick -= lWorksheetFeatures.Application_SheetBeforeRightClick;
            }
            else
            {
                //subscribe to before rightclick for context menus.
                Globals.ThisAddIn.Application.SheetBeforeRightClick += lWorksheetFeatures.Application_SheetBeforeRightClick;
            }
        }


    }
}
