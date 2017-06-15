using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EQUIToolsLib;

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

        private void btn_AssetStats_Click(object sender, EventArgs e)
        {
            AssetStats lAssetStats = new AssetStats(""); // does not have standalone yet ! 
        }

        private void btn_DocManger_Click(object sender, EventArgs e)
        {
            DocManager lDocManger = new DocManager();
        }

        private void btn_ErrorStats_Click(object sender, EventArgs e)
        {
            ErrorStats lErrorStats = new ErrorStats("", "0", ""); // does not have standalone yet!
        }

        private void btn_logDetails_Click(object sender, EventArgs e)
        {
            LogDetails lLogDetails = new LogDetails("", "", 0); //does not have standalone yet!
        }

        private void Btn_MXxWoOverview_Click(object sender, EventArgs e)
        {
            MXxWOoverview lMXxWOoverview = new MXxWOoverview("", false); // does not have standalone yet!
        }
    }
}
