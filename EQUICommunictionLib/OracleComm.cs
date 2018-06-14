﻿using System;
using System.Data;
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
            try
            {
                using (OracleDataAdapter adapter = new OracleDataAdapter(Query, Conn))
                {
                    adapter.SelectCommand.CommandTimeout = maxEXECtime;
                    DataTable table = new DataTable();
                    adapter.Fill(table);
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

        public string GetClobMaximo7(string as_query, bool RealtimeConn = false)
        {
            try
            {
                if (Conn.State != ConnectionState.Open) { Conn.Open(); }
            }
            catch (Exception ex)
            {
                log.Error("Open Failed", ex);
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
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                log.Error("Command Failed", ex);
                return null;
            }
        }
    }
}
