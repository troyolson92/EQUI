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
            this.btn_sbcuStat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_sbcuStat
            // 
            this.btn_sbcuStat.Location = new System.Drawing.Point(19, 10);
            this.btn_sbcuStat.Margin = new System.Windows.Forms.Padding(2);
            this.btn_sbcuStat.Name = "btn_sbcuStat";
            this.btn_sbcuStat.Size = new System.Drawing.Size(101, 19);
            this.btn_sbcuStat.TabIndex = 1;
            this.btn_sbcuStat.Text = "SbcuStats";
            this.btn_sbcuStat.UseVisualStyleBackColor = true;
            this.btn_sbcuStat.Click += new System.EventHandler(this.btn_sbcuStat_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(131, 40);
            this.Controls.Add(this.btn_sbcuStat);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_sbcuStat;
    }
}

