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
    public class UltralogInspectionsController : Controller
    {
        private GADATAEntitiesWelding db = new GADATAEntitiesWelding();

        // GET: Welding/UltralogInspections
        public ActionResult Index()
        {
            
            var lastultime = DateTime.Now.AddDays(-1);
            var ultralogInspections =

            db.UltralogInspections.Include(u => u.Inspectionplan).Include(u => u.UltralogStations).Include(u => u.Users).Where(u => u.InspectionTime >= lastultime);
            return View(ultralogInspections);
        }


        // GET: Welding/UltralogInspections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UltralogInspections ultralogInspections = db.UltralogInspections.Find(id);
            if (ultralogInspections == null)
            {
                return HttpNotFound();
            }
            ViewBag.InspectionPlanID = new SelectList(db.Inspectionplan, "ID", "Name", ultralogInspections.InspectionPlanID);
            ViewBag.StationID = new SelectList(db.UltralogStations, "ID", "Name", ultralogInspections.StationID);
            ViewBag.InspectorID = new SelectList(db.Users, "ID", "CDSID", ultralogInspections.InspectorID);
            return View(ultralogInspections);
        }

        // POST: Welding/UltralogInspections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,InspectionPlanID,SpotID,InspectorComment,BodyNbr,InspectorID,InspectionTime,IndexOfTestSeq,Loose,OK,SmallNugget,StickWeld,BadTroughWeld,StationID,MeasuredThickness,MinIdentation,TotalThickness,PlanLenght")] UltralogInspections ultralogInspections)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ultralogInspections).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InspectionPlanID = new SelectList(db.Inspectionplan, "ID", "Name", ultralogInspections.InspectionPlanID);
            ViewBag.StationID = new SelectList(db.UltralogStations, "ID", "Name", ultralogInspections.StationID);
            ViewBag.InspectorID = new SelectList(db.Users, "ID", "CDSID", ultralogInspections.InspectorID);
            return View(ultralogInspections);
        }

        // GET: Welding/UltralogInspections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UltralogInspections ultralogInspections = db.UltralogInspections.Find(id);
            if (ultralogInspections == null)
            {
                return HttpNotFound();
            }
            return View(ultralogInspections);
        }

        // POST: Welding/UltralogInspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UltralogInspections ultralogInspections = db.UltralogInspections.Find(id);
            db.UltralogInspections.Remove(ultralogInspections);
            db.SaveChanges();
            return RedirectToAction("Index");
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
