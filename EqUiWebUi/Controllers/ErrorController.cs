using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>    /// Track Common Page Error
        /// </summary>    /// <returns></returns>
        public ActionResult Index()
        {
            TempData["error"] = "Error Occurred!";
            return View("Index");//use default Index error view
        }
        /// <summary>    /// Track Page Not Found Error
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            TempData["error"] = "Page not Found";
            return View("Index");//use default Index error view
        }
        /// <summary>
        /// Track Access Denied
        /// </summary>    /// <returns></returns>
        public ActionResult AccessDenied()
        {
            TempData["error"] = "Access Denied";
            return View();//custom Access Denied view
        }

        /// <summary>    /// Track Internal Server Error
        /// </summary>    /// <returns></returns>
        public ActionResult InternalServerError()
        {
            TempData["error"] = "Internal Server Error";
            return View("Index");//use default Index error view
        }
    }
}