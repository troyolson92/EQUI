using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Tools.Ribbon;
using System.Diagnostics;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using EQUICommunictionLib;
using log4net.Appender;
using System.Configuration;

namespace ExcelAddInEquipmentDatabase
{
    [System.Runtime.InteropServices.GuidAttribute("0B866AC0-9B93-40CD-827F-FAF350EC0C0E")]
    public partial class EquipmentDBRibbon
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //connection to databases
        ConnectionManager ConnectionManager = new ConnectionManager();
        EquiEntities db = new EquiEntities();
        //local worksheet function instance
        WorksheetFeatures lWorksheetFeatures = new WorksheetFeatures();
        //procedure manager instance 
        Forms.ProcedureManager ProcMngr;
        Microsoft.Office.Tools.CustomTaskPane ProcedureMangerTaskPane;
        //connection manager instance
        ExcelConnectionManager ExcelConnectionManager;
        //instance of date time pickers;
        dtPicker StartDatePicker;
        dtPicker EndDatePicker;
        //Refresh busy
        bool TriggerRefresh = false;

        private void EquipmentDBRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info($"Loaded EQUI_{ Properties.Settings.Default.SiteName}");
            //set build version
            g_config.Label = $"V:{FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion}";
            //set ribbon label
            rib2.Label = $"EQUI_{Properties.Settings.Default.SiteName}";

            //check if user is power user
            RoleProvider roleProvider = new RoleProvider();
            string[] userRoles = roleProvider.GetRolesForUser(Environment.UserDomainName + "\\" + Environment.UserName);
            if (userRoles.Contains("VSTOpoweruser") || userRoles.Contains("Administrator"))
            {
                log.Info("VSTOpoweruser enabled");
                btn_ConnectionManager.Enabled = true;
            }
            else
            {
                btn_ConnectionManager.Enabled = false;
            }

            //fill with templates
            gall_templates.Items.Clear();
            try
            {
                DirectoryInfo d = new DirectoryInfo(Properties.Settings.Default.TemplateBasepath);//Assuming Test is your Folder
                foreach (FileInfo file in d.GetFiles("*.xls*"))
                {
                    if (!file.Name.Contains('$'))
                    {
                        RibbonDropDownItem galleryItem = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                        galleryItem.Tag = $"{Properties.Settings.Default.EquiBasepath}\\Resources\\VSTO\\TEMPLATES\\{file.Name}";
                        galleryItem.Label = file.Name;
                        galleryItem.ScreenTip = "These templates will get you started.";
                        gall_templates.Items.Add(galleryItem);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in getting templates", ex);
            }

            //subscribe to workbook open event
            Globals.ThisAddIn.Application.WorkbookActivate += Application_WorkbookActivate;
            //subscribe to sheet change event.
            Globals.ThisAddIn.Application.SheetActivate += Application_SheetActivate;
            //subscribe to refresh finished 
            Globals.ThisAddIn.Application.AfterCalculate += Application_AfterCalculate;
            //subscribe to before right click for context menus.
            Globals.ThisAddIn.Application.SheetBeforeRightClick += lWorksheetFeatures.Application_SheetBeforeRightClick;

            try
            {
                //force the DSN connection to the host system
                System.Data.SqlClient.SqlConnectionStringBuilder sqlconnection = new System.Data.SqlClient.SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ConnectionString);
                ODBCManager.CreateDSN(DsnNames.DsnEqui, "Equi database", sqlconnection["Server"].ToString(), "SQL Server", AppDomain.CurrentDomain.BaseDirectory + @"\Drivers\SqlServer\SQLSRV32.dll", false, DsnNames.DsnEqui);
                log.Info("Equi DSN set");

                //There is an issue with deploying the oracle driver. 
                //ODBCManager.CreateDSN("MAXIMO", "MAXIMO reporting database", "dpmxarct", "ODBC for oracle", AppDomain.CurrentDomain.BaseDirectory + @"\Drivers\Oracle\msorcl32.dll", false, "MAXIMO");
                ODBCManager.CreateDSN(DsnNames.DsnMX7, "MAXIMO reporting database", "dpmxarct", "ODBC for oracle", @"C:\Windows\System32\msorcl32.dll", false, DsnNames.DsnMX7);
            }
            catch (Exception ex)
            {
                log.Error("Error setting up DSN", ex);
            }
            //find connections in wb
            dd_connections_update();
        }


        /// <summary>
        /// When a workbook gets focus load connection
        /// </summary>
        /// <param name="Wb"></param>
        void Application_WorkbookActivate(Excel.Workbook Wb)
        {
            Set_activeconnection();
        }

        /// <summary>
        /// when sheet changes load connection
        /// </summary>
        /// <param name="Sh"></param>
        void Application_SheetActivate(object Sh)
        {
            Set_activeconnection();
        }

        /// <summary>
        /// if user trigged refresh and wrap text is on apply wrapping
        /// </summary>
        void Application_AfterCalculate()
        {
            if (TriggerRefresh && tgbtn_Wrap.Checked)
            {
                SetWrapText(true);
            }
            TriggerRefresh = false;
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
                    log.Error(ex);
                    return null;
                }
            }
            return null;
        }

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
                catch (Exception ex)
                {
                    log.Error("Failed to update connections",ex);
                }
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

        private void btn_Query_Click(object sender, RibbonControlEventArgs e)
        {
            if (ProcMngr == null) return;
            //format date time to make days back work better 
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
                        if (ProcMngr.activeSystem == DsnNames.DsnMX7) //MX7connections
                        {
                            ProcMngr.MX7_ProcMngrToActiveConnection(db.QUERYS.Where(c => c.NAME == connection.Name && c.SYSTEM == DsnNames.DsnMX7).First().QUERY);
                            connection.Refresh();
                        }
                        else if (ProcMngr.activeSystem == DsnNames.DsnEqui) //GADATAconnections
                        {
                            ProcMngr.GADATA_ProcMngrToActiveConnection();
                            connection.Refresh();
                        }
                    }
                }
            }

        }
        private void dd_activeConnection_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            sync_with_activeconnection();
        }

        private void btn_EditProcedure_Click(object sender, RibbonControlEventArgs e)
        {
            if (dd_activeConnection.SelectedItem.Label == "RefreshAll") //this is not a connection to van not be edited
            {
                DialogResult result = MessageBox.Show("Please select an other connection. 'RefreshAll' is not a connection", "Warning", MessageBoxButtons.OK);
                btn_EditProcedure.Checked = false;
            }
            else
            {
                ProcedureMangerTaskPane.Visible = btn_EditProcedure.Checked;
            }
        }

        private void dd_ParameterSets_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
            ProcMngr.Load_ParmsSet(dd_ParameterSets.SelectedItem.Label);
            //loads the available parameters back into the ribbon
            set_RibonToProcedureManager();
        }

        /// <summary>
        /// Load dropdown boxes
        /// </summary>
        private void cb_Lochierarchy_itemsload(object sender, RibbonControlEventArgs e)
        {
            cb_Lochierarchy.Items.Clear();
            try
            {
                List<string> result = new List<string>() { "A", "B" };
                foreach (string thing in result)
                {
                    RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                    item.Label = thing;
                    cb_Lochierarchy.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                cb_Lochierarchy.Text = "%";
            }
        }

        private void cb_assets_itemsload(object sender, RibbonControlEventArgs e)
        {
            cb_assets.Items.Clear();
            try
            {
                List<string> result = new List<string>() { "e", "F" };
                foreach (string thing in result)
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
            catch (Exception ex)
            {
                log.Error(ex);
                cb_locations.Text = "%";
            }
        }

        private void cb_locations_itemsload(object sender, RibbonControlEventArgs e)
        {
            cb_locations.Items.Clear();
            try
            {
                List<string> result = new List<string>() { "c", "D" };
                foreach (string thing in result)
                {
                    RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                    item.Label = thing;
                    cb_locations.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                cb_locations.Text = "%";
            }
        }

        /// <summary>
        /// update procedure manager when ribbon text changes
        /// </summary>
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

        /// <summary>
        /// handle date picker and days back ribbon controls
        /// </summary>
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
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter number of days of data to get", "Number of days back", "10", -1, -1);
            if (Microsoft.VisualBasic.Information.IsNumeric(input))
            {
                ProcMngr.daysBack.input = input;
                ProcMngr.startDate.input = DateTime.Now.AddDays(Convert.ToInt32(input) * -1);
                ProcMngr.endDate.input = DateTime.Now;
            }
            else
            {
                DialogResult result = MessageBox.Show($"Please try it again '{input}' not a valid number", "User input error", MessageBoxButtons.OK);
                ProcMngr.daysBack.input = "10";
            }
        }

        /// <summary>
        /// Handle ribbon optional buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gall_templates_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application.Workbooks.Open(gall_templates.SelectedItem.Tag, Type.Missing, true);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void btn_ConnectionManager_Click(object sender, RibbonControlEventArgs e)
        {
            if (ExcelConnectionManager != null) ExcelConnectionManager.Dispose();
            ExcelConnectionManager = new ExcelConnectionManager();
            ExcelConnectionManager.Show();
        }

        private void btn_help_Click(object sender, RibbonControlEventArgs e)
        {
            string helpfile = Properties.Settings.Default.Helpfile;
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
                DialogResult result = MessageBox.Show($"Was not able to open the help file. File should be on <{helpfile}>", "File not found", MessageBoxButtons.OK);
                log.Error(ex);
            }
        }

        private void tbtn_StopRightClick_Click(object sender, RibbonControlEventArgs e)
        {
            if (tbtn_StopRightClick.Checked)
            {
                //UNsubscribe to before right click for context menus. (to play well with other workbooks that uses context menus in a shit way)
                Globals.ThisAddIn.Application.SheetBeforeRightClick -= lWorksheetFeatures.Application_SheetBeforeRightClick;
            }
            else
            {
                //subscribe to before right click for context menus.
                Globals.ThisAddIn.Application.SheetBeforeRightClick += lWorksheetFeatures.Application_SheetBeforeRightClick;
            }
        }

        private void tbtn_Wrap_Click(object sender, RibbonControlEventArgs e)
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

        private void btn_logfile_Click(object sender, RibbonControlEventArgs e)
        {
            string logfile = log4net.LogManager.GetRepository()
                                .GetAppenders()
                                .OfType<FileAppender>()
                                .FirstOrDefault().File;
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                process.StartInfo = startInfo;
                startInfo.FileName = logfile;
                process.Start();
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show($"Was not able to open the log file. File should be on <{logfile}>", "File not found", MessageBoxButtons.OK);
                log.Error(ex);
            }

        }
    }
}

