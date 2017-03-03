namespace ExcelAddInEquipmentDatabase.Forms
{
    partial class MXxWOoverview
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
            this.label1 = new System.Windows.Forms.Label();
            this.cb_ciblings = new System.Windows.Forms.CheckBox();
            this.cb_preventive = new System.Windows.Forms.CheckBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tb_asset = new System.Windows.Forms.TextBox();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Asset:";
            // 
            // cb_ciblings
            // 
            this.cb_ciblings.AutoSize = true;
            this.cb_ciblings.Location = new System.Drawing.Point(220, 9);
            this.cb_ciblings.Name = "cb_ciblings";
            this.cb_ciblings.Size = new System.Drawing.Size(128, 21);
            this.cb_ciblings.TabIndex = 1;
            this.cb_ciblings.Text = "Include Ciblings";
            this.cb_ciblings.UseVisualStyleBackColor = true;
            // 
            // cb_preventive
            // 
            this.cb_preventive.AutoSize = true;
            this.cb_preventive.Location = new System.Drawing.Point(354, 9);
            this.cb_preventive.Name = "cb_preventive";
            this.cb_preventive.Size = new System.Drawing.Size(146, 21);
            this.cb_preventive.TabIndex = 2;
            this.cb_preventive.Text = "Include Preventive";
            this.cb_preventive.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(15, 317);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(661, 304);
            this.webBrowser1.TabIndex = 3;
            // 
            // tb_asset
            // 
            this.tb_asset.Location = new System.Drawing.Point(75, 10);
            this.tb_asset.Name = "tb_asset";
            this.tb_asset.Size = new System.Drawing.Size(119, 22);
            this.tb_asset.TabIndex = 4;
            // 
            // btn_refresh
            // 
            this.btn_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_refresh.Location = new System.Drawing.Point(601, 7);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_refresh.TabIndex = 5;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 47);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(661, 242);
            this.dataGridView1.TabIndex = 6;
            // 
            // MXxWOoverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 644);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.tb_asset);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.cb_preventive);
            this.Controls.Add(this.cb_ciblings);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(706, 689);
            this.Name = "MXxWOoverview";
            this.Text = "MXxWOoverview";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cb_ciblings;
        private System.Windows.Forms.CheckBox cb_preventive;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox tb_asset;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}