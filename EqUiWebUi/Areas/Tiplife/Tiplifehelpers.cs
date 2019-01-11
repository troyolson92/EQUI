using System;

namespace EqUiWebUi.Areas.Tiplife
{
    public static class TiplifeHelpers
    {
        //returns a Css style based on value (used in tiplife)
        public static string getRowStyleByWearValue(double? pWear, int? nDress, double? nRparts, string Status, bool hasTipchanger)
        {
            //only in case of no tip changer animate on % wear and cars
            if (!hasTipchanger)
            {
                if (pWear.GetValueOrDefault(0) > 98 || nDress.GetValueOrDefault(0) > 210) //severe tiplife
                {
                    return "ani_PulseRedFast";
                }
                else if (pWear.GetValueOrDefault(0) > 90 || nDress.GetValueOrDefault(0) > 200) //mild tiplife
                {
                    return "ani_PulseRed";
                }
            }

            //handle status error animation
            if (Status == "Tipchanger")
            {
                return "ani_PulseRed";
            }
            else if (Status != "") //data error
            {
                return "ani_PulseBlueRepeat";
            }
            else //OK
            {
              return ""; 
            }
        }

        //tip life tool WTF is this function doing here SAM?  has nothing to do with rowstyling
        //to calculate the wear
        public static double getPwear(EqUiWebUi.Areas.Tiplife.Models.NGAC_TipDressLogFile tipDressLogFile)
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
    }
}