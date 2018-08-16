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

                case "STOerror":
                    return "TableLogtypeSTO";

                case "TIMELINE":
                    return "TableLogtypeTIMELINE";

                case "ALERT":
                    return "TableLogtypeAlert";

                default:
                    return logtype; //this is used for alerts the pass the animation direcly (when alert is active else they just pass "ALERT"

            }
        }


        //tiplife tool
        //returns a Css style based on value
        public static string getRowStyleByWearValue(double? pWear, int? nDress, double? nRparts, string Status)
        {

            if (pWear.GetValueOrDefault(0) > 98 || nDress.GetValueOrDefault(0) > 210 ) //severe tiplife
            {
                return "ani_PulseRed";
            } else if(pWear.GetValueOrDefault(0) > 90 || nDress.GetValueOrDefault(0) > 200 ) //mild tiplife
            {
                return "ani_PulseRedFast";
            }
            else if (Status != "") //data error
            {
                return "ani_PulseBlueRepeat";
            }
            else
            {
                return "";
            }
        }

    }
}