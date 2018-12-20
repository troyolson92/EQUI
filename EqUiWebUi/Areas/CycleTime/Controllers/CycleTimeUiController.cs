using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.CycleTime.Controllers
{
    public class CycleTimeUiController : Controller
    {
        // GET: CycleTime/CycleTimeUi
        public ActionResult Index()
        {
            BackgroundWork backgroundWork = new BackgroundWork();
            backgroundWork.GetP4CycleTime(null);

            return View();
        }
    }
}