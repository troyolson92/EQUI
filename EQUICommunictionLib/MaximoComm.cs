using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using log4net;

namespace EQUICommunictionLib
{
    public class MaximoComm
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        OracleConnection MaximoReportingConn = new OracleConnection(
          "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=gotsvl2149.got.volvocars.net)(PORT=1521)) (CONNECT_DATA=(SID=dpmxarct)));User Id=ARCTVCG;Password=vcg$tokfeb2017;");

        OracleConnection MaximoRealtimeConn = new OracleConnection(
          "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=gotora1mxa.got.volvocars.net)(PORT=1521)) (CONNECT_DATA=(SID=DPMXADGP)));User Id=FDENAYER;Password=volvo456;");

        //debugger
        myDebugger Debugger = new myDebugger();

        public string MX7connectionString
        {
            get { return @"ODBC;DSN=" + DsnMX7 + ";Description= MAXIMO7;UID=ARCTVCG;PWD=vcg$tokfeb2017;"; }
        }
        public string SystemMX7 { get { return "MX7"; } }
        public string SystemMX3 { get { return "MX3"; } }
        public string DsnMX7 { get { return "MAXIMO7"; } }
        public string DsnMX3 { get { return "MAXIMO3"; } }

        public void Make_DSN(string System)
        {
            if (System == SystemMX7)
            {
                ODBCManager.CreateDSN(DsnMX7, "odbc link MAXIMO7", "dpmxarct"
                    , "MAXIMO7 ODBC for oracle", @"C:\windows\system32\msorcl32.dll", true, "MAXIMO");
            }
            else if (System == SystemMX3)
            {
                throw new Exception("Not implemented");
              //  ODBCManager.CreateDSN(DsnMX3, "odbc link MAXIMO7", "dpmxarct"
              //   , "MAXIMO7 ODBC for oracle", @"C:\windows\system32\msorcl32.dll", true, "MAXIMO");

            }

        }

        public DataTable Oracle_runQuery(string Query, bool RealtimeConn = false, bool enblExeptions = false, int maxEXECtime = 300)
        {
            OracleConnection activeConn = MaximoReportingConn;
            if (RealtimeConn) { activeConn = MaximoRealtimeConn; }

            try
            {
                using (OracleDataAdapter adapter = new OracleDataAdapter(Query, activeConn))
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

        public string GetClobMaximo7(string as_query, bool RealtimeConn = false)
        {
            OracleConnection activeConn = MaximoReportingConn;
            if (RealtimeConn) { activeConn = MaximoRealtimeConn; }

            try
            {
                if (activeConn.State != ConnectionState.Open) { activeConn.Open(); }
            }
            catch (Exception ex)
            {
                log.Error("Open Failed", ex);
            }
            try
            {
                using (OracleCommand myCommand = new OracleCommand(as_query, activeConn))
                {
                    OracleDataReader dr = myCommand.ExecuteReader();
                    dr.Read();
                    OracleClob Clob = dr.GetOracleClob(0);
                    string result = Clob.Value;
                    Clob.Close();
                    dr.Close();
                    try
                    {
                        activeConn.Close();
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


        //dePRECIATED ? 
        /*
        public string GetMaximoDetails(string wonum,bool RealtimeConn = false)
        {
            string cmdFAILUREREMARK = (@"
                select  NVL2(LD.LDTEXT, LD.LDTEXT, '') LDTEXT
                from MAXIMO.FAILUREREMARK FM 
                left join MAXIMO.LONGDESCRIPTION LD on LD.LDKEY = FM.FAILUREREMARKID AND LD.LDOWNERTABLE = 'FAILUREREMARK'
                where fm.wonum = '{0}'
            ");
            cmdFAILUREREMARK = string.Format(cmdFAILUREREMARK, wonum);
            //
            string cmdLONGDESCRIPTION = (@"
                select NVL2(LD.LDTEXT, LD.LDTEXT, '') LDTEXT
                from MAXIMO.WORKORDER WO 
                left join MAXIMO.LONGDESCRIPTION LD on LD.LDKEY = WO.WORKORDERID AND LD.LDOWNERTABLE = 'WORKORDER'
                where WO.wonum = '{0}'
            ");
            cmdLONGDESCRIPTION = string.Format(cmdLONGDESCRIPTION, wonum);
            //
            string cmdLabor = (@"
            select 
             LABORCODE
            ,PERSON.DISPLAYNAME
            ,CRAFT
            ,PAYRATE
            ,PERSON.SUPERVISOR
            ,LABTRANS.ENTERDATE
            ,REGULARHRS
            ,to_timestamp('12/30/1899 00:00:00', 'MM/DD/YYYY  hh24:mi:ss') + REGULARHRS / 24 Converted
            from MAXIMO.LABTRANS  LABTRANS 
            left join MAXIMO.PERSON ON PERSON.PERSONID = LABTRANS.LABORCODE
            where LABTRANS.REFWO  = '{0}'
            ");
            cmdLabor = string.Format(cmdLabor, wonum);
            //
            string cmdWorkLog = (@"
            select 
            wl.logtype
            ,wl.CREATEBY
            ,wl.CREATEDATE
            ,wl.CLIENTVIEWABLE
            ,wl.DESCRIPTION
            ,ld.LDTEXT 
            from maximo.worklog wl
            left join maximo.longdescription ld  on 
            ld.ldownertable = 'WORKLOG'  
            AND  ld.ldownercol = 'DESCRIPTION'
            AND  ld.LDKEY = wl.WORKLOGID
            where
            wl.RECORDKEY = '{0}'
            ");
            cmdWorkLog = string.Format(cmdWorkLog, wonum);
            //
            StringBuilder sb = new StringBuilder();
            string newline = "<p></p>";
            sb.AppendLine(StringToHTML_Table("LONGDESCRIPTION", GetClobMaximo7(cmdLONGDESCRIPTION,RealtimeConn:RealtimeConn))).AppendLine(newline);
            sb.AppendLine(StringToHTML_Table("FAILUREREMARK", GetClobMaximo7(cmdFAILUREREMARK, RealtimeConn: RealtimeConn))).AppendLine(newline);
            sb.AppendLine(StringToHTML_Table("LABOR", DtToHTML_Table(Oracle_runQuery(cmdLabor, RealtimeConn: RealtimeConn)))).AppendLine(newline);
            sb.AppendLine(StringToHTML_Table("WORKLOG", DtToHTML_Table(Oracle_runQuery(cmdWorkLog, RealtimeConn: RealtimeConn)))).AppendLine(newline);
            //
            return sb.ToString();
        }
        */
        
        public  string DtToHTML_Table(DataTable dt)
        {
            if (dt.Rows.Count == 0) return ""; // enter code here

            StringBuilder builder = new StringBuilder();
            builder.Append("<html>");
            builder.Append("<head>");
            builder.Append("<title>");
            builder.Append("Page-");
            builder.Append(Guid.NewGuid());
            builder.Append("</title>");
            builder.Append("</head>");
            builder.Append(@"<style>

ins { 
    background-color: #cfc; 
    text-decoration: none; } 
del { 
    color: #999; 
    background-color:#FEC8C8; } 
</style>");
            builder.Append("<body>");
            builder.Append("<table border='3px' cellpadding='5' cellspacing='0' ");
            builder.Append("style='border: solid 2px Silver; font-size: small;'>");
            builder.Append("<tr align='left' valign='top'>");
            foreach (DataColumn c in dt.Columns)
            {
                builder.Append("<td align='left' valign='top'><b>");
                builder.Append(c.ColumnName);
                builder.Append("</b></td>");
            }
            builder.Append("</tr>");
            foreach (DataRow r in dt.Rows)
            {
                builder.Append("<tr align='left' valign='top'>");
                foreach (DataColumn c in dt.Columns)
                {
                    builder.Append("<td align='left' valign='top'>");
                    //manipulate to change from asci carigage return to HTML break
                    string message = r[c.ColumnName].ToString()
                               .Replace(Environment.NewLine, "<br />")
                               .Replace("\n", "<br />")
                               .Replace("\r", "<br />");
                    //
                    builder.Append(message);
                    builder.Append("</td>");
                }
                builder.Append("</tr>");
            }
            builder.Append("</table>");
            builder.Append("</body>");
            builder.Append("</html>");
            return builder.ToString();
        }

        public  string StringToHTML_Table(string header, string input)
        {

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(header);
            DataRow rw = dt.NewRow();
            if (input == null || input == "")
            {
                rw[header] = "null (no data)";
            }
            else
            {
                rw[header] = input;
            }
            dt.Rows.Add(rw);
            return DtToHTML_Table(dt);
        }

    }

}
