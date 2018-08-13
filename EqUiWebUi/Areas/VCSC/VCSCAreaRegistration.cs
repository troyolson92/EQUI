using System.Web.Mvc;

namespace EqUiWebUi.Areas.VCSC
{
    public class VCSCAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "VCSC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled(AreaName))
            {
                context.MapRoute(
                "VCSC_default",
                "VCSC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            }
        }
    }
}