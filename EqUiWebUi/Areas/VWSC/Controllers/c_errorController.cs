using EqUiWebUi.Areas.VWSC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using static EqUiWebUi.Areas.VWSC.Models.VWSCenums;

namespace EqUiWebUi.Areas.VWSC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class c_errorController : Controller
    {
        private GADATAEntitiesVWSC db = new GADATAEntitiesVWSC();

        // GET:VWSC/c_error
        public ActionResult Index()
        {
            return View();
        }

        // GET: VWSC/c_error/_List
        //Will return partial view with a list of the controllers in a controller class.
        //Filterable by enable bit
        public ActionResult _List(int? enable_mask, int? timer_id)
        {
            ViewBag.timer_id = timer_id;
            List<VWSC_c_error> list = new List<VWSC_c_error>();
            if (enable_mask is null)
            {
                list = db.c_error.ToList();
            }
            else
            {
                var setbits = Enumerable.Range(0, 32).Where(x => ((enable_mask >> x) & 1) == 1);
                foreach (int setbit in setbits)
                {
                    list.AddRange(db.c_error.Where(c => c.enable_bit == setbit + 1 && c.enable_bit != 0).ToList());
                }
            }
            return PartialView(list);
        }

        // GET: VWSC/c_error/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VWSC_c_error c_error = db.c_error.Find(id);
            if (c_error == null)
            {
                return HttpNotFound();
            }
            return View(c_error);
        }

        // GET: VWSC/c_error/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simplere) to create a new trigger pass ID = -1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VWSC_c_error c_error;

            if (id == -1) //create new
            {
                c_error = new VWSC_c_error();
                c_error.enable_bit = (int)Enable_bit.Disabled;
                c_error.C_operator = (int)LogicOperator.AND;
                c_error.flags = (int)c_error_flags.Insert_into_rt_alarm;
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

        // POST: VWSC/c_error/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VWSC_c_error c_error)
        {
            if (ModelState.IsValid)
            {
                if (c_error.id == -1)//add new
                {
                    db.c_error.Add(c_error);
                }
                else
                {
                    db.Entry(c_error).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_error);
        }

        // GET: VWSC/c_error/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VWSC_c_error c_error = db.c_error.Find(id);
            if (c_error == null)
            {
                return HttpNotFound();
            }
            return View(c_error);
        }

        // POST: VWSC/c_error/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VWSC_c_error c_error = db.c_error.Find(id);
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