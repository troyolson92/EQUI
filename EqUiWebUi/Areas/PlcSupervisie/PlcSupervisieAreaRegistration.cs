using System.Web.Mvc;

namespace EqUiWebUi.Areas.PlcSupervisie
{
    public class PlcSupervisieAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PlcSupervisie";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled(AreaName))
            {
                context.MapRoute(
                "PlcSupervisie_default",
                "PlcSupervisie/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
               );
            }
        }
    }
}