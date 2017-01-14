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
            testlabel();
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
            this.tab1 = this.Factory.CreateRibbonTab();
            this.rib2 = this.Factory.CreateRibbonTab();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btn_Query = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.btn_AssetManager = this.Factory.CreateRibbonButton();
            this.btn_ConnectionManager = this.Factory.CreateRibbonButton();
            this.cb_activeConnection = this.Factory.CreateRibbonComboBox();
            this.AssetManager = this.Factory.CreateRibbonGroup();
            this.cb_Lochierarchy = this.Factory.CreateRibbonComboBox();
            this.cb_locations = this.Factory.CreateRibbonComboBox();
            this.cb_assets = this.Factory.CreateRibbonComboBox();
            this.separator2 = this.Factory.CreateRibbonSeparator();
            this.btn_StartDate = this.Factory.CreateRibbonButton();
            this.btn_EndDate = this.Factory.CreateRibbonButton();
            this.btn_nDays = this.Factory.CreateRibbonButton();
            this.proc_parameters = this.Factory.CreateRibbonGroup();
            this.button1 = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.rib2.SuspendLayout();
            this.group2.SuspendLayout();
            this.AssetManager.SuspendLayout();
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
            this.rib2.Groups.Add(this.proc_parameters);
            this.rib2.Label = "EQdatabase";
            this.rib2.Name = "rib2";
            // 
            // group2
            // 
            this.group2.Items.Add(this.btn_Query);
            this.group2.Items.Add(this.separator1);
            this.group2.Items.Add(this.btn_AssetManager);
            this.group2.Items.Add(this.btn_ConnectionManager);
            this.group2.Items.Add(this.cb_activeConnection);
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
            // btn_AssetManager
            // 
            this.btn_AssetManager.Label = "MngAssets";
            this.btn_AssetManager.Name = "btn_AssetManager";
            this.btn_AssetManager.OfficeImageId = "FormulaMoreFunctionsMenu";
            this.btn_AssetManager.ShowImage = true;
            this.btn_AssetManager.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_AssetManager_Click);
            // 
            // btn_ConnectionManager
            // 
            this.btn_ConnectionManager.Label = "MngConnections";
            this.btn_ConnectionManager.Name = "btn_ConnectionManager";
            this.btn_ConnectionManager.OfficeImageId = "AdpDiagramAddRelatedTables";
            this.btn_ConnectionManager.ShowImage = true;
            this.btn_ConnectionManager.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ConnectionManager_Click);
            // 
            // cb_activeConnection
            // 
            this.cb_activeConnection.Label = "ActiveConn";
            this.cb_activeConnection.Name = "cb_activeConnection";
            this.cb_activeConnection.Text = null;
            this.cb_activeConnection.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_activeConnection_ItemsLoading);
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
            this.cb_Lochierarchy.Label = "Lochierarchy";
            this.cb_Lochierarchy.Name = "cb_Lochierarchy";
            this.cb_Lochierarchy.Text = null;
            this.cb_Lochierarchy.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_Lochierarchy_itemsload);
            // 
            // cb_locations
            // 
            this.cb_locations.Label = "Location";
            this.cb_locations.Name = "cb_locations";
            this.cb_locations.Text = null;
            this.cb_locations.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_locations_itemsload);
            // 
            // cb_assets
            // 
            this.cb_assets.Label = "Asset";
            this.cb_assets.Name = "cb_assets";
            this.cb_assets.Text = null;
            this.cb_assets.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_assets_itemsload);
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            // 
            // btn_StartDate
            // 
            this.btn_StartDate.Label = "StartDate";
            this.btn_StartDate.Name = "btn_StartDate";
            this.btn_StartDate.OfficeImageId = "FillRight";
            this.btn_StartDate.ShowImage = true;
            this.btn_StartDate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_StartDate_Click);
            // 
            // btn_EndDate
            // 
            this.btn_EndDate.Label = "EndDate";
            this.btn_EndDate.Name = "btn_EndDate";
            this.btn_EndDate.OfficeImageId = "FillLeft";
            this.btn_EndDate.ShowImage = true;
            this.btn_EndDate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_EndDate_Click);
            // 
            // btn_nDays
            // 
            this.btn_nDays.Label = "nDays";
            this.btn_nDays.Name = "btn_nDays";
            this.btn_nDays.OfficeImageId = "TableExportTableToSharePointList";
            this.btn_nDays.ShowImage = true;
            // 
            // proc_parameters
            // 
            this.proc_parameters.Items.Add(this.button1);
            this.proc_parameters.Name = "proc_parameters";
            // 
            // button1
            // 
            this.button1.Label = "button1";
            this.button1.Name = "button1";
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
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
            this.proc_parameters.ResumeLayout(false);
            this.proc_parameters.PerformLayout();

        }

        private void testlabel()
        {
                this.labelxx = this.Factory.CreateRibbonLabel();
                this.proc_parameters.Items.Add(this.labelxx);
                this.proc_parameters.Label = "proc_parameters";
                this.proc_parameters.Name = "proc_parameters";
                this.labelxx.Label = "labelxx";
                this.labelxx.Name = "labelxx";
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
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel labelxx;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Query;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ConnectionManager;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cb_activeConnection;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_StartDate;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_EndDate;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_nDays;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
    }

    partial class ThisRibbonCollection
    {
        internal EquipmentDBRibbon EquipmentDBRibbon
        {
            get { return this.GetRibbon<EquipmentDBRibbon>(); }
        }
    }
}
