﻿namespace EQUIToolsLib
{
    partial class SBCUStats
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea11 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend11 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea12 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend12 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart_sbcu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.trackBar1 = new MetroFramework.Controls.MetroTrackBar();
            this.trackBar2 = new MetroFramework.Controls.MetroTrackBar();
            this.chart_cilinder = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_midair = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cb_Tools = new MetroFramework.Controls.MetroComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chart_DressData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn_ChartDressData = new MetroFramework.Controls.MetroButton();
            this.Btn_ChartMidair = new MetroFramework.Controls.MetroButton();
            this.Btn_CharCilinder = new MetroFramework.Controls.MetroButton();
            this.btn_chart_sbcu = new MetroFramework.Controls.MetroButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dataViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.show3DSbcuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxSortMode = new System.Windows.Forms.ToolStripComboBox();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideLedendeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllXAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAllDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new MetroFramework.Controls.MetroProgressBar();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_sbcu = new System.Windows.Forms.Label();
            this.lbl_Cylinder = new System.Windows.Forms.Label();
            this.lbl_midair = new System.Windows.Forms.Label();
            this.lbl_dress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart_sbcu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_cilinder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_midair)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_DressData)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart_sbcu
            // 
            this.chart_sbcu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart_sbcu.BorderlineColor = System.Drawing.Color.Black;
            this.chart_sbcu.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea9.Name = "ChartArea1";
            this.chart_sbcu.ChartAreas.Add(chartArea9);
            legend9.AutoFitMinFontSize = 5;
            legend9.BorderColor = System.Drawing.Color.Black;
            legend9.DockedToChartArea = "ChartArea1";
            legend9.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend9.IsTextAutoFit = false;
            legend9.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend9.MaximumAutoSize = 10F;
            legend9.Name = "Legend1";
            this.chart_sbcu.Legends.Add(legend9);
            this.chart_sbcu.Location = new System.Drawing.Point(3, 63);
            this.chart_sbcu.Name = "chart_sbcu";
            this.chart_sbcu.Size = new System.Drawing.Size(1330, 156);
            this.chart_sbcu.TabIndex = 0;
            this.chart_sbcu.Text = "chart1";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.BackColor = System.Drawing.Color.Transparent;
            this.trackBar1.Location = new System.Drawing.Point(583, 27);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(387, 22);
            this.trackBar1.TabIndex = 8;
            this.trackBar1.Text = "metroTrackBar1";
            // 
            // trackBar2
            // 
            this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar2.BackColor = System.Drawing.Color.Transparent;
            this.trackBar2.Location = new System.Drawing.Point(583, 3);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(387, 18);
            this.trackBar2.TabIndex = 14;
            this.trackBar2.Text = "metroTrackBar1";
            // 
            // chart_cilinder
            // 
            this.chart_cilinder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart_cilinder.BorderlineColor = System.Drawing.Color.Black;
            this.chart_cilinder.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea10.Name = "ChartArea1";
            this.chart_cilinder.ChartAreas.Add(chartArea10);
            legend10.BorderColor = System.Drawing.Color.Black;
            legend10.DockedToChartArea = "ChartArea1";
            legend10.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend10.IsTextAutoFit = false;
            legend10.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend10.Name = "Legend1";
            this.chart_cilinder.Legends.Add(legend10);
            this.chart_cilinder.Location = new System.Drawing.Point(3, 255);
            this.chart_cilinder.Name = "chart_cilinder";
            this.chart_cilinder.Size = new System.Drawing.Size(1330, 156);
            this.chart_cilinder.TabIndex = 15;
            this.chart_cilinder.Text = "chart3";
            // 
            // chart_midair
            // 
            this.chart_midair.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart_midair.BorderlineColor = System.Drawing.Color.Black;
            this.chart_midair.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea11.Name = "ChartArea1";
            this.chart_midair.ChartAreas.Add(chartArea11);
            legend11.BorderColor = System.Drawing.Color.Black;
            legend11.DockedToChartArea = "ChartArea1";
            legend11.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend11.IsTextAutoFit = false;
            legend11.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend11.Name = "Legend1";
            this.chart_midair.Legends.Add(legend11);
            this.chart_midair.Location = new System.Drawing.Point(3, 447);
            this.chart_midair.Name = "chart_midair";
            this.chart_midair.Size = new System.Drawing.Size(1330, 156);
            this.chart_midair.TabIndex = 17;
            this.chart_midair.Text = "chart4";
            // 
            // cb_Tools
            // 
            this.cb_Tools.FormattingEnabled = true;
            this.cb_Tools.ItemHeight = 24;
            this.cb_Tools.Location = new System.Drawing.Point(303, 3);
            this.cb_Tools.Name = "cb_Tools";
            this.cb_Tools.Size = new System.Drawing.Size(161, 30);
            this.cb_Tools.TabIndex = 21;
            this.cb_Tools.UseSelectable = true;
            this.cb_Tools.DropDown += new System.EventHandler(this.cb_weldguns_DropDown);
            this.cb_Tools.SelectedIndexChanged += new System.EventHandler(this.cb_Tools_SelectedIndexChanged);
            this.cb_Tools.DropDownClosed += new System.EventHandler(this.cb_weldguns_DropDownClosed);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.chart_DressData, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.btn_ChartDressData, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.chart_midair, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.Btn_ChartMidair, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.chart_cilinder, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.Btn_CharCilinder, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.chart_sbcu, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_chart_sbcu, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 78);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1336, 799);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // chart_DressData
            // 
            this.chart_DressData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart_DressData.BorderlineColor = System.Drawing.Color.Black;
            this.chart_DressData.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea12.Name = "ChartArea1";
            this.chart_DressData.ChartAreas.Add(chartArea12);
            legend12.BorderColor = System.Drawing.Color.Black;
            legend12.DockedToChartArea = "ChartArea1";
            legend12.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend12.IsTextAutoFit = false;
            legend12.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend12.Name = "Legend1";
            this.chart_DressData.Legends.Add(legend12);
            this.chart_DressData.Location = new System.Drawing.Point(3, 639);
            this.chart_DressData.Name = "chart_DressData";
            this.chart_DressData.Size = new System.Drawing.Size(1330, 157);
            this.chart_DressData.TabIndex = 24;
            this.chart_DressData.Text = "chart2";
            // 
            // btn_ChartDressData
            // 
            this.btn_ChartDressData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ChartDressData.Location = new System.Drawing.Point(3, 609);
            this.btn_ChartDressData.Name = "btn_ChartDressData";
            this.btn_ChartDressData.Size = new System.Drawing.Size(1330, 24);
            this.btn_ChartDressData.TabIndex = 23;
            this.btn_ChartDressData.Text = "+ Dressdata (NGAC only)";
            this.btn_ChartDressData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ChartDressData.UseSelectable = true;
            this.btn_ChartDressData.Click += new System.EventHandler(this.btn_ChartDressData_Click);
            // 
            // Btn_ChartMidair
            // 
            this.Btn_ChartMidair.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_ChartMidair.Location = new System.Drawing.Point(3, 417);
            this.Btn_ChartMidair.Name = "Btn_ChartMidair";
            this.Btn_ChartMidair.Size = new System.Drawing.Size(1330, 24);
            this.Btn_ChartMidair.TabIndex = 20;
            this.Btn_ChartMidair.Text = "+ Midar (megaOhm)";
            this.Btn_ChartMidair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_ChartMidair.UseSelectable = true;
            this.Btn_ChartMidair.Click += new System.EventHandler(this.Btn_ChartMidair_Click);
            // 
            // Btn_CharCilinder
            // 
            this.Btn_CharCilinder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_CharCilinder.Location = new System.Drawing.Point(3, 225);
            this.Btn_CharCilinder.Name = "Btn_CharCilinder";
            this.Btn_CharCilinder.Size = new System.Drawing.Size(1330, 24);
            this.Btn_CharCilinder.TabIndex = 22;
            this.Btn_CharCilinder.Text = "+ Cylinder (totaltime ms)";
            this.Btn_CharCilinder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_CharCilinder.UseSelectable = true;
            this.Btn_CharCilinder.Click += new System.EventHandler(this.Btn_CharCilinder_Click);
            // 
            // btn_chart_sbcu
            // 
            this.btn_chart_sbcu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_chart_sbcu.Location = new System.Drawing.Point(3, 33);
            this.btn_chart_sbcu.Name = "btn_chart_sbcu";
            this.btn_chart_sbcu.Size = new System.Drawing.Size(1330, 24);
            this.btn_chart_sbcu.TabIndex = 26;
            this.btn_chart_sbcu.Text = "  +   SBCU (dsetup in mm | LongSbcu = Red | ShortScbu = Blue)";
            this.btn_chart_sbcu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_chart_sbcu.UseSelectable = true;
            this.btn_chart_sbcu.Click += new System.EventHandler(this.btn_chart_sbcu_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataViewToolStripMenuItem,
            this.show3DSbcuToolStripMenuItem,
            this.toolStripComboBoxSortMode,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1336, 30);
            this.menuStrip1.TabIndex = 27;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dataViewToolStripMenuItem
            // 
            this.dataViewToolStripMenuItem.Name = "dataViewToolStripMenuItem";
            this.dataViewToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
            this.dataViewToolStripMenuItem.Text = "Show data viewer";
            this.dataViewToolStripMenuItem.Click += new System.EventHandler(this.btn_dataview_Click);
            // 
            // show3DSbcuToolStripMenuItem
            // 
            this.show3DSbcuToolStripMenuItem.Name = "show3DSbcuToolStripMenuItem";
            this.show3DSbcuToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.show3DSbcuToolStripMenuItem.Text = "Show 3D Sbcu viewer";
            this.show3DSbcuToolStripMenuItem.Click += new System.EventHandler(this.btn_3dTemp_Click);
            // 
            // toolStripComboBoxSortMode
            // 
            this.toolStripComboBoxSortMode.DropDownWidth = 150;
            this.toolStripComboBoxSortMode.Name = "toolStripComboBoxSortMode";
            this.toolStripComboBoxSortMode.Size = new System.Drawing.Size(150, 26);
            this.toolStripComboBoxSortMode.Text = "Sortmode";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideLedendeToolStripMenuItem,
            this.showAllXAxisToolStripMenuItem,
            this.loadAllDataToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(73, 26);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // showHideLedendeToolStripMenuItem
            // 
            this.showHideLedendeToolStripMenuItem.Checked = true;
            this.showHideLedendeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showHideLedendeToolStripMenuItem.Name = "showHideLedendeToolStripMenuItem";
            this.showHideLedendeToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.showHideLedendeToolStripMenuItem.Text = "Show Ledende";
            this.showHideLedendeToolStripMenuItem.Click += new System.EventHandler(this.showHideLedendeToolStripMenuItem_Click);
            // 
            // showAllXAxisToolStripMenuItem
            // 
            this.showAllXAxisToolStripMenuItem.Name = "showAllXAxisToolStripMenuItem";
            this.showAllXAxisToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.showAllXAxisToolStripMenuItem.Text = "Show all X axis";
            this.showAllXAxisToolStripMenuItem.Click += new System.EventHandler(this.showAllXAxisToolStripMenuItem_Click);
            // 
            // loadAllDataToolStripMenuItem
            // 
            this.loadAllDataToolStripMenuItem.Name = "loadAllDataToolStripMenuItem";
            this.loadAllDataToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.loadAllDataToolStripMenuItem.Text = "LoadAllData";
            this.loadAllDataToolStripMenuItem.Click += new System.EventHandler(this.loadAllDataToolStripMenuItem_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.trackBar2, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.trackBar1, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.cb_Tools, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.progressBar1, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(23, 23);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.57534F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.42466F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1073, 52);
            this.tableLayoutPanel2.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(976, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "EndDate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(483, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "StartDate";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 24);
            this.label3.TabIndex = 22;
            this.label3.Text = "WeldGun data for Location:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(976, 27);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(94, 22);
            this.progressBar1.TabIndex = 23;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.lbl_dress, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_midair, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_Cylinder, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_sbcu, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 27);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(294, 22);
            this.tableLayoutPanel3.TabIndex = 24;
            // 
            // lbl_sbcu
            // 
            this.lbl_sbcu.AutoSize = true;
            this.lbl_sbcu.Location = new System.Drawing.Point(3, 0);
            this.lbl_sbcu.Name = "lbl_sbcu";
            this.lbl_sbcu.Size = new System.Drawing.Size(0, 17);
            this.lbl_sbcu.TabIndex = 0;
            // 
            // lbl_Cylinder
            // 
            this.lbl_Cylinder.AutoSize = true;
            this.lbl_Cylinder.Location = new System.Drawing.Point(76, 0);
            this.lbl_Cylinder.Name = "lbl_Cylinder";
            this.lbl_Cylinder.Size = new System.Drawing.Size(0, 17);
            this.lbl_Cylinder.TabIndex = 1;
            // 
            // lbl_midair
            // 
            this.lbl_midair.AutoSize = true;
            this.lbl_midair.Location = new System.Drawing.Point(149, 0);
            this.lbl_midair.Name = "lbl_midair";
            this.lbl_midair.Size = new System.Drawing.Size(0, 17);
            this.lbl_midair.TabIndex = 2;
            // 
            // lbl_dress
            // 
            this.lbl_dress.AutoSize = true;
            this.lbl_dress.Location = new System.Drawing.Point(222, 0);
            this.lbl_dress.Name = "lbl_dress";
            this.lbl_dress.Size = new System.Drawing.Size(0, 17);
            this.lbl_dress.TabIndex = 3;
            // 
            // SBCUStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 900);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(1351, 469);
            this.Name = "SBCUStats";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "GunData";
            ((System.ComponentModel.ISupportInitialize)(this.chart_sbcu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_cilinder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_midair)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_DressData)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_sbcu;
        private MetroFramework.Controls.MetroTrackBar trackBar1;
        private MetroFramework.Controls.MetroTrackBar trackBar2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_cilinder;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_midair;
        private MetroFramework.Controls.MetroComboBox cb_Tools;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroButton Btn_ChartMidair;
        private MetroFramework.Controls.MetroButton Btn_CharCilinder;
        private MetroFramework.Controls.MetroButton btn_ChartDressData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_DressData;
        private MetroFramework.Controls.MetroButton btn_chart_sbcu;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dataViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem show3DSbcuToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSortMode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideLedendeToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem showAllXAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAllDataToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lbl_dress;
        private System.Windows.Forms.Label lbl_midair;
        private System.Windows.Forms.Label lbl_Cylinder;
        private System.Windows.Forms.Label lbl_sbcu;
        private MetroFramework.Controls.MetroProgressBar progressBar1;
    }
}