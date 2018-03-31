using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.VASC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class vascController : Controller
    {
        // GET: VASC/vasc
        public ActionResult Index()
        {
            return View();
        }
    }
}