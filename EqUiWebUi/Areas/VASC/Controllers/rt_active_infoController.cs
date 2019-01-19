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
        public ActionResult Index(bool ShowNOKonly = false)
        {
            if (ShowNOKonly)
            {
                return View(db.rt_active_info.Where(c => c.vasc_state != (int)VASCState.STATE_CONNECTED && c.c_controller.enable_bit != (int)Enable_bit.Disabled).Include(r => r.c_controller).ToList());
            }
            else
            {
                return View(db.rt_active_info.Include(r => r.c_controller).ToList());
            }
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
