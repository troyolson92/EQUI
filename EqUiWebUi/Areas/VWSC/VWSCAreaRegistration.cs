using System.Web.Mvc;

namespace EqUiWebUi.Areas.VWSC
{
    public class VWSCAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "VWSC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "VWSC_default",
                "VWSC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}