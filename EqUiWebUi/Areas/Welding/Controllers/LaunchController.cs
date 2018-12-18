using EqUiWebUi.Areas.Welding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Welding.Controllers
{
    public class LaunchController : Controller

    {
        GADATAEntitiesWelding db = new GADATAEntitiesWelding();
    
        // GET: Launch
        public ActionResult Index()
        {
            return View();

        }
        // GET: launch

        public ActionResult doublespotCheck()
        {
            IQueryable<DoubleSpotCheck> data = db.DoubleSpotCheck.AsQueryable();
            return View(data);
        }
        public ActionResult CheckDubbelPrograms()
        {
            IQueryable<CheckDubbelPrograms> data = db.CheckDubbelPrograms.AsQueryable();
            return View(data);
        }
    
    public ActionResult BosProgramAvailable()
    {
        IQueryable<BosProgramAvailable> data = db.BosProgramAvailable.AsQueryable();
        return View(data);
    }
        public ActionResult dressrequired()
        {
            IQueryable<dressrequired> data = db.dressrequired.AsQueryable();
            return View(data);
        }
        public ActionResult WeldtimeSpotsSetup()
        {
            IQueryable<WeldtimeSpotsSetup> data = db.WeldtimeSpotsSetup.AsQueryable();
            return View(data);
        }

        public ActionResult StabilityInDicationV316()
        {
            IQueryable<StabilityInDicationV316> data = db.StabilityInDicationV316.AsQueryable();
            return View(data);
        }
        public ActionResult ComparePitchV316()
        {
            IQueryable<ComparePitchV316> data = db.ComparePitchV316.AsQueryable();
            return View(data);
        }





    }


}