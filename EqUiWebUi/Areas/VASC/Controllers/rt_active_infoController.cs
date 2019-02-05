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
        public ActionResult Index(string sessionName, bool ShowNOKonly = false)
        {
            ViewBag.sessionName = sessionName;
            ViewBag.ShowNOKonly = ShowNOKonly;
            if (ShowNOKonly)
            {
                if (sessionName is null)
                {
                    return View(db.rt_active_info.Where(c => c.vasc_state != (int)VASCState.STATE_CONNECTED && c.c_controller.enable_bit != (int)Enable_bit.Disabled).Include(r => r.c_controller).ToList());
                }
                else
                {
                    return View(db.rt_active_info.Where(c => c.vasc_state != (int)VASCState.STATE_CONNECTED && c.c_controller.enable_bit != (int)Enable_bit.Disabled && c.vasc_session.Contains(sessionName)).Include(r => r.c_controller).ToList());
                }
            }
            else
            {
                if (sessionName is null)
                {
                    return View(db.rt_active_info.Include(r => r.c_controller).ToList());
                }
                else
                {
                    return View(db.rt_active_info.Where(r => r.vasc_session.Contains(sessionName)).Include(r => r.c_controller).ToList());
                }
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
