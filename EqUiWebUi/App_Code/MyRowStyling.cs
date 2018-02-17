using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EqUiWebUi
{
    public static class MyRowStyling
    {
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
    }
}