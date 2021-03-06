﻿namespace ExcelAddInEquipmentDatabase
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
            this.tab1 = this.Factory.CreateRibbonTab();
            this.rib2 = this.Factory.CreateRibbonTab();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btn_Query = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.dd_activeConnection = this.Factory.CreateRibbonDropDown();
            this.dd_ParameterSets = this.Factory.CreateRibbonDropDown();
            this.btn_EditProcedure = this.Factory.CreateRibbonToggleButton();
            this.AssetManager = this.Factory.CreateRibbonGroup();
            this.cb_Lochierarchy = this.Factory.CreateRibbonComboBox();
            this.cb_locations = this.Factory.CreateRibbonComboBox();
            this.cb_assets = this.Factory.CreateRibbonComboBox();
            this.tbn_3 = this.Factory.CreateRibbonToggleButton();
            this.separator2 = this.Factory.CreateRibbonSeparator();
            this.btn_StartDate = this.Factory.CreateRibbonButton();
            this.btn_EndDate = this.Factory.CreateRibbonButton();
            this.btn_nDays = this.Factory.CreateRibbonButton();
            this.g_config = this.Factory.CreateRibbonGroup();
            this.btn_help = this.Factory.CreateRibbonButton();
            this.btn_ConnectionManager = this.Factory.CreateRibbonButton();
            this.gall_templates = this.Factory.CreateRibbonGallery();
            this.separator3 = this.Factory.CreateRibbonSeparator();
            this.tgbtn_Wrap = this.Factory.CreateRibbonToggleButton();
            this.tbtn_StopRightClick = this.Factory.CreateRibbonToggleButton();
            this.btn_logfile = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.rib2.SuspendLayout();
            this.group2.SuspendLayout();
            this.AssetManager.SuspendLayout();
            this.g_config.SuspendLayout();
            this.SuspendLayout();
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
            this.rib2.Groups.Add(this.g_config);
            this.rib2.Label = "EQUI";
            this.rib2.Name = "rib2";
            // 
            // group2
            // 
            this.group2.Items.Add(this.btn_Query);
            this.group2.Items.Add(this.separator1);
            this.group2.Items.Add(this.dd_activeConnection);
            this.group2.Items.Add(this.dd_ParameterSets);
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
            this.dd_activeConnection.SuperTip = "This sets the current workbook connection beeing controled by the ribbon";
            this.dd_activeConnection.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.dd_activeConnection_SelectionChanged);
            // 
            // dd_ParameterSets
            // 
            this.dd_ParameterSets.Enabled = false;
            this.dd_ParameterSets.Label = "pSets";
            this.dd_ParameterSets.Name = "dd_ParameterSets";
            this.dd_ParameterSets.OfficeImageId = "ImportMoreMenu";
            this.dd_ParameterSets.ShowImage = true;
            this.dd_ParameterSets.SuperTip = "This gives the option to set the procedure manager to a predefined set of values";
            this.dd_ParameterSets.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.dd_ParameterSets_SelectionChanged);
            // 
            // btn_EditProcedure
            // 
            this.btn_EditProcedure.Label = "Procedure parameters";
            this.btn_EditProcedure.Name = "btn_EditProcedure";
            this.btn_EditProcedure.OfficeImageId = "SmartArtInsert";
            this.btn_EditProcedure.ShowImage = true;
            this.btn_EditProcedure.SuperTip = "Shows the procedure manager and gives the user to control the active connection p" +
    "arameters";
            this.btn_EditProcedure.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_EditProcedure_Click);
            // 
            // AssetManager
            // 
            this.AssetManager.Items.Add(this.cb_Lochierarchy);
            this.AssetManager.Items.Add(this.cb_locations);
            this.AssetManager.Items.Add(this.cb_assets);
            this.AssetManager.Items.Add(this.tbn_3);
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
            this.cb_Lochierarchy.SuperTip = "Location hieracry filter. (structure from maximo) ";
            this.cb_Lochierarchy.Text = null;
            this.cb_Lochierarchy.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_Lochierarchy_itemsload);
            this.cb_Lochierarchy.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_Lochierarchy_TextChanged);
            // 
            // cb_locations
            // 
            this.cb_locations.Enabled = false;
            this.cb_locations.Label = "Location";
            this.cb_locations.Name = "cb_locations";
            this.cb_locations.SuperTip = "filter. (structure from maximo) ";
            this.cb_locations.Text = null;
            this.cb_locations.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_locations_itemsload);
            this.cb_locations.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_locations_TextChanged);
            // 
            // cb_assets
            // 
            this.cb_assets.Enabled = false;
            this.cb_assets.Label = "Asset";
            this.cb_assets.Name = "cb_assets";
            this.cb_assets.SuperTip = "filter. (structure from maximo) ";
            this.cb_assets.Text = null;
            this.cb_assets.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_assets_itemsload);
            this.cb_assets.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cb_assets_TextChanged);
            // 
            // tbn_3
            // 
            this.tbn_3.Enabled = false;
            this.tbn_3.Label = " ";
            this.tbn_3.Name = "tbn_3";
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
            this.btn_StartDate.SuperTip = "Sets the date time starting point of the query. ";
            this.btn_StartDate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_StartDate_Click);
            // 
            // btn_EndDate
            // 
            this.btn_EndDate.Enabled = false;
            this.btn_EndDate.Label = "EndDate";
            this.btn_EndDate.Name = "btn_EndDate";
            this.btn_EndDate.OfficeImageId = "FillLeft";
            this.btn_EndDate.ShowImage = true;
            this.btn_EndDate.SuperTip = "Sets the date time ending point of the query. ";
            this.btn_EndDate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_EndDate_Click);
            // 
            // btn_nDays
            // 
            this.btn_nDays.Enabled = false;
            this.btn_nDays.Label = "nDays";
            this.btn_nDays.Name = "btn_nDays";
            this.btn_nDays.OfficeImageId = "TableExportTableToSharePointList";
            this.btn_nDays.ShowImage = true;
            this.btn_nDays.SuperTip = "Sets the end date to now and the start date to now - x days";
            this.btn_nDays.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_nDays_Click);
            // 
            // g_config
            // 
            this.g_config.Items.Add(this.btn_help);
            this.g_config.Items.Add(this.btn_ConnectionManager);
            this.g_config.Items.Add(this.gall_templates);
            this.g_config.Items.Add(this.separator3);
            this.g_config.Items.Add(this.tgbtn_Wrap);
            this.g_config.Items.Add(this.tbtn_StopRightClick);
            this.g_config.Items.Add(this.btn_logfile);
            this.g_config.Label = "Configuration";
            this.g_config.Name = "g_config";
            // 
            // btn_help
            // 
            this.btn_help.Label = "Help";
            this.btn_help.Name = "btn_help";
            this.btn_help.OfficeImageId = "TentativeAcceptInvitation";
            this.btn_help.ShowImage = true;
            this.btn_help.SuperTip = "Show the help file";
            this.btn_help.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_help_Click);
            // 
            // btn_ConnectionManager
            // 
            this.btn_ConnectionManager.Label = "MngConnections";
            this.btn_ConnectionManager.Name = "btn_ConnectionManager";
            this.btn_ConnectionManager.OfficeImageId = "AdpDiagramAddRelatedTables";
            this.btn_ConnectionManager.ShowImage = true;
            this.btn_ConnectionManager.SuperTip = "And change and manager connections";
            this.btn_ConnectionManager.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_ConnectionManager_Click);
            // 
            // gall_templates
            // 
            this.gall_templates.Label = "Templates";
            this.gall_templates.Name = "gall_templates";
            this.gall_templates.OfficeImageId = "ReviewCompareMenu";
            this.gall_templates.ShowImage = true;
            this.gall_templates.SuperTip = "A list of templates for the most common connections";
            this.gall_templates.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.gall_templates_Click);
            // 
            // separator3
            // 
            this.separator3.Name = "separator3";
            // 
            // tgbtn_Wrap
            // 
            this.tgbtn_Wrap.Label = "WrapText";
            this.tgbtn_Wrap.Name = "tgbtn_Wrap";
            this.tgbtn_Wrap.OfficeImageId = "TextWrappingInLineWithText";
            this.tgbtn_Wrap.ShowImage = true;
            this.tgbtn_Wrap.SuperTip = "Wraps the text for ";
            this.tgbtn_Wrap.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.tbtn_Wrap_Click);
            // 
            // tbtn_StopRightClick
            // 
            this.tbtn_StopRightClick.Label = "StopRightClick";
            this.tbtn_StopRightClick.Name = "tbtn_StopRightClick";
            this.tbtn_StopRightClick.OfficeImageId = "InkDeleteAllInk";
            this.tbtn_StopRightClick.ShowImage = true;
            this.tbtn_StopRightClick.SuperTip = "Stops the plug in from overwriting the context menu on right click";
            this.tbtn_StopRightClick.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.tbtn_StopRightClick_Click);
            // 
            // btn_logfile
            // 
            this.btn_logfile.Label = "Debuglog";
            this.btn_logfile.Name = "btn_logfile";
            this.btn_logfile.OfficeImageId = "ScriptDebugger";
            this.btn_logfile.ShowImage = true;
            this.btn_logfile.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btn_logfile_Click);
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
            this.g_config.ResumeLayout(false);
            this.g_config.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonTab rib2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup AssetManager;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cb_assets;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cb_Lochierarchy;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cb_locations;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup g_config;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_Query;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_ConnectionManager;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_StartDate;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_EndDate;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_nDays;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dd_activeConnection;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dd_ParameterSets;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_help;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton tbtn_StopRightClick;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator3;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton tbn_3;
        internal Microsoft.Office.Tools.Ribbon.RibbonGallery gall_templates;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btn_EditProcedure;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton tgbtn_Wrap;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btn_logfile;
    }

    partial class ThisRibbonCollection
    {
        internal EquipmentDBRibbon EquipmentDBRibbon
        {
            get { return this.GetRibbon<EquipmentDBRibbon>(); }
        }
    }
}
