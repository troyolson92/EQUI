using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class MXxWOdetails : MetroFramework.Forms.MetroForm
    {
        MaximoComm lMaximocomm = new MaximoComm();
        BackgroundWorker bw = new BackgroundWorker();

        public MXxWOdetails(string wonum)
        {
            //8624949 wo gemaakt met 9 uur verslag in als test voor browser
            InitializeComponent();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;

            if (wonum != null)
            {
                tb_LONGDESCRIPTIONID.Text = wonum;
                tb_LONGDESCRIPTIONID.Enabled = false;
                btn_get.Visible = false;
                bw.RunWorkerAsync();

            }
            else
            {
                tb_LONGDESCRIPTIONID.Text = "8624949";
                tb_LONGDESCRIPTIONID.Enabled = true;
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

            string cmdFAILUREREMARK = (@"
                select LD.LDTEXT
                from MAXIMO.FAILUREREMARK FM 
                left join MAXIMO.LONGDESCRIPTION LD on LD.LDKEY = FM.FAILUREREMARKID AND LD.LDOWNERTABLE = 'FAILUREREMARK'
                where fm.wonum = '{0}'
            ");
            string cmdLONGDESCRIPTION = (@"
                select LD.LDTEXT
                from MAXIMO.WORKORDER WO 
                left join MAXIMO.LONGDESCRIPTION LD on LD.LDKEY = WO.WORKORDERID AND LD.LDOWNERTABLE = 'WORKORDER'
                where WO.wonum = '{0}'
            ");
            cmdFAILUREREMARK = string.Format(cmdFAILUREREMARK, tb_LONGDESCRIPTIONID.Text);
            cmdLONGDESCRIPTION = string.Format(cmdLONGDESCRIPTION, tb_LONGDESCRIPTIONID.Text);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(
@"<div>            
<table bgcolor=#00FF00>
<tr>
<td style = white-space:PRE>LONGDESCRIPTION                                                                                                                                        
                                                                                                                                                                                </td>
</tr>
</table>
</div>");
            sb.AppendLine(lMaximocomm.GetClobMaximo7(cmdLONGDESCRIPTION));
            sb.AppendLine("<div>---------------------------------------------------------------</div>").AppendLine(
@"<div>            
<table bgcolor=#00FF00>
<tr>
<td style = white-space:PRE>FAILUREREMARK                                                                                                                                        
                                                                                                                                                                                </td>
</tr>
</table>
</div>");
            sb.AppendLine(lMaximocomm.GetClobMaximo7(cmdFAILUREREMARK));

            wb_longdescrption.DocumentText = sb.ToString();
        }

        private void btn_get_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy) { return; }
            bw.RunWorkerAsync();
        }

    }
}
