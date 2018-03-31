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
    public class c_csv_logController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_csv_log
        public ActionResult Index()
        {
            return View(db.c_csv_log.ToList());
        }

        // GET: VASC/c_csv_log/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simplere) to create a new trigger pass ID = -1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            c_csv_log c_csv_log;

            if (id == -1) //create new alert
            {
                c_csv_log = new c_csv_log();
                //set default
                c_csv_log._Enable_bit = Enable_bit.Disabled;
                c_csv_log._Poll_Rate = Poll_rate.ReadOnConnect;
                c_csv_log._Csv_log_Flags = Csv_log_Flags.obtained_PCSDK;
            }
            else //find the existing alert 
            {
                c_csv_log = db.c_csv_log.Find(id);
                if (c_csv_log == null)
                {
                    return HttpNotFound();
                }
            }

            return View(c_csv_log);
        }

        // POST: VASC/c_csv_log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(c_csv_log c_csv_log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_csv_log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_csv_log);
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
