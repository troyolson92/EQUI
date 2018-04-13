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
    public class c_errorController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_error
        public ActionResult Index()
        {
            return View();
        }

        // GET: VASC/c_alarm_ignore/_List
        //Will return partial view with a list of the c_alarm_ignore.
        //Filterable by enable bit
        public ActionResult _List(int? enable_mask)
        {
            List<c_error> list = new List<c_error>();
            if (enable_mask is null)
            {
                list = db.c_error.ToList();
            }
            else
            {
                var setbits = Enumerable.Range(0, 32).Where(x => ((enable_mask + 1 >> x) & 1) == 1);
                foreach (int setbit in setbits)
                {
                    list.AddRange(db.c_error.Where(c => c.enable_bit == setbit && c.enable_bit != 0).ToList());
                }
            }
            return PartialView(list);
        }

        // GET: VASC/c_error/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simplere) to create a new trigger pass ID = -1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_error c_error;

            if (id == -1) //create new alert
            {
                c_error = new c_error();
                c_error.enable_bit = (int)Enable_bit.Disabled;
                c_error.C_operator = (int)LogicOperator.AND;
               // c_error._ErrorCategory = (int)ErrorCategory.Common;
                //set default

            }
            else //find the existing alert 
            {
                c_error = db.c_error.Find(id);

                if (c_error == null)
                {
                    return HttpNotFound();
                }
            }
            return View(c_error);
        }

        // POST: VASC/c_error/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(c_error c_error)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_error).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_error);
        }

        // GET: VASC/c_error/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_error c_error = db.c_error.Find(id);
            if (c_error == null)
            {
                return HttpNotFound();
            }
            return View(c_error);
        }

        // POST: VASC/c_error/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_error c_error = db.c_error.Find(id);
            db.c_error.Remove(c_error);
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
