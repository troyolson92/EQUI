using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.Odbc;
using System.Text.RegularExpressions;

namespace EQUICommunictionLib
{
    public class GadataComm
    {
        //debugger
        Debugger Debugger = new Debugger();
        //connection to GADATA
        SqlConnection Gadataconn = new SqlConnection("user id=EqUi; password=EqUi; server=SQLA001.gen.volvocars.net;" +
                                                      "Trusted_Connection=no; database=gadata; connection timeout=30");

        //conntion to GADATA admin
        SqlConnection GadataconnAdmin = new SqlConnection("user id=GADATA; password=GADATA987; server=SQLA001.gen.volvocars.net;" +
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
                using (Gadataconn)
                {
                    Gadataconn.Open();
                    // there is no need to map columns.  
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Gadataconn))
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
                    Gadataconn.Close();
                    Gadataconn.Dispose();
                }
            }
        }

        public void RunCommandGadata(string as_command, bool ab_admin = false)
        {
            SqlConnection lconn;
            if (ab_admin)
            { lconn = GadataconnAdmin; }
            else
            {lconn = Gadataconn;}

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
