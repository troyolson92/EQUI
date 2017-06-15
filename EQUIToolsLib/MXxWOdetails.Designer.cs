namespace EQUIToolsLib
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
            this.btn_get = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.tb_LONGDESCRIPTIONID = new MetroFramework.Controls.MetroTextBox();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.SuspendLayout();
            // 
            // wb_longdescrption
            // 
            this.wb_longdescrption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wb_longdescrption.Location = new System.Drawing.Point(12, 84);
            this.wb_longdescrption.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb_longdescrption.Name = "wb_longdescrption";
            this.wb_longdescrption.Size = new System.Drawing.Size(556, 445);
            this.wb_longdescrption.TabIndex = 1;
            // 
            // btn_get
            // 
            this.btn_get.Location = new System.Drawing.Point(493, 55);
            this.btn_get.Name = "btn_get";
            this.btn_get.Size = new System.Drawing.Size(75, 23);
            this.btn_get.TabIndex = 5;
            this.btn_get.Text = "Refresh";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(24, 55);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(69, 20);
            this.metroLabel1.TabIndex = 6;
            this.metroLabel1.Text = "WONUM:";
            // 
            // tb_LONGDESCRIPTIONID
            // 
            this.tb_LONGDESCRIPTIONID.Location = new System.Drawing.Point(99, 52);
            this.tb_LONGDESCRIPTIONID.Name = "tb_LONGDESCRIPTIONID";
            this.tb_LONGDESCRIPTIONID.Size = new System.Drawing.Size(86, 23);
            this.tb_LONGDESCRIPTIONID.TabIndex = 7;
            this.tb_LONGDESCRIPTIONID.Text = "metroTextBox1";
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroProgressSpinner1.Location = new System.Drawing.Point(198, 206);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(166, 162);
            this.metroProgressSpinner1.TabIndex = 8;
            // 
            // MXxWOdetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 541);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.tb_LONGDESCRIPTIONID);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.btn_get);
            this.Controls.Add(this.wb_longdescrption);
            this.Name = "MXxWOdetails";
            this.Text = "MXxWOdetails";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wb_longdescrption;
        private MetroFramework.Controls.MetroButton btn_get;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox tb_LONGDESCRIPTIONID;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
    }
}