using EqUiWebUi.Areas.Welding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Welding.Controllers
{
    public class WMController : Controller

    {
        GADATAEntitiesWelding db = new GADATAEntitiesWelding();
    
        // GET: Welding/WM
        public ActionResult Index()
        {
            return View();

        }
        // GET: Weldingmaster 


        public ActionResult LastWelds()
        {
            IQueryable<LastWelds> data = db.LastWelds.AsQueryable();
            return View(data);
        }

        public ActionResult TV()
        {
            IQueryable<QISViewer> data = db.QISViewer.AsQueryable();
            return View(data);
        }
   

    public ActionResult _ConnectionState()
    {
        IQueryable<ConnectionState> data = db.ConnectionState.AsQueryable();
        return PartialView(data);
    }

    public ActionResult _QISvieuwer()
    {
        IQueryable<QISViewer> data = db.QISViewer.AsQueryable();
        return PartialView(data);
    }
        public ActionResult _TimerBreakdowns_busy()
        {
            IQueryable<TimerBreakdowns_busy > data = db.TimerBreakdowns_busy.AsQueryable();
            return PartialView(data);
        }
        public ActionResult rt_ExtraControlesWM()
        {
            IQueryable<rt_ExtraControles> data = db.rt_ExtraControles.AsQueryable();
            return PartialView(data);
        }
        

    }


   
}