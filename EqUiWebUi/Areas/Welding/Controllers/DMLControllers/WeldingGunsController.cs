using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Welding.Models;

namespace EqUiWebUi.Areas.Welding.Controllers.DMLControllers
{
    public class WeldingGunsController : Controller
    {
        private GADATAEntitiesWelding db = new GADATAEntitiesWelding();

        // GET: Welding/WeldingGuns
        public ActionResult Index()
        {
            var weldingGun = db.WeldingGun.Include(w => w.WeldingGunVariant);
            return View(weldingGun.ToList());
        }

        // GET: Welding/WeldingGuns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeldingGun weldingGun = db.WeldingGun.Find(id);
            if (weldingGun == null)
            {
                return HttpNotFound();
            }
            return View(weldingGun);
        }

        // GET: Welding/WeldingGuns/Create
        public ActionResult Create()
        {
            ViewBag.VariantID = new SelectList(db.WeldingGunVariant, "ID", "Name");
            return View();
        }

        // POST: Welding/WeldingGuns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,ElectrodeNbr,VariantID,TimerID,RobotType")] WeldingGun weldingGun)
        {
            if (ModelState.IsValid)
            {
                db.WeldingGun.Add(weldingGun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VariantID = new SelectList(db.WeldingGunVariant, "ID", "Name", weldingGun.VariantID);
            return View(weldingGun);
        }

        // GET: Welding/WeldingGuns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeldingGun weldingGun = db.WeldingGun.Find(id);
            if (weldingGun == null)
            {
                return HttpNotFound();
            }
            ViewBag.VariantID = new SelectList(db.WeldingGunVariant, "ID", "Name", weldingGun.VariantID);
            return View(weldingGun);
        }

        // POST: Welding/WeldingGuns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,ElectrodeNbr,VariantID,TimerID,RobotType")] WeldingGun weldingGun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weldingGun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VariantID = new SelectList(db.WeldingGunVariant, "ID", "Name", weldingGun.VariantID);
            return View(weldingGun);
        }

        // GET: Welding/WeldingGuns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeldingGun weldingGun = db.WeldingGun.Find(id);
            if (weldingGun == null)
            {
                return HttpNotFound();
            }
            return View(weldingGun);
        }

        // POST: Welding/WeldingGuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WeldingGun weldingGun = db.WeldingGun.Find(id);
            db.WeldingGun.Remove(weldingGun);
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
