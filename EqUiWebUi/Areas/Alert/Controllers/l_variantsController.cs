using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Alert.Models;

namespace EqUiWebUi.Areas.Alert.Controllers
{
    public class l_variantsController : Controller
    {
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/l_variants
        public ActionResult Index()
        {
            var l_variants = db.l_variants.Include(l => l.c_triggers).Include(l => l.L_users);
            return View(l_variants.ToList());
        }

        // GET: Alert/l_variants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            l_variants l_variants = db.l_variants.Find(id);
            if (l_variants == null)
            {
                return HttpNotFound();
            }
            return View(l_variants);
        }

        // GET: Alert/l_variants/Create
        public ActionResult Create()
        {
            ViewBag.c_trigger_id = new SelectList(db.c_triggers, "id", "alertType");
            ViewBag.CreateUser = new SelectList(db.L_users, "id", "username");
            return View();
        }

        // POST: Alert/l_variants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(l_variants l_variants)
        {
            if (ModelState.IsValid)
            {
                l_variants.CreateDate = System.DateTime.Now;
                l_variants.CreateUser = CurrentUser.Getuser.id;
                db.l_variants.Add(l_variants);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.c_trigger_id = new SelectList(db.c_triggers, "id", "alertType", l_variants.c_trigger_id);
            ViewBag.CreateUser = new SelectList(db.L_users, "id", "username", l_variants.CreateUser);
            return View(l_variants);
        }

        // GET: Alert/l_variants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            l_variants l_variants = db.l_variants.Find(id);
            if (l_variants == null)
            {
                return HttpNotFound();
            }
            ViewBag.c_trigger_id = new SelectList(db.c_triggers, "id", "alertType", l_variants.c_trigger_id);
            ViewBag.CreateUser = new SelectList(db.L_users, "id", "username", l_variants.CreateUser);
            return View(l_variants);
        }

        // POST: Alert/l_variants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(l_variants l_variants)
        {
            if (ModelState.IsValid)
            {
                l_variants.CreateDate = System.DateTime.Now;
                l_variants.CreateUser = CurrentUser.Getuser.id;
                db.Entry(l_variants).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.c_trigger_id = new SelectList(db.c_triggers, "id", "alertType", l_variants.c_trigger_id);
            ViewBag.CreateUser = new SelectList(db.L_users, "id", "username", l_variants.CreateUser);
            return View(l_variants);
        }

        // GET: Alert/l_variants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            l_variants l_variants = db.l_variants.Find(id);
            if (l_variants == null)
            {
                return HttpNotFound();
            }
            return View(l_variants);
        }

        // POST: Alert/l_variants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            l_variants l_variants = db.l_variants.Find(id);
            db.l_variants.Remove(l_variants);
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
