namespace ABBCommTest
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.ControllerIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_addCntrl = new System.Windows.Forms.Button();
            this.tbox_ip = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_getConf = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ControllerIP});
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(688, 249);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // ControllerIP
            // 
            this.ControllerIP.Tag = "ControllerIP";
            this.ControllerIP.Width = 300;
            // 
            // btn_addCntrl
            // 
            this.btn_addCntrl.Location = new System.Drawing.Point(13, 301);
            this.btn_addCntrl.Name = "btn_addCntrl";
            this.btn_addCntrl.Size = new System.Drawing.Size(108, 23);
            this.btn_addCntrl.TabIndex = 1;
            this.btn_addCntrl.Text = "btn_addCntrl";
            this.btn_addCntrl.UseVisualStyleBackColor = true;
            this.btn_addCntrl.Click += new System.EventHandler(this.btn_addCtrl_Click);
            // 
            // tbox_ip
            // 
            this.tbox_ip.Location = new System.Drawing.Point(150, 301);
            this.tbox_ip.Name = "tbox_ip";
            this.tbox_ip.Size = new System.Drawing.Size(151, 22);
            this.tbox_ip.TabIndex = 2;
            this.tbox_ip.Text = "10.205.94.240";
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(13, 379);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 3;
            this.btn_start.Text = "btn_start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_startTask_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(13, 409);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 4;
            this.btn_stop.Text = "btn_stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_startStop_Click);
            // 
            // btn_getConf
            // 
            this.btn_getConf.Location = new System.Drawing.Point(13, 439);
            this.btn_getConf.Name = "btn_getConf";
            this.btn_getConf.Size = new System.Drawing.Size(75, 23);
            this.btn_getConf.TabIndex = 5;
            this.btn_getConf.Text = "btn_getConf";
            this.btn_getConf.UseVisualStyleBackColor = true;
            this.btn_getConf.Click += new System.EventHandler(this.btn_getConf_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 488);
            this.Controls.Add(this.btn_getConf);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.tbox_ip);
            this.Controls.Add(this.btn_addCntrl);
            this.Controls.Add(this.listView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btn_addCntrl;
        private System.Windows.Forms.TextBox tbox_ip;
        private System.Windows.Forms.ColumnHeader ControllerIP;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_getConf;
    }
}

