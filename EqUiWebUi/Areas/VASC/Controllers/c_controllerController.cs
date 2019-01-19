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
    public class c_controllerController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_controller
        public ActionResult Index()
        {
            return View();
        }

        // GET: VASC/c_controller/_List
        //Will return partial view with a list of the controllers in a controller class.
        //Filterable by enable bit
        public ActionResult _List(int? controllerclass, int? controller_id)
        {
            if (controller_id != null)
            {
                return PartialView(db.c_controller.Where(c => c.id == controller_id));
            }

            if (controllerclass is null)
            {
                return PartialView(db.c_controller);
            }
            else
            {
                return PartialView(db.c_controller.Where(c => c.class_id == controllerclass));
            }
        }

        // GET: VASC/c_controller/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_controller c_controller = db.c_controller.Find(id);
            if (c_controller == null)
            {
                return HttpNotFound();
            }
            return View(c_controller);
        }


        // GET: VASC/c_controller/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simplere) to create a new trigger pass ID = -1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_controller c_controller;

            if (id == -1) //create new
            {
                c_controller = new c_controller();
                c_controller.enable_bit = (int)Enable_bit.Disabled;
                c_controller.flags = (int)ControllerFlags.noflags;
            }
            else //find the existing alert 
            {
                c_controller = db.c_controller.Find(id);

                if (c_controller == null)
                {
                    return HttpNotFound();
                }
            }
            ViewBag.class_id = new SelectList(db.c_controller_class, "id", "name", c_controller.class_id);
            return View(c_controller);
        }

        // POST: VASC/c_controller/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(c_controller c_controller)
        {
            if (ModelState.IsValid)
            {
                if (c_controller.id == -1)//add new 
                {
                    db.c_controller.Add(c_controller);
                }
                else
                {
                    db.Entry(c_controller).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Close", "Home", new { area = "" });
            }
            ViewBag.class_id = new SelectList(db.c_controller_class, "id", "name", c_controller.class_id);
            return View(c_controller);
        }

        // GET: VASC/c_controller/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_controller c_controller = db.c_controller.Find(id);
            if (c_controller == null)
            {
                return HttpNotFound();
            }
            return View(c_controller);
        }

        // POST: VASC/c_controller/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_controller c_controller = db.c_controller.Find(id);
            db.c_controller.Remove(c_controller);
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
