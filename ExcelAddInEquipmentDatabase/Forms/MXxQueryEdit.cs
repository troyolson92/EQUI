﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class MXxQueryEdit : Form
    {
        string lTargetSystem;

        public string TargetSystem { set { lTargetSystem = value; } }

        public MXxQueryEdit()
        {
            InitializeComponent();
        }



        public string QueryName
        {
            get { return tb_QueryName.Text; }
            set { tb_QueryName.Text = value; }
        }
        public string QueryDiscription
        {
            get { return tb_QueryDiscription.Text; }
            set { tb_QueryDiscription.Text = value; }
        }
        public string Query
        {
            get { return rtb_Query.Text; }
            set { rtb_Query.Text = value; }
        }

        private void MXxQueryEdit_Load(object sender, EventArgs e)
        {
            var _point = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);
            Top = _point.Y;
            Left = _point.X - this.Size.Width;
            this.BringToFront();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                MaximoComm lMaxComm = new MaximoComm();
                lMaxComm.oracle_delete_Query_GADATA(lTargetSystem, QueryName);
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
              
            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            //save should work for new and update 

            try
            {
                MaximoComm lMaxComm = new MaximoComm();
                lMaxComm.oracle_send_new_Query_to_GADATA(lTargetSystem, QueryName, QueryDiscription, Query);
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
