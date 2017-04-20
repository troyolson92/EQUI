namespace ExcelAddInEquipmentDatabase.Forms
{
    partial class AssetStats
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.label2 = new MetroFramework.Controls.MetroLabel();
            this.trackBar1 = new MetroFramework.Controls.MetroTrackBar();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.cb_sortmode = new MetroFramework.Controls.MetroComboBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cb_preventive = new MetroFramework.Controls.MetroCheckBox();
            this.cb_spltDt = new MetroFramework.Controls.MetroCheckBox();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.trackBar2 = new MetroFramework.Controls.MetroTrackBar();
            this.cb_incCiblings = new MetroFramework.Controls.MetroCheckBox();
            this.tb_location = new MetroFramework.Controls.MetroTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea7.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea7.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea7);
            legend7.BorderColor = System.Drawing.Color.Black;
            legend7.MaximumAutoSize = 30F;
            legend7.Name = "Legend1";
            this.chart1.Legends.Add(legend7);
            this.chart1.Location = new System.Drawing.Point(9, 104);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(1787, 249);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(934, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "metroLabel1";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1550, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "metroLabel2";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.BackColor = System.Drawing.Color.Transparent;
            this.trackBar1.Location = new System.Drawing.Point(1160, 61);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(359, 23);
            this.trackBar1.Style = MetroFramework.MetroColorStyle.Blue;
            this.trackBar1.TabIndex = 8;
            this.trackBar1.Text = "metroTrackBar1";
            this.trackBar1.Value = 0;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // metroLabel3
            // 
            this.metroLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(1660, 216);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(107, 20);
            this.metroLabel3.TabIndex = 15;
            this.metroLabel3.Text = "Set X sort mode";
            // 
            // cb_sortmode
            // 
            this.cb_sortmode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_sortmode.FormattingEnabled = true;
            this.cb_sortmode.ItemHeight = 24;
            this.cb_sortmode.Location = new System.Drawing.Point(1660, 239);
            this.cb_sortmode.Name = "cb_sortmode";
            this.cb_sortmode.Size = new System.Drawing.Size(121, 30);
            this.cb_sortmode.TabIndex = 14;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(9, 565);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(0, 50);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1787, 333);
            this.webBrowser1.TabIndex = 16;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(9, 359);
            this.dataGridView1.MinimumSize = new System.Drawing.Size(0, 50);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1787, 200);
            this.dataGridView1.TabIndex = 17;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // cb_preventive
            // 
            this.cb_preventive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_preventive.AutoSize = true;
            this.cb_preventive.Checked = true;
            this.cb_preventive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_preventive.Location = new System.Drawing.Point(1644, 330);
            this.cb_preventive.Name = "cb_preventive";
            this.cb_preventive.Size = new System.Drawing.Size(137, 17);
            this.cb_preventive.TabIndex = 18;
            this.cb_preventive.Text = "Hide PP, PCI, WSCH";
            this.cb_preventive.UseVisualStyleBackColor = true;
            this.cb_preventive.CheckedChanged += new System.EventHandler(this.cb_preventive_CheckedChanged);
            // 
            // cb_spltDt
            // 
            this.cb_spltDt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_spltDt.AutoSize = true;
            this.cb_spltDt.Location = new System.Drawing.Point(1644, 284);
            this.cb_spltDt.Name = "cb_spltDt";
            this.cb_spltDt.Size = new System.Drawing.Size(110, 17);
            this.cb_spltDt.TabIndex = 19;
            this.cb_spltDt.Text = "Split downtime";
            this.cb_spltDt.UseVisualStyleBackColor = true;
            this.cb_spltDt.CheckedChanged += new System.EventHandler(this.cb_spltDt_CheckedChanged);
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroProgressSpinner1.Location = new System.Drawing.Point(692, 134);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(190, 183);
            this.metroProgressSpinner1.TabIndex = 20;
            // 
            // trackBar2
            // 
            this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar2.BackColor = System.Drawing.Color.Transparent;
            this.trackBar2.Enabled = false;
            this.trackBar2.Location = new System.Drawing.Point(1160, 32);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(359, 23);
            this.trackBar2.Style = MetroFramework.MetroColorStyle.Blue;
            this.trackBar2.TabIndex = 21;
            this.trackBar2.Value = 0;
            this.trackBar2.ValueChanged += new System.EventHandler(this.trackBar2_ValueChanged);
            // 
            // cb_incCiblings
            // 
            this.cb_incCiblings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_incCiblings.AutoSize = true;
            this.cb_incCiblings.Location = new System.Drawing.Point(1644, 307);
            this.cb_incCiblings.Name = "cb_incCiblings";
            this.cb_incCiblings.Size = new System.Drawing.Size(105, 17);
            this.cb_incCiblings.TabIndex = 22;
            this.cb_incCiblings.Text = "Show Ciblings";
            this.cb_incCiblings.UseVisualStyleBackColor = true;
            this.cb_incCiblings.CheckedChanged += new System.EventHandler(this.cb_incCiblings_CheckedChanged);
            // 
            // tb_location
            // 
            this.tb_location.Location = new System.Drawing.Point(23, 63);
            this.tb_location.Name = "tb_location";
            this.tb_location.Size = new System.Drawing.Size(75, 23);
            this.tb_location.TabIndex = 23;
            this.tb_location.Text = "metroTextBox1";
            // 
            // AssetStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1804, 944);
            this.Controls.Add(this.tb_location);
            this.Controls.Add(this.cb_incCiblings);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.cb_spltDt);
            this.Controls.Add(this.cb_preventive);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.cb_sortmode);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart1);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "AssetStats";
            this.Text = "AssetStats";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AssetStats_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroLabel label2;
        private MetroFramework.Controls.MetroTrackBar trackBar1;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox cb_sortmode;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private MetroFramework.Controls.MetroCheckBox cb_preventive;
        private MetroFramework.Controls.MetroCheckBox cb_spltDt;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private MetroFramework.Controls.MetroTrackBar trackBar2;
        private MetroFramework.Controls.MetroCheckBox cb_incCiblings;
        private MetroFramework.Controls.MetroTextBox tb_location;
    }
}