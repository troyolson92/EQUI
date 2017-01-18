using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelAddInEquipmentDatabase
{
    class MaximoComm
    {
        public string MX7connectionString
        {
            get{return @"ODBC;DSN=Max;Description= Max;UID=BGASTHUY;PWD=BGASTHUY$123;"; }
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
