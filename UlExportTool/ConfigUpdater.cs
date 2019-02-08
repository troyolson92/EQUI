using EQUICommunictionLib;
using System.Data.OleDb;
using System.IO;
using System.Data;

namespace UlExportTool
{
    class ConfigUpdater
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ConnectionString;
        ConnectionManager connectionManager = new ConnectionManager();

        public void UpdateUltralogConfig()
        {
            log.Info($"Program starting host:{ConnectionString}");
            log.Debug("Starting new config update session");
            UpdateUltraLogConfigTable("T_Picture");
            UpdateUltraLogConfigTable("T_PicturePoints");
            UpdateUltraLogConfigTable("T_PlanPoints");
            UpdateUltraLogConfigTable("T_PlansList");
            UpdateUltraLogConfigTable("T_PlatesList");
            UpdateUltraLogConfigTable("T_PointsList");
            log.Debug("Update complete press any key to exit");
        }

        public void UpdateUltraLogConfigTable(string tablename)
        {
            DataTable dt = new DataTable();
            if (!File.Exists(Properties.Settings.Default.LocalUlDB))
            {
                log.Fatal("local ultralog database not found");
                return;
            }
            //get date from UL
            string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Properties.Settings.Default.LocalUlDB}";
            if (Properties.Settings.Default.LocalUlDB.EndsWith(".accdb")) connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Properties.Settings.Default.LocalUlDB}"; 
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            OleDbCommand command = new OleDbCommand($"SELECT * FROM [{tablename}]", connection);
            log.Debug($"Get data from ultralog table: {tablename}");
            OleDbDataReader reader = command.ExecuteReader();
            dt.Load(reader);
            log.Debug($"Done records: {dt.Rows.Count}");
            //clear table on db
            log.Debug("clearing table on server");
            connectionManager.RunCommand($"DELETE [UL].[{tablename}] FROM [UL].[{tablename}]", enblExeptions: true);
            log.Debug("Done");
            //upload new data to db
            log.Debug("Copy new data to server");
            connectionManager.BulkCopy(dt, $"[UL].[{tablename}]", enblExeptions: true);
            log.Debug("Done");
        }
    }
}
