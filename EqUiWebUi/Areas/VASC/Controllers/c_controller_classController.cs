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
    public class c_controller_classController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_controller_class
        public ActionResult Index(int? controller_id)
        {
            ViewBag.controller_id = controller_id;
            if (controller_id == null)
            {
                return View(db.c_controller_class.ToList());
            }
            else
            {
                return View(db.c_controller_class.Where(c => c.id == db.c_controller.Where(cc => cc.id == controller_id).FirstOrDefault().c_controller_class.id).ToList());
            }
        }

        // GET: VASC/c_controller_class/Details/5
        public ActionResult _Details (int id, int? controller_id)
        {
            ViewBag.controller_id = controller_id;
            c_controller_class c_Controller_Class = db.c_controller_class.Find(id);
            return PartialView(c_Controller_Class);
        }

        // GET: VASC/c_controller_class/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simpler) to create a new trigger pass ID = -1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            c_controller_class c_controller_class;

            if (id == -1) //create new alert
            {
                c_controller_class = new c_controller_class();
                //set default

            }
            else //find the existing alert 
            {
                c_controller_class = db.c_controller_class.Find(id);
                
                if (c_controller_class == null)
                {
                    return HttpNotFound();
                }
            }
            return View(c_controller_class);
        }

        // POST: VASC/c_controller_class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( c_controller_class c_controller_class)
        {
            if (ModelState.IsValid)
            {
                if (c_controller_class.id == -1)//add new 
                {
                    db.c_controller_class.Add(c_controller_class);
                }
                else
                {
                    db.Entry(c_controller_class).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Close", "Home", new { area = "" });
            }
            return View(c_controller_class);
        }

        // GET: VASC/c_controller_class/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_controller_class c_controller_class = db.c_controller_class.Find(id);
            if (c_controller_class == null)
            {
                return HttpNotFound();
            }
            return View(c_controller_class);
        }

        // POST: VASC/c_controller_class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_controller_class c_controller_class = db.c_controller_class.Find(id);
            db.c_controller_class.Remove(c_controller_class);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: VASC/c_device_info/_getDeviceInfoData
        public ActionResult GetEventData(int? controller_id)
        {
            if (controller_id != null)
            {
                ViewBag.controller_id = controller_id;
                return View(db.rt_event.Where(c => c.c_controller_id == controller_id));
            }
            else
            {
                return View(db.rt_event);
            }
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
