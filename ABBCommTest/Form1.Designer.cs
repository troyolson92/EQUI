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
            this.btn_addCntrl = new System.Windows.Forms.Button();
            this.tbox_ip = new System.Windows.Forms.TextBox();
            this.btn_scanNetwork = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_expose = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_addCntrl
            // 
            this.btn_addCntrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_addCntrl.Location = new System.Drawing.Point(13, 615);
            this.btn_addCntrl.Name = "btn_addCntrl";
            this.btn_addCntrl.Size = new System.Drawing.Size(158, 23);
            this.btn_addCntrl.TabIndex = 1;
            this.btn_addCntrl.Text = "btn_addCntrl_byIp";
            this.btn_addCntrl.UseVisualStyleBackColor = true;
            this.btn_addCntrl.Click += new System.EventHandler(this.btn_addCtrl_Click);
            // 
            // tbox_ip
            // 
            this.tbox_ip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbox_ip.Location = new System.Drawing.Point(186, 616);
            this.tbox_ip.Name = "tbox_ip";
            this.tbox_ip.Size = new System.Drawing.Size(151, 22);
            this.tbox_ip.TabIndex = 2;
            this.tbox_ip.Text = "10.205.94.240";
            // 
            // btn_scanNetwork
            // 
            this.btn_scanNetwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_scanNetwork.Location = new System.Drawing.Point(186, 645);
            this.btn_scanNetwork.Name = "btn_scanNetwork";
            this.btn_scanNetwork.Size = new System.Drawing.Size(151, 23);
            this.btn_scanNetwork.TabIndex = 8;
            this.btn_scanNetwork.Text = "scanNetwork";
            this.btn_scanNetwork.UseVisualStyleBackColor = true;
            this.btn_scanNetwork.Click += new System.EventHandler(this.btn_scanNetwork_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1774, 582);
            this.dataGridView1.TabIndex = 10;
            // 
            // btn_expose
            // 
            this.btn_expose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_expose.Location = new System.Drawing.Point(13, 645);
            this.btn_expose.Name = "btn_expose";
            this.btn_expose.Size = new System.Drawing.Size(158, 23);
            this.btn_expose.TabIndex = 11;
            this.btn_expose.Text = "exposeDatagridToNet";
            this.btn_expose.UseVisualStyleBackColor = true;
            this.btn_expose.Click += new System.EventHandler(this.btn_expose_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(500, 616);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 52);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1799, 680);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_expose);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_scanNetwork);
            this.Controls.Add(this.tbox_ip);
            this.Controls.Add(this.btn_addCntrl);
            this.Name = "Form1";
            this.Text = "SDEBEUL TOOL FOR CFG ABB NFS SETTINGS!! STAY OFF!!";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_addCntrl;
        private System.Windows.Forms.TextBox tbox_ip;
        private System.Windows.Forms.Button btn_scanNetwork;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_expose;
        private System.Windows.Forms.Button button1;
    }
}

