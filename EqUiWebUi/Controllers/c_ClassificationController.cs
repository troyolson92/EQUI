using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Models;

namespace EqUiWebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class c_ClassificationController : Controller
    {
        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: c_Classification
        public ActionResult Index()
        {
            return View(db.c_Classification.ToList());
        }

        // GET: c_Classification/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: c_Classification/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Classification,Discription")] c_Classification c_Classification)
        {
            if (ModelState.IsValid)
            {
                db.c_Classification.Add(c_Classification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_Classification);
        }

        // GET: c_Classification/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_Classification c_Classification = db.c_Classification.Find(id);
            if (c_Classification == null)
            {
                return HttpNotFound();
            }
            return View(c_Classification);
        }

        // POST: c_Classification/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Classification,Discription")] c_Classification c_Classification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_Classification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_Classification);
        }

        // GET: c_Classification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_Classification c_Classification = db.c_Classification.Find(id);
            if (c_Classification == null)
            {
                return HttpNotFound();
            }
            return View(c_Classification);
        }

        // POST: c_Classification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_Classification c_Classification = db.c_Classification.Find(id);
            db.c_Classification.Remove(c_Classification);
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
