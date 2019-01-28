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
    public class h_production_issuesController : Controller
    {
        private GADATAEntitiesWelding db = new GADATAEntitiesWelding();

        // GET: Welding/h_production_issues
        public ActionResult Index()
        {
            var h_production_issues = db.h_production_issues.Include(h => h.c_production_issue).Include(h => h.c_reporter).Include(h => h.rt_spottable).Where(h => h.rt_spottable.weldProgNo > 80 && h.rt_spottable.weldProgNo < 237);
            return View(h_production_issues.ToList());
        }

        // GET: Welding/h_production_issues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            h_production_issues h_production_issues = db.h_production_issues.Find(id);
            if (h_production_issues == null)
            {
                return HttpNotFound();
            }
            return View(h_production_issues);
        }

        // GET: Welding/h_production_issues/Create
        public ActionResult Create()
        {
            ViewBag.issueId = new SelectList(db.c_production_issue, "id", "issue");
            ViewBag.reporterId = new SelectList(db.c_reporter, "id", "reporterName");
            ViewBag.spotid = new SelectList(db.rt_spottable.Where(c => c.weldProgNo > 80 && c.weldProgNo < 237 && c.Model == "V316").OrderBy(c => c.SpotName), "ID", "SpotName");
            return View();
        }

        // POST: Welding/h_production_issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,spotid,startdate,enddate,reporterId,issueId,quantity,lastBodyNbr,remarks")] h_production_issues h_production_issues)
        {
            if (ModelState.IsValid)
            {
                db.h_production_issues.Add(h_production_issues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.issueId = new SelectList(db.c_production_issue, "id", "issue", h_production_issues.issueId);
            ViewBag.reporterId = new SelectList(db.c_reporter, "id", "reporterName", h_production_issues.reporterId);
            ViewBag.spotid = new SelectList(db.rt_spottable.Where(c => c.weldProgNo > 80 && c.weldProgNo < 237 && c.Model == "V316").OrderBy(c => c.SpotName), "ID", "SpotName");
            return View(h_production_issues);
        }

        // GET: Welding/h_production_issues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            h_production_issues h_production_issues = db.h_production_issues.Find(id);
            if (h_production_issues == null)
            {
                return HttpNotFound();
            }
            ViewBag.issueId = new SelectList(db.c_production_issue, "id", "issue", h_production_issues.issueId);
            ViewBag.reporterId = new SelectList(db.c_reporter, "id", "reporterName", h_production_issues.reporterId);
            ViewBag.spotid = new SelectList(db.rt_spottable, "ID", "SpotName", h_production_issues.spotid);
            return View(h_production_issues);
        }

        // POST: Welding/h_production_issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,spotid,startdate,enddate,reporterId,issueId,quantity,lastBodyNbr,remarks")] h_production_issues h_production_issues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(h_production_issues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.issueId = new SelectList(db.c_production_issue, "id", "issue", h_production_issues.issueId);
            ViewBag.reporterId = new SelectList(db.c_reporter, "id", "reporterName", h_production_issues.reporterId);
            ViewBag.spotid = new SelectList(db.rt_spottable, "ID", "SpotName", h_production_issues.spotid);
            return View(h_production_issues);
        }

        // GET: Welding/h_production_issues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            h_production_issues h_production_issues = db.h_production_issues.Find(id);
            if (h_production_issues == null)
            {
                return HttpNotFound();
            }
            return View(h_production_issues);
        }

        // POST: Welding/h_production_issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            h_production_issues h_production_issues = db.h_production_issues.Find(id);
            db.h_production_issues.Remove(h_production_issues);
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
