using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Tiplife.Controllers
{
    /// <summary>
    /// control handles electrode data SPECIFC to Comau
    /// </summary>
    public class ComauElectrodeController : Controller
    {
        // GET: Tiplife/ComauElectrode
        public ActionResult Index()
        {
            return View();
        }
    }
}