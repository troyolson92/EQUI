using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class uc_Inputbox : UserControl
    {
        public uc_Inputbox()
        {
            InitializeComponent();
            tb_1.Enabled = rb_enable.Checked;
        }

        private void enable_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_enable.Checked) rb_enable.Checked = false;
            if (!rb_enable.Checked) rb_enable.Checked = true;
            tb_1.Enabled = rb_enable.Checked;
        }

        public string label
        {
            get { return lbl_1.Text; }
            set { lbl_1.Text = value; }
        }

        public string input
        {
            get { return tb_1.Text; }
            set { tb_1.Text = value; }
        }

        public bool active
        {
            get { return rb_enable.Checked; }
            set { rb_enable.Checked = value; }
        }



    }
}
