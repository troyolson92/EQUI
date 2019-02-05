using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Welding.Models;

namespace EqUiWebUi.Areas.Welding.Controllers
{
    public class WeldingController : Controller

    {
        GADATAEntitiesWelding db = new GADATAEntitiesWelding();
    
       
        // GET: Welding/Welding
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult _alertsAASPOT ()
        {
            IQueryable<AlertsAASPOT> data = db.AlertsAASPOT.AsQueryable();
            return View(data);
        }

        public ActionResult _AlertsUserRapport()
        {
            IQueryable<AlertsUsers> data = db.AlertsUsers.AsQueryable().Where(c => c.userAccept != null || c.userChanged != null || c.userClosed != null);
            return View(data);
        }



        public ActionResult ppmreweld()
        {
         
            return View();
        }

        }
    }
