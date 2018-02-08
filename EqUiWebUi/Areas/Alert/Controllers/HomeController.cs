using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Alert.Controllers
{
    public class HomeController : Controller
    {
        // GET: Alert/Home
        public ActionResult Index()
        {
            return View();
        }
<<<<<<< HEAD

        public ActionResult ConfigHangfire()
        {
            AlertEngine alertEngine = new AlertEngine();
            alertEngine.ConfigureHangfireAlertWork();
            return View();
        }
=======
>>>>>>> 5db2146... start implementing new alert system (and sms controller)
    }
}