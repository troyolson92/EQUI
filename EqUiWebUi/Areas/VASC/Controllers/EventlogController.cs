using EqUiWebUi.Areas.VASC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.VASC.Controllers
{
    public class EventlogController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/Eventlog
        public ActionResult Index()
        {
            return View();
        }

        // Get list of all events (h_alarm)
        public ActionResult Get_h_alarm(int? controller_id)
        {
            ViewBag.controller_id = controller_id;
            return View();
        }

        // Get list of all events  (h_alarm) (grid)
        public ActionResult _get_h_alarm(int? controller_id)
        {
            db.Database.CommandTimeout = 60;
            IQueryable<h_alarm> data = db.h_alarm;

            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (controller_id.HasValue)
            {
                data = data.Where(c => c.c_controller.id == controller_id);
            }
            else if (LocationRoot != "")
            {
                data = data.Where(c => (c.c_controller.LocationTree ?? "").Contains(LocationRoot));
            }

            return PartialView(data);
        }

        // Get list of all events (rt_alarm)
        public ActionResult Get_rt_alarm(int? controller_id)
        {
            ViewBag.controller_id = controller_id;
            return View();
        }

        // Get list of all events  (rt_alarm) (grid)
        public ActionResult _get_rt_alarm(int? controller_id)
        {
            db.Database.CommandTimeout = 60;
            IQueryable<rt_alarm> data = db.rt_alarm;

            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (controller_id.HasValue)
            {
                data = data.Where(c => c.c_controller.id == controller_id);
            }
            else if (LocationRoot != "")
            {
                data = data.Where(c => (c.c_controller.LocationTree ?? "").Contains(LocationRoot));
            }

            return PartialView(data);
        }

    }
}