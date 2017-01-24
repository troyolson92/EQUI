namespace ExcelAddInEquipmentDatabase.Forms
{
    partial class MXxWOdetails
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
            this.wb_longdescrption = new System.Windows.Forms.WebBrowser();
            this.btn_get = new System.Windows.Forms.Button();
            this.tb_LONGDESCRIPTIONID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // wb_longdescrption
            // 
            this.wb_longdescrption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wb_longdescrption.Location = new System.Drawing.Point(12, 34);
            this.wb_longdescrption.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb_longdescrption.Name = "wb_longdescrption";
            this.wb_longdescrption.Size = new System.Drawing.Size(556, 495);
            this.wb_longdescrption.TabIndex = 1;
            // 
            // btn_get
            // 
            this.btn_get.Location = new System.Drawing.Point(191, 5);
            this.btn_get.Name = "btn_get";
            this.btn_get.Size = new System.Drawing.Size(75, 23);
            this.btn_get.TabIndex = 2;
            this.btn_get.Text = "Get";
            this.btn_get.UseVisualStyleBackColor = true;
            this.btn_get.Click += new System.EventHandler(this.btn_get_Click);
            // 
            // tb_LONGDESCRIPTIONID
            // 
            this.tb_LONGDESCRIPTIONID.Location = new System.Drawing.Point(85, 6);
            this.tb_LONGDESCRIPTIONID.Name = "tb_LONGDESCRIPTIONID";
            this.tb_LONGDESCRIPTIONID.Size = new System.Drawing.Size(100, 22);
            this.tb_LONGDESCRIPTIONID.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "WONUM:";
            // 
            // MXxWOdetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 541);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_LONGDESCRIPTIONID);
            this.Controls.Add(this.btn_get);
            this.Controls.Add(this.wb_longdescrption);
            this.Name = "MXxWOdetails";
            this.Text = "MXxWOdetails";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wb_longdescrption;
        private System.Windows.Forms.Button btn_get;
        private System.Windows.Forms.TextBox tb_LONGDESCRIPTIONID;
        private System.Windows.Forms.Label label1;
    }
}