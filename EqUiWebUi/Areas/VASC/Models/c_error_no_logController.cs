using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.VASC.Models
{
    public class c_error_no_logController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_error_no_log
        public ActionResult Index()
        {
            return View(db.c_error_no_log.ToList());
        }


        // GET: VASC/c_error_no_log/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: VASC/c_error_no_log/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simplere) to create a new trigger pass ID = -1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_error_no_log c_error_no_log;

            if (id == -1) //create new alert
            {
                c_error_no_log = new c_error_no_log();
                c_error_no_log.enable_bit = (int)Enable_bit.Disabled;
                c_error_no_log.C_operator = (int)LogicOperator.AND;
                c_error_no_log._ErrorCategory = (int)ErrorCategory.Common;
                //set default

            }
            else //find the existing alert 
            {
                c_error_no_log = db.c_error_no_log.Find(id);

                if (c_error_no_log == null)
                {
                    return HttpNotFound();
                }
            }
            return View(c_error_no_log);
        }

        // POST: VASC/c_error_no_log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(c_error_no_log c_error_no_log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_error_no_log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_error_no_log);
        }

        // GET: VASC/c_error_no_log/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_error_no_log c_error_no_log = db.c_error_no_log.Find(id);
            if (c_error_no_log == null)
            {
                return HttpNotFound();
            }
            return View(c_error_no_log);
        }

        // POST: VASC/c_error_no_log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_error_no_log c_error_no_log = db.c_error_no_log.Find(id);
            db.c_error_no_log.Remove(c_error_no_log);
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
