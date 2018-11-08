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
    public class WikisController : Controller
    {
        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: Wikis
        public ActionResult Index()
        {
            return View(db.Wiki.ToList());
        }

        // Get partion shwowiki
        public ActionResult ShowWiki(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wiki wiki = db.Wiki.Find(id);
            if (wiki == null)
            {
                return HttpNotFound();
            }
            return View(wiki);
        }

        // GET: Wikis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wiki wiki = db.Wiki.Find(id);
            if (wiki == null)
            {
                return HttpNotFound();
            }
            return View(wiki);
        }

        // GET: Wikis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wiki wiki = db.Wiki.Find(id);
            if (wiki == null)
            {
                return HttpNotFound();
            }
            return View(wiki);
        }

        // POST: Wikis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] //to allow posting of raw HTML data
        public ActionResult Edit(Wiki wiki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wiki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wiki);
        }

        // GET: Wikis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wiki wiki = db.Wiki.Find(id);
            if (wiki == null)
            {
                return HttpNotFound();
            }
            return View(wiki);
        }

        // POST: Wikis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wiki wiki = db.Wiki.Find(id);
            db.Wiki.Remove(wiki);
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
