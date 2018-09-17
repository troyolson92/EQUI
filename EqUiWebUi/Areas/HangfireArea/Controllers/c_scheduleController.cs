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
    [Authorize(Roles = "Administrator")]
    public class c_scheduleController : Controller
    {
        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: HangfireArea/c_schedule
        public ActionResult Index()
        {
            return View(db.c_schedule.ToList());
        }

        // GET: HangfireArea/c_schedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_schedule c_schedule = db.c_schedule.Find(id);
            if (id == -1) //create new 
            {
                c_schedule = new c_schedule();
            }
            else //find the existing  
            {
                c_schedule =  db.c_schedule.Find(id);
                if (c_schedule == null)
                {
                    return HttpNotFound();
                }
            }

            return View(c_schedule);
        }

        // POST: HangfireArea/c_schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(c_schedule c_schedule)
        {
            if (ModelState.IsValid)
            {
                if (c_schedule.id == -1)//add new 
                {
                    db.c_schedule.Add(c_schedule);
                }
                else //update existing
                {
                    db.Entry(c_schedule).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_schedule);
        }

        // GET: HangfireArea/c_schedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_schedule c_schedule = db.c_schedule.Find(id);
            if (c_schedule == null)
            {
                return HttpNotFound();
            }
            return View(c_schedule);
        }

        // POST: HangfireArea/c_schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_schedule c_schedule = db.c_schedule.Find(id);
            db.c_schedule.Remove(c_schedule);
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
