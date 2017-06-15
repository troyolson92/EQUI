namespace EqUi_UtilManger
{
    partial class Form1
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
            this.btn_AssetStats = new System.Windows.Forms.Button();
            this.btn_sbcuStat = new System.Windows.Forms.Button();
            this.btn_DocManger = new System.Windows.Forms.Button();
            this.btn_ErrorStats = new System.Windows.Forms.Button();
            this.btn_logDetails = new System.Windows.Forms.Button();
            this.Btn_MXxWoOverview = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_AssetStats
            // 
            this.btn_AssetStats.Location = new System.Drawing.Point(25, 50);
            this.btn_AssetStats.Name = "btn_AssetStats";
            this.btn_AssetStats.Size = new System.Drawing.Size(104, 23);
            this.btn_AssetStats.TabIndex = 0;
            this.btn_AssetStats.Text = "AssetStats";
            this.btn_AssetStats.UseVisualStyleBackColor = true;
            this.btn_AssetStats.Click += new System.EventHandler(this.btn_AssetStats_Click);
            // 
            // btn_sbcuStat
            // 
            this.btn_sbcuStat.Location = new System.Drawing.Point(25, 12);
            this.btn_sbcuStat.Name = "btn_sbcuStat";
            this.btn_sbcuStat.Size = new System.Drawing.Size(104, 23);
            this.btn_sbcuStat.TabIndex = 1;
            this.btn_sbcuStat.Text = "SbcuStats";
            this.btn_sbcuStat.UseVisualStyleBackColor = true;
            this.btn_sbcuStat.Click += new System.EventHandler(this.btn_sbcuStat_Click);
            // 
            // btn_DocManger
            // 
            this.btn_DocManger.Location = new System.Drawing.Point(25, 91);
            this.btn_DocManger.Name = "btn_DocManger";
            this.btn_DocManger.Size = new System.Drawing.Size(104, 23);
            this.btn_DocManger.TabIndex = 2;
            this.btn_DocManger.Text = "DocManager";
            this.btn_DocManger.UseVisualStyleBackColor = true;
            this.btn_DocManger.Click += new System.EventHandler(this.btn_DocManger_Click);
            // 
            // btn_ErrorStats
            // 
            this.btn_ErrorStats.Location = new System.Drawing.Point(25, 132);
            this.btn_ErrorStats.Name = "btn_ErrorStats";
            this.btn_ErrorStats.Size = new System.Drawing.Size(104, 23);
            this.btn_ErrorStats.TabIndex = 3;
            this.btn_ErrorStats.Text = "ErrorStats";
            this.btn_ErrorStats.UseVisualStyleBackColor = true;
            this.btn_ErrorStats.Click += new System.EventHandler(this.btn_ErrorStats_Click);
            // 
            // btn_logDetails
            // 
            this.btn_logDetails.Location = new System.Drawing.Point(25, 171);
            this.btn_logDetails.Name = "btn_logDetails";
            this.btn_logDetails.Size = new System.Drawing.Size(104, 23);
            this.btn_logDetails.TabIndex = 4;
            this.btn_logDetails.Text = "Logdetails";
            this.btn_logDetails.UseVisualStyleBackColor = true;
            this.btn_logDetails.Click += new System.EventHandler(this.btn_logDetails_Click);
            // 
            // Btn_MXxWoOverview
            // 
            this.Btn_MXxWoOverview.AutoSize = true;
            this.Btn_MXxWoOverview.Location = new System.Drawing.Point(25, 210);
            this.Btn_MXxWoOverview.Name = "Btn_MXxWoOverview";
            this.Btn_MXxWoOverview.Size = new System.Drawing.Size(123, 27);
            this.Btn_MXxWoOverview.TabIndex = 5;
            this.Btn_MXxWoOverview.Text = "MXxWoOverview";
            this.Btn_MXxWoOverview.UseVisualStyleBackColor = true;
            this.Btn_MXxWoOverview.Click += new System.EventHandler(this.Btn_MXxWoOverview_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 257);
            this.Controls.Add(this.Btn_MXxWoOverview);
            this.Controls.Add(this.btn_logDetails);
            this.Controls.Add(this.btn_ErrorStats);
            this.Controls.Add(this.btn_DocManger);
            this.Controls.Add(this.btn_sbcuStat);
            this.Controls.Add(this.btn_AssetStats);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_AssetStats;
        private System.Windows.Forms.Button btn_sbcuStat;
        private System.Windows.Forms.Button btn_DocManger;
        private System.Windows.Forms.Button btn_ErrorStats;
        private System.Windows.Forms.Button btn_logDetails;
        private System.Windows.Forms.Button Btn_MXxWoOverview;
    }
}

