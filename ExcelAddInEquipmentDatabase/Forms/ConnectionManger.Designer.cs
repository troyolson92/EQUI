namespace ExcelAddInEquipmentDatabase
{
    partial class ConnectionManger
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
            this.t_connections = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Edit = new System.Windows.Forms.Button();
            this.lb_connections = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lb_procParms = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_GADATA_Create = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_procedures = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btn_MX7_create = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btn_MX3_create = new System.Windows.Forms.Button();
            this.t_connections.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // t_connections
            // 
            this.t_connections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t_connections.Controls.Add(this.tabPage1);
            this.t_connections.Controls.Add(this.tabPage2);
            this.t_connections.Controls.Add(this.tabPage3);
            this.t_connections.Controls.Add(this.tabPage4);
            this.t_connections.Location = new System.Drawing.Point(12, 12);
            this.t_connections.Name = "t_connections";
            this.t_connections.SelectedIndex = 0;
            this.t_connections.Size = new System.Drawing.Size(489, 334);
            this.t_connections.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_Delete);
            this.tabPage1.Controls.Add(this.btn_Edit);
            this.tabPage1.Controls.Add(this.lb_connections);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(481, 305);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Connections";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.Location = new System.Drawing.Point(402, 47);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Delete.TabIndex = 12;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Edit
            // 
            this.btn_Edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Edit.Location = new System.Drawing.Point(402, 17);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(75, 23);
            this.btn_Edit.TabIndex = 11;
            this.btn_Edit.Text = "Edit";
            this.btn_Edit.UseVisualStyleBackColor = true;
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // lb_connections
            // 
            this.lb_connections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_connections.FormattingEnabled = true;
            this.lb_connections.ItemHeight = 16;
            this.lb_connections.Location = new System.Drawing.Point(5, 17);
            this.lb_connections.Name = "lb_connections";
            this.lb_connections.Size = new System.Drawing.Size(391, 260);
            this.lb_connections.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lb_procParms);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btn_GADATA_Create);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.cb_procedures);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(481, 305);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "New Gadata";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lb_procParms
            // 
            this.lb_procParms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_procParms.FormattingEnabled = true;
            this.lb_procParms.ItemHeight = 16;
            this.lb_procParms.Location = new System.Drawing.Point(20, 71);
            this.lb_procParms.Name = "lb_procParms";
            this.lb_procParms.ScrollAlwaysVisible = true;
            this.lb_procParms.Size = new System.Drawing.Size(455, 180);
            this.lb_procParms.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(338, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Available stored procedures in gadata.volvo schema";
            // 
            // btn_GADATA_Create
            // 
            this.btn_GADATA_Create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_GADATA_Create.Location = new System.Drawing.Point(393, 268);
            this.btn_GADATA_Create.Name = "btn_GADATA_Create";
            this.btn_GADATA_Create.Size = new System.Drawing.Size(75, 23);
            this.btn_GADATA_Create.TabIndex = 2;
            this.btn_GADATA_Create.Text = "Create";
            this.btn_GADATA_Create.UseVisualStyleBackColor = true;
            this.btn_GADATA_Create.Click += new System.EventHandler(this.btn_GADATA_Create_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "StoredProc:";
            // 
            // cb_procedures
            // 
            this.cb_procedures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_procedures.FormattingEnabled = true;
            this.cb_procedures.Location = new System.Drawing.Point(106, 35);
            this.cb_procedures.Name = "cb_procedures";
            this.cb_procedures.Size = new System.Drawing.Size(370, 24);
            this.cb_procedures.TabIndex = 0;
            this.cb_procedures.SelectedIndexChanged += new System.EventHandler(this.cb_GADATA_procedures_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btn_MX7_create);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(481, 305);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "New_Maximo7";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btn_MX7_create
            // 
            this.btn_MX7_create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_MX7_create.Location = new System.Drawing.Point(400, 276);
            this.btn_MX7_create.Name = "btn_MX7_create";
            this.btn_MX7_create.Size = new System.Drawing.Size(75, 23);
            this.btn_MX7_create.TabIndex = 3;
            this.btn_MX7_create.Text = "Create";
            this.btn_MX7_create.UseVisualStyleBackColor = true;
            this.btn_MX7_create.Click += new System.EventHandler(this.btn_MX7_create_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btn_MX3_create);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(481, 305);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "New_Maximo3";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btn_MX3_create
            // 
            this.btn_MX3_create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_MX3_create.Location = new System.Drawing.Point(400, 276);
            this.btn_MX3_create.Name = "btn_MX3_create";
            this.btn_MX3_create.Size = new System.Drawing.Size(75, 23);
            this.btn_MX3_create.TabIndex = 4;
            this.btn_MX3_create.Text = "Create";
            this.btn_MX3_create.UseVisualStyleBackColor = true;
            this.btn_MX3_create.Click += new System.EventHandler(this.btn_MX3_create_Click);
            // 
            // ConnectionManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 358);
            this.Controls.Add(this.t_connections);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionManger";
            this.ShowIcon = false;
            this.Text = "ConnectionManger";
            this.t_connections.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl t_connections;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Edit;
        private System.Windows.Forms.ListBox lb_connections;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cb_procedures;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_GADATA_Create;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lb_procParms;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btn_MX7_create;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btn_MX3_create;


    }
}