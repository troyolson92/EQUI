using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EqUi_UtilManger
{
    static class Program
    {
        [STAThread]
        static void Main(string[] Args)
        {
            //file://///gnlsnm0101.gen.volvocars.net\proj\6308-SHR-VCC22700\VSTO\DEPLOYMENTBASE\EqUi_UtilManger.application
            //file://///gnlsnm0101.gen.volvocars.net\proj\6308-SHR-VCC22700\VSTO\DEPLOYMENTBASE\EqUi_UtilManger.application?param1=foo&param2=bar
            //\\gnlsnm0101.gen.volvocars.net\proj\6308-SHR-VCC22700\VSTO\DEPLOYMENTBASE
           /*
            if (Args != null)
            {
                foreach (string Arg in Args)
                {
                    MessageBox.Show(Arg.ToString());
                }
            }

            string[] cmdLine = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;
            if (cmdLine != null)
            {
                foreach (string value in cmdLine)
                {
                    MessageBox.Show(value.ToString());
                    MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.Programs));
                }
            }
            else
            {
                MessageBox.Show("no parms");
            }
            */

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
