using System;
using System.Linq;
using System.Threading;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace UltralogExportTool
{
    /// <summary>
    /// Main
    /// </summary>
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static int Main(string[] args)
        {
            log.Info("Application startup");
            //start system tray in his own threath
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                SystemTray systemTray = new SystemTray(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            }).Start();

            //client mode
            restart:
                try
                {
                    log.Info("UltralogClient startup");
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
