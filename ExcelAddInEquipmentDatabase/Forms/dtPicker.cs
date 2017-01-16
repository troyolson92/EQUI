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
        //RibbonParameter _RibbonParameter;

        public dtPicker(RibbonParameter RibbonParameter)
        {
            InitializeComponent();
            RibbonParameter.input = dateTimePicker1.Value.ToString();
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
            //Debug.WriteLine("{0} should be set to: {1}", QueryParameter, this.dateTimePicker1.Value.ToString("yyyy-MM-dd hh:mm:ss"));
            //_RibbonParameter.input = dateTimePicker1.Value.ToString();
            this.Hide();
        }

        private void dtPicker_Activated(object sender, EventArgs e)
        {
            dateTimePicker1.Select();
            SendKeys.Send("%{DOWN}");
        }

    }
}
