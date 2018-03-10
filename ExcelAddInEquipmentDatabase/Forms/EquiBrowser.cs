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
            Hide();
            InitializeComponent();
            webBrowser1.Navigate(url);      
        }
    }
}
