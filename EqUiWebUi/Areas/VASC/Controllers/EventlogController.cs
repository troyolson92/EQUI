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

        // Get list of all events
        public ActionResult GetEvents(int? controller_id)
        {
            ViewBag.controller_id = controller_id;
            return View();
        }

        // Get list of all events (grid)
        public ActionResult _getEvents(int? controller_id)
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

    }
}