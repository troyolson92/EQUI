﻿namespace ExcelAddInEquipmentDatabase.Forms
{
    partial class uc_Datebox
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
            this.lbl_1 = new MetroFramework.Controls.MetroLabel();
            this.rb_enable = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dt_1 = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_1
            // 
            this.lbl_1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_1.AutoSize = true;
            this.lbl_1.Location = new System.Drawing.Point(43, 0);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(37, 32);
            this.lbl_1.TabIndex = 1;
            this.lbl_1.Text = "lbl_1";
            // 
            // rb_enable
            // 
            this.rb_enable.AutoCheck = false;
            this.rb_enable.AutoSize = true;
            this.rb_enable.Location = new System.Drawing.Point(3, 2);
            this.rb_enable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rb_enable.Name = "rb_enable";
            this.rb_enable.Size = new System.Drawing.Size(16, 0);
            this.rb_enable.TabIndex = 2;
            this.rb_enable.TabStop = true;
            this.rb_enable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb_enable.UseVisualStyleBackColor = true;
            this.rb_enable.Click += new System.EventHandler(this.enable_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 211F));
            this.tableLayoutPanel1.Controls.Add(this.rb_enable, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dt_1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(294, 32);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // dt_1
            // 
            this.dt_1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dt_1.CustomFormat = "yyyy-MM-dd hh:mm:ss";
            this.dt_1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_1.Location = new System.Drawing.Point(86, 2);
            this.dt_1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dt_1.Name = "dt_1";
            this.dt_1.Size = new System.Drawing.Size(205, 22);
            this.dt_1.TabIndex = 4;
            // 
            // uc_Datebox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(399, 36);
            this.MinimumSize = new System.Drawing.Size(199, 36);
            this.Name = "uc_Datebox";
            this.Size = new System.Drawing.Size(301, 36);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DateTimePicker dt_1;
        private MetroFramework.Controls.MetroLabel lbl_1;
        private System.Windows.Forms.RadioButton rb_enable;
    }
}
