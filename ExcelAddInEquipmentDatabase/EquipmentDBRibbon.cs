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
using System.Text.RegularExpressions;
using System.IO;
using EQUICommunictionLib;
using EQUIToolsLib;
using System.Threading;


namespace ExcelAddInEquipmentDatabase
{
    [System.Runtime.InteropServices.GuidAttribute("0B866AC0-9B93-40CD-827F-FAF350EC0C0E")]
    public partial class EquipmentDBRibbon
    {
        //
        //debugger
        myDebugger Debugger = new myDebugger();
        //connection to gadata
        GadataComm lGadataComm = new GadataComm();
        //connection to maximo
        MaximoComm lMaximoComm = new MaximoComm();
        //connection to GADATA for maximo querys
        OracleQuery lMaximoQuery = new OracleQuery();
        //local Users data instance 
        RoleProvider roleProvider = new RoleProvider();
        public string[] userRoles;
        //local Asset data instance
        applData.ASSETSDataTable lASSETS = new applData.ASSETSDataTable();
        //local ParameterSets data instance 
        // applData.QUERYParametersDataTable lParameterSets = new applData.QUERYParametersDataTable();
        //local worsksheet function instance
        WorksheetFeatures lWorksheetFeatures = new WorksheetFeatures();
        //procedure manager instance 
        //StoredProcedureManger ProcMngr;
        Forms.ProcedureManager ProcMngr;
        Microsoft.Office.Tools.CustomTaskPane ProcedureMangerTaskPane;
        //asset manager instance
        AssetManager AssetMngr;
        //connection manager instance
        ConnectionManger ConnMng;
        //intance of datetimepickers;
        dtPicker StartDatePicker;
        dtPicker EndDatePicker;
        //if user trigger a refresh (because i'm an idiot and did not find a refresh finished event)
        bool TriggerRefresh = false;

        private void EquipmentDBRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            // temp change update location
            ClickOnceUtil lClickOnce = new ClickOnceUtil();
            lClickOnce.CheckUpdateLocation();

            //set build version
            Assembly _assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(_assembly.Location);
            g_config.Label = string.Format("V:{0}", fvi.ProductVersion, "");
            //init debugger
            Debugger.Init();
            //Set user name and level
            dd_user_update();
            //Set controls according to user level
            apply_userLevel();
            //check here for offline mode. (disabels Querys)

            //force the DSN connection to the host system
            lGadataComm.Make_DSN();
            lMaximoComm.Make_DSN(lMaximoComm.SystemMX7);
            //find connections in wb
            dd_connections_update();
            //fill with templates
            gall_templates_update();
            //subscribe to workbook open event
            Globals.ThisAddIn.Application.WorkbookActivate += Application_WorkbookActivate;
            //subscribe to sheet change event.
            Globals.ThisAddIn.Application.SheetActivate += Application_SheetActivate;
            //subscribe to before rightclick for context menus.
            Globals.ThisAddIn.Application.SheetBeforeRightClick += lWorksheetFeatures.Application_SheetBeforeRightClick;
            //subscribe to refresh finished 
            Globals.ThisAddIn.Application.AfterCalculate += Application_AfterCalculate;
            //run background tick for refresh events 
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer
            {
                Interval = (60 * 1000) // 60 secs
            };
            timer.Tick += new EventHandler(Refresh_Tick);
            timer.Start();
        }

        void Application_AfterCalculate()
        {
            if (TriggerRefresh && tgbtn_Wrap.Checked)
            {
                SetWrapText(true);
            }
            TriggerRefresh = false;
        }

        void apply_userLevel()
        {
            userRoles = roleProvider.GetRolesForUser(dd_User.SelectedItem.Tag);
            if (userRoles.Contains("Administrator"))
           {
              dd_User.Enabled = true; //inpersonator 
              btn_AssetManager.Enabled = true;
              btn_ErrorMngr.Enabled = true;
           }
           else
           {
              dd_User.Enabled = false;
              btn_AssetManager.Enabled = false;
              btn_ErrorMngr.Enabled = false;
           }

            if(userRoles.Contains("AAOSR"))
            {
                Properties.Settings.Default.IsRobotgroup = true;
            }
            else
            {
                Properties.Settings.Default.IsRobotgroup = false;
            }

            if (userRoles.Contains("VSTOpoweruser"))
            {
                btn_ConnectionManager.Enabled = true;
                Properties.Settings.Default.IsPowerUser = true;
            }
            else
            {
                btn_ConnectionManager.Enabled = false;
                Properties.Settings.Default.IsPowerUser = false;
            }
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
                try
                {
                    Excel.QueryTable oTable = oListobject.QueryTable;
                    Excel.WorkbookConnection conn = oTable.WorkbookConnection;
                    return conn.Name.ToString();
                }
                catch (Exception ex)
                {
                    Debugger.Exeption(ex);
                    return null;
                }
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
                ProcMngr = new Forms.ProcedureManager(dd_activeConnection.SelectedItem.Label);
                //
                if (ProcedureMangerTaskPane != null) ProcedureMangerTaskPane.Dispose();
                ProcedureMangerTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(ProcMngr, "ProcedureManager");
                ProcedureMangerTaskPane.Width = ProcMngr.MaxWith() + 50;
                ProcedureMangerTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionLeft;
                ProcedureMangerTaskPane.Visible = btn_EditProcedure.Checked;
            }
            //event to reset button on taskpane close
            ProcedureMangerTaskPane.VisibleChanged += ProcedureMangerTaskPane_VisibleChanged;
            //event handeler for sheet Hide. (to trigger sync with ribbon)
            ProcMngr.MouseLeave += ProcMngr_Deactivate;
            //loads the available parameters back into the ribbon
            set_RibonToProcedureManager();
            //load parameter sets available on db 
            dd_ParameterSets_update();
        }

        void ProcedureMangerTaskPane_VisibleChanged(object sender, EventArgs e)
        {
            Microsoft.Office.Tools.CustomTaskPane ltp = (Microsoft.Office.Tools.CustomTaskPane)sender;
            btn_EditProcedure.Checked = ltp.Visible;
        }

        void ProcMngr_Deactivate(object sender, EventArgs e)
        {
            set_RibonToProcedureManager();
        }

        #region population of comboboxes (dynamic filtering)
        //population of dynamic boxes
        private void load_assetsDataset()
        {
            if ((lASSETS == null) || (lASSETS.Count == 0))
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
                List<string> distinctresult = data.Distinct().ToList();
                foreach (string thing in distinctresult)
                {
                    RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                    if (cb_Lochierarchy.Items.Count() > 500)
                    {
                        item.Label = "More items not loading...";
                        cb_Lochierarchy.Items.Add(item);
                        break;
                    }
                    item.Label = thing;
                    cb_Lochierarchy.Items.Add(item);

                }
            }
            catch (Exception e)
            {
                cb_Lochierarchy.Text = "%";
                Debugger.Exeption(e);
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
                List<string> distinctresult = data.Distinct().ToList();
                foreach (string thing in distinctresult)
                {
                    RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                    if (cb_locations.Items.Count() > 500)
                    {
                        item.Label = "More items not loading...";
                        cb_locations.Items.Add(item);
                        break;
                    }
                    item.Label = thing;
                    cb_locations.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                cb_locations.Text = "%";
                Debugger.Exeption(e);
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
                List<string> distinctresult = data.Distinct().ToList();
                foreach (string thing in distinctresult)
                {
                    RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                    if (cb_assets.Items.Count() > 500)
                    {
                        item.Label = "More items not loading...";
                        cb_assets.Items.Add(item);
                        break;
                    }
                    item.Label = thing;
                    cb_assets.Items.Add(item);

                }
            }
            catch (Exception e)
            {
                cb_locations.Text = "%";
                Debugger.Exeption(e);
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
                            break;
                    }
                }
                catch (Exception e)
                {
                    Debugger.Exeption(e);
                }
            }
        }
        private void dd_user_update()
        {
            dd_User.Items.Clear();
            //
            RibbonDropDownItem CurrentUser = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
            CurrentUser.Label = Environment.UserName; // currentuser;
            CurrentUser.Tag = Environment.UserDomainName + "\\" + Environment.UserName; // currentuser;
            dd_User.Items.Add(CurrentUser);
            //
            RibbonDropDownItem defaultUser = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
            defaultUser.Label = "default user"; // default;
            defaultUser.Tag = "default user"; //default;
            dd_User.Items.Add(defaultUser);
            //

            try
            {
                GADATAUserRoleProvider db = new GADATAUserRoleProvider();
                var users = from u in db.L_users
                            select u.username;
                users.Distinct().ToList();
                foreach (string user in users)
                {
                    RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                    item.Label = user.Split('\\')[1]; //get the cds id 
                    item.Tag = user; //full user name 
                    dd_User.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                Debugger.Exeption(e);
            }

        }
        private void gall_templates_update()
        {
            gall_templates.Items.Clear();

            List<string> Files = new List<string>(new string[] 
            { 
               @"\\gnlsnm0101.gen.volvocars.net\proj\6308-SHR-VCC22700\VSTO\Templates\EqDbGADATATemplate.xlsx"
              ,@"\\gnlsnm0101.gen.volvocars.net\proj\6308-SHR-VCC22700\VSTO\Templates\EqDbGBDATATemplate.xlsx" 
              ,@"\\gnlsnm0101.gen.volvocars.net\proj\6308-SHR-VCC22700\VSTO\Templates\StandAloneTemplate.xlsm"
            });
            foreach (string file in Files)
            {
                RibbonDropDownItem galleryItem = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                galleryItem.Tag = file;
                galleryItem.Label = Path.GetFileName(file);
                galleryItem.ScreenTip = "These templates will get you started.";
                gall_templates.Items.Add(galleryItem);
            }
        }
        private void gall_templates_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application.Workbooks.Open(gall_templates.SelectedItem.Tag, Type.Missing, true);
            }
            catch (Exception ex)
            {
                Debugger.Exeption(ex);
            }
        }

        private void dd_ParameterSets_update()
        {
            dd_ParameterSets.Enabled = false;
            dd_ParameterSets.Items.Clear();
            RibbonDropDownItem item;
            item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
            item.Label = "UserDef";
            dd_ParameterSets.Items.Add(item);
            foreach (string setName in ProcMngr.GADATA_Select_ParmSet_list(ProcMngr.activeSystem, ProcMngr.ProcedureName))
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
            cb_assets.Text = ProcMngr.assets.input;
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
                Debugger.Message(string.Format("Please try it again '{0}' not a valid number ", input));
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
                Debugger.Message("Please select an other connection. 'RefreshAll' is not a connection");
                btn_EditProcedure.Checked = false;
            }
            else
            {
                ProcedureMangerTaskPane.Visible = btn_EditProcedure.Checked;
            }
        }
        //refresh the active connection or refresh all connections if needed
        private void btn_Query_Click(object sender, RibbonControlEventArgs e)
        {
            //also set Startdate = enddate - daysback to make maximo work beter.
            if (ProcMngr == null) { Debugger.Message("ProcMnger is null"); return; }
            //format datatime to make days back work better 
            if (ProcMngr.daysBack.active) // added to make dateRange selection possible.
            {
                ProcMngr.startDate.input = DateTime.Now.AddDays(Convert.ToInt32(ProcMngr.daysBack.input) * -1);
                ProcMngr.endDate.input = DateTime.Now;
            }
            //Always include child assets for now. (will make tis beter or server side later) 
            if ((!String.IsNullOrEmpty(ProcMngr.locations.input) && Char.IsLetter(ProcMngr.locations.input[0])) == false) //because of GB locations issue
            {
                ProcMngr.locations.input = Regex.Replace(ProcMngr.locations.input, @"[A-Za-z\s]", "%");
                if (ProcMngr.locations.input.TrimEnd().EndsWith("%") == false) { ProcMngr.locations.input = ProcMngr.locations.input + "%"; }
                cb_locations.Text = ProcMngr.locations.input;
            }
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
                        //disable text wraping 
                        SetWrapText(false);
                        TriggerRefresh = true;
                        //connects the ribbon filter controls with the procMngr
                        if (ProcMngr.activeSystem == lMaximoComm.DsnMX7) //MX7connections
                        {
                            ProcMngr.MX7_ProcMngrToActiveConnection(lMaximoQuery.oracle_get_QueryTemplate_from_GADATA(connection.Name, lMaximoComm.SystemMX7));
                            connection.Refresh();
                        }
                        else if (ProcMngr.activeSystem == lGadataComm.DsnGADATA) //GADATAconnections
                        {
                            ProcMngr.GADATA_ProcMngrToActiveConnection();
                            connection.Refresh();
                        }
                        else
                        {
                            Debugger.Message("Unable to find connection");
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
            string helpfile = @"https://sharepoint.volvocars.net/sites/vcg_ga_aaosr/PublicDocuments/VSTO/Manuals/VSTO EQdatabase.pdf";
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
                Debugger.Message(@"Was not able to open the help file.
                                 File should be on <" + helpfile + @">
                                    Exeption:" + ex.Message);
            }
        }

        private void btn_ErrorMngr_Click(object sender, RibbonControlEventArgs e)
        {
            Forms.ErrorManager lErrorManager = new Forms.ErrorManager();
            lErrorManager.Show();
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            if (tbtn_Autorefresh.Checked)
            {
                Excel.Workbooks workbooks = Globals.ThisAddIn.Application.Workbooks;
                foreach (Excel._Workbook Workbook in workbooks)
                {
                    Workbook.RefreshAll();
                }
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

        private void dd_User_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            apply_userLevel();
        }

        private void tgbtn_Wrap_Click(object sender, RibbonControlEventArgs e)
        {
            SetWrapText(tgbtn_Wrap.Checked);
        }

        private static void SetWrapText(bool state)
        {
            Excel.Worksheet Sheet = Globals.ThisAddIn.Application.ActiveSheet as Excel.Worksheet;
            foreach (Excel.ListObject oListobject in Sheet.ListObjects)
            {
                Sheet.get_Range(oListobject.Name).Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
                Sheet.get_Range(oListobject.Name).Columns.WrapText = state;
                if (state) Sheet.get_Range(oListobject.Name).EntireColumn.AutoFit();
            }

        }

        //
    }
}

