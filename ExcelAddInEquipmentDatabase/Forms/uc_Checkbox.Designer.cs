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
            this.SuspendLayout();
            // 
            // lbl_1
            // 
            this.lbl_1.AutoSize = true;
            this.lbl_1.Location = new System.Drawing.Point(26, 4);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(38, 17);
            this.lbl_1.TabIndex = 1;
            this.lbl_1.Text = "lbl_1";
            // 
            // rb_enable
            // 
            this.rb_enable.AutoSize = true;
            this.rb_enable.Location = new System.Drawing.Point(3, 5);
            this.rb_enable.Name = "rb_enable";
            this.rb_enable.Size = new System.Drawing.Size(17, 16);
            this.rb_enable.TabIndex = 2;
            this.rb_enable.TabStop = true;
            this.rb_enable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb_enable.UseVisualStyleBackColor = true;
            this.rb_enable.CheckedChanged += new System.EventHandler(this.enable_CheckedChanged);
            // 
            // cb_1
            // 
            this.cb_1.AutoSize = true;
            this.cb_1.Location = new System.Drawing.Point(207, 5);
            this.cb_1.Name = "cb_1";
            this.cb_1.Size = new System.Drawing.Size(18, 17);
            this.cb_1.TabIndex = 3;
            this.cb_1.UseVisualStyleBackColor = true;
            // 
            // uc_Checkbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cb_1);
            this.Controls.Add(this.rb_enable);
            this.Controls.Add(this.lbl_1);
            this.Name = "uc_Checkbox";
            this.Size = new System.Drawing.Size(228, 29);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_1;
        private System.Windows.Forms.RadioButton rb_enable;
        private System.Windows.Forms.CheckBox cb_1;
    }
}
