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

        [HttpGet]
        public ActionResult CookieSet()
        {
            HttpCookie cookie = new HttpCookie("EQUICookie");
            cookie["Name"] = "test";
            cookie["iets"] = "bla";
            cookie["num"] = "10";
            Response.Cookies.Add(cookie);

            return View();
        }

        [HttpGet]
        public ActionResult CookieGet()
        {
            HttpCookie cookie = Request.Cookies["EQUICookie"];
            if (cookie != null)
            {
                string name = cookie["Name"];
                string iets = cookie["iets"];


            }

            return View();
        }
    }
}