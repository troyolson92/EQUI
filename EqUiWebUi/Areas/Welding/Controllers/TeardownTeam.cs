using EqUiWebUi.Areas.Welding.Models;
using Hangfire.Server;
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



        public static class TeardownData
        {
            public static List<EqUiWebUi.Areas.Welding.Models.StartTeardown> StartTeardown { get; set; }
            public static List<EqUiWebUi.Areas.Welding.Models.type> Type { get; set; }

        }

        public partial class StartDummy
        {
            public string Type { get; set; }
            public string BodyNbr { get; set; }
            public string TDTStartTime { get; set; }

            public class _TDT
            {
                GADATAEntitiesWelding db = new GADATAEntitiesWelding();

                public void TDT(PerformContext context)
                {
                    List<StartDummy> data = new List<Areas.Welding.Controllers.TeardownTeamController.StartDummy>();
                }
            }
        }


        public ActionResult _startTeardown()
        {
            var data = db.ComparePitchV316.Select (p => new StartDummy() {
                Type = p.AlternativeNumber
                ,BodyNbr = p.id.ToString()
                ,TDTStartTime = p.SpotID

                }).ToList();

            return View(data);
        }


            }

        }












