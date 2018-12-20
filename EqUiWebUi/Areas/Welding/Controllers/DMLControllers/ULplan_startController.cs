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
    public class ULplan_startController : Controller
    {
        private GADATAEntitiesWelding db = new GADATAEntitiesWelding();

        // GET: Welding/ULplan_start
        public ActionResult Index()
        {
            return View(db.ULplan_start.ToList());
        }

        // GET: Welding/ULplan_start/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ULplan_start uLplan_start = db.ULplan_start.Find(id);
            if (uLplan_start == null)
            {
                return HttpNotFound();
            }
            return View(uLplan_start);
        }

        // GET: Welding/ULplan_start/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Welding/ULplan_start/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,planid,lastplannumber,Startinspectiontime,planlenght,inspectorid,StationId,Worklocation,bodynr,plancounter")] ULplan_start uLplan_start)
        {
            if (ModelState.IsValid)
            {
                db.ULplan_start.Add(uLplan_start);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uLplan_start);
        }

        // GET: Welding/ULplan_start/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ULplan_start uLplan_start = db.ULplan_start.Find(id);
            if (uLplan_start == null)
            {
                return HttpNotFound();
            }
            return View(uLplan_start);
        }

        // POST: Welding/ULplan_start/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,planid,lastplannumber,Startinspectiontime,planlenght,inspectorid,StationId,Worklocation,bodynr,plancounter")] ULplan_start uLplan_start)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uLplan_start).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uLplan_start);
        }

        // GET: Welding/ULplan_start/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ULplan_start uLplan_start = db.ULplan_start.Find(id);
            if (uLplan_start == null)
            {
                return HttpNotFound();
            }
            return View(uLplan_start);
        }

        // POST: Welding/ULplan_start/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ULplan_start uLplan_start = db.ULplan_start.Find(id);
            db.ULplan_start.Remove(uLplan_start);
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
