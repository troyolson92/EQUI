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
        // http://gnl1004zcbqc2/Clickonce/EqUi_UtilManger.application?Tool=SBCUstats
        // http://gnl1004zcbqc2/Clickonce/EqUi_UtilManger.application?Tool=SBCUstats&target=32070WS02A
        // http://gnl1004zcbqc2/Clickonce/EqUi_UtilManger.application?Tool=MaximoTool&Viewmode=vm_QualityOnLocation&target=A%20STN32000
        // http://equi/EqUi_UtilManager/EqUi_UtilManger.application?Tool=SBCUstats

        /*
         * chlickonce extention support for Chrome! 
         *chrome-extension://kekahkplibinaibelipdcikofmedafmb/install/plugin.html
         */

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
                            else
                            {
                                Application.Run(new SBCUStats());
                            }
                            break;

                        case "MaximoTool":
                            if (col["Viewmode"] == null) { col["Viewmode"] = ""; }
                            //
                            if (col["target"] != null)
                            {
                                Application.Run(new MXxWOoverview(col["target"], col["Viewmode"]));
                            }
                            else
                            {
                                Application.Run(new MXxWOoverview(col["Viewmode"]));
                            }
                            break;

                          case "ERRORstats":
                                if (col["target"] != null)
                                {
                                    Application.Run(new ErrorStats(col["target"], col["Logtype"], col["Errornum"], col["logtext"]));
                                }
                                else
                                {
                                    MessageBox.Show(string.Format("Tool: '{0}' is not a valid tool", values[0]));

                                }
                                break;

                            default:
                            MessageBox.Show(string.Format("Tool: '{0}' is not a valid tool",values[0]));
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
