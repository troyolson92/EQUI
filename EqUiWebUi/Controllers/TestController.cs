using EqUiWebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            var model = new MyViewModel();
            model.Year = 2019;
            model.Month = 1;
            return View(model);
        }

        public ActionResult Months(int year)
        {
            if (year == 2011)
            {
                return Json(
                    Enumerable.Range(1, 3).Select(x => new { value = x, text = x }),
                    JsonRequestBehavior.AllowGet
                );
            }
            return Json(
                Enumerable.Range(1, 12).Select(x => new { value = x, text = x }),
                JsonRequestBehavior.AllowGet
            );
        }

        [HttpPost]
        public ActionResult Subscribe(MyViewModel model)
        {
            return View("Index", model);
        }
    }
}