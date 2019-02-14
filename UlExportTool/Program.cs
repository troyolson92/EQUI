using System;
using System.Linq;

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
            //parse args
            var ConfigUpdate = args.SingleOrDefault(arg => arg.StartsWith("ConfigUpdate"));
            var ClearAll = args.SingleOrDefault(arg => arg.StartsWith("ClearAll"));

            //start system try
            SystemTray systemTray = new SystemTray(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

            //config update mode
            if (ConfigUpdate != null)
            {
                log.Info("ConfigUpdate startup");
                try
                {
                    bool bClearAll = false;
                    if (ClearAll != null) bClearAll = true;
                    ConfigUpdater ConfigUpdater = new ConfigUpdater();
                    ConfigUpdater.UpdateUltralogConfig(DBname: "default", ClearAll: bClearAll);
                }
                catch(Exception ex)
                {
                    log.Error(ex);
                }
                Console.ReadLine();
                return 0;
            }

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
