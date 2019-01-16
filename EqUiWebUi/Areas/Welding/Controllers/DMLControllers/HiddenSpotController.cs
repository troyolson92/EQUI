using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Welding.Models;

namespace EqUiWebUi.Areas.Welding.Controllers.DMLControllers
{
    public class HiddenSpotController : Controller
    {
        private GADATAEntitiesWelding db = new GADATAEntitiesWelding();

        // GET: Welding/HiddenSpot
        public ActionResult Index()
        {
            var rt_spottable = db.rt_spottable.Include(r => r.c_timer).Where(r => r.c_timer.enable_bit != -1 && r.weldProgNo < 237 && r.c_timer.Name != "dummy" &&
            r.c_timer.Name != "test" && r.c_timer.Name != "DannyBoy");
            return View(rt_spottable.ToList());
        }

        // GET: Welding/HiddenSpot/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rt_spottable rt_spottable = db.rt_spottable.Find(id);
            if (rt_spottable == null)
            {
                return HttpNotFound();
            }
            return View(rt_spottable);
        }

       

        // GET: Welding/HiddenSpot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rt_spottable rt_spottable = db.rt_spottable.Find(id);
            if (rt_spottable == null)
            {
                return HttpNotFound();
            }
            ViewBag.timerId = new SelectList(db.c_timer, "ID", "Name", rt_spottable.timerId);
            return View(rt_spottable);
        }

        // POST: Welding/HiddenSpot/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,C_timestamp,timerId,SpotName,pjv_joiningpointdata_id,vwscComment,Zone,Comment1,Comment2,Comment3,PlateCombinationtId,weldProgNo,ElectrodeDia,AlternativeNumber,Model,Variant,JobCode,NuggetDemand,HiddenSpot,GeoSpot,SpotLeft,SpotRight,isDead")] rt_spottable rt_spottable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rt_spottable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           ViewBag.timerId = new SelectList(db.c_timer, "ID", "Name", rt_spottable.timerId);
            return View(rt_spottable);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
