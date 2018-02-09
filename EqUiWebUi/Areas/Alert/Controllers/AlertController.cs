using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Alert.Controllers
{
    public class AlertController : Controller
    {
        // GET: Alert/Alert
        public ActionResult Index()
        {
            return View();
        }

        public void configureHangfire()
        {
            AlertEngine alertEngine = new AlertEngine();
            alertEngine.ConfigureHangfireAlertWork();
        }

    }
}