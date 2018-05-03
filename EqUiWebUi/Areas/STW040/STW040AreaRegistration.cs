using System.Web.Mvc;

namespace EqUiWebUi.Areas.STW040
{
    public class STW040AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "STW040";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "STW040_default",
                "STW040/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}