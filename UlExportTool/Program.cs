using System;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using EQUICommunictionLib;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace UlExportTool
{
    //test 

    /// <summary>
    /// Main
    /// </summary>
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();
        static void Main(string[] args)
        {
            if (Properties.Settings.Default.HideConsole)
            {
                FreeConsole(); // closes the console
            }
            RunProgram Program = new RunProgram();
            Program.Start();
        }
    }

    /// <summary>
    /// Import UL data
    /// </summary>
    public class RunProgram
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ConnectionManager connectionManager = new ConnectionManager();
        DataTable ULdata = new DataTable();
        DateTime lastUlDate;
        double lastUldateDbl, tmp_lastUldateDbl;
        int UlDataRowCount;
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ConnectionString;

        /// <summary>
        /// Start processing data
        /// </summary>
        public void Start()
        {
            try
            {
                log.Info("Program starting");
                CheckProgramRunning();

                //get last ULMeasurement date and time
                lastUldateDbl = GetLastUlDateData().ToOADate();
                log.Info($"System started host: { Dns.GetHostName()} Last data on server: {lastUlDate.ToString()}");

                while (true)
                {
                    if (CheckAndGetUlData(Properties.Settings.Default.LocalUlDB, Dns.GetHostName(), lastUldateDbl))
                    { // get new data success
                        UlDataRowCount = ULdata.Rows.Count;
                        if (UlDataRowCount > 0)
                        {
                            //get maxdatetime in table
                            tmp_lastUldateDbl = GetMaxDatetime(ULdata);
                            //export datat to GADATA
                            if (ExportDatabase(ULdata))
                            {
                                //if upload ok, lastuploaddatetime is updated
                                lastUldateDbl = tmp_lastUldateDbl;
                            }
                        }//if uldata contains new data
                        else
                        {
                            log.Debug("No new local data");
                        }
                    }
                    System.Threading.Thread.Sleep(5000);
                }
            }
            catch (SqlException ex)
            {
                log.Error("MAIN error", ex);
            }
        }

        /// <summary>
        /// check program already running kill it if it is
        /// </summary>
        void CheckProgramRunning()
        {
            Process[] processlist = Process.GetProcesses();
            int process_ID;
            string test;
            try
            {
                foreach (Process theprocess in processlist)
                {
                    test = theprocess.ProcessName;

                    if (string.Equals(theprocess.ProcessName, "ulexporttool", StringComparison.CurrentCultureIgnoreCase))
                    {
                        log.Info("Program instance already running");
                        process_ID = theprocess.Id;
                        if (process_ID != Process.GetCurrentProcess().Id)
                        {
                            Process p = Process.GetProcessById(process_ID);
                            p.Kill();
                            log.Info("Program instance killed");
                        }
                        //end check program already running V1.0.1.0
                    }
                }
            }
            catch (SqlException ex)
            {
                log.Error("Failed to kill program", ex);
            }
        }

        /// <summary>
        /// Get last UL date on server for specific client
        /// </summary>
        /// <returns>date time</returns>
        DateTime GetLastUlDateData()
        {
            int retries = 3;
            while (retries > 0)
            {
                try
                {
                    DataTable dt = connectionManager.RunQuery($"SELECT TOP 1  MAX(InspectionTime) FROM[UL].[UltralogData_RAW] where InspectionLaptop = '{Dns.GetHostName()}'", enblExeptions: true);
                    retries = 0; // break loop if everything ok
                    log.Debug($"GetLastUlDateData: {Convert.ToDateTime(dt.Rows[0][0])}");
                    return Convert.ToDateTime(dt.Rows[0][0]);
                } //try
                catch (SqlException ex)
                {
                    log.Error($"GetLastUlDateData failure retries left: {retries - 1}", ex);
                    retries--;
                    System.Threading.Thread.Sleep(10000); //wait until retry
                }//catch
            }//while
            log.Fatal("Failed to get GetLastUlDateData return default");
            return new DateTime(1900, 01, 01);
        }//void

        /// <summary>
        /// Get all new data from local UL database
        /// </summary>
        /// <param name="as_localDB"></param>
        /// <param name="as_HostName"></param>
        /// <param name="as_starttimedbl"></param>
        /// <returns></returns>
        bool CheckAndGetUlData(string as_localDB, string as_HostName, double as_starttimedbl)
        {
            if (File.Exists(as_localDB))
            {
                int retries = 3;
                while (retries > 0)
                {
                    try
                    {
                        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + as_localDB;
                        string strSQL = @"SELECT T_PointsList.Name AS spotname
, T_InspectedPoints.ClassName AS EvaluationClass
, T_InspectedPoints.Comments
, T_USResult.Comment
, T_Parts.Name AS Partname
, T_PlansList.Name AS Planname
, T_USResult.Thickness AS measuredThickness
, T_Joint.Diameter
, CDate(CDbl([T_InspectedPoints.Date]) + CDbl([T_InspectedPoints.Time])) AS UlDateTime
, T_InspectedPoints.Date, T_InspectedPoints.Time
, T_Parts.Points AS Planlenght
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
, '{0}' AS computername
,(CDbl([T_InspectedPoints.Date]) + CDbl([T_InspectedPoints.Time])) AS ULDateTimeDbl 
FROM T_TestingStations 
INNER JOIN(T_PlatesList AS T_PlatesList_2 
RIGHT JOIN (T_Parts INNER JOIN (((T_PlansList INNER JOIN T_Joint ON T_PlansList.PlanID = T_Joint.PlanID) 
INNER JOIN((T_PlatesList AS T_PlatesList_1 
INNER JOIN (T_PlatesList INNER JOIN T_PointsList ON T_PlatesList.PlateID = T_PointsList.Plate1) ON T_PlatesList_1.PlateID = T_PointsList.Plate2) 
INNER JOIN T_PlanPoints ON T_PointsList.PointID = T_PlanPoints.PointID) ON(T_PlansList.PlanID = T_PlanPoints.PlanID) AND(T_Joint.JointID = T_PlanPoints.JointID)) 
INNER JOIN(T_InspectedPoints INNER JOIN T_USResult ON T_InspectedPoints.IDInspection = T_USResult.IDInspection) ON T_PlanPoints.PlanPointID = T_InspectedPoints.PlanPointID) ON(T_PlansList.PlanID = T_Parts.PlanID) AND(T_Parts.PartID = T_InspectedPoints.PartID)) ON T_PlatesList_2.PlateID = T_PointsList.Plate3) ON(T_TestingStations.StationID = T_PlanPoints.TestingStationID) AND(T_TestingStations.StationID = T_InspectedPoints.StationID) WHERE(((CDbl([T_InspectedPoints.Date]) + CDbl([T_InspectedPoints.Time])) > " + as_starttimedbl.ToString().Replace(',', '.') + ")) ";

                        OleDbConnection connection = new OleDbConnection(connectionString);
                        connection.Open();
                        OleDbCommand command = new OleDbCommand(string.Format(strSQL,as_HostName), connection);
                        OleDbDataReader reader = command.ExecuteReader();
                        ULdata.Load(reader);
                        int rowCount = ULdata.Rows.Count;
                        log.Debug($"System polled, records : {rowCount}");
                        retries = 0; // break loop if everything ok
                        return true;
                    } //try
                    catch (SqlException ex)
                    {
                        log.Error($"CheckAndUploadUlData failure retries left: {retries - 1}",ex);
                        retries--;
                        System.Threading.Thread.Sleep(10000); //wait until retry
                    }//catch
                }//while
                return false;
            }
            else
            {
                log.Fatal("local Database not found");
                return false;
            }

        }

        /// <summary>
        /// Export data from data table to table on remote server
        /// </summary>
        /// <param name="adt_table">local data table</param>
        /// <returns></returns>
        bool ExportDatabase(DataTable adt_table)
        {
            log.Info("Start upload to database");
            int retries = 3;
            while (retries > 0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        // Note that the column positions in the source DataTable  
                        // match the column positions in the destination table so  
                        // there is no need to map columns.  
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                        {
                            bulkCopy.DestinationTableName = "[UL].[UltralogData_RAW]";
                            try
                            {
                                // Write from the source to the destination.
                                bulkCopy.WriteToServer(adt_table);
                                connection.Close();
                                connection.Dispose();
                                ULdata.Clear(); // clear list if upload succes
                                retries = 0; // break loop if everything ok
                                return true;
                            }
                            catch (Exception ex)
                            {
                                log.Error("bulkcopy", ex);
                            }//catch
                        }//using

                    }//using

                }//try
                catch (SqlException ex)
                {
                    log.Error($"exporting data  retries left: {(retries - 1)}", ex);
                    retries--;
                    System.Threading.Thread.Sleep(10000); //wait until retry
                }//catch
            }//while
            log.Fatal("Failed to export data to server");
            return false;
        }

        /// <summary>
        /// Get max date from data table
        /// </summary>
        /// <param name="adt_table"></param>
        /// <returns>double</returns>
        double GetMaxDatetime(DataTable adt_table)
        {
            double tmp_maxdatetime;
            tmp_maxdatetime = Convert.ToDouble(ULdata.Rows[0].ItemArray[30]);
            for (int i = 0; i < ULdata.Rows.Count; i++)
            {
                if (Convert.ToDouble(ULdata.Rows[i].ItemArray[30]) > tmp_maxdatetime)
                {
                    tmp_maxdatetime = Convert.ToDouble(ULdata.Rows[i].ItemArray[30]);
                }
            }
            return tmp_maxdatetime;
        }
    }
}
