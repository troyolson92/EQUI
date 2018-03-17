using System.Web.Mvc;

namespace EqUiWebUi.Areas.HanfireArea
{
    public class HanfireAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HanfireArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HanfireArea_default",
                "HanfireArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}