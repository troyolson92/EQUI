namespace EQUIToolsLib
{
    partial class MXxBomTree
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
            this.btn_get = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.tb_item = new MetroFramework.Controls.MetroTextBox();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.Btn_copy = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // btn_get
            // 
            this.btn_get.Location = new System.Drawing.Point(493, 55);
            this.btn_get.Name = "btn_get";
            this.btn_get.Size = new System.Drawing.Size(75, 23);
            this.btn_get.TabIndex = 5;
            this.btn_get.Text = "Refresh";
            this.btn_get.UseSelectable = true;
            this.btn_get.Click += new System.EventHandler(this.btn_get_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(24, 55);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(72, 20);
            this.metroLabel1.TabIndex = 6;
            this.metroLabel1.Text = "ItemNUM:";
            // 
            // tb_item
            // 
            // 
            // 
            // 
            this.tb_item.CustomButton.Image = null;
            this.tb_item.CustomButton.Location = new System.Drawing.Point(64, 1);
            this.tb_item.CustomButton.Name = "";
            this.tb_item.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tb_item.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tb_item.CustomButton.TabIndex = 1;
            this.tb_item.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tb_item.CustomButton.UseSelectable = true;
            this.tb_item.CustomButton.Visible = false;
            this.tb_item.Lines = new string[] {
        "metroTextBox1"};
            this.tb_item.Location = new System.Drawing.Point(102, 52);
            this.tb_item.MaxLength = 32767;
            this.tb_item.Name = "tb_item";
            this.tb_item.PasswordChar = '\0';
            this.tb_item.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tb_item.SelectedText = "";
            this.tb_item.SelectionLength = 0;
            this.tb_item.SelectionStart = 0;
            this.tb_item.ShortcutsEnabled = true;
            this.tb_item.Size = new System.Drawing.Size(86, 23);
            this.tb_item.TabIndex = 7;
            this.tb_item.Text = "metroTextBox1";
            this.tb_item.UseSelectable = true;
            this.tb_item.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tb_item.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroProgressSpinner1.Location = new System.Drawing.Point(344, 12);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(71, 66);
            this.metroProgressSpinner1.TabIndex = 8;
            this.metroProgressSpinner1.UseSelectable = true;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(23, 90);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(534, 428);
            this.treeView1.TabIndex = 9;
            // 
            // Btn_copy
            // 
            this.Btn_copy.Location = new System.Drawing.Point(398, 55);
            this.Btn_copy.Name = "Btn_copy";
            this.Btn_copy.Size = new System.Drawing.Size(75, 23);
            this.Btn_copy.TabIndex = 10;
            this.Btn_copy.Text = "ToClipBoard";
            this.Btn_copy.UseSelectable = true;
            this.Btn_copy.Click += new System.EventHandler(this.Btn_copy_Click);
            // 
            // MXxBomTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 541);
            this.Controls.Add(this.Btn_copy);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.tb_item);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.btn_get);
            this.Controls.Add(this.treeView1);
            this.Name = "MXxBomTree";
            this.Text = "TreeNodes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btn_get;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox tb_item;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private System.Windows.Forms.TreeView treeView1;
        private MetroFramework.Controls.MetroButton Btn_copy;
    }
}