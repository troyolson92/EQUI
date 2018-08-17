using EqUiWebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AssetsController : Controller
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();
        // GET: Assets
        public ActionResult Index()
        {
            return View(db.ASSETS.AsQueryable());
        }
    }
}