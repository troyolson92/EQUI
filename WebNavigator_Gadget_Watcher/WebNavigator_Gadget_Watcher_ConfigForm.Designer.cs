﻿namespace WebNavigator_Gadget_Watcher
{
    partial class WebNavigator_Gadget_Watcher_ConfigForm
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
            this.panel_setup = new System.Windows.Forms.Panel();
            this.tb_ExportedFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_readConfig = new System.Windows.Forms.Button();
            this.cb_WatchConfig = new System.Windows.Forms.CheckBox();
            this.tb_xmlConfig = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_data = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_netExportPass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_netExportUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_netExportLocation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_StartMonitoring = new System.Windows.Forms.Button();
            this.panel_setup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_setup
            // 
            this.panel_setup.Controls.Add(this.tb_ExportedFile);
            this.panel_setup.Controls.Add(this.label2);
            this.panel_setup.Controls.Add(this.btn_readConfig);
            this.panel_setup.Controls.Add(this.cb_WatchConfig);
            this.panel_setup.Controls.Add(this.tb_xmlConfig);
            this.panel_setup.Controls.Add(this.label1);
            this.panel_setup.Location = new System.Drawing.Point(13, 12);
            this.panel_setup.Name = "panel_setup";
            this.panel_setup.Size = new System.Drawing.Size(839, 130);
            this.panel_setup.TabIndex = 0;
            // 
            // tb_ExportedFile
            // 
            this.tb_ExportedFile.Location = new System.Drawing.Point(167, 80);
            this.tb_ExportedFile.Name = "tb_ExportedFile";
            this.tb_ExportedFile.Size = new System.Drawing.Size(530, 20);
            this.tb_ExportedFile.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Gadget exported jpg file";
            // 
            // btn_readConfig
            // 
            this.btn_readConfig.Location = new System.Drawing.Point(722, 12);
            this.btn_readConfig.Name = "btn_readConfig";
            this.btn_readConfig.Size = new System.Drawing.Size(95, 23);
            this.btn_readConfig.TabIndex = 3;
            this.btn_readConfig.Text = "Read config";
            this.btn_readConfig.UseVisualStyleBackColor = true;
            this.btn_readConfig.Click += new System.EventHandler(this.btn_readConfig_Click);
            // 
            // cb_WatchConfig
            // 
            this.cb_WatchConfig.AutoSize = true;
            this.cb_WatchConfig.Location = new System.Drawing.Point(25, 51);
            this.cb_WatchConfig.Name = "cb_WatchConfig";
            this.cb_WatchConfig.Size = new System.Drawing.Size(340, 17);
            this.cb_WatchConfig.TabIndex = 2;
            this.cb_WatchConfig.Text = "Watch config file for changes (Appl restart when change detected)";
            this.cb_WatchConfig.UseVisualStyleBackColor = true;
            // 
            // tb_xmlConfig
            // 
            this.tb_xmlConfig.Location = new System.Drawing.Point(167, 15);
            this.tb_xmlConfig.Name = "tb_xmlConfig";
            this.tb_xmlConfig.Size = new System.Drawing.Size(530, 20);
            this.tb_xmlConfig.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gadget xml config file";
            // 
            // dgv_data
            // 
            this.dgv_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_data.Location = new System.Drawing.Point(13, 177);
            this.dgv_data.Name = "dgv_data";
            this.dgv_data.Size = new System.Drawing.Size(839, 340);
            this.dgv_data.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_StartMonitoring);
            this.panel1.Controls.Add(this.tb_netExportPass);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tb_netExportUser);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tb_netExportLocation);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(13, 535);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(839, 88);
            this.panel1.TabIndex = 6;
            // 
            // tb_netExportPass
            // 
            this.tb_netExportPass.Location = new System.Drawing.Point(234, 44);
            this.tb_netExportPass.Name = "tb_netExportPass";
            this.tb_netExportPass.Size = new System.Drawing.Size(94, 20);
            this.tb_netExportPass.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(175, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Password";
            // 
            // tb_netExportUser
            // 
            this.tb_netExportUser.Location = new System.Drawing.Point(73, 44);
            this.tb_netExportUser.Name = "tb_netExportUser";
            this.tb_netExportUser.Size = new System.Drawing.Size(94, 20);
            this.tb_netExportUser.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "User";
            // 
            // tb_netExportLocation
            // 
            this.tb_netExportLocation.Location = new System.Drawing.Point(167, 18);
            this.tb_netExportLocation.Name = "tb_netExportLocation";
            this.tb_netExportLocation.Size = new System.Drawing.Size(650, 20);
            this.tb_netExportLocation.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Jpg Export networkLocation";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Configuration";
            // 
            // btn_StartMonitoring
            // 
            this.btn_StartMonitoring.Location = new System.Drawing.Point(722, 47);
            this.btn_StartMonitoring.Name = "btn_StartMonitoring";
            this.btn_StartMonitoring.Size = new System.Drawing.Size(95, 23);
            this.btn_StartMonitoring.TabIndex = 6;
            this.btn_StartMonitoring.Text = "StartMonitoring";
            this.btn_StartMonitoring.UseVisualStyleBackColor = true;
            this.btn_StartMonitoring.Click += new System.EventHandler(this.btn_StartMonitoring_Click);
            // 
            // WebNavigator_Gadget_Watcher_ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 648);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv_data);
            this.Controls.Add(this.panel_setup);
            this.Name = "WebNavigator_Gadget_Watcher_ConfigForm";
            this.Text = "WebNavigator_Gadget_Watcher";
            this.panel_setup.ResumeLayout(false);
            this.panel_setup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_setup;
        private System.Windows.Forms.TextBox tb_ExportedFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_readConfig;
        private System.Windows.Forms.CheckBox cb_WatchConfig;
        private System.Windows.Forms.TextBox tb_xmlConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_data;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_netExportPass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_netExportUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_netExportLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_StartMonitoring;
    }
}

