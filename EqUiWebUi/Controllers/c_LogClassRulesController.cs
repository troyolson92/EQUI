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
    public class c_LogClassRulesController : Controller
    {
        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: c_LogClassRules
        public ActionResult Index()
        {
            return View();
        }

        //Make a partial view to get the rules. (used in the classification tool)
        //allow to filter by Classifcation / Subgroup or LogClassSystem or combination off
        public ActionResult _GetLogClassRulesGrid(int? c_ClassificationId, int? c_SubgroupId, int? c_logClassSystem_id)
        {
            IQueryable<c_LogClassRules> c_LogClassRules = db.c_LogClassRules.Include(c => c.c_logClassSystem).Where(c => 
            ((c.c_logClassSystem_id == c_logClassSystem_id) || (c_logClassSystem_id == null))
            &&
            ((c.c_ClassificationId == c_ClassificationId) || (c_ClassificationId == null))
            &&
            ((c.c_SubgroupId == c_SubgroupId) || (c_SubgroupId == null))
            );
            return PartialView(c_LogClassRules);
        }

        // GET: c_LogClassRules/Create
        public ActionResult Create()
        {
            ViewBag.c_logClassSystem_id = new SelectList(db.c_logClassSystem, "id", "Name");
            ViewBag.c_SubgroupId = new SelectList(db.c_Subgroup, "id", "Subgroup");
            ViewBag.c_ClassificationId = new SelectList(db.c_Classification.OrderBy(c => c.Classification), "id", "Classification");
            return View();
        }

        // POST: c_LogClassRules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(c_LogClassRules c_LogClassRules)
        {
            if (ModelState.IsValid)
            {
                db.c_LogClassRules.Add(c_LogClassRules);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.c_logClassSystem_id = new SelectList(db.c_logClassSystem, "id", "Name");
            ViewBag.c_SubgroupId = new SelectList(db.c_Subgroup, "id", "Subgroup");
            ViewBag.c_ClassificationId = new SelectList(db.c_Classification.OrderBy(c => c.Classification), "id", "Classification");
            return View(c_LogClassRules);
        }

        // GET: c_LogClassRules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_LogClassRules c_LogClassRules = db.c_LogClassRules.Find(id);
            if (c_LogClassRules == null)
            {
                return HttpNotFound();
            }
            ViewBag.c_logClassSystem_id = new SelectList(db.c_logClassSystem, "id", "Name");
            ViewBag.c_SubgroupId = new SelectList(db.c_Subgroup, "id", "Subgroup");
            ViewBag.c_ClassificationId = new SelectList(db.c_Classification.OrderBy(c => c.Classification), "id", "Classification");
            return View(c_LogClassRules);
        }

        // POST: c_LogClassRules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( c_LogClassRules c_LogClassRules)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_LogClassRules).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.c_logClassSystem_id = new SelectList(db.c_logClassSystem, "id", "Name");
            ViewBag.c_SubgroupId = new SelectList(db.c_Subgroup, "id", "Subgroup");
            ViewBag.c_ClassificationId = new SelectList(db.c_Classification.OrderBy(c => c.Classification), "id", "Classification");
            return View(c_LogClassRules);
        }

        // GET: c_LogClassRules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_LogClassRules c_LogClassRules = db.c_LogClassRules.Find(id);
            if (c_LogClassRules == null)
            {
                return HttpNotFound();
            }
            return View(c_LogClassRules);
        }

        // POST: c_LogClassRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_LogClassRules c_LogClassRules = db.c_LogClassRules.Find(id);
            db.c_LogClassRules.Remove(c_LogClassRules);
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
