namespace ExcelAddInEquipmentDatabase.Forms
{
    partial class ProcedureManger
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_SetDelete = new System.Windows.Forms.Button();
            this.btn_SaveSetConfirm = new System.Windows.Forms.Button();
            this.cb_ParmSetNames = new System.Windows.Forms.ComboBox();
            this.Btn_saveSet = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 2);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(570, 622);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // btn_SetDelete
            // 
            this.btn_SetDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_SetDelete.Location = new System.Drawing.Point(503, 14);
            this.btn_SetDelete.Name = "btn_SetDelete";
            this.btn_SetDelete.Size = new System.Drawing.Size(31, 23);
            this.btn_SetDelete.TabIndex = 8;
            this.btn_SetDelete.Text = "delete";
            this.btn_SetDelete.UseVisualStyleBackColor = true;
            this.btn_SetDelete.Visible = false;
            this.btn_SetDelete.Click += new System.EventHandler(this.btn_SetDelete_Click);
            // 
            // btn_SaveSetConfirm
            // 
            this.btn_SaveSetConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_SaveSetConfirm.Location = new System.Drawing.Point(453, 14);
            this.btn_SaveSetConfirm.Name = "btn_SaveSetConfirm";
            this.btn_SaveSetConfirm.Size = new System.Drawing.Size(26, 23);
            this.btn_SaveSetConfirm.TabIndex = 7;
            this.btn_SaveSetConfirm.Text = "Save";
            this.btn_SaveSetConfirm.UseVisualStyleBackColor = true;
            this.btn_SaveSetConfirm.Visible = false;
            this.btn_SaveSetConfirm.Click += new System.EventHandler(this.btn_SaveSetConfirm_Click);
            // 
            // cb_ParmSetNames
            // 
            this.cb_ParmSetNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_ParmSetNames.FormattingEnabled = true;
            this.cb_ParmSetNames.Location = new System.Drawing.Point(203, 13);
            this.cb_ParmSetNames.Name = "cb_ParmSetNames";
            this.cb_ParmSetNames.Size = new System.Drawing.Size(226, 24);
            this.cb_ParmSetNames.TabIndex = 6;
            this.cb_ParmSetNames.Visible = false;
            this.cb_ParmSetNames.SelectedIndexChanged += new System.EventHandler(this.cb_ParmSetNames_SelectedIndexChanged);
            // 
            // Btn_saveSet
            // 
            this.Btn_saveSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_saveSet.Location = new System.Drawing.Point(3, 14);
            this.Btn_saveSet.Name = "Btn_saveSet";
            this.Btn_saveSet.Size = new System.Drawing.Size(165, 23);
            this.Btn_saveSet.TabIndex = 5;
            this.Btn_saveSet.Text = "Manage ParmSets";
            this.Btn_saveSet.UseVisualStyleBackColor = true;
            this.Btn_saveSet.Click += new System.EventHandler(this.Btn_saveSet_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btn_SetDelete, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.cb_ParmSetNames, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_SaveSetConfirm, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.Btn_saveSet, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 629);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(570, 40);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // ProcedureManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ProcedureManger";
            this.Size = new System.Drawing.Size(576, 686);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_SetDelete;
        private System.Windows.Forms.Button btn_SaveSetConfirm;
        private System.Windows.Forms.ComboBox cb_ParmSetNames;
        private System.Windows.Forms.Button Btn_saveSet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
