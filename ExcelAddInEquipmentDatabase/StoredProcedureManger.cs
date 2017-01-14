using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelAddInEquipmentDatabase
{
    public partial class StoredProcedureManger : Form
    {
        public StoredProcedureManger()
        {
            InitializeComponent();
        }

        private void StoredProcedureManger_Load(object sender, EventArgs e)
        {
            CheckBox nCheckbox = new CheckBox();
            nCheckbox.Name = "@Test";
            nCheckbox.Text = "Test";
            nCheckbox.Checked = true;
            flowLayoutPanel1.Controls.Add(nCheckbox);

            TextBox nTextBox = new TextBox();
            nTextBox.Name = "@Test";
            nTextBox.Text = "Test";
            flowLayoutPanel1.Controls.Add(nTextBox);


            DateTimePicker nDateTimePicker = new DateTimePicker();
            nDateTimePicker.Name = "@Test";
            nDateTimePicker.Text = DateTime.Now.ToString(); ;
            flowLayoutPanel1.Controls.Add(nDateTimePicker);


            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.AutoScroll = true;
        }


    }
}
