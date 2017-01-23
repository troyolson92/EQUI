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
            InitializeComponent();
            tb_LONGDESCRIPTIONID.Text = "7809810";
        }

        private void btn_get_Click(object sender, EventArgs e)
        {
            string cmd = (@"
            select 
            LONGDESCRIPTION.LDTEXT
            from 
            MAXIMO.LONGDESCRIPTION LONGDESCRIPTION
            where LONGDESCRIPTION.LONGDESCRIPTIONID = '{0}'
            ");
            cmd = string.Format(cmd, tb_LONGDESCRIPTIONID.Text);
            wb_longdescrption.DocumentText = lMaximocomm.GetClobMaximo7(cmd);
        }

    }
}
