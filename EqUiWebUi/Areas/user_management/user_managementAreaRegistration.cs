using System.Web.Mvc;

namespace EqUiWebUi.Areas.user_management
{
    public class user_managementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "user_management";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "user_management_default",
                "user_management/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}