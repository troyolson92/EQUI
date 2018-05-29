﻿using System;
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

                case "SlowSpeed":
                    return "TableLogtypeSLOWSpeed";

                case "BarrelLow":
                    return "TableLogtypeBarrelLOW";

                case "ALERT":
                    return "TableLogtypeAlert";

                default:
                    return "";

            }
        }


        //tiplife tool
        //returns a Css style based on value
        public static string getRowStyleByWearValue(double? pWear, int? nDress, double? nRparts, string Status)
        {

            if (pWear.GetValueOrDefault(0) > 98 || nDress.GetValueOrDefault(0) > 210 ) //severe tiplife
            {
                return "TableTipwearValueDanger";
            } else if(pWear.GetValueOrDefault(0) > 90 || nDress.GetValueOrDefault(0) > 200 ) //mild tiplife
            {
                return "TableTipwearValueHigh";
            }
            else if (Status != "") //data error
            {
                return "PulseBlueRepeat";
            }
            else
            {
                return "";
            }
        }

    }
}