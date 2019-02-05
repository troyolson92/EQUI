using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Welding.Controllers
{
    public class QteamConfigController : Controller
    {
         [Authorize(Roles = "QteamPoweruser")]

        public ActionResult Config()
        {
            return View();
        }
    }
}