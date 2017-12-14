using System;
using EQUICommunictionLib;
using System.Data;

namespace EQUIToolsLib
{
    public partial class EditControlLimits : MetroFramework.Forms.MetroForm
    {

        //standalone
        public EditControlLimits()
        {
            InitializeComponent();
            tb_LCL.Text = "10";
            tb_UCL.Text = "10";
            tb_location.Text = "Enter a location";
            tb_System.Text = "Enter a system";
        }

        //preinit
        public EditControlLimits(string system, string target)
        {
            InitializeComponent();
            tb_location.Enabled = false;
            tb_System.Enabled = false;
            tb_location.Text = target;
            tb_System.Text = system;
            getControlLimits(system, target);



        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

            Close();
            Dispose();
        }

        private void btn_set_Click(object sender, EventArgs e)
        {

        }

        //helper functions
        string qrySBCU = @"EXEC GADATA.[C3G].[SbcuSpcSetLimit] @Weldgunname = '{0}%' , @show = {1}, @set = {2}, @UCL = {3}, @LCL = {4}";

        private void getControlLimits(string system, string location)
        {
            EQUICommunictionLib.GadataComm gadataComm = new GadataComm();
       
            DataTable dt = gadataComm.RunQueryGadata(string.Format(qrySBCU, tb_location.Text,0,0,0,0));
            if (dt.Rows.Count == 0) return;

            tb_LCL.Text = dt.Rows[0].Field<string>("LCL").ToString();
            tb_UCL.Text = dt.Rows[0].Field<string>("UCL").ToString();


        }
    }
}
