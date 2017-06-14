using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MXxWOoverview
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
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
                }
            }
            else
            {
                MessageBox.Show("no parms");
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ExcelAddInEquipmentDatabase.Forms.MXxWOoverview("99070R01", false));
        }
    }
}
