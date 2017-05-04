using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.Odbc;
using System.Text.RegularExpressions;

namespace ExcelAddInEquipmentDatabase
{
    public class GadataComm
    {
        //debugger
        Debugger Debugger = new Debugger();
        //connection to GADATA
        SqlConnection Gadataconn = new SqlConnection("user id=EqUi; password=EqUi; server=SQLA001.gen.volvocars.net;" +
                                                      "Trusted_Connection=no; database=gadata; connection timeout=30");

        //conntion to GADATA admin
        SqlConnection GadataconnA = new SqlConnection("user id=GADATA; password=GADATA987; server=SQLA001.gen.volvocars.net;" +
                                                      "Trusted_Connection=no; database=gadata; connection timeout=60");

        public string DsnGADATA { get { return "GADATA"; } }

        public string GADATAconnectionString
        {
            get { return @"ODBC;DSN=" + DsnGADATA + ";Description= GADATA;UID=EqUi;PWD=EqUi;APP=SQLFront;WSID=GNL1004ZCBQC2\\EQUI;DATABASE=GADATA"; }
        }
        public void make_DSN() 
        {
            ODBCManager.CreateDSN(DsnGADATA, "odbc link to sql001.gen.volvocars.net"
                , "sqla001.gen.volvocars.net", "SQL Server", @"C:\windows\system32\SQLSRV32.dll", false, DsnGADATA);
        }

        public void BulkCopyToGadata(string as_schema, DataTable adt_table, string as_destination)
        {
            {
                string connectionString = "user id=GADATA; password=GADATA987; server=SQLA001.gen.volvocars.net; Trusted_Connection=no; database=gadata; connection timeout=30";
                using (SqlConnection connection =
                           new SqlConnection(connectionString))
                {
                    connection.Open();
                    // there is no need to map columns.  
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "[" + as_schema + "].[" + as_destination + "]";
                        try
                        {
                            // Write from the source to the destination.
                            bulkCopy.WriteToServer(adt_table);
                        }
                        catch (Exception ex)
                        {
                           Debugger.Exeption(ex);
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public void RunCommandGadata(string as_command, bool ab_admin = false)
        {
            SqlConnection lconn;
            if (ab_admin)
            { lconn = Gadataconn; }
            else
            {lconn = GadataconnA;}

            try
            {
                lconn.Open();
            }
            catch (Exception e)
            {
               Debugger.Exeption(e);
            }

            try
            {
                using (SqlCommand myCommand = new SqlCommand(as_command, lconn))
                {
                    myCommand.ExecuteNonQuery();
                    Object returnValue = myCommand.ExecuteScalar();
                    try
                    {
                        lconn.Close();
                    }
                    catch (Exception e)
                    {
                       Debugger.Exeption(e);
                    }
                }
            }
            catch (Exception e)
            {
               Debugger.Exeption(e);
            }


        }

        public DataTable RunQueryGadata(string as_query)
        {
            try
            {
                Gadataconn.Open();
            }
            catch (Exception e)
            {
               Debugger.Exeption(e);
            }

            try
            {
                using (SqlCommand myCommand = new SqlCommand(as_query, Gadataconn))
                {
                    DataTable dt = new DataTable();
                    dt.Load(myCommand.ExecuteReader());
                    try
                    {
                        Gadataconn.Close();
                    }
                    catch (Exception e)
                    {
                       Debugger.Exeption(e);
                    }
                    return dt;
                }
            }
            catch (Exception e)
            {
               Debugger.Exeption(e);
                DataTable dt = new DataTable();
                return dt;
            }


        }

        public SqlCommand get_GADATA_sp_parameters(string sp_name)
        {
            SqlCommand cmd = new SqlCommand(sp_name, Gadataconn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Gadataconn.Open();
            }
            catch (Exception e)
            {
               Debugger.Exeption(e);
            }
            try
            {
                    SqlCommandBuilder.DeriveParameters(cmd);
                    Gadataconn.Close();
                    return cmd;
            }
            catch (Exception e)
            {
                Debugger.Exeption(e);
                return cmd;
            }

        }

        //ProcManagerSets
        public void delete_ParmSet(string System, string Procname, string Setname)
        {
            using (applDataTableAdapters.QUERYParametersTableAdapter adapter = new applDataTableAdapters.QUERYParametersTableAdapter())
            {
                adapter.DeleteSet(System, Procname, Setname);
            }
        }

        public void insert_ParmSet(string System,string Procname, string Setname, string Parm, string Value)
        {

            using (applDataTableAdapters.QUERYParametersTableAdapter adapter = new applDataTableAdapters.QUERYParametersTableAdapter())
            {
                adapter.Insert(System, Procname, Setname, "", Parm, Value);
            }
        }

        public string Select_ParmSet_value (string System, string Procname, string Setname, string Parm)
        {
                using (applDataTableAdapters.QUERYParametersTableAdapter adapter = new applDataTableAdapters.QUERYParametersTableAdapter())
                {
                    applData.QUERYParametersDataTable lDT = new applData.QUERYParametersDataTable();
                    adapter.Fill(lDT, System, Procname, Setname, Parm);
                    if (lDT.Count() == 0) return null;
                    return (from a in lDT select a.Value).First();
                }
        }

        public List<string> Select_ParmSet_list(string System, string Procname)
        {
          using (applDataTableAdapters.QUERYParametersTableAdapter adapter = new applDataTableAdapters.QUERYParametersTableAdapter())
                {
                    applData.QUERYParametersDataTable lDT = new applData.QUERYParametersDataTable();
                    adapter.FillByProcname(lDT, System, Procname);
                    if (lDT.Count() == 0) return new List<string>();
                    return (from a in lDT select a.SETNAME).Distinct().ToList();
                }
        }
    }
    
    public static class MyStringExtensions
    {
        public static bool Like(this string toSearch, string toFind)
        {
            if (toSearch == null) { return false; }
            return new Regex(@"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").Replace(toFind, ch => @"\" + ch).Replace('_', '.').Replace("%", ".*") + @"\z", RegexOptions.Singleline).IsMatch(toSearch);
        }
    }
}
