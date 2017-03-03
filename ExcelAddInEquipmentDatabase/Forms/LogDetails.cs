using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class LogDetails : Form
    {
        public LogDetails(string Errornum)
        {
            InitializeComponent();
            this.Text = string.Format("Errornum: {0}", Errornum);
        }
    }
}
