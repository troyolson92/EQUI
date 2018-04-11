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
    [Authorize(Roles = "Administrator")]
    public class c_alarm_ignoreController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_alarm_ignore
        public ActionResult Index()
        {
            return View(db.c_alarm_ignore.ToList());
        }

        // GET: VASC/c_alarm_ignore/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simplere) to create a new trigger pass ID = -1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_alarm_ignore c_alarm_ignore;

            if (id == -1) //create new alert
            {
                c_alarm_ignore = new c_alarm_ignore();
                c_alarm_ignore.enable_bit = (int)Enable_bit.Disabled;
                c_alarm_ignore.C_operator = (int)LogicOperator.AND;
                c_alarm_ignore._ErrorCategory = (int)ErrorCategory.Common;
                //set default

            }
            else //find the existing alert 
            {
                c_alarm_ignore = db.c_alarm_ignore.Find(id);

                if (c_alarm_ignore == null)
                {
                    return HttpNotFound();
                }
            }
            return View(c_alarm_ignore);
        }

        // POST: VASC/c_alarm_ignore/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(c_alarm_ignore c_alarm_ignore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_alarm_ignore).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_alarm_ignore);
        }

        // GET: VASC/c_alarm_ignore/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_alarm_ignore c_alarm_ignore = db.c_alarm_ignore.Find(id);
            if (c_alarm_ignore == null)
            {
                return HttpNotFound();
            }
            return View(c_alarm_ignore);
        }

        // POST: VASC/c_alarm_ignore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_alarm_ignore c_alarm_ignore = db.c_alarm_ignore.Find(id);
            db.c_alarm_ignore.Remove(c_alarm_ignore);
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
