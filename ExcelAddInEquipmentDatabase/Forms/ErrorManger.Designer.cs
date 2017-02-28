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
            this.tb_LogCodeFilterFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_GetLogs = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_classification = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_Subgroup = new System.Windows.Forms.ComboBox();
            this.btn_ApplyManual = new System.Windows.Forms.Button();
            this.dg_Rules = new System.Windows.Forms.DataGridView();
            this.btn_ApplyRules = new System.Windows.Forms.Button();
            this.cb_OverRideManualSet = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_Results = new System.Windows.Forms.Label();
            this.tb_LogCodeFilterUntil = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Rules)).BeginInit();
            this.SuspendLayout();
            // 
            // dg_Result
            // 
            this.dg_Result.AllowUserToAddRows = false;
            this.dg_Result.AllowUserToDeleteRows = false;
            this.dg_Result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg_Result.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dg_Result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Result.Location = new System.Drawing.Point(12, 73);
            this.dg_Result.Name = "dg_Result";
            this.dg_Result.RowTemplate.Height = 24;
            this.dg_Result.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Result.Size = new System.Drawing.Size(1252, 271);
            this.dg_Result.TabIndex = 0;
            // 
            // cb_system
            // 
            this.cb_system.FormattingEnabled = true;
            this.cb_system.Location = new System.Drawing.Point(76, 6);
            this.cb_system.Name = "cb_system";
            this.cb_system.Size = new System.Drawing.Size(97, 24);
            this.cb_system.TabIndex = 1;
            this.cb_system.SelectedIndexChanged += new System.EventHandler(this.cb_system_SelectedIndexChanged);
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
            this.label2.Location = new System.Drawing.Point(210, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "LogTextFilter:";
            // 
            // tb_logTextFilter
            // 
            this.tb_logTextFilter.Location = new System.Drawing.Point(310, 6);
            this.tb_logTextFilter.Name = "tb_logTextFilter";
            this.tb_logTextFilter.Size = new System.Drawing.Size(144, 22);
            this.tb_logTextFilter.TabIndex = 4;
            this.tb_logTextFilter.Text = "%";
            // 
            // tb_LogCodeFilterFrom
            // 
            this.tb_LogCodeFilterFrom.Location = new System.Drawing.Point(634, 6);
            this.tb_LogCodeFilterFrom.Name = "tb_LogCodeFilterFrom";
            this.tb_LogCodeFilterFrom.Size = new System.Drawing.Size(80, 22);
            this.tb_LogCodeFilterFrom.TabIndex = 6;
            this.tb_LogCodeFilterFrom.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(480, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "LogCodeFilter: between";
            // 
            // btn_GetLogs
            // 
            this.btn_GetLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_GetLogs.Location = new System.Drawing.Point(1166, 3);
            this.btn_GetLogs.Name = "btn_GetLogs";
            this.btn_GetLogs.Size = new System.Drawing.Size(99, 23);
            this.btn_GetLogs.TabIndex = 7;
            this.btn_GetLogs.Text = "Get Results";
            this.btn_GetLogs.UseVisualStyleBackColor = true;
            this.btn_GetLogs.Click += new System.EventHandler(this.btn_GetLogs_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 357);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Classification:";
            // 
            // cb_classification
            // 
            this.cb_classification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_classification.FormattingEnabled = true;
            this.cb_classification.Location = new System.Drawing.Point(102, 350);
            this.cb_classification.Name = "cb_classification";
            this.cb_classification.Size = new System.Drawing.Size(552, 24);
            this.cb_classification.TabIndex = 9;
            this.cb_classification.SelectedIndexChanged += new System.EventHandler(this.cb_classification_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(660, 357);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Subgroup:";
            // 
            // cb_Subgroup
            // 
            this.cb_Subgroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_Subgroup.FormattingEnabled = true;
            this.cb_Subgroup.Location = new System.Drawing.Point(740, 350);
            this.cb_Subgroup.Name = "cb_Subgroup";
            this.cb_Subgroup.Size = new System.Drawing.Size(392, 24);
            this.cb_Subgroup.TabIndex = 11;
            this.cb_Subgroup.SelectedIndexChanged += new System.EventHandler(this.cb_Subgroup_SelectedIndexChanged);
            // 
            // btn_ApplyManual
            // 
            this.btn_ApplyManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ApplyManual.Location = new System.Drawing.Point(1151, 351);
            this.btn_ApplyManual.Name = "btn_ApplyManual";
            this.btn_ApplyManual.Size = new System.Drawing.Size(113, 23);
            this.btn_ApplyManual.TabIndex = 12;
            this.btn_ApplyManual.Text = "Apply Manual";
            this.btn_ApplyManual.UseVisualStyleBackColor = true;
            this.btn_ApplyManual.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // dg_Rules
            // 
            this.dg_Rules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dg_Rules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Rules.Location = new System.Drawing.Point(12, 412);
            this.dg_Rules.Name = "dg_Rules";
            this.dg_Rules.RowTemplate.Height = 24;
            this.dg_Rules.Size = new System.Drawing.Size(1252, 147);
            this.dg_Rules.TabIndex = 13;
            this.dg_Rules.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dg_Rules_CellValidating);
            this.dg_Rules.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_Rules_RowValidated);
            this.dg_Rules.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dg_Rules_UserAddedRow);
            this.dg_Rules.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dg_Rules_UserDeletingRow);
            // 
            // btn_ApplyRules
            // 
            this.btn_ApplyRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ApplyRules.Location = new System.Drawing.Point(1151, 573);
            this.btn_ApplyRules.Name = "btn_ApplyRules";
            this.btn_ApplyRules.Size = new System.Drawing.Size(113, 23);
            this.btn_ApplyRules.TabIndex = 14;
            this.btn_ApplyRules.Text = "Apply Rules";
            this.btn_ApplyRules.UseVisualStyleBackColor = true;
            this.btn_ApplyRules.Click += new System.EventHandler(this.btn_ApplyRules_Click);
            // 
            // cb_OverRideManualSet
            // 
            this.cb_OverRideManualSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_OverRideManualSet.AutoSize = true;
            this.cb_OverRideManualSet.Location = new System.Drawing.Point(15, 573);
            this.cb_OverRideManualSet.Name = "cb_OverRideManualSet";
            this.cb_OverRideManualSet.Size = new System.Drawing.Size(158, 21);
            this.cb_OverRideManualSet.TabIndex = 16;
            this.cb_OverRideManualSet.Text = "Override manual set";
            this.cb_OverRideManualSet.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 392);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(271, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Rules for this Classification and Subgroup";
            // 
            // lbl_Results
            // 
            this.lbl_Results.AutoSize = true;
            this.lbl_Results.Location = new System.Drawing.Point(9, 43);
            this.lbl_Results.Name = "lbl_Results";
            this.lbl_Results.Size = new System.Drawing.Size(249, 17);
            this.lbl_Results.TabIndex = 18;
            this.lbl_Results.Text = "LogText Records that mach the Query";
            // 
            // tb_LogCodeFilterUntil
            // 
            this.tb_LogCodeFilterUntil.Location = new System.Drawing.Point(758, 6);
            this.tb_LogCodeFilterUntil.Name = "tb_LogCodeFilterUntil";
            this.tb_LogCodeFilterUntil.Size = new System.Drawing.Size(80, 22);
            this.tb_LogCodeFilterUntil.TabIndex = 19;
            this.tb_LogCodeFilterUntil.Text = "99999";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(720, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 17);
            this.label7.TabIndex = 20;
            this.label7.Text = "and";
            // 
            // ErrorManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 608);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_LogCodeFilterUntil);
            this.Controls.Add(this.lbl_Results);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_OverRideManualSet);
            this.Controls.Add(this.btn_ApplyRules);
            this.Controls.Add(this.dg_Rules);
            this.Controls.Add(this.btn_ApplyManual);
            this.Controls.Add(this.cb_Subgroup);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_classification);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_GetLogs);
            this.Controls.Add(this.tb_LogCodeFilterFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_logTextFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_system);
            this.Controls.Add(this.dg_Result);
            this.MinimumSize = new System.Drawing.Size(1294, 653);
            this.Name = "ErrorManger";
            this.Text = "ErrorManger";
            ((System.ComponentModel.ISupportInitialize)(this.dg_Result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Rules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_Result;
        private System.Windows.Forms.ComboBox cb_system;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_logTextFilter;
        private System.Windows.Forms.TextBox tb_LogCodeFilterFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_GetLogs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_classification;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_Subgroup;
        private System.Windows.Forms.DataGridView dg_Rules;
        private System.Windows.Forms.Button btn_ApplyRules;
        private System.Windows.Forms.CheckBox cb_OverRideManualSet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_Results;
        private System.Windows.Forms.Button btn_ApplyManual;
        private System.Windows.Forms.TextBox tb_LogCodeFilterUntil;
        private System.Windows.Forms.Label label7;
    }
}