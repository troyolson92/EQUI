using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.VASC.Models;

namespace EqUiWebUi.Areas.VASC.Controllers
{
    public class rt_active_infoController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/rt_active_info
        public ActionResult Index()
        {
            var rt_active_info = db.rt_active_info.Include(r => r.c_controller);
            return View(rt_active_info);
        }

        // GET: VASC/rt_active_info/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rt_active_info rt_active_info = db.rt_active_info.Find(id);
            if (rt_active_info == null)
            {
                return HttpNotFound();
            }
            return View(rt_active_info);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
