using EqUiWebUi.Areas.Welding.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Welding.Controllers

{
    public class TeardownTeamController : Controller
    {
        GADATAEntitiesWelding db = new GADATAEntitiesWelding();
        // GET: TeardownTeam
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _TDTResults()
        {
            IQueryable<TDTResults> data = db.TDTResults.AsQueryable();
            return View(data);
        }

        public ActionResult _StartTeardown()
        {

            return View();
        }
        
        }

    }
