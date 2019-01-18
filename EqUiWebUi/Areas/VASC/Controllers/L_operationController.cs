using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.VASC.Models;

namespace EqUiWebUi.Areas.VASC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class L_operationController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/L_operation
        public ActionResult Index(int? controller_id, string sessionName)
        {
            ViewBag.controller_id = controller_id;
            ViewBag.sessionName = sessionName;
            return View();
        }

        // GET: VASC/L_operation/_List
        //Will return partial view with a list of the L_operation.
        public ActionResult _List(int? controller_id, string sessionName)
        {
            ViewBag.controller_id = controller_id;
            if (controller_id is null)
            {
                if (sessionName is null)
                {
                    //all 
                    return PartialView(db.L_operation.ToList());
                }
                else
                {
                    //for a session
                    return PartialView(db.L_operation.Where(c => c.Vasc_name.Contains(sessionName)).ToList());
                }
            }
            else
            {
                //for a controller
                return PartialView(db.L_operation.Where(c => (c.controller_id == controller_id && c.Vasc_name.Contains(sessionName)) || (c.controller_id == null && c.Vasc_name.Contains(sessionName))).ToList());
            }

        }
    }
}