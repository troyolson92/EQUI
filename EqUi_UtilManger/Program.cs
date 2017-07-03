using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Web;
using System.Collections.Specialized;

namespace EqUi_UtilManger
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            String[] Args = Environment.GetCommandLineArgs();
            NameValueCollection col = new NameValueCollection();
            //

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                string queryString = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
                if (queryString != null)
                {
                    MessageBox.Show("actURL= " + queryString);
                    col = HttpUtility.ParseQueryString(queryString);
                    HandleQueryParms(col);
                }
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }

            }


        }

        private static void HandleQueryParms(NameValueCollection col)
        {
            foreach (string key in col.Keys)
            {
                if (string.IsNullOrEmpty(key)) continue;

                string[] values = col.GetValues(key);
                if (values == null) continue;

                if (key == "Tool")
                {
                    switch (values[0])
                    {
                        case "SBCUstats":

                            break;

                        case "MaximoTool":

                            break;

                        default:
                            MessageBox.Show(string.Format("Tool: '{0}' is not a valid tool",values[0]));
                            break;


                    }
                }


            }

        }
    }
}
