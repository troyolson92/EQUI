using System.Web.Mvc;

namespace EqUiWebUi.Areas.CycleTime
{
    public class CycleTimeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CycleTime";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled(AreaName))
            {
                context.MapRoute(
                "CycleTime_default",
                "CycleTime/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                );
            }
        }
    }
}