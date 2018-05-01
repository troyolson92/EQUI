using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.PJV.Models;

namespace EqUiWebUi.Areas.PJV.Controllers
{
    public class PJVController : Controller
    {

        private GADATAEntitiesPJV db = new GADATAEntitiesPJV();

        // GET: PJV/Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: PJV/Modifications
        public ActionResult GetChanges()
        {
            if (Session["LocationRoot"].ToString() != "")
            {
                string locationRoot = Session["LocationRoot"].ToString();
                return View(db.ProcessPoints.Where(p => p.Delta > 0 && (p.locationtree ?? "").Contains(locationRoot)));
            }
            else
            {
                return View(db.ProcessPoints.Where(p => p.Delta > 0));
            }

        }

        //GET: PJV/L_operation
        public ActionResult GetL_operation()
        {
            return View(db.L_operation);
        }

    }
}