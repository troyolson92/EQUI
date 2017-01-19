namespace ExcelAddInEquipmentDatabase.Forms
{
    partial class uc_Checkbox
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
            this.lbl_1 = new System.Windows.Forms.Label();
            this.rb_enable = new System.Windows.Forms.RadioButton();
            this.cb_1 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_1
            // 
            this.lbl_1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_1.AutoSize = true;
            this.lbl_1.Location = new System.Drawing.Point(32, 0);
            this.lbl_1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(40, 23);
            this.lbl_1.TabIndex = 1;
            this.lbl_1.Text = "lbl_1";
            // 
            // rb_enable
            // 
            this.rb_enable.AutoSize = true;
            this.rb_enable.Location = new System.Drawing.Point(2, 2);
            this.rb_enable.Margin = new System.Windows.Forms.Padding(2);
            this.rb_enable.Name = "rb_enable";
            this.rb_enable.Size = new System.Drawing.Size(14, 13);
            this.rb_enable.TabIndex = 2;
            this.rb_enable.TabStop = true;
            this.rb_enable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb_enable.UseVisualStyleBackColor = true;
            this.rb_enable.Click += new System.EventHandler(this.enable_CheckedChanged);
            // 
            // cb_1
            // 
            this.cb_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_1.AutoSize = true;
            this.cb_1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_1.Location = new System.Drawing.Point(65, 2);
            this.cb_1.Margin = new System.Windows.Forms.Padding(2);
            this.cb_1.Name = "cb_1";
            this.cb_1.Size = new System.Drawing.Size(40, 19);
            this.cb_1.TabIndex = 3;
            this.cb_1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Controls.Add(this.rb_enable, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cb_1, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(135, 23);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // uc_Checkbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(250, 30);
            this.MinimumSize = new System.Drawing.Size(80, 30);
            this.Name = "uc_Checkbox";
            this.Size = new System.Drawing.Size(140, 28);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_1;
        private System.Windows.Forms.RadioButton rb_enable;
        private System.Windows.Forms.CheckBox cb_1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
