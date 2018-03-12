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
    public partial class EquiBrowser : MetroFramework.Forms.MetroForm
    {
        public EquiBrowser(string url)
        {
            System.Diagnostics.Process.Start(url);

            this.Close();
            //temp 
            /*
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(url);  
            */
        }
    }
}
