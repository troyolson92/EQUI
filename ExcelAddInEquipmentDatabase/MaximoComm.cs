﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelAddInEquipmentDatabase
{
    class MaximoComm
    {
        public string MX7connectionString
        {
            get { return @"ODBC;DSN=" + DsnMX7 + ";Description= MAXIMO7;UID=ARCTVCG;PWD=volvo123;"; }
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

    }
    public class OracleQueryParm
    {
        public string ParameterName { get; set; }
        public string Defaultvalue { get; set; }
    }
}
