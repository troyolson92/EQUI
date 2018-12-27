namespace EqUiWebUi.Areas.Welding
{
    public static class WeldingHelpers
    {
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