namespace ExcelAddInEquipmentDatabase
{
    partial class EquipmentDBRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public EquipmentDBRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Office.Tools.Ribbon.RibbonDialogLauncher ribbonDialogLauncherImpl1 = this.Factory.CreateRibbonDialogLauncher();
            this.tab1 = this.Factory.CreateRibbonTab();
            this.rib2 = this.Factory.CreateRibbonTab();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btn_Query = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.dd_activeConnection = this.Factory.CreateRibbonDropDown();
            this.dd_preselect = this.Factory.CreateRibbonDropDown();
            this.btn_EditProcedure = this.Factory.CreateRibbonButton();
            this.AssetManager = this.Factory.CreateRibbonGroup();
            this.cb_Lochierarchy = this.Factory.CreateRibbonComboBox();
            this.cb_locations = this.Factory.CreateRibbonComboBox();
            this.cb_assets = this.Factory.CreateRibbonComboBox();
            this.separator2 = this.Factory.CreateRibbonSeparator();
            this.btn_StartDate = this.Factory.CreateRibbonButton();
            this.btn_EndDate = this.Factory.CreateRibbonButton();
            this.btn_nDays = this.Factory.CreateRibbonButton();
            this.gr3 = this.Factory.CreateRibbonGroup();
            this.Btn_debugging = this.Factory.CreateRibbonToggleButton();
            this.proc_parameters = this.Factory.CreateRibbonGroup();
            this.btn_ConnectionManager = this.Factory.CreateRibbonButton();
            this.btn_AssetManager = this.Factory.CreateRibbonButton();
            this.btn_refreshconn = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.rib2.SuspendLayout();
            this.group2.SuspendLayout();
            this.AssetManager.SuspendLayout();
            this.gr3.SuspendLayout();
            this.proc_parameters.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // rib2
            // 
            this.rib2.Groups.Add(this.group2);
            this.rib2.Groups.Add(this.AssetManager);
            this.rib2.Groups.Add(this.gr3);
            this.rib2.Groups.Add(this.proc_parameters);
            this.rib2.Label = "EQDATABASE";
            this.rib2.Name = "rib2";
            // 
            // group2
            // 
            this.group2.Items.Add(this.btn_Query);
            this.group2.Items.Add(this.separator1);
            this.group2.Items.Add(this.dd_activeConnection);
            this.group2.Items.Add(this.dd_preselect);
            this.group2.Items.Add(this.btn_EditProcedure);
            this.group2.Label = "Connection Manager";
            this.group2.Name = "group2";
            // 
            // btn_Query
            // 
            this.btn_Query.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btn_Query.Label = "Query";
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.OfficeImageId = "DataRefreshAll";
            this.btn_Query.ShowImage = true;
            this.btn_Query.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_Query_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // dd_activeConnection
            // 
            this.dd_activeConnection.Label = "ActiveConn";
            this.dd_activeConnection.Name = "dd_activeConnection";
            this.dd_activeConnection.OfficeImageId = "OrganizationChartSelectLevel";
            this.dd_activeConnection.ShowImage = true;
            this.dd_activeConnection.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.dd_activeConnection_SelectionChanged);
            // 
            // dd_preselect
            // 
            this.dd_preselect.Enabled = false;
            this.dd_preselect.Label = "uQuery";
            this.dd_preselect.Name = "dd_preselect";
            this.dd_preselect.OfficeImageId = "ImportMoreMenu";
            this.dd_preselect.ShowImage = true;
            // 
            // btn_EditProcedure
            // 
            this.btn_EditProcedure.Label = "Procedure parameters";
            this.btn_EditProcedure.Name = "btn_EditProcedure";
            this.btn_EditProcedure.OfficeImageId = "SmartArtInsert";
            this.btn_EditProcedure.ShowImage = true;
            this.btn_EditProcedure.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_EditProcedure_Click);
            // 
            // AssetManager
            // 
            this.AssetManager.Items.Add(this.cb_Lochierarchy);
            this.AssetManager.Items.Add(this.cb_locations);
            this.AssetManager.Items.Add(this.cb_assets);
            this.AssetManager.Items.Add(this.separator2);
            this.AssetManager.Items.Add(this.btn_StartDate);
            this.AssetManager.Items.Add(this.btn_EndDate);
            this.AssetManager.Items.Add(this.btn_nDays);
            this.AssetManager.Label = "Asset and time manager";
            this.AssetManager.Name = "AssetManager";
            // 
            // cb_Lochierarchy
            // 
            this.cb_Lochierarchy.Enabled = false;
            this.cb_Lochierarchy.Label = "Lochierarchy";
            this.cb_Lochierarchy.Name = "cb_Lochierarchy";
            this.cb_Lochierarchy.Text = null;
            this.cb_Lochierarchy.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_Lochierarchy_itemsload);
            this.cb_Lochierarchy.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_Lochierarchy_TextChanged);
            // 
            // cb_locations
            // 
            this.cb_locations.Enabled = false;
            this.cb_locations.Label = "Location";
            this.cb_locations.Name = "cb_locations";
            this.cb_locations.Text = null;
            this.cb_locations.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_locations_itemsload);
            this.cb_locations.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_locations_TextChanged);
            // 
            // cb_assets
            // 
            this.cb_assets.Enabled = false;
            this.cb_assets.Label = "Asset";
            this.cb_assets.Name = "cb_assets";
            this.cb_assets.Text = null;
            this.cb_assets.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_assets_itemsload);
            this.cb_assets.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_assets_TextChanged);
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            // 
            // btn_StartDate
            // 
            this.btn_StartDate.Enabled = false;
            this.btn_StartDate.Label = "StartDate";
            this.btn_StartDate.Name = "btn_StartDate";
            this.btn_StartDate.OfficeImageId = "FillRight";
            this.btn_StartDate.ShowImage = true;
            this.btn_StartDate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_StartDate_Click);
            // 
            // btn_EndDate
            // 
            this.btn_EndDate.Enabled = false;
            this.btn_EndDate.Label = "EndDate";
            this.btn_EndDate.Name = "btn_EndDate";
            this.btn_EndDate.OfficeImageId = "FillLeft";
            this.btn_EndDate.ShowImage = true;
            this.btn_EndDate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_EndDate_Click);
            // 
            // btn_nDays
            // 
            this.btn_nDays.Enabled = false;
            this.btn_nDays.Label = "nDays";
            this.btn_nDays.Name = "btn_nDays";
            this.btn_nDays.OfficeImageId = "TableExportTableToSharePointList";
            this.btn_nDays.ShowImage = true;
            this.btn_nDays.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_nDays_Click);
            // 
            // gr3
            // 
            this.gr3.Items.Add(this.Btn_debugging);
            this.gr3.Items.Add(this.btn_refreshconn);
            this.gr3.Label = "DEBUGGING";
            this.gr3.Name = "gr3";
            // 
            // Btn_debugging
            // 
            this.Btn_debugging.Label = "Enbl_dbg";
            this.Btn_debugging.Name = "Btn_debugging";
            // 
            // proc_parameters
            // 
            this.proc_parameters.DialogLauncher = ribbonDialogLauncherImpl1;
            this.proc_parameters.Items.Add(this.btn_ConnectionManager);
            this.proc_parameters.Items.Add(this.btn_AssetManager);
            this.proc_parameters.Label = "Configuration";
            this.proc_parameters.Name = "proc_parameters";
            // 
            // btn_ConnectionManager
            // 
            this.btn_ConnectionManager.Label = "MngConnections";
            this.btn_ConnectionManager.Name = "btn_ConnectionManager";
            this.btn_ConnectionManager.OfficeImageId = "AdpDiagramAddRelatedTables";
            this.btn_ConnectionManager.ShowImage = true;
            this.btn_ConnectionManager.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ConnectionManager_Click);
            // 
            // btn_AssetManager
            // 
            this.btn_AssetManager.Label = "MngAssets";
            this.btn_AssetManager.Name = "btn_AssetManager";
            this.btn_AssetManager.OfficeImageId = "FormulaMoreFunctionsMenu";
            this.btn_AssetManager.ShowImage = true;
            this.btn_AssetManager.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_AssetManager_Click);
            // 
            // btn_refreshconn
            // 
            this.btn_refreshconn.Label = "connRefresh";
            this.btn_refreshconn.Name = "btn_refreshconn";
            this.btn_refreshconn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_refreshconn_Click);
            // 
            // EquipmentDBRibbon
            // 
            this.Name = "EquipmentDBRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Tabs.Add(this.rib2);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.EquipmentDBRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.rib2.ResumeLayout(false);
            this.rib2.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.AssetManager.ResumeLayout(false);
            this.AssetManager.PerformLayout();
            this.gr3.ResumeLayout(false);
            this.gr3.PerformLayout();
            this.proc_parameters.ResumeLayout(false);
            this.proc_parameters.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonTab rib2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_AssetManager;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup AssetManager;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cb_assets;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cb_Lochierarchy;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cb_locations;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup proc_parameters;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Query;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ConnectionManager;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_StartDate;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_EndDate;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_nDays;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_EditProcedure;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dd_activeConnection;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dd_preselect;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup gr3;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton Btn_debugging;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_refreshconn;
    }

    partial class ThisRibbonCollection
    {
        internal EquipmentDBRibbon EquipmentDBRibbon
        {
            get { return this.GetRibbon<EquipmentDBRibbon>(); }
        }
    }
}
