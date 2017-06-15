using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using EQUICommunictionLib;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class MXxQueryEdit : Form
    {
        //debugger
        myDebugger Debugger = new myDebugger();

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
           /* var _point = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);
            Top = _point.Y;
            Left = _point.X - this.Size.Width;*/
            BringToFront();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                MaximoQuery lMaximoQuery = new MaximoQuery();
                lMaximoQuery.oracle_delete_Query_GADATA(lTargetSystem, QueryName);
                Close();
                Dispose();
            }
            catch (Exception ex)
            {
               Debugger.Exeption(ex);
                MessageBox.Show(ex.Message, "OEPS", MessageBoxButtons.OK);
            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (QueryName.Length < 4)
            {
                MessageBox.Show("Queryname is to short", "OEPS", MessageBoxButtons.OK);
                return;
            }
            if (Query.Length < 4)
            {
                MessageBox.Show("Query is to short", "OEPS", MessageBoxButtons.OK);
                return;
            }


            try
            {
                MaximoQuery lMaximoQuery = new MaximoQuery();
                try
                { // I know this is stupid but to use UPDATE command i need the build a dataset and AARG 
                    lMaximoQuery.oracle_delete_Query_GADATA(lTargetSystem, QueryName);
                }
                catch
                {
                    //
                }
                lMaximoQuery.oracle_send_new_Query_to_GADATA(lTargetSystem, QueryName, QueryDiscription, Query);
                Close();
                Dispose();
            }
            catch (Exception ex)
            {
               Debugger.Exeption(ex);
                MessageBox.Show(ex.Message, "OEPS", MessageBoxButtons.OK);
            }
        }

    }
}
