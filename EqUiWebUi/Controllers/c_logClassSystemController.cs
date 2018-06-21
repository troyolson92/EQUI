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
    public class c_logClassSystemController : Controller
    {
        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: c_logClassSystem
        public ActionResult Index()
        {
            var c_logClassSystem = db.c_logClassSystem.Include(c => c.c_datasource);
            return View(c_logClassSystem.ToList());
        }
        // GET: c_logClassSystem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_logClassSystem c_logClassSystem = db.c_logClassSystem.Find(id);
            if (c_logClassSystem == null)
            {
                return HttpNotFound();
            }
            ViewBag.c_datasource_id = new SelectList(db.c_datasource, "Id", "Name", c_logClassSystem.c_datasource_id);
            return View(c_logClassSystem);
        }

        // POST: c_logClassSystem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] //to alow posting of raw html data
        public ActionResult Edit( c_logClassSystem c_logClassSystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_logClassSystem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.c_datasource_id = new SelectList(db.c_datasource, "Id", "Name", c_logClassSystem.c_datasource_id);
            return View(c_logClassSystem);
        }

        // GET: c_logClassSystem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_logClassSystem c_logClassSystem = db.c_logClassSystem.Find(id);
            if (c_logClassSystem == null)
            {
                return HttpNotFound();
            }
            return View(c_logClassSystem);
        }

        // POST: c_logClassSystem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_logClassSystem c_logClassSystem = db.c_logClassSystem.Find(id);
            db.c_logClassSystem.Remove(c_logClassSystem);
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
