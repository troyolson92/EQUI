namespace ExcelAddInEquipmentDatabase.Forms
{
    partial class ErrorManger
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
            this.dg_Result = new System.Windows.Forms.DataGridView();
            this.cb_system = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_logTextFilter = new System.Windows.Forms.TextBox();
            this.tb_LogCodeFilter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_GetLogs = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_classification = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_Subgroup = new System.Windows.Forms.ComboBox();
            this.btn_Set = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Result)).BeginInit();
            this.SuspendLayout();
            // 
            // dg_Result
            // 
            this.dg_Result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg_Result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Result.Location = new System.Drawing.Point(12, 36);
            this.dg_Result.Name = "dg_Result";
            this.dg_Result.RowTemplate.Height = 24;
            this.dg_Result.Size = new System.Drawing.Size(699, 281);
            this.dg_Result.TabIndex = 0;
            // 
            // cb_system
            // 
            this.cb_system.FormattingEnabled = true;
            this.cb_system.Location = new System.Drawing.Point(66, 6);
            this.cb_system.Name = "cb_system";
            this.cb_system.Size = new System.Drawing.Size(85, 24);
            this.cb_system.TabIndex = 1;
            this.cb_system.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "System:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "LogTextFilter:";
            // 
            // tb_logTextFilter
            // 
            this.tb_logTextFilter.Location = new System.Drawing.Point(248, 6);
            this.tb_logTextFilter.Name = "tb_logTextFilter";
            this.tb_logTextFilter.Size = new System.Drawing.Size(153, 22);
            this.tb_logTextFilter.TabIndex = 4;
            this.tb_logTextFilter.Text = "%";
            // 
            // tb_LogCodeFilter
            // 
            this.tb_LogCodeFilter.Location = new System.Drawing.Point(503, 4);
            this.tb_LogCodeFilter.Name = "tb_LogCodeFilter";
            this.tb_LogCodeFilter.Size = new System.Drawing.Size(80, 22);
            this.tb_LogCodeFilter.TabIndex = 6;
            this.tb_LogCodeFilter.Text = "%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(407, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "LogCodeFilter:";
            // 
            // btn_GetLogs
            // 
            this.btn_GetLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_GetLogs.Location = new System.Drawing.Point(599, 3);
            this.btn_GetLogs.Name = "btn_GetLogs";
            this.btn_GetLogs.Size = new System.Drawing.Size(113, 23);
            this.btn_GetLogs.TabIndex = 7;
            this.btn_GetLogs.Text = "Get Results";
            this.btn_GetLogs.UseVisualStyleBackColor = true;
            this.btn_GetLogs.Click += new System.EventHandler(this.btn_GetLogs_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 332);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Classification:";
            // 
            // cb_classification
            // 
            this.cb_classification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_classification.FormattingEnabled = true;
            this.cb_classification.Location = new System.Drawing.Point(103, 325);
            this.cb_classification.Name = "cb_classification";
            this.cb_classification.Size = new System.Drawing.Size(237, 24);
            this.cb_classification.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(367, 332);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Subgroup:";
            // 
            // cb_Subgroup
            // 
            this.cb_Subgroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_Subgroup.FormattingEnabled = true;
            this.cb_Subgroup.Location = new System.Drawing.Point(447, 325);
            this.cb_Subgroup.Name = "cb_Subgroup";
            this.cb_Subgroup.Size = new System.Drawing.Size(136, 24);
            this.cb_Subgroup.TabIndex = 11;
            // 
            // btn_Set
            // 
            this.btn_Set.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Set.Location = new System.Drawing.Point(599, 326);
            this.btn_Set.Name = "btn_Set";
            this.btn_Set.Size = new System.Drawing.Size(113, 23);
            this.btn_Set.TabIndex = 12;
            this.btn_Set.Text = "Set Selection";
            this.btn_Set.UseVisualStyleBackColor = true;
            this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // ErrorManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 371);
            this.Controls.Add(this.btn_Set);
            this.Controls.Add(this.cb_Subgroup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_classification);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_GetLogs);
            this.Controls.Add(this.tb_LogCodeFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_logTextFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_system);
            this.Controls.Add(this.dg_Result);
            this.MinimumSize = new System.Drawing.Size(741, 416);
            this.Name = "ErrorManger";
            this.Text = "ErrorManger";
            ((System.ComponentModel.ISupportInitialize)(this.dg_Result)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_Result;
        private System.Windows.Forms.ComboBox cb_system;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_logTextFilter;
        private System.Windows.Forms.TextBox tb_LogCodeFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_GetLogs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_classification;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_Subgroup;
        private System.Windows.Forms.Button btn_Set;
    }
}