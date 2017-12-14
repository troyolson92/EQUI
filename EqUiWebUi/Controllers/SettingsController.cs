using EqUiWebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult GetUserGrants(string username)
        {
            GADATAEntities gADATAEntities = new GADATAEntities();
            List<UserPermisions> data = (from userPermisions in gADATAEntities.UserPermisions
                                     select userPermisions).ToList();

            return View(data);
        }
    }
}