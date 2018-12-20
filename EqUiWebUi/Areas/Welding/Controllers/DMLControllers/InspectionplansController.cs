using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Welding.Models;

namespace EqUiWebUi.Areas.Welding
{
    public class InspectionplansController : Controller
    {
        private GADATAEntitiesWelding db = new GADATAEntitiesWelding();

        // GET: Welding/Inspectionplans
        public ActionResult Index()
        {
            return View(db.Inspectionplan.ToList());
        }

        // GET: Welding/Inspectionplans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspectionplan inspectionplan = db.Inspectionplan.Find(id);
            if (inspectionplan == null)
            {
                return HttpNotFound();
            }
            return View(inspectionplan);
        }

        // GET: Welding/Inspectionplans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Welding/Inspectionplans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CreatorID,Lenght,Name,Date,PlanActive,SpotIdent,WorkTime,WorkGroup,WorkLocation,SpotBefore,SpotAfter")] Inspectionplan inspectionplan)
        {
            if (ModelState.IsValid)
            {
                db.Inspectionplan.Add(inspectionplan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inspectionplan);
        }

        // GET: Welding/Inspectionplans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspectionplan inspectionplan = db.Inspectionplan.Find(id);
            if (inspectionplan == null)
            {
                return HttpNotFound();
            }
            return View(inspectionplan);
        }

        // POST: Welding/Inspectionplans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CreatorID,Lenght,Name,Date,PlanActive,SpotIdent,WorkTime,WorkGroup,WorkLocation,SpotBefore,SpotAfter")] Inspectionplan inspectionplan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspectionplan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inspectionplan);
        }

        // GET: Welding/Inspectionplans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspectionplan inspectionplan = db.Inspectionplan.Find(id);
            if (inspectionplan == null)
            {
                return HttpNotFound();
            }
            return View(inspectionplan);
        }

        // POST: Welding/Inspectionplans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inspectionplan inspectionplan = db.Inspectionplan.Find(id);
            db.Inspectionplan.Remove(inspectionplan);
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
