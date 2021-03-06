﻿using System;
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
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (LocationRoot != "")
            {
                return View(db.ProcessPoints.Where(p => p.Delta > 0 && (p.locationtree ?? "").Contains(LocationRoot)));
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