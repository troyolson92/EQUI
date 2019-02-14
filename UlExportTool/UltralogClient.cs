using System;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Data.OleDb;
using System.Data.SqlClient;
using EQUICommunictionLib;

namespace UltralogExportTool
{
    /// <summary>
    /// Import UL data
    /// </summary>
    public class UltralogClient
    {
        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ConnectionString;
        ConnectionManager connectionManager = new ConnectionManager();
        DataTable ULdata = new DataTable();
        DateTime lastrecord;

        /// <summary>
        /// Start processing data
        /// </summary>
        public void Start()
        {
            log.Info($"Program starting host:{ConnectionString}");
            CheckProgramRunning();
            while (true)
            {
                Update_rt_active_info();
                if (CheckAndGetUlData())
                {
                    if (ULdata.Rows.Count > 0)
                    {
                        ExportDatabase();
                    }
                }
                System.Threading.Thread.Sleep(5000);
            }
        }

        /// <summary>
        /// check program already running kill it if it is
        /// </summary>
        void CheckProgramRunning()
        {
            Process[] processlist = Process.GetProcesses();
            try
            {
                foreach (Process theprocess in processlist)
                {
                    if (string.Equals(theprocess.ProcessName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (theprocess.Id != Process.GetCurrentProcess().Id)
                        {
                            log.Info("Program instance already running");
                            Process p = Process.GetProcessById(theprocess.Id);
                            p.Kill();
                            log.Info("Program instance killed");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                log.Error("Failed to kill program", ex);
            }
        }

        /// <summary>
        /// handle the rt_active_info table 
        /// add new Inspection laptop if needed.
        /// handle multiple laptops 
        /// update rt_active info if data available
        /// </summary>
        void Update_rt_active_info()
        {
            DataTable dt = connectionManager.RunQuery($"SELECT * from [Ul].[rt_active_info] where [InspectionLaptop] = '{Dns.GetHostName()}'", enblExeptions: true);
            //new InspectionLaptop
            if (dt.Rows.Count == 0)
            {
                string qry = $@"INSERT INTO [UL].[rt_active_info]
                               ([InspectionLaptop]
                               ,[Heartbeat])
                            VALUES
                               ('{Dns.GetHostName()}'
                               ,getdate())";
                connectionManager.RunCommand(qry, enblExeptions: true);
                log.Info($"New inspection laptop added: {Dns.GetHostName()}");
            }
            else if (dt.Rows.Count != 1) // more than 1 row in rt active info
            {
                string qry = $"DELETE [Ul].[rt_active_info] from [Ul].[rt_active_info] where [InspectionLaptop] = '{Dns.GetHostName()}'";
                connectionManager.RunCommand(qry, enblExeptions: true);
                log.Warn($"Inspection laptop was multiple times in rt_active_info!: {Dns.GetHostName()}");
            }
            else
            {
                if (lastrecord == DateTime.MinValue)
                {
                    lastrecord = dt.Rows[0].Field<DateTime?>("ULDateTime").GetValueOrDefault(DateTime.MinValue);
                    log.Info($"Startup mode ULDateTimeDbl from rt_active_info last record:<{lastrecord}>");
                }

                if (ULdata.Rows.Count != 0)
                {
                    lastrecord = ULdata.Rows[ULdata.Rows.Count - 1].Field<DateTime>("ULDateTime");
                    string qry = $@"UPDATE [UL].[rt_active_info]
                               SET [Partname] = '{ULdata.Rows[ULdata.Rows.Count - 1].Field<string>("Partname")}'
                                  ,[InspectionPlanname] = '{ULdata.Rows[ULdata.Rows.Count - 1].Field<string>("Planname")}'
                                  ,[Planlength] = '{ULdata.Rows[ULdata.Rows.Count - 1].Field<int?>("Planlength")}'
                                  ,[IndexOfTestsequence] = '{ULdata.Rows[ULdata.Rows.Count - 1].Field<int?>("IndexOfTestsequence")}'
                                  ,[Inspector] = '{ULdata.Rows[ULdata.Rows.Count - 1].Field<string>("Inspector")}'
                                  ,[teststation] = '{ULdata.Rows[ULdata.Rows.Count - 1].Field<string>("teststation")}'
                                  ,[ULDateTime] = '{ULdata.Rows[ULdata.Rows.Count - 1].Field<DateTime>("ULDateTime")}'
                                  ,[Heartbeat] = getdate()
                             WHERE InspectionLaptop = '{Dns.GetHostName()}'";
                    connectionManager.RunCommand(qry, enblExeptions: true);
                    log.Debug($"rt_active_info updated last record:<{lastrecord}>");
                }
                else
                {
                    string qry = $@"UPDATE [UL].[rt_active_info]
                               SET [Heartbeat] = getdate()
                             WHERE InspectionLaptop = '{Dns.GetHostName()}'";
                    connectionManager.RunCommand(qry, enblExeptions: true);
                    log.Debug("rt_active_info Heartbeat updated");
                }
            }
        }

        /// <summary>
        /// Get all new data from local UL database
        /// </summary>
        bool CheckAndGetUlData()
        {
            if (File.Exists(Properties.Settings.Default.LocalUlDB))
            {
                try
                {
                    string connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={Properties.Settings.Default.LocalUlDB}";
                    if (Properties.Settings.Default.LocalUlDB.EndsWith(".accdb")) connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={Properties.Settings.Default.LocalUlDB}";
                    #region sqlRegion    
                    string sql = $@"
SELECT
  NULL AS id
, T_PointsList.Name AS spotname
, T_InspectedPoints.ClassName AS EvaluationClass
, T_InspectedPoints.Comments
, T_USResult.Comment
, T_Parts.Name AS Partname
, T_PlansList.Name AS Planname
, T_USResult.Thickness AS measuredThickness
, T_Joint.Diameter
, CDate(CDbl([T_InspectedPoints.Date]) + CDbl([T_InspectedPoints.Time])) AS ULDateTime
, T_Parts.Points AS Planlength
, T_USResult.BWECount AS bwe
, T_USResult.FECount AS flawecho
, T_USResult.GPCount AS gasporeecho
, T_PlanPoints.Sequence AS IndexOfTestSequence
, T_Joint.ThresholdFE AS MinIndentation
, T_Joint.Tolerance AS MinIndentationMM
, T_InspectedPoints.Operator AS inspector
, T_TestingStations.Name AS testStation
, T_PlatesList.Name AS namePlate1
, T_PlatesList.Material AS MaterialPlate1
, T_PlatesList.Thickness AS ThicknessPlate1
, T_PlatesList_1.Name AS namePlate2
, T_PlatesList_1.Material AS MaterialPlate2
, T_PlatesList_1.Thickness AS ThicknessPlate2
, T_PlatesList_2.Name AS namePlate3
, T_PlatesList_2.Material AS MaterialPlate3
, T_PlatesList_2.Thickness AS ThicknessPlate3
, '{Dns.GetHostName()}' AS computername
FROM T_TestingStations 
INNER JOIN (T_PlatesList AS T_PlatesList_2 
RIGHT JOIN (T_Parts INNER JOIN (((T_PlansList INNER JOIN T_Joint ON T_PlansList.PlanID = T_Joint.PlanID) 
INNER JOIN (
(T_PlatesList AS T_PlatesList_1 
INNER JOIN (T_PlatesList INNER JOIN T_PointsList ON T_PlatesList.PlateID = T_PointsList.Plate1) ON T_PlatesList_1.PlateID = T_PointsList.Plate2) 
INNER JOIN T_PlanPoints ON T_PointsList.PointID = T_PlanPoints.PointID) ON(T_PlansList.PlanID = T_PlanPoints.PlanID) AND(T_Joint.JointID = T_PlanPoints.JointID)
) 
INNER JOIN(T_InspectedPoints INNER JOIN T_USResult ON T_InspectedPoints.IDInspection = T_USResult.IDInspection) ON T_PlanPoints.PlanPointID = T_InspectedPoints.PlanPointID) ON(T_PlansList.PlanID = T_Parts.PlanID) AND(T_Parts.PartID = T_InspectedPoints.PartID)) ON T_PlatesList_2.PlateID = T_PointsList.Plate3) ON(T_TestingStations.StationID = T_PlanPoints.TestingStationID) AND(T_TestingStations.StationID = T_InspectedPoints.StationID) 
WHERE(((CDbl([T_InspectedPoints.Date]) + CDbl([T_InspectedPoints.Time])) > {lastrecord.ToOADate().ToString().Replace(',','.')}))";
                    #endregion
                    OleDbConnection connection = new OleDbConnection(connectionString);
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(sql, connection);
                    OleDbDataReader reader = command.ExecuteReader();
                    ULdata.Clear();
                    ULdata.Load(reader);
                    log.Debug($"Ultralog new records > last record<{lastrecord}>, count: {ULdata.Rows.Count}");
                    return true;
                }
                catch (SqlException ex)
                {
                    log.Error("CheckAndUploadUlData failure", ex);
                    return false;
                }
            }
            else
            {
                log.Fatal("local ultralog database not found");
                return false;
            }
        }

        /// <summary>
        /// Export data from data table to table on remote server
        /// </summary>
        void ExportDatabase()
        {
            log.Debug("Start upload to database");
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "[UL].[rt_UltralogData]";
                        try
                        {
                            // Write from the source to the destination.
                            bulkCopy.WriteToServer(ULdata);
                            connection.Close();
                            connection.Dispose();
                            log.Debug("upload to database completed");
                            return;
                        }
                        catch (Exception ex)
                        {
                            log.Error("bulkcopy", ex);
                        }
                    }
                }

            }//try
            catch (SqlException ex)
            {
                log.Error("exporting data", ex);
            }//catch
            log.Fatal("Failed to export data to server");
        }
    }
}
