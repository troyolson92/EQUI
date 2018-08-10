using System.Web.Mvc;

namespace EqUiWebUi.Areas.Maximo_ui
{
    public class Maximo_uiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Maximo_ui";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled(AreaName))
            {
                context.MapRoute(
                "Maximo_ui_default",
                "Maximo_ui/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                );
            }
        }
    }
}