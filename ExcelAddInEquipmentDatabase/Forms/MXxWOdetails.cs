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
        public MXxWOdetails()
        {
            //8624949 wo gemaakt met 9 uur verslag in als test voor browser
            InitializeComponent();
            tb_LONGDESCRIPTIONID.Text = "8624949";
        }

        private void btn_get_Click(object sender, EventArgs e)
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

            string sEb = "*********************************************************";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(sEb).AppendLine("LONGDESCRIPTION").AppendLine(sEb).AppendLine("").AppendLine("");
            sb.AppendLine(lMaximocomm.GetClobMaximo7(cmdLONGDESCRIPTION));
            sb.AppendLine("").Append(sEb).AppendLine("").AppendLine("");
            sb.AppendLine(sEb).AppendLine("FAILUREREMARK").AppendLine(sEb).AppendLine("").AppendLine("");
            sb.AppendLine(lMaximocomm.GetClobMaximo7(cmdFAILUREREMARK));
            sb.AppendLine("").Append(sEb).AppendLine("").AppendLine("");

            wb_longdescrption.DocumentText = sb.ToString();
        }

    }
}
