﻿namespace ExcelAddInEquipmentDatabase.Forms
{
    partial class MXxWOoverview
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_refresh = new MetroFramework.Controls.MetroButton();
            this.cb_ciblings = new MetroFramework.Controls.MetroCheckBox();
            this.cb_preventive = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.tb_location = new MetroFramework.Controls.MetroTextBox();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(15, 355);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1271, 297);
            this.webBrowser1.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 107);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(1271, 242);
            this.dataGridView1.TabIndex = 6;
            // 
            // btn_refresh
            // 
            this.btn_refresh.Location = new System.Drawing.Point(1200, 55);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_refresh.TabIndex = 7;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // cb_ciblings
            // 
            this.cb_ciblings.AutoSize = true;
            this.cb_ciblings.Location = new System.Drawing.Point(321, 3);
            this.cb_ciblings.Name = "cb_ciblings";
            this.cb_ciblings.Size = new System.Drawing.Size(113, 17);
            this.cb_ciblings.TabIndex = 8;
            this.cb_ciblings.Text = "include ciblings";
            this.cb_ciblings.UseVisualStyleBackColor = true;
            this.cb_ciblings.CheckedChanged += new System.EventHandler(this.cb_ciblings_CheckedChanged);
            // 
            // cb_preventive
            // 
            this.cb_preventive.AutoSize = true;
            this.cb_preventive.Location = new System.Drawing.Point(440, 3);
            this.cb_preventive.Name = "cb_preventive";
            this.cb_preventive.Size = new System.Drawing.Size(223, 17);
            this.cb_preventive.TabIndex = 9;
            this.cb_preventive.Text = "include PP, PCI, WSCH (preventive)";
            this.cb_preventive.UseVisualStyleBackColor = true;
            this.cb_preventive.CheckedChanged += new System.EventHandler(this.cb_preventive_CheckedChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(3, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(64, 20);
            this.metroLabel1.TabIndex = 10;
            this.metroLabel1.Text = "Location:";
            // 
            // tb_location
            // 
            this.tb_location.Location = new System.Drawing.Point(73, 3);
            this.tb_location.Name = "tb_location";
            this.tb_location.Size = new System.Drawing.Size(182, 23);
            this.tb_location.TabIndex = 11;
            this.tb_location.Text = "metroTextBox1";
            this.tb_location.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroProgressSpinner1.Location = new System.Drawing.Point(571, 136);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(188, 183);
            this.metroProgressSpinner1.TabIndex = 12;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cb_ciblings, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_location, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cb_preventive, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(23, 63);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(971, 38);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // MXxWOoverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 689);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.webBrowser1);
            this.MinimumSize = new System.Drawing.Size(706, 689);
            this.Name = "MXxWOoverview";
            this.Text = "MXxWOoverview";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private MetroFramework.Controls.MetroButton btn_refresh;
        private MetroFramework.Controls.MetroCheckBox cb_ciblings;
        private MetroFramework.Controls.MetroCheckBox cb_preventive;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox tb_location;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}