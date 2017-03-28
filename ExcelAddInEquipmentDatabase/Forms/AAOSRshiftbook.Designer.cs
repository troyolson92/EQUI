namespace ExcelAddInEquipmentDatabase.Forms
{
    partial class AAOSRshiftbook
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
            this.Btn_update = new MetroFramework.Controls.MetroButton();
            this.Btn_cancel = new MetroFramework.Controls.MetroButton();
            this.Btn_delete = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.cob_Status = new MetroFramework.Controls.MetroComboBox();
            this.tb_Ci = new MetroFramework.Controls.MetroTextBox();
            this.tb_WO = new MetroFramework.Controls.MetroTextBox();
            this.tb_userdescription = new MetroFramework.Controls.MetroTextBox();
            this.btn_accept = new MetroFramework.Controls.MetroButton();
            this.lbl_message = new MetroFramework.Controls.MetroTextBox();
            this.lbl_locatie = new MetroFramework.Controls.MetroLabel();
            this.lbl_reference = new MetroFramework.Controls.MetroLabel();
            this.lbl_shiftbookID = new MetroFramework.Controls.MetroLabel();
            this.lbl_timestamp = new MetroFramework.Controls.MetroLabel();
            this.lbl_downtime = new MetroFramework.Controls.MetroLabel();
            this.tb_usercomment = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Btn_update
            // 
            this.Btn_update.Location = new System.Drawing.Point(558, 672);
            this.Btn_update.Name = "Btn_update";
            this.Btn_update.Size = new System.Drawing.Size(75, 23);
            this.Btn_update.TabIndex = 0;
            this.Btn_update.Text = "Save";
            this.Btn_update.Click += new System.EventHandler(this.Btn_update_Click);
            // 
            // Btn_cancel
            // 
            this.Btn_cancel.Location = new System.Drawing.Point(457, 672);
            this.Btn_cancel.Name = "Btn_cancel";
            this.Btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_cancel.TabIndex = 1;
            this.Btn_cancel.Text = "Cancel";
            this.Btn_cancel.Click += new System.EventHandler(this.Btn_cancel_Click);
            // 
            // Btn_delete
            // 
            this.Btn_delete.Location = new System.Drawing.Point(512, 191);
            this.Btn_delete.Name = "Btn_delete";
            this.Btn_delete.Size = new System.Drawing.Size(121, 23);
            this.Btn_delete.TabIndex = 3;
            this.Btn_delete.Text = "Delete record";
            this.Btn_delete.Click += new System.EventHandler(this.Btn_delete_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(12, 132);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(90, 20);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Omschrijving";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(13, 191);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(89, 20);
            this.metroLabel2.TabIndex = 5;
            this.metroLabel2.Text = "Commentaar";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(451, 47);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(45, 20);
            this.metroLabel3.TabIndex = 6;
            this.metroLabel3.Text = "Status";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(475, 92);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(21, 20);
            this.metroLabel4.TabIndex = 7;
            this.metroLabel4.Text = "CI";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(463, 126);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(33, 20);
            this.metroLabel5.TabIndex = 8;
            this.metroLabel5.Text = "WO";
            // 
            // cob_Status
            // 
            this.cob_Status.FormattingEnabled = true;
            this.cob_Status.ItemHeight = 24;
            this.cob_Status.Location = new System.Drawing.Point(512, 47);
            this.cob_Status.Name = "cob_Status";
            this.cob_Status.Size = new System.Drawing.Size(121, 30);
            this.cob_Status.TabIndex = 9;
            this.cob_Status.DropDown += new System.EventHandler(this.cob_Status_DropDown);
            // 
            // tb_Ci
            // 
            this.tb_Ci.Location = new System.Drawing.Point(512, 89);
            this.tb_Ci.Name = "tb_Ci";
            this.tb_Ci.Size = new System.Drawing.Size(121, 23);
            this.tb_Ci.TabIndex = 10;
            this.tb_Ci.Text = "0";
            // 
            // tb_WO
            // 
            this.tb_WO.Location = new System.Drawing.Point(512, 123);
            this.tb_WO.Name = "tb_WO";
            this.tb_WO.Size = new System.Drawing.Size(121, 23);
            this.tb_WO.TabIndex = 11;
            this.tb_WO.Text = "0";
            // 
            // tb_userdescription
            // 
            this.tb_userdescription.Location = new System.Drawing.Point(12, 155);
            this.tb_userdescription.Name = "tb_userdescription";
            this.tb_userdescription.Size = new System.Drawing.Size(621, 23);
            this.tb_userdescription.TabIndex = 12;
            this.tb_userdescription.Text = "metroTextBox3";
            // 
            // btn_accept
            // 
            this.btn_accept.Location = new System.Drawing.Point(326, 672);
            this.btn_accept.Name = "btn_accept";
            this.btn_accept.Size = new System.Drawing.Size(75, 23);
            this.btn_accept.TabIndex = 14;
            this.btn_accept.Text = "Accept";
            this.btn_accept.Click += new System.EventHandler(this.btn_accept_Click);
            // 
            // lbl_message
            // 
            this.lbl_message.Location = new System.Drawing.Point(27, 661);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(268, 23);
            this.lbl_message.TabIndex = 15;
            this.lbl_message.Text = "metroTextBox1";
            // 
            // lbl_locatie
            // 
            this.lbl_locatie.AutoSize = true;
            this.lbl_locatie.Location = new System.Drawing.Point(12, 57);
            this.lbl_locatie.Name = "lbl_locatie";
            this.lbl_locatie.Size = new System.Drawing.Size(87, 20);
            this.lbl_locatie.TabIndex = 16;
            this.lbl_locatie.Text = "metroLabel6";
            // 
            // lbl_reference
            // 
            this.lbl_reference.AutoSize = true;
            this.lbl_reference.Location = new System.Drawing.Point(12, 77);
            this.lbl_reference.Name = "lbl_reference";
            this.lbl_reference.Size = new System.Drawing.Size(87, 20);
            this.lbl_reference.TabIndex = 17;
            this.lbl_reference.Text = "metroLabel7";
            // 
            // lbl_shiftbookID
            // 
            this.lbl_shiftbookID.AutoSize = true;
            this.lbl_shiftbookID.Location = new System.Drawing.Point(12, 97);
            this.lbl_shiftbookID.Name = "lbl_shiftbookID";
            this.lbl_shiftbookID.Size = new System.Drawing.Size(87, 20);
            this.lbl_shiftbookID.TabIndex = 18;
            this.lbl_shiftbookID.Text = "metroLabel8";
            // 
            // lbl_timestamp
            // 
            this.lbl_timestamp.AutoSize = true;
            this.lbl_timestamp.Location = new System.Drawing.Point(190, 57);
            this.lbl_timestamp.Name = "lbl_timestamp";
            this.lbl_timestamp.Size = new System.Drawing.Size(87, 20);
            this.lbl_timestamp.TabIndex = 19;
            this.lbl_timestamp.Text = "metroLabel9";
            // 
            // lbl_downtime
            // 
            this.lbl_downtime.AutoSize = true;
            this.lbl_downtime.Location = new System.Drawing.Point(190, 77);
            this.lbl_downtime.Name = "lbl_downtime";
            this.lbl_downtime.Size = new System.Drawing.Size(95, 20);
            this.lbl_downtime.TabIndex = 20;
            this.lbl_downtime.Text = "metroLabel10";
            // 
            // tb_usercomment
            // 
            this.tb_usercomment.Location = new System.Drawing.Point(12, 220);
            this.tb_usercomment.Name = "tb_usercomment";
            this.tb_usercomment.Size = new System.Drawing.Size(621, 435);
            this.tb_usercomment.TabIndex = 21;
            this.tb_usercomment.Text = "";
            // 
            // AAOSRshiftbook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 707);
            this.Controls.Add(this.tb_usercomment);
            this.Controls.Add(this.lbl_downtime);
            this.Controls.Add(this.lbl_timestamp);
            this.Controls.Add(this.lbl_shiftbookID);
            this.Controls.Add(this.lbl_reference);
            this.Controls.Add(this.lbl_locatie);
            this.Controls.Add(this.lbl_message);
            this.Controls.Add(this.btn_accept);
            this.Controls.Add(this.tb_userdescription);
            this.Controls.Add(this.tb_WO);
            this.Controls.Add(this.tb_Ci);
            this.Controls.Add(this.cob_Status);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.Btn_delete);
            this.Controls.Add(this.Btn_cancel);
            this.Controls.Add(this.Btn_update);
            this.Name = "AAOSRshiftbook";
            this.Text = "AAOSRshiftbook";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton Btn_update;
        private MetroFramework.Controls.MetroButton Btn_cancel;
        private MetroFramework.Controls.MetroButton Btn_delete;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox cob_Status;
        private MetroFramework.Controls.MetroTextBox tb_Ci;
        private MetroFramework.Controls.MetroTextBox tb_WO;
        private MetroFramework.Controls.MetroTextBox tb_userdescription;
        private MetroFramework.Controls.MetroButton btn_accept;
        private MetroFramework.Controls.MetroTextBox lbl_message;
        private MetroFramework.Controls.MetroLabel lbl_locatie;
        private MetroFramework.Controls.MetroLabel lbl_reference;
        private MetroFramework.Controls.MetroLabel lbl_shiftbookID;
        private MetroFramework.Controls.MetroLabel lbl_timestamp;
        private MetroFramework.Controls.MetroLabel lbl_downtime;
        private System.Windows.Forms.RichTextBox tb_usercomment;
    }
}