using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Models;

namespace EqUiWebUi.Areas.HangfireArea.Controllers
{
    public class c_jobController : Controller
    {
        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: HangfireArea/c_job
        public ActionResult Index()
        {
            var c_job = db.c_job.Include(c => c.c_datasource).Include(c => c.c_schedule);
            return View(c_job.ToList());
        }

        // GET: HangfireArea/c_job/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_job c_job = db.c_job.Find(id);
            if (id == -1) //create new 
            {
                c_job = new c_job();
            }
            else //find the existing  
            {
                c_job = db.c_job.Find(id);
                if (c_job == null)
                {
                    return HttpNotFound();
                }
            }
            ViewBag.c_datasource_id = new SelectList(db.c_datasource, "Id", "Name", c_job.c_datasource_id);
            ViewBag.c_schedule_id = new SelectList(db.c_schedule, "id", "name", c_job.c_schedule_id);
            return View(c_job);
        }

        // POST: HangfireArea/c_job/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(c_job c_job)
        {
            if (ModelState.IsValid)
            {
                if (c_job.id == -1)//add new 
                {
                    db.c_job.Add(c_job);
                }
                else //update existing
                {
                    db.Entry(c_job).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.c_datasource_id = new SelectList(db.c_datasource, "Id", "Name", c_job.c_datasource_id);
            ViewBag.c_schedule_id = new SelectList(db.c_schedule, "id", "name", c_job.c_schedule_id);
            return View(c_job);
        }

        // GET: HangfireArea/c_job/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_job c_job = db.c_job.Find(id);
            if (c_job == null)
            {
                return HttpNotFound();
            }
            return View(c_job);
        }

        // POST: HangfireArea/c_job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_job c_job = db.c_job.Find(id);
            db.c_job.Remove(c_job);
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
