namespace ExcelAddInEquipmentDatabase
{
    partial class StoredProcedureManger
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Btn_saveSet = new System.Windows.Forms.Button();
            this.cb_ParmSetNames = new System.Windows.Forms.ComboBox();
            this.btn_SaveSetConfirm = new System.Windows.Forms.Button();
            this.btn_SetDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 14);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(770, 209);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // Btn_saveSet
            // 
            this.Btn_saveSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_saveSet.Location = new System.Drawing.Point(12, 235);
            this.Btn_saveSet.Name = "Btn_saveSet";
            this.Btn_saveSet.Size = new System.Drawing.Size(86, 23);
            this.Btn_saveSet.TabIndex = 1;
            this.Btn_saveSet.Text = "ParmSets";
            this.Btn_saveSet.UseVisualStyleBackColor = true;
            this.Btn_saveSet.Click += new System.EventHandler(this.Btn_saveSet_Click);
            // 
            // cb_ParmSetNames
            // 
            this.cb_ParmSetNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_ParmSetNames.FormattingEnabled = true;
            this.cb_ParmSetNames.Location = new System.Drawing.Point(104, 234);
            this.cb_ParmSetNames.Name = "cb_ParmSetNames";
            this.cb_ParmSetNames.Size = new System.Drawing.Size(226, 24);
            this.cb_ParmSetNames.TabIndex = 2;
            this.cb_ParmSetNames.Visible = false;
            this.cb_ParmSetNames.SelectedIndexChanged += new System.EventHandler(this.cb_ParmSetNames_SelectedIndexChanged);
            // 
            // btn_SaveSetConfirm
            // 
            this.btn_SaveSetConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_SaveSetConfirm.Location = new System.Drawing.Point(336, 234);
            this.btn_SaveSetConfirm.Name = "btn_SaveSetConfirm";
            this.btn_SaveSetConfirm.Size = new System.Drawing.Size(64, 23);
            this.btn_SaveSetConfirm.TabIndex = 3;
            this.btn_SaveSetConfirm.Text = "Save";
            this.btn_SaveSetConfirm.UseVisualStyleBackColor = true;
            this.btn_SaveSetConfirm.Visible = false;
            this.btn_SaveSetConfirm.Click += new System.EventHandler(this.btn_SaveSetConfirm_Click);
            // 
            // btn_SetDelete
            // 
            this.btn_SetDelete.Location = new System.Drawing.Point(406, 234);
            this.btn_SetDelete.Name = "btn_SetDelete";
            this.btn_SetDelete.Size = new System.Drawing.Size(60, 23);
            this.btn_SetDelete.TabIndex = 4;
            this.btn_SetDelete.Text = "delete";
            this.btn_SetDelete.UseVisualStyleBackColor = true;
            this.btn_SetDelete.Visible = false;
            this.btn_SetDelete.Click += new System.EventHandler(this.btn_SetDelete_Click);
            // 
            // StoredProcedureManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(795, 270);
            this.Controls.Add(this.btn_SetDelete);
            this.Controls.Add(this.btn_SaveSetConfirm);
            this.Controls.Add(this.cb_ParmSetNames);
            this.Controls.Add(this.Btn_saveSet);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Location = new System.Drawing.Point(947, 357);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1475, 758);
            this.MinimumSize = new System.Drawing.Size(813, 310);
            this.Name = "StoredProcedureManger";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "StoredProcedureManger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StoredProcedureManger_FormClosing);
            this.Shown += new System.EventHandler(this.StoredProcedureManger_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button Btn_saveSet;
        private System.Windows.Forms.ComboBox cb_ParmSetNames;
        private System.Windows.Forms.Button btn_SaveSetConfirm;
        private System.Windows.Forms.Button btn_SetDelete;
    }
}