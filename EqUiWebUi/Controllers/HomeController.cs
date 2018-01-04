using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Models;

namespace EqUiWebUi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var cookie = Request.Cookies["equi_user"];
            if (cookie == null)
            {
                try
                {
                    //test with cookie make cookie in main view
                    cookie = new HttpCookie("equi_user");
                    cookie["User"] = System.Web.HttpContext.Current.User.Identity.Name;
                    GADATAEntities gADATAEntities = new GADATAEntities();
                    cookie["LocationRoot"] = gADATAEntities.UserPermisions.FirstOrDefault(u => u.username == System.Web.HttpContext.Current.User.Identity.Name).LocationRoot;
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                }
                catch (Exception ex)
                {
                    //failed to build a cookie make a default one
                    //test with cookie make cookie in main view
                    cookie = new HttpCookie("equi_user");
                    cookie["User"] = System.Web.HttpContext.Current.User.Identity.Name;
                    cookie["LocationRoot"] = "VCG -> A";
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                }
            }
            return View();
        }


        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        //render an external site in our content (embedded)
        public ActionResult Rendersite(string url)
        {
            ViewBag.redirectURL = url;
            return View();
        }

        public ActionResult Rendersites(string url1, string url2, string url3, string url4)
        {
            ViewBag.url1 = url1 == null ? "" : url1;
            ViewBag.url2 = url2 == null ? "" : url2;
            ViewBag.url3 = url3 == null ? "" : url3;
            ViewBag.url4 = url4 == null ? "" : url4;

            return View();
        }
    }
}