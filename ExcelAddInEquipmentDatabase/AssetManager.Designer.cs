namespace ExcelAddInEquipmentDatabase
{
    partial class AssetManager
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
            this.CB_ASSET = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CB_LOCHIERARCHY = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbl_clock = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CB_LOCATION = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CB_UNIVERSE = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CB_SYSTEMID = new System.Windows.Forms.ComboBox();
            this.btn_MX7toGADATA = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // CB_ASSET
            // 
            this.CB_ASSET.FormattingEnabled = true;
            this.CB_ASSET.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CB_ASSET.Location = new System.Drawing.Point(388, 60);
            this.CB_ASSET.Name = "CB_ASSET";
            this.CB_ASSET.Size = new System.Drawing.Size(224, 24);
            this.CB_ASSET.TabIndex = 0;
            this.CB_ASSET.Text = "%";
            this.CB_ASSET.DropDown += new System.EventHandler(this.CB_ASSET_DropDown);
            this.CB_ASSET.TextChanged += new System.EventHandler(this.CB_ALL_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(334, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Asset";
            // 
            // CB_LOCHIERARCHY
            // 
            this.CB_LOCHIERARCHY.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CB_LOCHIERARCHY.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.CB_LOCHIERARCHY.FormattingEnabled = true;
            this.CB_LOCHIERARCHY.Location = new System.Drawing.Point(108, 33);
            this.CB_LOCHIERARCHY.Name = "CB_LOCHIERARCHY";
            this.CB_LOCHIERARCHY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CB_LOCHIERARCHY.Size = new System.Drawing.Size(726, 24);
            this.CB_LOCHIERARCHY.TabIndex = 2;
            this.CB_LOCHIERARCHY.Text = "%";
            this.CB_LOCHIERARCHY.DropDown += new System.EventHandler(this.CB_LOCHIERARCHY_DropDown);
            this.CB_LOCHIERARCHY.TextChanged += new System.EventHandler(this.CB_ALL_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Lochierarchy";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 95);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1267, 414);
            this.dataGridView1.TabIndex = 4;
            // 
            // lbl_clock
            // 
            this.lbl_clock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_clock.AutoSize = true;
            this.lbl_clock.Location = new System.Drawing.Point(1143, 10);
            this.lbl_clock.Name = "lbl_clock";
            this.lbl_clock.Size = new System.Drawing.Size(46, 17);
            this.lbl_clock.TabIndex = 5;
            this.lbl_clock.Text = "label3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Location";
            // 
            // CB_LOCATION
            // 
            this.CB_LOCATION.FormattingEnabled = true;
            this.CB_LOCATION.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CB_LOCATION.Location = new System.Drawing.Point(108, 63);
            this.CB_LOCATION.Name = "CB_LOCATION";
            this.CB_LOCATION.Size = new System.Drawing.Size(220, 24);
            this.CB_LOCATION.TabIndex = 8;
            this.CB_LOCATION.Text = "%";
            this.CB_LOCATION.DropDown += new System.EventHandler(this.CB_LOCATION_DropDown);
            this.CB_LOCATION.TextChanged += new System.EventHandler(this.CB_ALL_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Universe";
            // 
            // CB_UNIVERSE
            // 
            this.CB_UNIVERSE.Enabled = false;
            this.CB_UNIVERSE.FormattingEnabled = true;
            this.CB_UNIVERSE.Location = new System.Drawing.Point(108, 3);
            this.CB_UNIVERSE.Name = "CB_UNIVERSE";
            this.CB_UNIVERSE.Size = new System.Drawing.Size(118, 24);
            this.CB_UNIVERSE.TabIndex = 11;
            this.CB_UNIVERSE.Text = "VCG -> A%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(245, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "SystemID";
            // 
            // CB_SYSTEMID
            // 
            this.CB_SYSTEMID.Enabled = false;
            this.CB_SYSTEMID.FormattingEnabled = true;
            this.CB_SYSTEMID.Location = new System.Drawing.Point(327, 3);
            this.CB_SYSTEMID.Name = "CB_SYSTEMID";
            this.CB_SYSTEMID.Size = new System.Drawing.Size(106, 24);
            this.CB_SYSTEMID.TabIndex = 13;
            this.CB_SYSTEMID.Text = "PRODMID%";
            // 
            // btn_MX7toGADATA
            // 
            this.btn_MX7toGADATA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_MX7toGADATA.Location = new System.Drawing.Point(1146, 60);
            this.btn_MX7toGADATA.Name = "btn_MX7toGADATA";
            this.btn_MX7toGADATA.Size = new System.Drawing.Size(112, 23);
            this.btn_MX7toGADATA.TabIndex = 15;
            this.btn_MX7toGADATA.Text = "MX7->gadata";
            this.btn_MX7toGADATA.UseVisualStyleBackColor = true;
            this.btn_MX7toGADATA.Click += new System.EventHandler(this.btn_MX7toGADATA_Click);
            // 
            // AssetManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1294, 521);
            this.Controls.Add(this.btn_MX7toGADATA);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CB_SYSTEMID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CB_UNIVERSE);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CB_LOCATION);
            this.Controls.Add(this.lbl_clock);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CB_LOCHIERARCHY);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CB_ASSET);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AssetManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AssetManager";
            this.Load += new System.EventHandler(this.form_load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CB_ASSET;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CB_LOCHIERARCHY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbl_clock;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CB_LOCATION;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CB_UNIVERSE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CB_SYSTEMID;
        private System.Windows.Forms.Button btn_MX7toGADATA;
    }
}