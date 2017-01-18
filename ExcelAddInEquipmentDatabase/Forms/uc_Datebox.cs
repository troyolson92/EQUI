﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class uc_Datebox : UserControl
    {
        public uc_Datebox()
        {
            InitializeComponent();
            dt_1.Enabled = rb_enable.Checked;
        }

        private void enable_CheckedChanged(object sender, EventArgs e)
        {
            if (dt_1.Enabled)
            {
                rb_enable.Checked = false;
                dt_1.Enabled = false;
            }
            else
            {
                rb_enable.Checked = true;
                dt_1.Enabled = true;
            }
        }

        public string label
        {
            get { return lbl_1.Text; }
            set { lbl_1.Text = value; }
        }

        public System.DateTime input
        {
            get { return dt_1.Value; }
            set { dt_1.Value = value; }
        }

        public bool active
        {
            get { return rb_enable.Checked; }
            set 
            { 
                rb_enable.Checked = value;
                dt_1.Enabled = value;
            }
        }
        public bool hide_active
        {
            set { rb_enable.Visible = !value; }
        }
    }
}
