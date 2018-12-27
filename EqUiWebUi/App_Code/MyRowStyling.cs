using System;

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
                    return "TableLogtypeRed";

                case "OKREQ":
                    return "TableLogtypeDarkYellow";

                case "COMP":
                case "TECHCOMP":
                    return "TableLogtypeLightBlue";

                default:
                    return "";
            }
        }

        //returns a Css style based on log type (used in supervision)
        public static string getRowStyleLogtype(string logtype)
        {
            switch (logtype)
            {
                case "LIVE":
                    return "TableLogtypeRed"; //marks a breakdown that is ongoing 

                case "BREAKDOWN":
                case "STOerror":
                    return "TableLogtypeDarkYellow"; //marks a breakdown that has been resolved

                case "TIMELINE":
                    return "TableLogtypeLightGreen"; //marks begin of each new production shift

                case "ALERT": //COMPLETED ALERT! alert that has been resolved 
                    return "TableLogtypeLightBlue";

                default:
                    return logtype; //this is used for alerts the pass the animation directly (when alert is active else they just pass "ALERT"
            }
        }

        //returns a Css style based on value (used in tiplife)
        public static string getRowStyleByWearValue(double? pWear, int? nDress, double? nRparts, string Status)
        {
            if (pWear.GetValueOrDefault(0) > 98 || nDress.GetValueOrDefault(0) > 210) //severe tiplife
            {
                return "ani_PulseRed";
            }
            else if (pWear.GetValueOrDefault(0) > 90 || nDress.GetValueOrDefault(0) > 200) //mild tiplife
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

        //tip life tool WTF is this function doing here SAM?  has nothing to do with rowstyling
        //to calculate the wear
        public static double getPwear(EqUiWebUi.Areas.Tiplife.Models.TipDressLogFile tipDressLogFile)
        {
            if (tipDressLogFile.Wear_Fixed >= tipDressLogFile.Wear_Move)
            {
                return Math.Round((tipDressLogFile.Wear_Fixed.GetValueOrDefault() / tipDressLogFile.Max_Wear_Fixed.GetValueOrDefault()) * 100, 0);
            }
            else
            {
                return Math.Round((tipDressLogFile.Wear_Move.GetValueOrDefault() / tipDressLogFile.Max_Wear_Move.GetValueOrDefault()) * 100, 0);
            }
        }

        //jens zijn tools
        //AutomaticWorkFlowULPlans
        public static string getRowStyleAutomaticWorkFlowULPlans(string Last30min_productionStatus)
        {
            switch (Last30min_productionStatus)
            {
                case "production running":
                    return "TableLogtypeGreen";

                case "no production":
                    return "TableLogtypeRed";

                case "LOW production":
                    return "TableLogtypeYellow";

                default:
                    return "";
            }
        }
    }
}