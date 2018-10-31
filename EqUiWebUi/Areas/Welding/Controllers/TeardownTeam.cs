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

        public ActionResult TeardownTest()
        {
            IQueryable<test> data = db.test.AsQueryable();
            return View(data);

        }
           public ActionResult SaveSpot(int id,string propertyName, string value)
            {

            var Status = false;
            var message = "";
            //update data to gadata

            var spot = db.test.Find(id);
            if (spot != null)
            {
                db.Entry(spot).Property(propertyName).CurrentValue = value;
                db.SaveChanges();
                Status = true;
            }
            else
            {
                message = "error!!: contact Jens Coppejans";
            }
            
            var response = new { value = value, status = Status, message = message };
            JObject o = JObject.FromObject(Response);
            return Content(o.ToString());

            }



        }

    }
