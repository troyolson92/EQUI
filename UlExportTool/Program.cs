//V1.0.0.0 : first production ready program
//V1.0.1.0 : added : check if already running in process.


using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Net;
using System.Data.OleDb;
using System.Timers;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace UlExportTool
{

    static class Buffer
    {
        static List<string> _Logbuffer; // Static List instance
        static Buffer() { _Logbuffer = new List<string>(); }
        public static void Record(string value) { _Logbuffer.Add(value); }
        public static void Delete(string value) { _Logbuffer.Remove(value); }
        public static Int32 Count() { return _Logbuffer.Count(); }
        public static List<string> getbuffer() { return _Logbuffer; }
        public static void Display()
        {
            foreach (var value in _Logbuffer)
            {
                //Console.WriteLine(value);
            }
        }
        public static bool Contains(string file) { if (_Logbuffer.Contains(file)) { return true; } else { return false; } }
    }

    static class DebugLocal
    {
        public static void Init()
        {
            if (System.IO.File.Exists("C:\\USLT\\UlExport_Debug.log"))
            {
                try
                {
                    System.IO.File.Move("C:\\USLT\\UlExport_Debug.log", "C:\\USLT\\" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + " UlExport_Debug.log");
                }
                catch
                {

                }
            }
            Trace.Listeners.Add(new TextWriterTraceListener("C:\\USLT\\UlExport_Debug.log"));
            //Trace.Listeners.Add(new TextWriterTraceListener("C:\\USLT\\" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + " UlExport_Debug.log"));
            Trace.AutoFlush = true;
            /*Trace.Indent();
            Trace.Unindent();
            Trace.Flush();*/
        }
        public static void Restart()
        {
            //Console.WriteLine("System will restart in 10 seconds");
            System.Threading.Thread.Sleep(10000);
            var fileName = Assembly.GetExecutingAssembly().Location;
            System.Diagnostics.Process.Start(fileName);
            Environment.Exit(0);
        }
        public static void Message(string ls_part, string ls_message)
        {
            Trace.WriteLine("DT: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss,ffff") + " P: " + ls_part + " M: " + ls_message);
            //Console.WriteLine("DT: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss,ffff") + " P: " + ls_part + " M: " + ls_message);
            /*using (EventLog eventlog = new EventLog("Application"))
            {
                eventlog.Source = "Application";
                eventlog.WriteEntry(ls_message, EventLogEntryType.Information, 101, 1);
            }*/
        }
    }

    public class ConsoleSpiner
    {
        //int counter;
        public ConsoleSpiner()
        {
            //counter = 0;
        }
        public void Turn()
        {
            //counter++;
            //switch (counter % 3)
            //{
            //    case 0: Console.Write("/"); break;
            //    case 1: Console.Write("-"); break;
            //    case 2: Console.Write("\\"); break;
            //    default: Console.Write("-"); break;
            //}
            //Console.Write(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
            //Console.SetCursorPosition(Console.CursorLeft - 19, Console.CursorTop);
        }
    }

    public class RunProgram
    {
        DataTable ULdata = new DataTable();
        DateTime lastUlDate;
        double lastUldateDbl, tmp_lastUldateDbl;
        bool Isbusy = true;
        int UlDataRowCount;

        string ConnectionString = "user id = AASPOT_a; password = AASPOT_a; server = SQLA001.gen.volvocars.net; Trusted_Connection = no; database = gadata; connection timeout = 10";
        String LocalUlDB = "C:\\USLT\\ULtraLog\\Ultralog.mdb";
        string hostName;

        public void Start()
        {
            try
            {
                CheckProgramRunning();
                DebugLocal.Init();
                DebugLocal.Message("INFO", "START PROGRAM");
                hostName = Dns.GetHostName();
                Timer TriggerTimer = new System.Timers.Timer(5000); //run every 1 second

                TriggerTimer.Start();
                TriggerTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

                //Console.Title = "Volvo  UlExportTool version: 18W29D1";
                //Console.BufferHeight = 100;//Int16.MaxValue - 1;
                //Console.WindowWidth = 100;
                //DebugLocal.Init();
                DebugLocal.Message("INFO", "System restarted");
                DebugLocal.Message("INFO", "Hostname : " + hostName);
                ConsoleSpiner spin = new ConsoleSpiner();

                //get last ULMeasurement date and time
                lastUlDate = GetLastUlDateData(hostName, ConnectionString);
                lastUldateDbl = lastUlDate.ToOADate();// convert datetime to double
                DebugLocal.Message("INFO", "Last data in GADATA: " + lastUlDate.ToString());
                //DebugLocal.Message("INFO", "Last dataDBL in GADATA: " + lastUlDate.ToOADate());

                // if no connection ( date = 2 ), retryuntil connection
                while (lastUldateDbl == 2)
                {
                    DebugLocal.Message("INFO", "NO connection with SQL GADATA");
                    lastUlDate = GetLastUlDateData(hostName, ConnectionString);
                    lastUldateDbl = lastUlDate.ToOADate();// convert datetime to double
                    System.Threading.Thread.Sleep(10000);
                }

                DebugLocal.Message("INFO", "connection GADATA OK");

                if (CheckAndGetUlData(LocalUlDB, hostName, lastUldateDbl))
                { // get new data succes
                    UlDataRowCount = ULdata.Rows.Count;
                    if (UlDataRowCount > 0)
                    {
                        //get maxdatetime in table
                        tmp_lastUldateDbl = GetMaxDatetime(ULdata);

                        //export datat to GADATA
                        if (ExportDatabase(ULdata, "UltralogData_RAW", ConnectionString))
                        {
                            //if upload ok, lastuploaddatetime is updated
                            lastUldateDbl = tmp_lastUldateDbl;
                        }
                    }//if uldata containt new data
                    else
                    {
                        DebugLocal.Message("INFO", "No new local data");
                    }
                }

                Isbusy = false;

                while (true)
                {
                    System.Threading.Thread.Sleep(100);
                    spin.Turn();
                    //if (CheckAndGetUlData(LocalUlDB, hostName, lastUldateDbl))
                    //{ // get new data succes
                    //    UlDataRowCount = ULdata.Rows.Count;
                    //    if (UlDataRowCount > 0)
                    //    {
                    //        //get maxdatetime in table
                    //        tmp_lastUldateDbl = GetMaxDatetime(ULdata);

                    //        //export datat to GADATA
                    //        if (ExportDatabase(ULdata, "UltralogData_RAW", ConnectionString))
                    //        {
                    //            //if upload ok, lastuploaddatetime is updated
                    //            lastUldateDbl = tmp_lastUldateDbl;
                    //        }
                    //    }//if uldata containt new data
                    //    else
                    //    {
                    //        DebugLocal.Message("INFO", "No new local data");
                    //    }
                    //}

                }
            }//try
            catch (SqlException exy)
            {
                DebugLocal.Message("ERROR", "CheckRunProgram FAILED");
                DebugLocal.Message("ERROR", exy.Message);
            }
        }

        //check program already running V1.0.1.0
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
                        process_ID = theprocess.Id;
                        if (process_ID != Process.GetCurrentProcess().Id)
                        {
                            Process p = Process.GetProcessById(process_ID);
                            p.Kill();
                            DebugLocal.Message("INFO", "processID " + process_ID + "killed");
                        }
                        //end check program already running V1.0.1.0
                    }
                }

            }//try
            catch (SqlException ex)
            {
                DebugLocal.Message("ERROR", "CheckRunProgram FAILED");
                DebugLocal.Message("ERROR", ex.Message);
            }
        }

        void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (Isbusy)
            {

            }//Is busy
            else
            {
                Isbusy = true;
                if (CheckAndGetUlData(LocalUlDB, hostName, lastUldateDbl))
                { // get new data succes
                    UlDataRowCount = ULdata.Rows.Count;
                    if (UlDataRowCount > 0)
                    {
                        //get maxdatetime in table
                        tmp_lastUldateDbl = GetMaxDatetime(ULdata);

                        //export datat to GADATA
                        if (ExportDatabase(ULdata, "UltralogData_RAW", ConnectionString))
                        {
                            //if upload ok, lastuploaddatetime is updated
                            lastUldateDbl = tmp_lastUldateDbl;
                        }
                    }//if uldata containt new data
                    else
                    {
                        DebugLocal.Message("INFO", "No new local data");
                    }
                }
                else
                {// get new data failed

                }
                Isbusy = false;
            }// not busy
        }
        DateTime GetLastUlDateData(string as_ComputerName, string as_connectionstring)
        {
            int retries = 3;

            while (retries > 0)
            {
                try
                {
                    SqlConnection connection;
                    SqlDataAdapter adapter;
                    SqlCommand command = new SqlCommand();
                    SqlParameter param;
                    SqlParameter param1;
                    DataSet ds = new DataSet();

                    DebugLocal.Message("INFO", "start connecting using GetLastUlDateData");

                    connection = new SqlConnection(as_connectionstring);

                    connection.Open();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[UL].[GetLastULDate]";

                    param = new SqlParameter("@laptopName", as_ComputerName);
                    param.Direction = ParameterDirection.Input;
                    param.DbType = DbType.String;

                    param1 = new SqlParameter("@LastInspectionDate", "");
                    param1.Direction = ParameterDirection.Output;
                    param1.DbType = DbType.DateTime;

                    command.Parameters.Add(param);
                    command.Parameters.Add(param1);

                    adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);

                    //Console.WriteLine(ds.Tables[0].Rows[0][0].ToString());

                    connection.Close();
                    retries = 0; // break loop if everything ok
                    return Convert.ToDateTime(ds.Tables[0].Rows[0][0]);
                } //try
                catch (SqlException ex)
                {
                    DebugLocal.Message("ERROR", "GetLastUlDateData ; retries left " + (retries - 1));
                    DebugLocal.Message("ERROR", ex.Message);
                    retries--;
                    System.Threading.Thread.Sleep(10000); //wait until retry
                }//catch
            }//while
            return new DateTime(1900, 01, 01);
        }//void
        bool CheckAndGetUlData(string as_localDB, string as_HostName, double as_starttimedbl)
        {
            if (File.Exists(as_localDB))
            {
                int retries = 3;
                while (retries > 0)
                {
                    try
                    {
                        //string hostName = Dns.GetHostName();
                        //string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + as_localDB;
                        //string strSQL = "SELECT T_PointsList.Name AS spotname, T_InspectedPoints.ClassName AS EvaluationClass, T_InspectedPoints.Comments, T_USResult.Comment,T_Parts.Name AS Partname, T_PlansList.Name AS Planname, T_USResult.Thickness AS measuredThickness, T_Joint.Diameter, CDate(CDbl([T_InspectedPoints.Date])+CDbl([T_InspectedPoints.Time])) as UlDateTime,  T_InspectedPoints.Date, T_InspectedPoints.Time, T_Parts.Points AS Planlenght, T_USResult.BWECount AS bwe, T_USResult.FECount AS flawecho, T_USResult.GPCount AS gasporeecho, T_PlanPoints.Sequence AS IndexOfTestSequence, T_Joint.ThresholdFE AS MinIndentation, T_Joint.Tolerance AS MinIndentationMM, T_InspectedPoints.Operator AS inspector, T_TestingStations.Name AS testStation, T_PlatesList.Name AS namePlate1, T_PlatesList.Material AS MaterialPlate1, T_PlatesList.Thickness AS ThicknessPlate1, T_PlatesList_1.Name AS namePlate2, T_PlatesList_1.Material AS MaterialPlate2, T_PlatesList_1.Thickness AS ThicknessPlate2, T_PlatesList_2.Name AS namePlate3, T_PlatesList_2.Material AS MaterialPlate3, T_PlatesList_2.Thickness AS ThicknessPlate3,  T_InspectedPoints.ExportToCentralDatastore, '" + as_HostName + "' AS computername FROM T_TestingStations INNER JOIN(T_PlatesList AS T_PlatesList_2 RIGHT JOIN (T_Parts INNER JOIN (((T_PlansList INNER JOIN T_Joint ON T_PlansList.PlanID = T_Joint.PlanID) INNER JOIN((T_PlatesList AS T_PlatesList_1 INNER JOIN (T_PlatesList INNER JOIN T_PointsList ON T_PlatesList.PlateID = T_PointsList.Plate1) ON T_PlatesList_1.PlateID = T_PointsList.Plate2) INNER JOIN T_PlanPoints ON T_PointsList.PointID = T_PlanPoints.PointID) ON(T_PlansList.PlanID = T_PlanPoints.PlanID) AND(T_Joint.JointID = T_PlanPoints.JointID)) INNER JOIN(T_InspectedPoints INNER JOIN T_USResult ON T_InspectedPoints.IDInspection = T_USResult.IDInspection) ON T_PlanPoints.PlanPointID = T_InspectedPoints.PlanPointID) ON(T_PlansList.PlanID = T_Parts.PlanID) AND(T_Parts.PartID = T_InspectedPoints.PartID)) ON T_PlatesList_2.PlateID = T_PointsList.Plate3) ON(T_TestingStations.StationID = T_PlanPoints.TestingStationID) AND(T_TestingStations.StationID = T_InspectedPoints.StationID) ";
                        string strSQL = "SELECT T_PointsList.Name AS spotname, T_InspectedPoints.ClassName AS EvaluationClass, T_InspectedPoints.Comments, T_USResult.Comment, T_Parts.Name AS Partname, T_PlansList.Name AS Planname, T_USResult.Thickness AS measuredThickness, T_Joint.Diameter, CDate(CDbl([T_InspectedPoints.Date]) + CDbl([T_InspectedPoints.Time])) AS UlDateTime, T_InspectedPoints.Date, T_InspectedPoints.Time, T_Parts.Points AS Planlenght, T_USResult.BWECount AS bwe, T_USResult.FECount AS flawecho, T_USResult.GPCount AS gasporeecho, T_PlanPoints.Sequence AS IndexOfTestSequence, T_Joint.ThresholdFE AS MinIndentation, T_Joint.Tolerance AS MinIndentationMM, T_InspectedPoints.Operator AS inspector, T_TestingStations.Name AS testStation, T_PlatesList.Name AS namePlate1, T_PlatesList.Material AS MaterialPlate1, T_PlatesList.Thickness AS ThicknessPlate1, T_PlatesList_1.Name AS namePlate2, T_PlatesList_1.Material AS MaterialPlate2, T_PlatesList_1.Thickness AS ThicknessPlate2, T_PlatesList_2.Name AS namePlate3, T_PlatesList_2.Material AS MaterialPlate3, T_PlatesList_2.Thickness AS ThicknessPlate3, '" + as_HostName + "' AS computername,(CDbl([T_InspectedPoints.Date]) + CDbl([T_InspectedPoints.Time])) AS ULDateTimeDbl FROM T_TestingStations INNER JOIN(T_PlatesList AS T_PlatesList_2 RIGHT JOIN (T_Parts INNER JOIN (((T_PlansList INNER JOIN T_Joint ON T_PlansList.PlanID = T_Joint.PlanID) INNER JOIN((T_PlatesList AS T_PlatesList_1 INNER JOIN (T_PlatesList INNER JOIN T_PointsList ON T_PlatesList.PlateID = T_PointsList.Plate1) ON T_PlatesList_1.PlateID = T_PointsList.Plate2) INNER JOIN T_PlanPoints ON T_PointsList.PointID = T_PlanPoints.PointID) ON(T_PlansList.PlanID = T_PlanPoints.PlanID) AND(T_Joint.JointID = T_PlanPoints.JointID)) INNER JOIN(T_InspectedPoints INNER JOIN T_USResult ON T_InspectedPoints.IDInspection = T_USResult.IDInspection) ON T_PlanPoints.PlanPointID = T_InspectedPoints.PlanPointID) ON(T_PlansList.PlanID = T_Parts.PlanID) AND(T_Parts.PartID = T_InspectedPoints.PartID)) ON T_PlatesList_2.PlateID = T_PointsList.Plate3) ON(T_TestingStations.StationID = T_PlanPoints.TestingStationID) AND(T_TestingStations.StationID = T_InspectedPoints.StationID) WHERE(((CDbl([T_InspectedPoints.Date]) + CDbl([T_InspectedPoints.Time])) > " + as_starttimedbl.ToString().Replace(',', '.') + ")) ";

                        DebugLocal.Message("INFO", "start query to local UltralogDB");

                        OleDbConnection connection = new OleDbConnection(connectionString);
                        connection.Open();
                        OleDbCommand command = new OleDbCommand(strSQL, connection);
                        OleDbDataReader reader = command.ExecuteReader();
                        ULdata.Load(reader);
                        int rowCount = ULdata.Rows.Count;
                        DebugLocal.Message("INFO", "System polled, records : " + rowCount);

                        retries = 0; // break loop if everything ok
                        return true;
                    } //try
                    catch (SqlException ex)
                    {
                        DebugLocal.Message("ERROR", "CheckAndUploadUlData ; retries left " + (retries - 1));
                        DebugLocal.Message("ERROR", ex.Message);
                        retries--;
                        System.Threading.Thread.Sleep(10000); //wait until retry
                    }//catch
                }//while
                return false;
            }
            else
            {
                DebugLocal.Message("ERROR", "local Database not found");
                return false;
            }

        }
        bool ExportDatabase(DataTable adt_table, string as_destination, string as_connectionstring)
        {

            DebugLocal.Message("INFO", "Start upload to database");
            int retries = 3;
            while (retries > 0)
            {
                try
                {

                    //string connectionString = "user id=AASPOT_a; password=AASPOT_a; server=SQLA001.gen.volvocars.net; Trusted_Connection=no; database=gadata; connection timeout=30";
                    using (SqlConnection connection = new SqlConnection(as_connectionstring))
                    {
                        connection.Open();
                        // Perform an initial count on the destination table.
                        SqlCommand commandRowCount = new SqlCommand("SELECT COUNT(*) FROM [UL].[" + as_destination + "];", connection);
                        long countStart = System.Convert.ToInt32(commandRowCount.ExecuteScalar());
                        // Note that the column positions in the source DataTable  
                        // match the column positions in the destination table so  
                        // there is no need to map columns.  
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                        {
                            bulkCopy.DestinationTableName = "[UL].[" + as_destination + "]";
                            try
                            {
                                // Write from the source to the destination.
                                bulkCopy.WriteToServer(adt_table);

                                //see how many rows were added. 
                                long countEnd = System.Convert.ToInt32(
                                commandRowCount.ExecuteScalar());
                                connection.Close();
                                connection.Dispose();
                                DebugLocal.Message("INFO", "Detected: " + adt_table.Rows.Count + "/" + (countEnd - countStart) + " new rows were added to " + as_destination);
                                DebugLocal.Message("INFO", "Upload to database complete");
                                ULdata.Clear(); // clear list if upload succes
                                retries = 0; // break loop if everything ok
                                return true;
                            }
                            catch (Exception ex)
                            {
                                DebugLocal.Message("Bukcopy", ex.Message);
                                DebugLocal.Message("INFO", ex.HelpLink);
                            }//catch
                        }//using

                    }//using

                }//try
                catch (SqlException ex)
                {
                    DebugLocal.Message("ERROR", "ExportDatabase ; retries left " + (retries - 1));
                    DebugLocal.Message("ERROR", ex.Message);
                    retries--;
                    System.Threading.Thread.Sleep(10000); //wait until retry
                }//catch
            }//while
            return false;
        }
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

    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();

        static void Main(string[] args)
        {
            FreeConsole(); // closes the console
            RunProgram Program = new RunProgram();
            Program.Start();
        }

        static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
