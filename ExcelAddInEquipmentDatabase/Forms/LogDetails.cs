using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class LogDetails : MetroFramework.Forms.MetroForm
    {
        GadataComm lGadataComm = new GadataComm();
        BackgroundWorker bw = new BackgroundWorker();

        public LogDetails(string Errornum)
        {
            InitializeComponent();
                        bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            this.Text = string.Format("Errornum: {0}", Errornum);
            
            if (Errornum != null)
            {
                tb_errorId.Text = Errornum;
                tb_errorId.Enabled = false;
                btn_get.Visible = false;
                bw.RunWorkerAsync();
            }
            else
            {
                tb_errorId.Text = "xxxx";
                tb_errorId.Enabled = true;
                btn_get.Visible = true;
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            metroProgressSpinner1.Hide();
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            getMaximoDetails();
        }

        private void getMaximoDetails()
        {
            metroProgressSpinner1.Show();

            string cmd1 = (@"

            ");
            string cmd2 = (@"

            ");
            cmd1 = string.Format(cmd1, tb_errorId.Text);
            cmd2 = string.Format(cmd2, tb_errorId.Text);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(
@"<div><table bgcolor=#00FF00>
<tr>
<th>    Errorinfo1                                             </th>
</tr>
</table></div>");
            sb.AppendLine(lGadataComm.RunQueryGadata(cmd1).ToString());
            sb.AppendLine("<div>---------------------------------------------------------------</div>").AppendLine(
@"<div><table bgcolor=#00FF00>
<tr>
<th>    Errorinfo12                                             </th>
</tr>
</table></div>");
            sb.AppendLine(lGadataComm.RunQueryGadata(cmd2).ToString());

            webBrowser1.DocumentText = sb.ToString();
        }

        private void btn_get_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy) { return; }
            bw.RunWorkerAsync();        
        }

    }
}
