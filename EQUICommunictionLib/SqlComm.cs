﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace EQUICommunictionLib
{
    class SqlComm
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        SqlConnection Conn = new SqlConnection();

        public SqlComm(string ConnectionString)
        {
            Conn.ConnectionString = ConnectionString;
        }

        public void BulkCopy(string as_schema, DataTable adt_table, string as_destination, bool enblExeptions = false, int maxEXECtime = 300)
        {
            try
            {
                Conn.Open();
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
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Conn))
                {
                    bulkCopy.BulkCopyTimeout = maxEXECtime;
                    bulkCopy.DestinationTableName = "[" + as_schema + "].[" + as_destination + "]";
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(adt_table);
                    try
                    {
                        Conn.Close();
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

        public void RunCommand(string sqlCommand, bool enblExeptions = false, int maxEXECtime = 300)
        {
            try
            {
                Conn.Open();
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
                using (SqlCommand myCommand = new SqlCommand(sqlCommand, Conn))
                {
                    myCommand.CommandTimeout = maxEXECtime;
                    myCommand.ExecuteNonQuery();

                    try
                    {
                        Conn.Close();
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
                log.Error("Command failed", ex);
                if (enblExeptions)
                {
                    throw ex;
                }
            }


        }

        public DataTable RunQuery(string sqlQuery, bool enblExeptions = false, int maxEXECtime = 300)
        {
            try
            {
                Conn.Open();
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
                using (SqlCommand myCommand = new SqlCommand(sqlQuery, Conn))
                {
                    DataTable dt = new DataTable();
                    myCommand.CommandTimeout = maxEXECtime;
                    dt.Load(myCommand.ExecuteReader());
                    try
                    {
                        Conn.Close();
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

        public SqlCommand Get_sp_parameters(string sp_name)
        {
            SqlCommand cmd = new SqlCommand(sp_name, Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Conn.Open();
            }
            catch (Exception ex)
            {
                log.Error("Connection Failed", ex);
            }
            try
            {
                SqlCommandBuilder.DeriveParameters(cmd);
                Conn.Close();
                return cmd;
            }
            catch (Exception ex)
            {
                log.Error("Command failed", ex);
                return cmd;
            }

        }

        public DataTable RunParametercommand(string proc, SqlCommand sqlCommand)
        {
            try
            {
                Conn.Open();
            }
            catch (Exception ex)
            {
                log.Error("Connection Failed", ex);
            }
            try
            {
                sqlCommand.Connection = Conn;
                sqlCommand.CommandText = proc;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 300;
                DataTable dt = new DataTable();
                dt.Load(sqlCommand.ExecuteReader());

                try
                {
                    Conn.Close();
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

