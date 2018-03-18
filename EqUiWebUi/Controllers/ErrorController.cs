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
        [HttpGet]
        public ActionResult Index()
        {
            TempData["error"] = "Error Occurred!";
            return View("Index");
        }
        /// <summary>    /// Track Page Not Found Error
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NotFound()
        {
            TempData["error"] = "Page not Found";
            return View("Index");
        }
        /// <summary>
        /// Track Access Denied
        /// </summary>    /// <returns></returns>
        [HttpGet]
        public ActionResult AccessDenied()
        {
            TempData["error"] = "Access Denied";
            return View("Index");
        }

        /// <summary>    /// Track Internal Server Error
        /// </summary>    /// <returns></returns>
        [HttpGet]
        public ActionResult InternalServerError()
        {
            TempData["error"] = "Internal Server Error";
            return View("Index");
        }
    }
}