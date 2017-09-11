using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace EQUICommunictionLib
{
    public class MaximoComm
    {
        OracleConnection Maximo7conn = new OracleConnection(
          "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=gotsvl2149.got.volvocars.net)(PORT=1521)) (CONNECT_DATA=(SID=dpmxarct)));User Id=ARCTVCG;Password=vcg$tokfeb2017;");
       
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

        public void make_DSN(string System)
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

        //
        public DataTable oracle_runQuery(string Query)
        {
            try
            {
                using (OracleDataAdapter dadapter = new OracleDataAdapter(Query, Maximo7conn))
                {
                    //get location and asset data from maximo
                    DataTable table = new DataTable();
                    dadapter.Fill(table);
                    return table;
                }
            }
            catch (Exception e)
            {
               Debugger.Exeption(e);
                DataTable table = new DataTable();
                return table;
            }

        }

        //comm to maximo

        public string GetClobMaximo7(string as_query)
        {
            try
            {
                if (Maximo7conn.State != ConnectionState.Open) { Maximo7conn.Open(); }
            }
            catch (Exception e)
            {
               Debugger.Exeption(e);
            }
            try
            {
                using (OracleCommand myCommand = new OracleCommand(as_query, Maximo7conn))
                {
                    OracleDataReader dr = myCommand.ExecuteReader();
                    dr.Read();
                    OracleClob Clob = dr.GetOracleClob(0);
                    string result = Clob.Value;
                    Clob.Close();
                    dr.Close();
                    try
                    {
                       Maximo7conn.Close();
                    }
                    catch (Exception e)
                    {
                       Debugger.Exeption(e);
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
               Debugger.Exeption(e);
                return null;
            }
        }

        public string getMaximoDetails(string wonum)
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
            sb.AppendLine(StringToHTML_Table("LONGDESCRIPTION", GetClobMaximo7(cmdLONGDESCRIPTION))).AppendLine(newline);
            sb.AppendLine(StringToHTML_Table("FAILUREREMARK", GetClobMaximo7(cmdFAILUREREMARK))).AppendLine(newline);
            sb.AppendLine(StringToHTML_Table("LABOR", DtToHTML_Table(oracle_runQuery(cmdLabor)))).AppendLine(newline);
            sb.AppendLine(StringToHTML_Table("WORKLOG", DtToHTML_Table(oracle_runQuery(cmdWorkLog)))).AppendLine(newline);

            DataTable dt = oracle_runQuery(cmdWorkLog);

            //
            return sb.ToString();
        }

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
