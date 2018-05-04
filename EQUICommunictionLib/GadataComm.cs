using System;
using System.Data;
using System.Data.SqlClient;
using log4net;


namespace EQUICommunictionLib
{
    public class GadataComm
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //connection to GADATA
        SqlConnection Gadataconn = new SqlConnection("user id=EqUi; password=EqUi; server=SQLA001.gen.volvocars.net;" +
                                                      "Trusted_Connection=no; database=gadata; connection timeout=15");

        //conntion to GADATA admin
        SqlConnection GadataconnAdmin = new SqlConnection("user id=EqUiAdmin; password=EqUiAdmin; server=SQLA001.gen.volvocars.net;" +
                                                      "Trusted_Connection=no; database=gadata; connection timeout=15");

        public string DsnGADATA { get { return "GADATA"; } }

        public string GADATAconnectionString
        {
            get { return @"ODBC;DSN=" + DsnGADATA + ";Description= GADATA;UID=EqUi;PWD=EqUi;APP=SQLFront;WSID=GNL1004ZCBQC2\\EQUI;DATABASE=GADATA"; }
        }
        public void Make_DSN() 
        {
            ODBCManager.CreateDSN(DsnGADATA, "odbc link to sql001.gen.volvocars.net"
                , "sqla001.gen.volvocars.net", "SQL Server", @"C:\windows\system32\SQLSRV32.dll", false, DsnGADATA);
            log.Debug("Created DSN in regEx for GADATA");
        }

        public void BulkCopyToGadata(string as_schema, DataTable adt_table, string as_destination, bool runAsAdmin = true, bool enblExeptions = false, int maxEXECtime = 300)
        {
            SqlConnection lconn;
            if (runAsAdmin)
            {
                lconn = GadataconnAdmin;
            }
            else
            {
                lconn = Gadataconn;
            }

            try
            {
                lconn.Open();
            }
            catch (Exception ex)
            {
                log.Error("Connection Failed", ex);
                if (enblExeptions)
                {
                    throw ex;
                }
            }

            try
            {
                // there is no need to map columns.  
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(lconn))
                {
                    bulkCopy.BulkCopyTimeout = maxEXECtime;
                    bulkCopy.DestinationTableName = "[" + as_schema + "].[" + as_destination + "]";
                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(adt_table);
                        try
                        {
                            lconn.Close();
                        }
                        catch (Exception ex)
                        {
                            log.Error("Failed to close", ex);
                            if (enblExeptions)
                            {
                                throw ex;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                log.Error("Bulkcopy failed", ex);
                if (enblExeptions)
                {
                    throw ex;
                }
            }
        }

        public void RunCommandGadata(string sqlCommand, bool runAsAdmin = false, bool enblExeptions = false, int maxEXECtime  = 300)
        {
            SqlConnection lconn;
            if (runAsAdmin)
            {
                lconn = GadataconnAdmin;
            }else
            {
                lconn = Gadataconn;
            }

            try
            {
                lconn.Open();
            }
            catch (Exception ex)
            {
                log.Error("Connection Failed",ex);
                if (enblExeptions)
                {
                    throw ex;
                }
            }

            try
            {
                using (SqlCommand myCommand = new SqlCommand(sqlCommand, lconn))
                {
                    myCommand.CommandTimeout = maxEXECtime;
                    myCommand.ExecuteNonQuery();

                    try
                    {
                        lconn.Close();
                    }
                    catch (Exception ex)
                    {
                        log.Error("Failed to close",ex);
                        if (enblExeptions)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Command failed",ex);
                if (enblExeptions)
                {
                    throw ex;
                }
            }


        }

        public DataTable RunQueryGadata(string sqlQuery, bool runAsAdmin = false, bool enblExeptions = false, int maxEXECtime = 300)
        {
            SqlConnection lconn;
            if (runAsAdmin)
            {
                lconn = GadataconnAdmin;
            }
            else
            {
                lconn = Gadataconn;
            }

            try
            {
                lconn.Open();
            }
            catch (Exception ex)
            {
                log.Error("Connection Failed", ex);
                if (enblExeptions)
                {
                    throw ex;
                }
            }

            try
            {
                using (SqlCommand myCommand = new SqlCommand(sqlQuery, lconn))
                {
                    DataTable dt = new DataTable();
                    myCommand.CommandTimeout = maxEXECtime;
                    dt.Load(myCommand.ExecuteReader());
                    try
                    {
                        lconn.Close();
                    }
                    catch (Exception ex)
                    {
                        log.Error("Failed to close", ex);
                        if (enblExeptions)
                        {
                            throw ex;
                        }
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                log.Error("Command failed", ex);
                if (enblExeptions)
                {
                    throw ex;
                }
                DataTable dt = new DataTable();
                return dt;
            }


        }

        public SqlCommand Get_GADATA_sp_parameters(string sp_name)
        {
            SqlCommand cmd = new SqlCommand(sp_name, Gadataconn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Gadataconn.Open();
            }
            catch (Exception ex)
            {
                log.Error("Connection Failed", ex);
            }
            try
            {
                    SqlCommandBuilder.DeriveParameters(cmd);
                    Gadataconn.Close();
                    return cmd;
            }
            catch (Exception ex)
            {
                log.Error("Command failed", ex);
                return cmd;
            }

        }

        public void InsertSnaphotGadata(int id, string htmlResult)
        {
            SqlConnection lconn = Gadataconn;
            try
            {
                lconn.Open();
            }
            catch (Exception ex)
            {
                log.Error("Connection Failed", ex);
            }
            try
            {
                using (SqlCommand myCommand = new SqlCommand("INSERT INTO GADATA.EqUi.l_querySnapshots VALUES(@id, getdate(), @htmlresult)", lconn))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myCommand.Parameters.AddWithValue("@htmlresult", htmlResult);

                    myCommand.CommandTimeout = 300;
                    myCommand.ExecuteNonQuery();
                    try
                    {
                        lconn.Close();
                    }
                    catch (Exception ex)
                    {
                        log.Error("Failed to close", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Command failed", ex);
            }


        }

        public DataTable RunParametercommand(string proc, SqlCommand sqlCommand)
        {
            SqlConnection lconn = Gadataconn;
            try
            {
                lconn.Open();
            }
            catch (Exception ex)
            {
                log.Error("Connection Failed", ex);
            }
            try
            {
                sqlCommand.Connection = lconn;
                sqlCommand.CommandText = proc;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 300;
                DataTable dt = new DataTable();
                dt.Load(sqlCommand.ExecuteReader());

                    try
                    {
                        lconn.Close();
                    }
                    catch (Exception ex)
                    {
                        log.Error("Failed to close", ex);
                    }

                    return dt;
            }
            catch (Exception ex)
            {
                log.Error("Command failed", ex);
                DataTable dt = new DataTable();
                return dt;
            }


        }
    }
    
}
