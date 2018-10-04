using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.VWSC.Models;

namespace EqUiWebUi.Areas.VWSC.Controllers
{
        [Authorize(Roles = "Administrator")]
        public class c_timer_classController : Controller
        {
            private GADATAEntitiesVWSC  db = new GADATAEntitiesVWSC();

        // GET: VWSC/c_timer_class
        public ActionResult Index(int? timer_id)
            {
                ViewBag.timer_id = timer_id;
                if (timer_id == null)
                {
                    return View(db.c_timer_class.ToList());
                }
                else
                {
                    return View(db.c_timer_class.Where(c => c.id == db.c_timer_class.Where(cc => cc.id == timer_id).FirstOrDefault().id).ToList());
                }
            }

        // GET: VWSC/c_timer_class/Details/5
        public ActionResult _Details(int id, int? timer_id)
            {
                ViewBag.timer_id = timer_id;
                 VWSC_c_timer_class c_timer_class = db.c_timer_class.Find(id);
                return PartialView(c_timer_class);
            }

        // GET: VWSC/c_timer_class/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simplere) to create a new trigger pass ID = -1
        public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                VWSC_c_timer_class c_timer_class;

                if (id == -1) //create new alert
                {
                c_timer_class = new VWSC_c_timer_class();
                    //set default

                }
                else //find the existing alert 
                {
                c_timer_class = db.c_timer_class.Find(id);

                    if (c_timer_class == null)
                    {
                        return HttpNotFound();
                    }
                }
                return View(c_timer_class);
            }

        // POST: VWSC/c_timer_class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(VWSC_c_timer_class c_timer_class)
            {
                if (ModelState.IsValid)
                {
                    if (c_timer_class.id == -1)//add new 
                    {
                        db.c_timer_class.Add(c_timer_class);
                    }
                    else
                    {
                        db.Entry(c_timer_class).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(c_timer_class);
            }

        // GET: VWSC/c_timer_class/Delete/5
        public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                VWSC_c_timer_class c_timer_class = db.c_timer_class.Find(id);
                if (c_timer_class == null)
                {
                    return HttpNotFound();
                }
                return View(c_timer_class);
            }

        // POST: VWSC/c_timer_class/Delete/5
        [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                VWSC_c_timer_class c_timer_class = db.c_timer_class.Find(id);
                db.c_timer_class.Remove(c_timer_class);
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