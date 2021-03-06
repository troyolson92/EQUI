﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EqUiWebUi.Areas.VWSC.Models;
using static EqUiWebUi.Areas.VWSC.Models.VWSCenums;

namespace EqUiWebUi.Areas.VWSC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class c_bosch_viewController : Controller
    {
        private GADATAEntitiesVWSC db = new GADATAEntitiesVWSC();

        // GET: VWSC/c_bosch_view
        public ActionResult Index()
        {
            return View();
        }

        // GET: VWSC/c_bosch_view/_List
        //Will return partial view with a list of the controllers in a controller class.
        //Filterable by enable bit
        public ActionResult _List(int? enable_mask, int? timer_id)
        {
            ViewBag.timer_id = timer_id;
            List<VWSC_c_bosch_view> list = new List<VWSC_c_bosch_view>();
            if (enable_mask is null)
            {
                list = db.c_bosch_view.ToList();
            }
            else
            {
                var setbits = Enumerable.Range(0, 32).Where(x => ((enable_mask >> x) & 1) == 1);
                foreach (int setbit in setbits)
                {
                    list.AddRange(db.c_bosch_view.Where(c => c.enable_bit == setbit + 1 && c.enable_bit != 0).ToList());
                }
            }
            return PartialView(list);
        }

        // GET: VWSC/c_bosch_view/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VWSC_c_bosch_view c_Bosch_View = db.c_bosch_view.Find(id);
            if (c_Bosch_View == null)
            {
                return HttpNotFound();
            }
            return View(c_Bosch_View);
        }


        // GET: VWSC/c_bosch_view/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simplere) to create a new trigger pass ID = -1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VWSC_c_bosch_view c_bosch_view;

            if (id == -1) //create new
            {
                c_bosch_view = new VWSC_c_bosch_view();
                c_bosch_view.enable_bit = (int)Enable_bit.Disabled;
            }
            else //find the existing alert 
            {
                c_bosch_view = db.c_bosch_view.Find(id);

                if (c_bosch_view == null)
                {
                    return HttpNotFound();
                }
            }
            return View(c_bosch_view);
        }

        // POST: VWSC/c_bosch_view/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VWSC_c_bosch_view c_bosch_view)
        {
            if (ModelState.IsValid)
            {
                if (c_bosch_view.id == -1)//add new 
                {
                    db.c_bosch_view.Add(c_bosch_view);
                }
                else
                {
                    db.Entry(c_bosch_view).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_bosch_view);
        }

        // GET: VWSC/c_bosch_view/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VWSC_c_bosch_view c_bosch_view = db.c_bosch_view.Find(id);
            if (c_bosch_view == null)
            {
                return HttpNotFound();
            }
            return View(c_bosch_view);
        }

        // POST: VWSC/c_bosch_view/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VWSC_c_bosch_view c_bosch_view = db.c_bosch_view.Find(id);
            db.c_bosch_view.Remove(c_bosch_view);
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