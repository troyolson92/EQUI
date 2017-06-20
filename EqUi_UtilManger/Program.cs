using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NDesk.Options;
using System.Deployment.Application;

namespace EqUi_UtilManger
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            String[] Args = Environment.GetCommandLineArgs();
            //

            /*
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                string queryString = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
                if (queryString != null)
                {
                    MessageBox.Show("actURL= " + queryString);
                }
                {
                    MessageBox.Show("actURL= " + "empty");
                }

            }*/


            if (Args == null || Args.Length < 2 )      //direct run show launch form     
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                bool show_help = false;
                List<string> names = new List<string>();
                int repeat = 1;

                var p = new OptionSet() {
                { "n|name=", "the {NAME} of someone to greet.",
                   v => names.Add (v) },
                { "r|repeat=", "the number of {TIMES} to repeat the greeting.\n this must be an integer.",
                    (int v) => repeat = v },
                { "v", "increase debug message verbosity",
                   v => { if (v != null) Console.WriteLine("Verbosity"); } },
                { "h|help",  "show this message and exit", 
                   v => show_help = v != null },
                };


                List<string> extra;
                try
                {
                    extra = p.Parse(Args);
                }
                catch (OptionException e)
                {
                    MessageBox.Show(string.Format(@"
                    greet:
                    {0}
                    Try `greet --help' for more information.",
                    e.Message));
                    return;
                }

                if (show_help)
                {
                    ShowHelp(p);
                    return;
                }

                string message;
                if (extra.Count > 0)
                {
                    message = string.Join(" ", extra.ToArray());
                    Debug("Using new message: {0}", message);
                }
                else
                {
                    message = "Hello {0}!";
                    Debug("Using default message: {0}", message);
                }

                foreach (string name in names)
                {
                    for (int i = 0; i < repeat; ++i)
                        Console.WriteLine(message, name);
                }
            }
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: greet [OPTIONS]+ message");
            Console.WriteLine("Greet a list of individuals with an optional message.");
            Console.WriteLine("If no message is specified, a generic greeting is used.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

        static void Debug(string format, params object[] args)
        {
        }
    }
}
