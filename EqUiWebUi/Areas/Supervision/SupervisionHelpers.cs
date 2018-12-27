namespace EqUiWebUi.Areas.Supervision
{
    public static class SupervisionHelpers
    {

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
    }
}