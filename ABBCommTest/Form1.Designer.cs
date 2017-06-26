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
            this.btn_writeNFS = new System.Windows.Forms.Button();
            this.btn_scanNetwork = new System.Windows.Forms.Button();
            this.btnDirMap = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_expose = new System.Windows.Forms.Button();
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
            // btn_writeNFS
            // 
            this.btn_writeNFS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_writeNFS.Location = new System.Drawing.Point(353, 645);
            this.btn_writeNFS.Name = "btn_writeNFS";
            this.btn_writeNFS.Size = new System.Drawing.Size(137, 23);
            this.btn_writeNFS.TabIndex = 5;
            this.btn_writeNFS.Text = "WRITE NFS CFG";
            this.btn_writeNFS.UseVisualStyleBackColor = true;
            this.btn_writeNFS.Click += new System.EventHandler(this.btn_writeNFS_Click);
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
            // btnDirMap
            // 
            this.btnDirMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDirMap.Location = new System.Drawing.Point(990, 645);
            this.btnDirMap.Name = "btnDirMap";
            this.btnDirMap.Size = new System.Drawing.Size(75, 23);
            this.btnDirMap.TabIndex = 9;
            this.btnDirMap.Text = "DirMap";
            this.btnDirMap.UseVisualStyleBackColor = true;
            this.btnDirMap.Click += new System.EventHandler(this.btnDirMap_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1053, 582);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 680);
            this.Controls.Add(this.btn_expose);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnDirMap);
            this.Controls.Add(this.btn_scanNetwork);
            this.Controls.Add(this.btn_writeNFS);
            this.Controls.Add(this.tbox_ip);
            this.Controls.Add(this.btn_addCntrl);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_addCntrl;
        private System.Windows.Forms.TextBox tbox_ip;
        private System.Windows.Forms.Button btn_writeNFS;
        private System.Windows.Forms.Button btn_scanNetwork;
        private System.Windows.Forms.Button btnDirMap;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_expose;
    }
}

