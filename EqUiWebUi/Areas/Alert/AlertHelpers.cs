namespace EqUiWebUi.Areas.Alert
{
    public static class AlertHelpers
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
    }
}