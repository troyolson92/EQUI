using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.VASC.Models;

namespace EqUiWebUi.Areas.VASC.Controllers
{
    public class c_jobController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_job
        public ActionResult Index()
        {
            return View();
        }

        // GET: VASC/c_job/_List
        //Will return partial view with a list of the c_alarm_ignore.
        //Filterable by enable bit
        public ActionResult _List(int? enable_mask)
        {
            List<c_job> list = new List<c_job>();
            if (enable_mask is null)
            {
                list = db.c_job.ToList();
            }
            else
            {
                var setbits = Enumerable.Range(0, 32).Where(x => ((enable_mask >> x) & 1) == 1);
                foreach (int setbit in setbits)
                {
                    list.AddRange(db.c_job.Where(c => c.enable_bit == setbit + 1 && c.enable_bit != 0).ToList());
                }
            }
            return PartialView(list);
        }

        // GET: VASC/c_job/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_job c_job;

            if (id == -1) //create new
            {
                c_job = new c_job();
                c_job.enable_bit = (int)Enable_bit.Disabled;
                c_job.flags = (int)c_jobFlags.noAction;

            }
            else //find the existing alert 
            {
                c_job = db.c_job.Find(id);

                if (c_job == null)
                {
                    return HttpNotFound();
                }
            }
            return View(c_job);
        }

        // POST: VASC/c_job/Edit/5
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
                else
                {
                    db.Entry(c_job).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Close", "Home", new { area = "" });
            }
            return View(c_job);
        }

        // GET: VASC/c_job/Delete/5
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

        // POST: VASC/c_job/Delete/5
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
