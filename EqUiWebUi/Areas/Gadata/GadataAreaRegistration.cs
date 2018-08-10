using System.Web.Mvc;

namespace EqUiWebUi.Areas.Gadata
{
    public class GadataAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Gadata";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled(AreaName))
            {
                context.MapRoute(
                "Gadata_default",
                "Gadata/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            }
        }
    }
}