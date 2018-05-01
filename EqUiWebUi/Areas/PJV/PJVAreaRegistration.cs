using System.Web.Mvc;

namespace EqUiWebUi.Areas.PJV
{
    public class PJVAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PJV";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PJV_default",
                "PJV/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}