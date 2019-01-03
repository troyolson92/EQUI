using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ExcelAddInEquipmentDatabase
{
    class EquiModelMetaData
    {
    }

    //QUERYS meta data
    [MetadataType(typeof(QUERYS))]
    public partial class QUERYS
    {
        public string QueryBody
        {
            get {
                return QUERY.Trim().TrimEnd(';').ToUpper();
            }
        }

        public List<OracleQueryParm> OracleQueryParms
        {
            get
            {
                //gets part of the query containing the params
                List<string> ParmLines = QUERY.ToUpper().Split(new string[] { "SELECT" }, StringSplitOptions.None)[0].Trim().Split(new string[] { "DEFINE" }, StringSplitOptions.None).ToList();
                List<OracleQueryParm> ParmList = new List<OracleQueryParm>();
                foreach (string parm in ParmLines)
                {
                    if (parm.Contains("="))
                    {
                        string ParmName = parm.Split('=')[0].Trim();
                        string ParmValue = parm.Split('=')[1].Trim().Split('\'')[1];
                        ParmList.Add(new OracleQueryParm { ParameterName = ParmName, Defaultvalue = ParmValue });
                    }
                }
                return ParmList;
            }
        }

    }

    public class OracleQueryParm
    {
        public string ParameterName { get; set; }
        public string Defaultvalue { get; set; }
    }
}
