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
    public partial class MXxWOdetails : Form
    {
        MaximoComm lMaximocomm = new MaximoComm();
        public MXxWOdetails(string wonum)
        {
            //8624949 wo gemaakt met 9 uur verslag in als test voor browser
            InitializeComponent();
            if (wonum != null)
            {
                tb_LONGDESCRIPTIONID.Text = wonum;
                tb_LONGDESCRIPTIONID.Enabled = false;
                btn_get.Visible = false;
                getMaximoDetails();
            }
            else
            {
                tb_LONGDESCRIPTIONID.Text = "8624949";
                tb_LONGDESCRIPTIONID.Enabled = true;
                btn_get.Visible = true;
            }

        }
        private void getMaximoDetails()
        {
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

            string sEb = "<div>***************************</div>";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(sEb).AppendLine("<div>LONGDESCRIPTION</div>").AppendLine(sEb);
            sb.AppendLine(lMaximocomm.GetClobMaximo7(cmdLONGDESCRIPTION));

            sb.AppendLine(sEb).AppendLine("<div>FAILUREREMARK</div>").AppendLine(sEb);
            sb.AppendLine(lMaximocomm.GetClobMaximo7(cmdFAILUREREMARK));

            wb_longdescrption.DocumentText = sb.ToString();
        }

        private void btn_get_Click(object sender, EventArgs e)
        {
            getMaximoDetails();
        }

    }
}
