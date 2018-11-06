﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace EQUICommunictionLib
{
    class OracleComm
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        OracleConnection Conn = new OracleConnection();

        public OracleComm(string ConnectionString)
        {
            Conn.ConnectionString = ConnectionString;
        }

        public DataTable RunQuery(string Query, bool enblExeptions = false, int maxEXECtime = 300)
        {
            //THIS MUST RUN AS CULTRUE en-US or we get BULLSHIT for the maximo reporting connection.
            CultureInfo UserCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            //
            try
            {
                using (OracleDataAdapter adapter = new OracleDataAdapter(Query, Conn))
                {
                    
                    adapter.SelectCommand.CommandTimeout = maxEXECtime;
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    //Set the culture back to orignial.
                    Thread.CurrentThread.CurrentCulture = UserCulture;
                    //
                    return table;
                }
            }
            catch (Exception ex)
            {
                log.Error("Command Failed", ex);
                if (enblExeptions)
                {
                    throw ex;
                }
                DataTable table = new DataTable();
                return table;
            }
        }

        public void RunCommand(string sqlCommand, bool enblExeptions = false, int maxEXECtime = 300)
        {
            //never needed this before but its already here... :) 
            throw new NotImplementedException();
        }

        public string GetCLOB(string as_query, bool enblExeptions = false)
        {
            try
            {
                if (Conn.State != ConnectionState.Open) { Conn.Open(); }
            }
            catch (Exception ex)
            {
                log.Error("Open Failed", ex);
                if (enblExeptions)
                {
                    throw ex;
                }

            }
            try
            {
                using (OracleCommand myCommand = new OracleCommand(as_query, Conn))
                {
                    OracleDataReader dr = myCommand.ExecuteReader();
                    dr.Read();
                    OracleClob Clob = dr.GetOracleClob(0);
                    string result = Clob.Value;
                    Clob.Close();
                    dr.Close();
                    try
                    {
                        Conn.Close();
                    }
                    catch (Exception ex)
                    {
                        log.Error("Close Failed", ex);
                        if (enblExeptions)
                        {
                            throw ex;
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                log.Error("Command Failed", ex);
                //disbale this how to I handle an empty clob ? 
                /*
                if (enblExeptions)
                {
                    throw ex;
                }
                */
                return null;
            }
        }

        public bool CheckPassWordExpired()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = Conn;

                try
                {
                    cmd.Connection.Open();
                }
                catch (OracleException ex)
                {
                    //allow to continue if the password is simply expired, otherwise just show the message
                    if (ex.Number != 28001)
                    {
                        log.Error("Non Authenticaion error",ex);
                        throw ex;
                    }
                    else
                    {
                        log.Info("Password is expired");
                        return true;
                    }
                }
            return false;
        }

        public void ChangePassWord(string newPW = "")
        {
            // THIS IS BULLSHIT BLOCK AND IS NOT SUPPORTED IN ORACLE AT ALL 
            throw new NotSupportedException();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = Conn;
            if (Conn.State != ConnectionState.Open) { Conn.Open(); }
            cmd.CommandText = "SysChangePassword";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlConnectionStringBuilder Connbuilder = new SqlConnectionStringBuilder(Conn.ConnectionString);
            cmd.Parameters.Add("username", Connbuilder.UserID);
            cmd.Parameters.Add("newpassword", newPW);
            string debug = cmd.ToString();
            cmd.ExecuteNonQuery();
            log.Info("Password has been changed");
        }
    }
}
