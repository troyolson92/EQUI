using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.VWSC.Models;
using static EqUiWebUi.Areas.VWSC.Models.VWSCenums;

namespace EqUiWebUi.Areas.VWSC.Controllers
{
    public class rt_active_infoController : Controller
    {
        private GADATAEntitiesVWSC db = new GADATAEntitiesVWSC();

        // GET: VWSC/rt_active_info
        public ActionResult Index(bool ShowNOKonly = false)
        {
            if (ShowNOKonly)
            {
                return View(db.rt_active_info.Where(c => c.vwsc_state != (int)VWSCState.STATE_CONNECTED).Include(r => r.c_timer));
            }
            else
            {
                return View(db.rt_active_info.Include(c => c.c_timer));
            }
        }

        // GET: VWSC/rt_active_info/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VWSC_rt_active_info rt_active_info = db.rt_active_info.Find(id);
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