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
    public partial class uc_Inputbox : MetroFramework.Controls.MetroUserControl 
    {
        //this indicates that this box is used for varchar or integers. (needed for rebuilding the qurery)
        bool lIntOnly = false;

        public uc_Inputbox()
        {
            InitializeComponent();
            tb_1.Enabled = rb_enable.Checked;
        }

        private void enable_CheckedChanged(object sender, EventArgs e)
        {
            if (tb_1.Enabled)
            {
                rb_enable.Checked = false;
                tb_1.Enabled = false;
            }
            else
            {
                rb_enable.Checked = true;
                tb_1.Enabled = true;
            }
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
            set 
            { 
                rb_enable.Checked = value;
                tb_1.Enabled = value;
            }
        }

        public bool intOnly
        {
            get { return lIntOnly; }
            set { lIntOnly = value; }
        
        }
        public bool hide_active
        {
            set { rb_enable.Visible = !value; }
        }


    }
}
