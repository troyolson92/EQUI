namespace ExcelAddInEquipmentDatabase.Forms
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.trackBar1 = new MetroFramework.Controls.MetroTrackBar();
            this.cb_sortmode = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.trackBar2 = new MetroFramework.Controls.MetroTrackBar();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn_dataview = new MetroFramework.Controls.MetroButton();
            this.chart4 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.cb_weldguns = new MetroFramework.Controls.MetroComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(8, 124);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(1334, 242);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(570, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "metroLabel1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1186, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "metroLabel2";
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.Color.Transparent;
            this.trackBar1.Location = new System.Drawing.Point(796, 67);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(359, 23);
            this.trackBar1.TabIndex = 8;
            this.trackBar1.Text = "metroTrackBar1";
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // cb_sortmode
            // 
            this.cb_sortmode.FormattingEnabled = true;
            this.cb_sortmode.ItemHeight = 24;
            this.cb_sortmode.Location = new System.Drawing.Point(278, 56);
            this.cb_sortmode.Name = "cb_sortmode";
            this.cb_sortmode.Size = new System.Drawing.Size(121, 30);
            this.cb_sortmode.TabIndex = 9;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(9, 101);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(290, 20);
            this.metroLabel1.TabIndex = 10;
            this.metroLabel1.Text = "SBCU (dsetup mm Long = Red | Short = Blue)";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(156, 66);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(110, 20);
            this.metroLabel3.TabIndex = 13;
            this.metroLabel3.Text = "Set X sort mode:";
            // 
            // trackBar2
            // 
            this.trackBar2.BackColor = System.Drawing.Color.Transparent;
            this.trackBar2.Location = new System.Drawing.Point(796, 38);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(359, 23);
            this.trackBar2.TabIndex = 14;
            this.trackBar2.Text = "metroTrackBar1";
            this.trackBar2.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // chart3
            // 
            this.chart3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart3.BorderlineColor = System.Drawing.Color.Black;
            this.chart3.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.Name = "ChartArea1";
            this.chart3.ChartAreas.Add(chartArea2);
            this.chart3.Location = new System.Drawing.Point(8, 399);
            this.chart3.Name = "chart3";
            this.chart3.Size = new System.Drawing.Size(1334, 209);
            this.chart3.TabIndex = 15;
            this.chart3.Text = "chart3";
            // 
            // btn_dataview
            // 
            this.btn_dataview.Location = new System.Drawing.Point(499, 63);
            this.btn_dataview.Name = "btn_dataview";
            this.btn_dataview.Size = new System.Drawing.Size(37, 23);
            this.btn_dataview.TabIndex = 16;
            this.btn_dataview.Text = "+";
            this.btn_dataview.Click += new System.EventHandler(this.btn_dataview_Click);
            // 
            // chart4
            // 
            this.chart4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart4.BorderlineColor = System.Drawing.Color.Black;
            this.chart4.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea3.Name = "ChartArea1";
            this.chart4.ChartAreas.Add(chartArea3);
            this.chart4.Location = new System.Drawing.Point(8, 634);
            this.chart4.Name = "chart4";
            this.chart4.Size = new System.Drawing.Size(1334, 243);
            this.chart4.TabIndex = 17;
            this.chart4.Text = "chart4";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(423, 66);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(70, 20);
            this.metroLabel4.TabIndex = 18;
            this.metroLabel4.Text = "DataView:";
            // 
            // metroLabel5
            // 
            this.metroLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(8, 376);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(147, 20);
            this.metroLabel5.TabIndex = 19;
            this.metroLabel5.Text = "Cylinder (totaltime ms)";
            // 
            // metroLabel6
            // 
            this.metroLabel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(9, 611);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(127, 20);
            this.metroLabel6.TabIndex = 20;
            this.metroLabel6.Text = "MidAir (MegaOhm)";
            // 
            // cb_weldguns
            // 
            this.cb_weldguns.FormattingEnabled = true;
            this.cb_weldguns.ItemHeight = 24;
            this.cb_weldguns.Location = new System.Drawing.Point(238, 17);
            this.cb_weldguns.Name = "cb_weldguns";
            this.cb_weldguns.Size = new System.Drawing.Size(161, 30);
            this.cb_weldguns.TabIndex = 21;
            this.cb_weldguns.DropDown += new System.EventHandler(this.cb_weldguns_DropDown);
            // 
            // SBCUStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 900);
            this.Controls.Add(this.cb_weldguns);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.chart4);
            this.Controls.Add(this.btn_dataview);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.cb_sortmode);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart1);
            this.MinimumSize = new System.Drawing.Size(1351, 469);
            this.Name = "SBCUStats";
            this.Text = "SBCUStats Location:";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroLabel label2;
        private MetroFramework.Controls.MetroTrackBar trackBar1;
        private MetroFramework.Controls.MetroComboBox cb_sortmode;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroTrackBar trackBar2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private MetroFramework.Controls.MetroButton btn_dataview;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart4;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroComboBox cb_weldguns;
    }
}