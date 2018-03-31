using System.Web.Mvc;

namespace EqUiWebUi.Areas.VASC
{
    public class VASCAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "VASC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "VASC_default",
                "VASC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}