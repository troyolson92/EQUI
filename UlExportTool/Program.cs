using System;
using System.Linq;
using System.Runtime.InteropServices;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace UlExportTool
{
    /// <summary>
    /// Main
    /// </summary>
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();
        static int Main(string[] args)
        {
            //parse args
            var ConfigUpdate = args.SingleOrDefault(arg => arg.StartsWith("ConfigUpdate"));
            var ShowConsole = args.SingleOrDefault(arg => arg.StartsWith("ShowConsole"));

            //config update mode
            if (ConfigUpdate != null)
            {
                try
                {
                    ConfigUpdater ConfigUpdater = new ConfigUpdater();
                    ConfigUpdater.UpdateUltralogConfig();
                }
                catch(Exception ex)
                {
                    log.Error(ex);
                }
                Console.ReadLine();
                return 0;
            }

            //client mode
            if (Properties.Settings.Default.HideConsole && ShowConsole == null)
            {
                FreeConsole(); // closes the console
            }
            restart:
                try
                {
                    UltralogClient client = new UltralogClient();
                    client.Start();
                }
                catch (Exception ex)
                {
                    log.Error("Something went wrong", ex);
                    System.Threading.Thread.Sleep(5000);
                    goto restart;
                }

            return -1;
        }
    }


}
