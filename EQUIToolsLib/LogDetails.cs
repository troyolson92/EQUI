using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EQUICommunictionLib;

namespace EQUIToolsLib
{
    public partial class LogDetails : MetroFramework.Forms.MetroForm
    {
        //debugger
        myDebugger Debugger = new myDebugger();
        GadataComm lGdataComm = new GadataComm();
        DataTable dt;
        BackgroundWorker bw = new BackgroundWorker();
        MaximoComm lMaximoComm = new MaximoComm();
        string slogtype = "";

        public LogDetails(string Location, string LogType, string Errornum, int RefId)
        {
            InitializeComponent();
                        bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            this.Text = string.Format("Errornum: {0}", Errornum);
            slogtype = LogType;

            if (Errornum != null)
            {
                tb_errorId.Text = Errornum;
                tb_errorId.Enabled = false;
                tb_location.Text = Location;
                tb_location.Enabled = false;
                tb_refid.Text = RefId.ToString();
                tb_refid.Enabled = false;
                btn_get.Visible = false;
                bw.RunWorkerAsync();
            }
            else
            {
                tb_errorId.Text = "xxxx";
                tb_errorId.Enabled = true;
                tb_location.Text = "xxxx";
                tb_location.Enabled = true;
                tb_refid.Text = "xxxx";
                tb_refid.Enabled = true;
                btn_get.Visible = true;
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            metroProgressSpinner1.Hide();
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            getErrorDetails();
        }

        private void getErrorDetails()
        {
            metroProgressSpinner1.Show();

            //query all instances of the error 
            string qry = string.Format(
            @"EXEC [EqUi].[GetErrorInfoData] @Location  = '{0}' ,@ERRORNUM = {1} ,@Refid = {2} ,@logtype ='{3}'"
            , tb_location.Text, tb_errorId.Text, tb_refid.Text, slogtype);

            //fill dataset
            dt = lGdataComm.RunQueryGadata(qry);
            //check if the result was valid 

            StringBuilder sb = new StringBuilder();
            string newline = "<p></p>";
            if (dt.Rows.Count != 0)
            {
                DataRow myRow = dt.Rows[0];
                foreach (DataColumn dc in myRow.Table.Columns)
                {
                    if (myRow[dc.ColumnName] == DBNull.Value)
                    {
                        myRow[dc.ColumnName] = "null (no data)";
                    }
                    sb.AppendLine(lMaximoComm.StringToHTML_Table( dc.ColumnName,myRow.Field<string>(dc.ColumnName).ToString() )).AppendLine(newline);
                }
            }
            else
            {
                sb.AppendLine("No valid result from query").AppendLine(newline);
            }
            webBrowser1.DocumentText = sb.ToString();
        }

        private void btn_get_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy) { return; }
            bw.RunWorkerAsync();        
        }


    }
}
