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
    public partial class uc_Checkbox : UserControl
    {
        public uc_Checkbox()
        {
            InitializeComponent();
            cb_1.Enabled = rb_enable.Checked;
        }

        private void enable_CheckedChanged(object sender, EventArgs e)
        {
            cb_1.Enabled = rb_enable.Checked;
        }

        public string label
        {
            get { return lbl_1.Text; }
            set { lbl_1.Text = value; }
        }

        public bool input
        {
            get { return cb_1.Checked; }
            set { cb_1.Checked = value; }
        }

        public bool enabeld
        {
            get { return rb_enable.Checked; }
            set { rb_enable.Checked = value; }
        }

    }
}
