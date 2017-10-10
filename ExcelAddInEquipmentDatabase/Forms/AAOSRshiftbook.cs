﻿using System;
using System.Data;
using System.Windows.Forms;
using EQUICommunictionLib;
using System.Data.SqlClient;

/*TEMP MODULE UNTIL MAX COMM IS UP!*/

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class AAOSRshiftbook : MetroFramework.Forms.MetroForm
    {
        GadataComm lGadataComm = new GadataComm();
        DataTable dt = new DataTable();
        myDebugger Debugger = new myDebugger();

        public AAOSRshiftbook()
        {
            InitializeComponent();
        }

        private void init ()
        {

            if (dt.Rows.Count == 0)
            {
                Debugger.Message("OEPS something whent wrong nu Query result");
                this.Hide();
                this.Dispose();
                return;
            }
            // init know fields
            lbl_locatie.Text = "Robot: " + dt.Rows[0].Field<String>("Robot") + "  " + dt.Rows[0].Field<String>("controller_type") + " " + dt.Rows[0].Field<int>("Controller_id").ToString();
            lbl_reference.Text = "Breakdownref: " + dt.Rows[0].Field<int>("Breakdownreference").ToString();
            lbl_timestamp.Text = "Timestamp: " + dt.Rows[0].Field<DateTime>("Timestamp").ToString();
            lbl_downtime.Text = "Downtime: " + dt.Rows[0].Field<int>("Downtime").ToString() + " min";
            lbl_shiftbookID.Text = "ShiftbookID: " + dt.Rows[0].Field<Int64>("ShiftbookID").ToString();
            //
            cob_Status.Items.Insert(0, dt.Rows[0].Field<String>("STATUS"));
            cob_Status.SelectedIndex = 0;
            tb_Ci.Text = dt.Rows[0].Field<int>("CI").ToString();
            tb_WO.Text = dt.Rows[0].Field<int>("WO").ToString();
            tb_userdescription.Text = dt.Rows[0].Field<String>("Omschrijving");
            tb_usercomment.Text = dt.Rows[0].Field<String>("Commentaar") + Environment.NewLine + Environment.UserName + "  | " + DateTime.Now.ToString() + Environment.NewLine +  "***************************" + Environment.NewLine;

            //if redirected to other shiftbook item
            if (dt.Rows[0].Field<Boolean>("Redirect"))
            {
                lbl_message.Text = "**Open item found**";
            }
            else
            {
                lbl_message.Text = "**New item created**";
            }

            //if state = Wassign
            if (dt.Rows[0].Field<String>("STATUS") == "WASSIGN")
            {
                btn_accept.Enabled = true;
            }
            else
            {
                btn_accept.Enabled = false;
            }

        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? This will delete this shiftbook item!!!", "Confirmation", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                string shiftbookID = dt.Rows[0].Field<Int64>("ShiftbookID").ToString();
                RunQuery("0", "0", shiftbookID, "0", "0", "0", "0", "0", 5);
            }
            this.Hide();
            this.Dispose();
        }

        private void btn_accept_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Accept this task? OPGEPAST WERK ","Confirmation", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                string shiftbookID = dt.Rows[0].Field<Int64>("ShiftbookID").ToString();
                RunQuery("0", "0", shiftbookID, "0", "0", "0", "0", "0", 6);
            }
            this.Hide();
            this.Dispose();
        }

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Dispose();
        }

        private void Btn_update_Click(object sender, EventArgs e)
        {
            RunQuery(
                  dt.Rows[0].Field<String>("Robot")
                , dt.Rows[0].Field<int>("Breakdownreference").ToString()
                , dt.Rows[0].Field<Int64>("ShiftbookID").ToString()
                , tb_WO.Text
                , tb_Ci.Text
                , cob_Status.Text
                , tb_userdescription.Text
                , tb_usercomment.Text
                , 1);

            this.Hide();
            this.Dispose();
        }

        private void RunQuery(string Robot,string BreakdownID,string shiftbookID,string wo,string ci,string state,string userDescription,string userComment,int Runmode)
        {
            string proc = "GADATA.dbo.Qinfo_Shiftbook";

            using (SqlCommand Cmd = new SqlCommand())
            {
                Cmd.Parameters.AddWithValue("@Robot",Robot);
                Cmd.Parameters.AddWithValue("@BreakdownID", BreakdownID);
                Cmd.Parameters.AddWithValue("@ShiftbookID", shiftbookID);
                Cmd.Parameters.AddWithValue("@user", Environment.UserName);
                Cmd.Parameters.AddWithValue("@WO", wo);
                Cmd.Parameters.AddWithValue("@CI", ci);
                Cmd.Parameters.AddWithValue("@STATE", state);
                Cmd.Parameters.AddWithValue("@userDescription", userDescription); //willy proofing 
                Cmd.Parameters.AddWithValue("@userComment", tb_usercomment.ToString()); //willy proofing 
                Cmd.Parameters.AddWithValue("@Runmode", Runmode);
                dt = lGadataComm.RunParametercommand(proc, Cmd);
            }

        }
 
        public void AddIndependantShiftbook(string sRobot,string sErrorType,string sShortText,string sIDX)
        {
         //Ask to confirm an independant location
         //cursor needs to be on data sheet and on valid line
            string sLocation = Microsoft.VisualBasic.Interaction.InputBox("Use this location?", "Shiftbook", "99020R01", -1, -1);
            if (sLocation.Length != 0)
            {
                RunQuery(sLocation, "0", "0", "0", "0", "0", sErrorType + ": " + sShortText.Trim() + " (id" + sIDX + ") ", "0", 3);
                init();
                this.Show();
            }
        }

        public void  EditShiftbook(string sRobot,string sBreakdownID ,string sShiftbookID )
        {
         //run a query to populate shiftbook form. (if needed new record is added)
         //cursor needs to be on data sheet and on valid line
           RunQuery(sRobot, sBreakdownID, sShiftbookID, "0", "0", "0", "0", "0", 2);
           init();
           this.Show();
        }

        private void cob_Status_DropDown(object sender, EventArgs e)
        {
            cob_Status.Items.Clear();
            cob_Status.Items.Insert(0, "TECHCOMP");
            cob_Status.Items.Insert(1, "IPRG");
            cob_Status.Items.Insert(2, "EXEC");
            cob_Status.Items.Insert(3, "COMP");
            cob_Status.Items.Insert(4, "WASSIGN");
        }

    }
}
