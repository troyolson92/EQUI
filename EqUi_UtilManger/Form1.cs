using EQUIToolsLib;
using System;
using System.Windows.Forms;
using EQUICommunictionLib;

namespace EqUi_UtilManger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void btn_sbcuStat_Click(object sender, EventArgs e)
        {
            SBCUStats lSBCUStats = new SBCUStats();
        }

        private void btn_blockSleep_Click(object sender, EventArgs e)
        {
            SleepBlock sleepBlock = new SleepBlock();
        }

    }
}
