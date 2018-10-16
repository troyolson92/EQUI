using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Welding.Models;

namespace EqUiWebUi.Areas.Welding
{
    public class WeldingController : Controller
    {
       
        // GET: Welding/Welding
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pagina1()
        {
            ViewBag.data1 = "Dit is een string";
            ViewBag.data2 = 10;
            return View();
        }

        }
    }
