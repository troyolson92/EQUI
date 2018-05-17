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
            this.tabcontainer = new System.Windows.Forms.TabControl();
            this.tp_connections = new System.Windows.Forms.TabPage();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.lb_connections = new System.Windows.Forms.ListBox();
            this.tp_GADATA = new System.Windows.Forms.TabPage();
            this.lv_GADATA_procParms = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_GADATA_Create = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_GADTA_procedures = new System.Windows.Forms.ComboBox();
            this.tp_MX7 = new System.Windows.Forms.TabPage();
            this.lbl_MX7_procDiscription = new System.Windows.Forms.Label();
            this.btn_MX7_new = new System.Windows.Forms.Button();
            this.btn_MX7_edit = new System.Windows.Forms.Button();
            this.lv_MX7_procParms = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_MX7_QueryNames = new System.Windows.Forms.ComboBox();
            this.btn_MX7_create = new System.Windows.Forms.Button();
            this.tp_STO = new System.Windows.Forms.TabPage();
            this.lbl_STO_procDiscription = new System.Windows.Forms.Label();
            this.btn_STO_new = new System.Windows.Forms.Button();
            this.btn_STO_edit = new System.Windows.Forms.Button();
            this.lv_STO_procParms = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_STO_QueryNames = new System.Windows.Forms.ComboBox();
            this.btn_STO_create = new System.Windows.Forms.Button();
            this.tabcontainer.SuspendLayout();
            this.tp_connections.SuspendLayout();
            this.tp_GADATA.SuspendLayout();
            this.tp_MX7.SuspendLayout();
            this.tp_STO.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabcontainer
            // 
            this.tabcontainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabcontainer.Controls.Add(this.tp_connections);
            this.tabcontainer.Controls.Add(this.tp_GADATA);
            this.tabcontainer.Controls.Add(this.tp_MX7);
            this.tabcontainer.Controls.Add(this.tp_STO);
            this.tabcontainer.Location = new System.Drawing.Point(9, 10);
            this.tabcontainer.Margin = new System.Windows.Forms.Padding(2);
            this.tabcontainer.Name = "tabcontainer";
            this.tabcontainer.SelectedIndex = 0;
            this.tabcontainer.Size = new System.Drawing.Size(367, 283);
            this.tabcontainer.TabIndex = 0;
            // 
            // tp_connections
            // 
            this.tp_connections.Controls.Add(this.btn_Delete);
            this.tp_connections.Controls.Add(this.lb_connections);
            this.tp_connections.Location = new System.Drawing.Point(4, 22);
            this.tp_connections.Margin = new System.Windows.Forms.Padding(2);
            this.tp_connections.Name = "tp_connections";
            this.tp_connections.Padding = new System.Windows.Forms.Padding(2);
            this.tp_connections.Size = new System.Drawing.Size(359, 257);
            this.tp_connections.TabIndex = 0;
            this.tp_connections.Text = "Connections";
            this.tp_connections.UseVisualStyleBackColor = true;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.Location = new System.Drawing.Point(303, 14);
            this.btn_Delete.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(56, 19);
            this.btn_Delete.TabIndex = 12;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // lb_connections
            // 
            this.lb_connections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_connections.FormattingEnabled = true;
            this.lb_connections.Location = new System.Drawing.Point(4, 14);
            this.lb_connections.Margin = new System.Windows.Forms.Padding(2);
            this.lb_connections.Name = "lb_connections";
            this.lb_connections.Size = new System.Drawing.Size(294, 212);
            this.lb_connections.TabIndex = 10;
            // 
            // tp_GADATA
            // 
            this.tp_GADATA.Controls.Add(this.lv_GADATA_procParms);
            this.tp_GADATA.Controls.Add(this.label2);
            this.tp_GADATA.Controls.Add(this.btn_GADATA_Create);
            this.tp_GADATA.Controls.Add(this.label1);
            this.tp_GADATA.Controls.Add(this.cb_GADTA_procedures);
            this.tp_GADATA.Location = new System.Drawing.Point(4, 22);
            this.tp_GADATA.Margin = new System.Windows.Forms.Padding(2);
            this.tp_GADATA.Name = "tp_GADATA";
            this.tp_GADATA.Padding = new System.Windows.Forms.Padding(2);
            this.tp_GADATA.Size = new System.Drawing.Size(359, 257);
            this.tp_GADATA.TabIndex = 1;
            this.tp_GADATA.Text = "New Gadata";
            this.tp_GADATA.UseVisualStyleBackColor = true;
            this.tp_GADATA.Enter += new System.EventHandler(this.Tp_GADATA_Enter);
            // 
            // lv_GADATA_procParms
            // 
            this.lv_GADATA_procParms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_GADATA_procParms.Location = new System.Drawing.Point(5, 54);
            this.lv_GADATA_procParms.Name = "lv_GADATA_procParms";
            this.lv_GADATA_procParms.Size = new System.Drawing.Size(346, 171);
            this.lv_GADATA_procParms.TabIndex = 5;
            this.lv_GADATA_procParms.UseCompatibleStateImageBehavior = false;
            this.lv_GADATA_procParms.View = System.Windows.Forms.View.Details;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Available stored procedures in gadata.volvo schema";
            // 
            // btn_GADATA_Create
            // 
            this.btn_GADATA_Create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_GADATA_Create.Location = new System.Drawing.Point(295, 230);
            this.btn_GADATA_Create.Margin = new System.Windows.Forms.Padding(2);
            this.btn_GADATA_Create.Name = "btn_GADATA_Create";
            this.btn_GADATA_Create.Size = new System.Drawing.Size(56, 19);
            this.btn_GADATA_Create.TabIndex = 2;
            this.btn_GADATA_Create.Text = "Create";
            this.btn_GADATA_Create.UseVisualStyleBackColor = true;
            this.btn_GADATA_Create.Click += new System.EventHandler(this.Btn_GADATA_Create_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "StoredProc:";
            // 
            // cb_GADTA_procedures
            // 
            this.cb_GADTA_procedures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_GADTA_procedures.FormattingEnabled = true;
            this.cb_GADTA_procedures.Location = new System.Drawing.Point(80, 28);
            this.cb_GADTA_procedures.Margin = new System.Windows.Forms.Padding(2);
            this.cb_GADTA_procedures.Name = "cb_GADTA_procedures";
            this.cb_GADTA_procedures.Size = new System.Drawing.Size(278, 21);
            this.cb_GADTA_procedures.TabIndex = 0;
            this.cb_GADTA_procedures.SelectedIndexChanged += new System.EventHandler(this.Cb_GADATA_procedures_SelectedIndexChanged);
            // 
            // tp_MX7
            // 
            this.tp_MX7.Controls.Add(this.lbl_MX7_procDiscription);
            this.tp_MX7.Controls.Add(this.btn_MX7_new);
            this.tp_MX7.Controls.Add(this.btn_MX7_edit);
            this.tp_MX7.Controls.Add(this.lv_MX7_procParms);
            this.tp_MX7.Controls.Add(this.label3);
            this.tp_MX7.Controls.Add(this.label4);
            this.tp_MX7.Controls.Add(this.cb_MX7_QueryNames);
            this.tp_MX7.Controls.Add(this.btn_MX7_create);
            this.tp_MX7.Location = new System.Drawing.Point(4, 22);
            this.tp_MX7.Margin = new System.Windows.Forms.Padding(2);
            this.tp_MX7.Name = "tp_MX7";
            this.tp_MX7.Padding = new System.Windows.Forms.Padding(2);
            this.tp_MX7.Size = new System.Drawing.Size(359, 257);
            this.tp_MX7.TabIndex = 2;
            this.tp_MX7.Text = "New_Maximo7";
            this.tp_MX7.UseVisualStyleBackColor = true;
            this.tp_MX7.Enter += new System.EventHandler(this.Tp_MX7_Enter);
            // 
            // lbl_MX7_procDiscription
            // 
            this.lbl_MX7_procDiscription.AutoSize = true;
            this.lbl_MX7_procDiscription.Location = new System.Drawing.Point(4, 62);
            this.lbl_MX7_procDiscription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_MX7_procDiscription.Name = "lbl_MX7_procDiscription";
            this.lbl_MX7_procDiscription.Size = new System.Drawing.Size(35, 13);
            this.lbl_MX7_procDiscription.TabIndex = 11;
            this.lbl_MX7_procDiscription.Text = "label7";
            // 
            // btn_MX7_new
            // 
            this.btn_MX7_new.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_MX7_new.Location = new System.Drawing.Point(69, 234);
            this.btn_MX7_new.Margin = new System.Windows.Forms.Padding(2);
            this.btn_MX7_new.Name = "btn_MX7_new";
            this.btn_MX7_new.Size = new System.Drawing.Size(107, 19);
            this.btn_MX7_new.TabIndex = 10;
            this.btn_MX7_new.Text = "Upload new Query";
            this.btn_MX7_new.UseVisualStyleBackColor = true;
            this.btn_MX7_new.Click += new System.EventHandler(this.Btn_MX7_new_Click);
            // 
            // btn_MX7_edit
            // 
            this.btn_MX7_edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_MX7_edit.Location = new System.Drawing.Point(8, 234);
            this.btn_MX7_edit.Margin = new System.Windows.Forms.Padding(2);
            this.btn_MX7_edit.Name = "btn_MX7_edit";
            this.btn_MX7_edit.Size = new System.Drawing.Size(56, 19);
            this.btn_MX7_edit.TabIndex = 9;
            this.btn_MX7_edit.Text = "Edit";
            this.btn_MX7_edit.UseVisualStyleBackColor = true;
            this.btn_MX7_edit.Click += new System.EventHandler(this.Btn_MX7_edit_Click);
            // 
            // lv_MX7_procParms
            // 
            this.lv_MX7_procParms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_MX7_procParms.Location = new System.Drawing.Point(5, 79);
            this.lv_MX7_procParms.Name = "lv_MX7_procParms";
            this.lv_MX7_procParms.Size = new System.Drawing.Size(346, 140);
            this.lv_MX7_procParms.TabIndex = 8;
            this.lv_MX7_procParms.UseCompatibleStateImageBehavior = false;
            this.lv_MX7_procParms.View = System.Windows.Forms.View.Details;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Available Querys in gada.volvo.Query table";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Query:";
            // 
            // cb_MX7_QueryNames
            // 
            this.cb_MX7_QueryNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_MX7_QueryNames.FormattingEnabled = true;
            this.cb_MX7_QueryNames.Location = new System.Drawing.Point(69, 31);
            this.cb_MX7_QueryNames.Margin = new System.Windows.Forms.Padding(2);
            this.cb_MX7_QueryNames.Name = "cb_MX7_QueryNames";
            this.cb_MX7_QueryNames.Size = new System.Drawing.Size(278, 21);
            this.cb_MX7_QueryNames.TabIndex = 5;
            this.cb_MX7_QueryNames.DropDown += new System.EventHandler(this.Tp_MX7_Enter);
            this.cb_MX7_QueryNames.SelectedIndexChanged += new System.EventHandler(this.Cb_MX7_QueryNames_SelectedIndexChanged);
            // 
            // btn_MX7_create
            // 
            this.btn_MX7_create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_MX7_create.Location = new System.Drawing.Point(300, 236);
            this.btn_MX7_create.Margin = new System.Windows.Forms.Padding(2);
            this.btn_MX7_create.Name = "btn_MX7_create";
            this.btn_MX7_create.Size = new System.Drawing.Size(56, 19);
            this.btn_MX7_create.TabIndex = 3;
            this.btn_MX7_create.Text = "Create";
            this.btn_MX7_create.UseVisualStyleBackColor = true;
            this.btn_MX7_create.Click += new System.EventHandler(this.Btn_MX7_create_Click);
            // 
            // tp_STO
            // 
            this.tp_STO.Controls.Add(this.lbl_STO_procDiscription);
            this.tp_STO.Controls.Add(this.btn_STO_new);
            this.tp_STO.Controls.Add(this.btn_STO_edit);
            this.tp_STO.Controls.Add(this.lv_STO_procParms);
            this.tp_STO.Controls.Add(this.label5);
            this.tp_STO.Controls.Add(this.label6);
            this.tp_STO.Controls.Add(this.cb_STO_QueryNames);
            this.tp_STO.Controls.Add(this.btn_STO_create);
            this.tp_STO.Location = new System.Drawing.Point(4, 22);
            this.tp_STO.Margin = new System.Windows.Forms.Padding(2);
            this.tp_STO.Name = "tp_STO";
            this.tp_STO.Padding = new System.Windows.Forms.Padding(2);
            this.tp_STO.Size = new System.Drawing.Size(359, 257);
            this.tp_STO.TabIndex = 3;
            this.tp_STO.Text = "New_STO";
            this.tp_STO.UseVisualStyleBackColor = true;
            this.tp_STO.Enter += new System.EventHandler(this.Tp_STO_Enter);
            // 
            // lbl_STO_procDiscription
            // 
            this.lbl_STO_procDiscription.AutoSize = true;
            this.lbl_STO_procDiscription.Location = new System.Drawing.Point(13, 65);
            this.lbl_STO_procDiscription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_STO_procDiscription.Name = "lbl_STO_procDiscription";
            this.lbl_STO_procDiscription.Size = new System.Drawing.Size(35, 13);
            this.lbl_STO_procDiscription.TabIndex = 15;
            this.lbl_STO_procDiscription.Text = "label7";
            // 
            // btn_STO_new
            // 
            this.btn_STO_new.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_STO_new.Location = new System.Drawing.Point(69, 237);
            this.btn_STO_new.Margin = new System.Windows.Forms.Padding(2);
            this.btn_STO_new.Name = "btn_STO_new";
            this.btn_STO_new.Size = new System.Drawing.Size(107, 19);
            this.btn_STO_new.TabIndex = 14;
            this.btn_STO_new.Text = "Upload new Query";
            this.btn_STO_new.UseVisualStyleBackColor = true;
            this.btn_STO_new.Click += new System.EventHandler(this.Btn_STO_new_Click);
            // 
            // btn_STO_edit
            // 
            this.btn_STO_edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_STO_edit.Location = new System.Drawing.Point(8, 237);
            this.btn_STO_edit.Margin = new System.Windows.Forms.Padding(2);
            this.btn_STO_edit.Name = "btn_STO_edit";
            this.btn_STO_edit.Size = new System.Drawing.Size(56, 19);
            this.btn_STO_edit.TabIndex = 13;
            this.btn_STO_edit.Text = "Edit";
            this.btn_STO_edit.UseVisualStyleBackColor = true;
            this.btn_STO_edit.Click += new System.EventHandler(this.Btn_STO_edit_Click);
            // 
            // lv_STO_procParms
            // 
            this.lv_STO_procParms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_STO_procParms.Location = new System.Drawing.Point(8, 82);
            this.lv_STO_procParms.Name = "lv_STO_procParms";
            this.lv_STO_procParms.Size = new System.Drawing.Size(346, 136);
            this.lv_STO_procParms.TabIndex = 12;
            this.lv_STO_procParms.UseCompatibleStateImageBehavior = false;
            this.lv_STO_procParms.View = System.Windows.Forms.View.Details;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Available Querys in gada.volvo.Query table";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 37);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Query:";
            // 
            // cb_STO_QueryNames
            // 
            this.cb_STO_QueryNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_STO_QueryNames.FormattingEnabled = true;
            this.cb_STO_QueryNames.Location = new System.Drawing.Point(78, 34);
            this.cb_STO_QueryNames.Margin = new System.Windows.Forms.Padding(2);
            this.cb_STO_QueryNames.Name = "cb_STO_QueryNames";
            this.cb_STO_QueryNames.Size = new System.Drawing.Size(278, 21);
            this.cb_STO_QueryNames.TabIndex = 9;
            this.cb_STO_QueryNames.DropDown += new System.EventHandler(this.Tp_STO_Enter);
            this.cb_STO_QueryNames.SelectedIndexChanged += new System.EventHandler(this.Cb_STO_QueryNames_SelectedIndexChanged);
            // 
            // btn_STO_create
            // 
            this.btn_STO_create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_STO_create.Location = new System.Drawing.Point(300, 237);
            this.btn_STO_create.Margin = new System.Windows.Forms.Padding(2);
            this.btn_STO_create.Name = "btn_STO_create";
            this.btn_STO_create.Size = new System.Drawing.Size(56, 19);
            this.btn_STO_create.TabIndex = 4;
            this.btn_STO_create.Text = "Create";
            this.btn_STO_create.UseVisualStyleBackColor = true;
            this.btn_STO_create.Click += new System.EventHandler(this.Btn_STO_create_Click);
            // 
            // ConnectionManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 303);
            this.Controls.Add(this.tabcontainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionManger";
            this.ShowIcon = false;
            this.Text = "ConnectionManger";
            this.Shown += new System.EventHandler(this.ConnectionManger_Shown);
            this.tabcontainer.ResumeLayout(false);
            this.tp_connections.ResumeLayout(false);
            this.tp_GADATA.ResumeLayout(false);
            this.tp_GADATA.PerformLayout();
            this.tp_MX7.ResumeLayout(false);
            this.tp_MX7.PerformLayout();
            this.tp_STO.ResumeLayout(false);
            this.tp_STO.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabcontainer;
        private System.Windows.Forms.TabPage tp_connections;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.ListBox lb_connections;
        private System.Windows.Forms.TabPage tp_GADATA;
        private System.Windows.Forms.ComboBox cb_GADTA_procedures;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_GADATA_Create;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tp_MX7;
        private System.Windows.Forms.Button btn_MX7_create;
        private System.Windows.Forms.TabPage tp_STO;
        private System.Windows.Forms.Button btn_STO_create;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_MX7_QueryNames;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_STO_QueryNames;
        private System.Windows.Forms.ListView lv_GADATA_procParms;
        private System.Windows.Forms.ListView lv_MX7_procParms;
        private System.Windows.Forms.ListView lv_STO_procParms;
        private System.Windows.Forms.Button btn_MX7_new;
        private System.Windows.Forms.Button btn_MX7_edit;
        private System.Windows.Forms.Button btn_STO_new;
        private System.Windows.Forms.Button btn_STO_edit;
        private System.Windows.Forms.Label lbl_MX7_procDiscription;
        private System.Windows.Forms.Label lbl_STO_procDiscription;


    }
}