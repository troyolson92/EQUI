using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace ExcelAddInEquipmentDatabase
{
    class MaximoComm
    {
        OracleConnection Maximo7conn = new OracleConnection("data source = dpmxarct;user id = ARCTVCG;password=vcg$tokfeb2017");
        //debugger
        Debugger Debugger = new Debugger();

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

        //maximo comm TO GADATA

        public void oracle_update_Query_to_GADATA(string System, string Queryname, string QueryDiscription, string Query)
        {
            using (applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable())
            {
                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    var  ds = from a in lQUERYS
                         where a.SYSTEM == System && a.NAME == Queryname
                         select a;
                    //adapter.Update()
                  
                }
            }
        }

        public void oracle_delete_Query_GADATA(string System, string Queryname)
             {
             using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    adapter.Delete(System, Queryname);
                }
             }

        public void oracle_send_new_Query_to_GADATA(string System, string Queryname, string QueryDiscription, string Query)
        {

                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    adapter.Insert(System, Queryname, QueryDiscription, Query);
                }
        }

        public string oracle_get_QueryTemplate_from_GADATA(string QueryName, string System)
        {
            string Query;
            using (applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable())
            {
                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    adapter.Fill(lQUERYS);
                }
                Query = (from a in lQUERYS
                         where a.SYSTEM == System && a.NAME == QueryName
                         select a.QUERY).First().ToString();
            }
            return Query.Trim().TrimEnd(';').ToUpper();
        }

        public string oracle_get_QueryDiscription_from_GADATA(string QueryName, string System)
        {
            string Disciption;
            using (applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable())
            {
                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    adapter.Fill(lQUERYS);
                }
                Disciption = (from a in lQUERYS
                         where a.SYSTEM == System && a.NAME == QueryName
                         select a.DISCRIPTION).First().ToString();
            }
            return Disciption.Trim();
        }

        public List<OracleQueryParm> oracle_get_QueryParms_from_GADATA(string QueryName, string System)
        {
            string Query;
            using (applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable())
            {
                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    adapter.Fill(lQUERYS);
                }
                Query = (from a in lQUERYS
                         where a.SYSTEM == System && a.NAME == QueryName
                         select a.QUERY).First().ToString();
            }
            //gets part of the query containing the params
            List<string> ParmLines = Query.ToUpper().Split(new string[] { "SELECT" }, StringSplitOptions.None)[0].Trim()
                                                    .Split(new string[] { "DEFINE" }, StringSplitOptions.None).ToList();

            List<OracleQueryParm> _parmList = new List<OracleQueryParm>();
            foreach (string parm in ParmLines)
            {
                if (parm.Contains("="))
                {
                    string ParmName = parm.Split('=')[0].Trim();
                    string ParmValue = parm.Split('=')[1].Trim().Split('\'')[1];
                    _parmList.Add(new OracleQueryParm { ParameterName = ParmName, Defaultvalue = ParmValue });
                }
            }
            return _parmList;
        }

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
                    builder.Append(r[c.ColumnName]);
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
    public class OracleQueryParm
    {
        public string ParameterName { get; set; }
        public string Defaultvalue { get; set; }
    }
}
