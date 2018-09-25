namespace ExcelAddInEquipmentDatabase.Forms
{
    partial class uc_Inputbox
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
            this.tb_1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_1
            // 
            this.lbl_1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_1.AutoSize = true;
            this.lbl_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_1.Location = new System.Drawing.Point(30, 6);
            this.lbl_1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_1.Name = "lbl_1";
            this.lbl_1.Size = new System.Drawing.Size(48, 25);
            this.lbl_1.TabIndex = 1;
            this.lbl_1.Text = "lbl_1";
            this.lbl_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rb_enable
            // 
            this.rb_enable.AutoCheck = false;
            this.rb_enable.AutoSize = true;
            this.rb_enable.BackColor = System.Drawing.Color.White;
            this.rb_enable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_enable.Location = new System.Drawing.Point(2, 7);
            this.rb_enable.Margin = new System.Windows.Forms.Padding(2);
            this.rb_enable.Name = "rb_enable";
            this.rb_enable.Size = new System.Drawing.Size(14, 13);
            this.rb_enable.TabIndex = 2;
            this.rb_enable.TabStop = true;
            this.rb_enable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb_enable.UseVisualStyleBackColor = false;
            this.rb_enable.Click += new System.EventHandler(this.enable_CheckedChanged);
            // 
            // tb_1
            // 
            this.tb_1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.tb_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_1.Lines = new string[0];
            this.tb_1.Location = new System.Drawing.Point(142, 2);
            this.tb_1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tb_1.MaxLength = 32767;
            this.tb_1.Name = "tb_1";
            this.tb_1.PasswordChar = '\0';
            this.tb_1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tb_1.SelectedText = "";
            this.tb_1.SelectionLength = 0;
            this.tb_1.SelectionStart = 0;
            this.tb_1.ShortcutsEnabled = true;
            this.tb_1.Size = new System.Drawing.Size(148, 20);
            this.tb_1.TabIndex = 3;
            this.tb_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // uc_Inputbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.rb_enable);
            this.Controls.Add(this.lbl_1);
            this.Controls.Add(this.tb_1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(300, 30);
            this.MinimumSize = new System.Drawing.Size(150, 30);
            this.Name = "uc_Inputbox";
            this.Size = new System.Drawing.Size(298, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rb_enable;
        private System.Windows.Forms.Label lbl_1;
        private System.Windows.Forms.TextBox tb_1;
    }
}
