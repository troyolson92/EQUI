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
    public class StoringsIngaveController : Controller
    {
        private GADATAEntitiesWelding db = new GADATAEntitiesWelding();

        // GET: Welding/StoringsIngave
        public ActionResult Index()
        {
            var storingsIngave_new = db.StoringsIngave_new.Include(s => s.Users).Include(s => s.StoringName).Include(s => s.StoringsCause);
            return View(storingsIngave_new.ToList());
        }

        // GET: Welding/StoringsIngave/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoringsIngave_new storingsIngave_new = db.StoringsIngave_new.Find(id);
            if (storingsIngave_new == null)
            {
                return HttpNotFound();
            }
            return View(storingsIngave_new);
        }

        // GET: Welding/StoringsIngave/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.Users, "ID", "CDSID");
            ViewBag.Nameid = new SelectList(db.StoringName, "id", "reason");
            ViewBag.Causeid = new SelectList(db.StoringsCause, "id", "Cause");
            return View();
        }

        // POST: Welding/StoringsIngave/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userid,Causeid,Nameid,StartDate,enddate,starttime,Endtime,worklocation,userComment")] StoringsIngave_new storingsIngave_new)
        {
            if (ModelState.IsValid)
            {
                db.StoringsIngave_new.Add(storingsIngave_new);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.Users, "ID", "CDSID", storingsIngave_new.userid);
            ViewBag.Nameid = new SelectList(db.StoringName, "id", "reason", storingsIngave_new.Nameid);
            ViewBag.Causeid = new SelectList(db.StoringsCause, "id", "Cause", storingsIngave_new.Causeid);
            return View(storingsIngave_new);
        }

        // GET: Welding/StoringsIngave/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoringsIngave_new storingsIngave_new = db.StoringsIngave_new.Find(id);
            if (storingsIngave_new == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.Users, "ID", "CDSID", storingsIngave_new.userid);
            ViewBag.Nameid = new SelectList(db.StoringName, "id", "reason", storingsIngave_new.Nameid);
            ViewBag.Causeid = new SelectList(db.StoringsCause, "id", "Cause", storingsIngave_new.Causeid);
            return View(storingsIngave_new);
        }

        // POST: Welding/StoringsIngave/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userid,Causeid,Nameid,StartDate,enddate,starttime,Endtime,worklocation,userComment")] StoringsIngave_new storingsIngave_new)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storingsIngave_new).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.Users, "ID", "CDSID", storingsIngave_new.userid);
            ViewBag.Nameid = new SelectList(db.StoringName, "id", "reason", storingsIngave_new.Nameid);
            ViewBag.Causeid = new SelectList(db.StoringsCause, "id", "Cause", storingsIngave_new.Causeid);
            return View(storingsIngave_new);
        }

        // GET: Welding/StoringsIngave/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoringsIngave_new storingsIngave_new = db.StoringsIngave_new.Find(id);
            if (storingsIngave_new == null)
            {
                return HttpNotFound();
            }
            return View(storingsIngave_new);
        }

        // POST: Welding/StoringsIngave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoringsIngave_new storingsIngave_new = db.StoringsIngave_new.Find(id);
            db.StoringsIngave_new.Remove(storingsIngave_new);
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
