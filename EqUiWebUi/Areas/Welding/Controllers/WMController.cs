﻿using EqUiWebUi.Areas.Welding.Models;
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

        public ActionResult BrownfieldLastMeasurement()
        {
            IQueryable<BrownfieldLastMeasurement> data = db.BrownfieldLastMeasurements.AsQueryable();
            return View(data);

        }

        public ActionResult GreenLastMeasurement()
        {
            IQueryable<GreenFieldLastMeasurement> data = db.GreenFieldLastMeasurements.AsQueryable();
            return View(data);
        }
    



    }


}