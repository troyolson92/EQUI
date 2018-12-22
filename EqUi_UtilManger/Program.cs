using EQUIToolsLib;
using System;
using System.Collections.Specialized;
using System.Deployment.Application;
using System.Web;
using System.Windows.Forms;

namespace EqUi_UtilManger
{
    static class Program
    {

        // http://gnl1004zcbqc2/Clickonce/EqUi_UtilManger.application
        // http://equi/EqUi_UtilManager/EqUi_UtilManger.application?Tool=SBCUstats
        // http://equi/EqUi_UtilManager/EqUi_UtilManger.application?Tool=sharepoint

        [STAThread] //because of issue with webbrower
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //
            String[] Args = Environment.GetCommandLineArgs();
            NameValueCollection col = new NameValueCollection();
            //
            string queryString = null;
            //
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                queryString = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
            }
            //
            if (queryString != null)
                {
                    col = HttpUtility.ParseQueryString(queryString);
                    HandleQueryParms(col);
                }
            else
            {
                Application.Run(new Form1());
            }
        }

        public static void HandleQueryParms(NameValueCollection col)
        {
            foreach (string key in col.Keys)
            {
                if (string.IsNullOrEmpty(key)) continue;
                //
                string[] values = col.GetValues(key);
                if (values == null) continue;
                //
                switch (key)
                {
                case  "Tool":
                    switch (col["Tool"])
                    {
                        case "SBCUstats":
                            if (col["target"] != null)
                            {
                                Application.Run(new SBCUStats(col["target"]));
                            }
                            else if (col["Controller"] != null && col["Toolid"] != null)
                                {
                                    Application.Run(new SBCUStats(col["Controller"],col["Toolid"]));
                                }
                            else
                            {
                                Application.Run(new SBCUStats());
                            }
                            break;

                            default:
                                MessageBox.Show(string.Format("Tool: '{0}' is not a valid tool", values[0]));
                                break;

                        }
                    break;
                //
                default:
                     //
                    break;
                }
            }

        }
    }
}
