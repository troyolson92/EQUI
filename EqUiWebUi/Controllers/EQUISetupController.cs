using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class EQUISetupController : Controller
    {
        // GET: EQUISetup
        public ActionResult Index()
        {
            return View();
        }

        // Get partial settings section.
        public ActionResult _settings()
        {

            return PartialView();
        }

    }
}