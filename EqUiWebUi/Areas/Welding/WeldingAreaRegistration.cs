using System.Web.Mvc;

namespace EqUiWebUi.Areas.Welding
{
    public class WeldingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Welding";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled(AreaName))
            {
                context.MapRoute(
                "Welding_default",
                "Welding/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                );
            }
        }
    }
}