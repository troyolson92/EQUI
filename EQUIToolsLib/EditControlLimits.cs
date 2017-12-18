using System;
using EQUICommunictionLib;
using System.Data;

namespace EQUIToolsLib
{
    public partial class EditControlLimits : MetroFramework.Forms.MetroForm
    {
        //debugger
        myDebugger Debugger = new myDebugger();

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
            getControlLimits(tb_System.Text, tb_location.Text);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void btn_set_Click(object sender, EventArgs e)
        {
            SetControlLimits(tb_System.Text, tb_location.Text);
        }

        //helper functions
        string qrySBCU = @"EXEC GADATA.[C3G].[SbcuSpcSetLimit] @Weldgunname = '{0}%' , @show = {1}, @set = {2}, @UCL = {3}, @LCL = {4}";
        string qryCilinder = @"EXEC GADATA.[C3G].[CilinderSpcSetLimit] @Weldgunname = '{0}%' , @show = {1}, @set = {2}, @UCL = {3}, @LCL = {4}";

        private void getControlLimits(string system, string location)
        {
            string qry;
            if (system == "Sbcu")
            {
                qry = qrySBCU;
            }
            else if (system == "Cilinder")
            {
                qry = qryCilinder;
            }
            else
            {
                Debugger.Message("system not valid");
                return;
            }
            EQUICommunictionLib.GadataComm gadataComm = new GadataComm();
                string cmd = string.Format(qry, tb_location.Text, 1, 0, 0, 0);

                DataTable dt = gadataComm.RunQueryGadata(cmd);
                if (dt.Rows.Count == 0)
                {
                    Debugger.Message("server did not reply");
                    return;
                }
                tb_LCL.Text = dt.Rows[0].Field<double>("LCL").ToString();
                tb_UCL.Text = dt.Rows[0].Field<double>("UCL").ToString();
        }

        private void SetControlLimits(string system, string location)
        {
            string qry;
            if (system == "Sbcu")
            {
                qry = qrySBCU;
            }
            else if (system == "Cilinder")
            {
                qry = qryCilinder;
            }
            else
            {
                Debugger.Message("system not valid");
                return;
            }
            //try parse
            double lcl;
                if (!Double.TryParse(tb_LCL.Text, out lcl))
                {
                    Debugger.Message(string.Format("{0} lcl is outside the range of a Double.", lcl));
                    return;
                }

                double ucl;
                if (!Double.TryParse(tb_UCL.Text, out ucl))
                {
                    Debugger.Message(string.Format("{0} ucl is outside the range of a Double.", ucl));
                    return;
                }

                EQUICommunictionLib.GadataComm gadataComm = new GadataComm();
                string cmd = string.Format(qry, tb_location.Text, 0, 1, ucl.ToString().Replace(',', '.'), lcl.ToString().Replace(',', '.'));
                DataTable dt = gadataComm.RunQueryGadata(cmd);
                if (dt.Rows.Count == 0)
                {
                    Debugger.Message("server did not reply");
                    return;
                }
                tb_LCL.Text = dt.Rows[0].Field<double>("LCL").ToString();
                tb_UCL.Text = dt.Rows[0].Field<double>("UCL").ToString();
            Debugger.Message("OK");
        }
    }
}
