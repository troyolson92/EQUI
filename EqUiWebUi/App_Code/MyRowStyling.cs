using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EqUiWebUi
{
    public static class MyRowStyling
    {
        //returns a Css style based on status (used in alerts)
        public static string getRowStyleStatus(string status)
        {
            switch (status)
            {
                case "WGK":
                    return "TableStatusWGK";

                case "COMP":
                    return "TableStatusCOMP";

                default:
                    return "";

            }
        }

        //returns a Css style based on logtype
        public static string getRowStyleLogtype(string logtype)
        {
            switch (logtype)
            {
                case "SHIFTBOOK":
                    return "TableLogtypeSHIFTBOOK";

                case "WARNING":
                    return "TableLogtypeWARNING";

                case "LIVE":
                    return "TableLogtypeLIVE";

                case "BREAKDOWN":
                    return "TableLogtypeBREAKDOWN";

                case "TIMELINE":
                    return "TableLogtypeTIMELINE";

                case "SLOWSpeed":
                    return "TableLogtypeSLOWSpeed";

                case "BarrelLOW":
                    return "TableLogtypeBarrelLOW";

                default:
                    return "";

            }
        }

        //returns a Css style based on value
        public static string getRowStyleByWearValue(double? pWear, int? nDress)
        {
            if (pWear.GetValueOrDefault(0) > 98 || nDress.GetValueOrDefault(0) > 210)
            {
                return "TableTipwearValueDanger";
            } else if(pWear.GetValueOrDefault(0) > 80 || nDress.GetValueOrDefault(0) > 200)
            {
                return "TableTipwearValueHigh";
            }
            else
            {
                return "";
            }
        }

    }
}