using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Models;

namespace EqUiWebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class c_SubgroupController : Controller
    {
        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: c_Subgroup
        public ActionResult Index()
        {
            return View(db.c_Subgroup.ToList());
        }
        // GET: c_Subgroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: c_Subgroup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Subgroup,Discription")] c_Subgroup c_Subgroup)
        {
            if (ModelState.IsValid)
            {
                db.c_Subgroup.Add(c_Subgroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_Subgroup);
        }

        // GET: c_Subgroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_Subgroup c_Subgroup = db.c_Subgroup.Find(id);
            if (c_Subgroup == null)
            {
                return HttpNotFound();
            }
            return View(c_Subgroup);
        }

        // POST: c_Subgroup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Subgroup,Discription")] c_Subgroup c_Subgroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_Subgroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_Subgroup);
        }

        // GET: c_Subgroup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_Subgroup c_Subgroup = db.c_Subgroup.Find(id);
            if (c_Subgroup == null)
            {
                return HttpNotFound();
            }
            return View(c_Subgroup);
        }

        // POST: c_Subgroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_Subgroup c_Subgroup = db.c_Subgroup.Find(id);
            db.c_Subgroup.Remove(c_Subgroup);
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
