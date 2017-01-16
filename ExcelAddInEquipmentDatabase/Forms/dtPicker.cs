using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ExcelAddInEquipmentDatabase
{
    public partial class dtPicker : Form
    {
        Forms.uc_Datebox luc_Datebox;

        public DateTime selectedDate
        {
            get {return dateTimePicker1.Value;}
            set { dateTimePicker1.Value = value; }
        }
     
        public dtPicker(Forms.uc_Datebox uc_Datebox)
        {
            InitializeComponent();
            luc_Datebox = uc_Datebox;
        }

        private void dtPicker_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
               this.Hide();
            }
        }

        private void dtPicker_Shown(object sender, EventArgs e)
        {
            var _point = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);
            Top = _point.Y;
            Left = _point.X;
        }

        private void dtPicker_Deactivate(object sender, EventArgs e)
        {
            luc_Datebox.input = dateTimePicker1.Value;
            this.Hide();
        }

        private void dtPicker_Activated(object sender, EventArgs e)
        {
            dateTimePicker1.Select();
            SendKeys.Send("%{DOWN}");
        }

    }
}
