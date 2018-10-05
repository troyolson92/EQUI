using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.VWSC.Models;

namespace EqUiWebUi.Areas.VWSC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class L_operationController : Controller
    {
        private GADATAEntitiesVWSC db = new GADATAEntitiesVWSC();

        // GET: VWSC/L_operation
        public ActionResult Index(int? timer_id, string sessionName)
        {
            ViewBag.timer_id = timer_id;
            ViewBag.sessionName = sessionName;
            return View();
        }

        // GET: VWSC/L_operation/_List
        //Will return partial view with a list of the L_operation.
        public ActionResult _List(int? timer_id, string sessionName)
        {
            ViewBag.timer_id = timer_id;
            if (timer_id is null)
            {
                if (sessionName is null)
                {
                    //all 
                    return PartialView(db.L_operation);
                }
                else
                {
                    //for a session
                    return PartialView(db.L_operation.Where(c => c.Vwsc_name.Contains(sessionName)));
                }
            }
            else
            {
                //for a controller
                return PartialView(db.L_operation.Where(c => (c.controller_id == timer_id && c.Vwsc_name.Contains(sessionName)) || (c.controller_id == null && c.Vwsc_name.Contains(sessionName))));
            }

        }
    }
}