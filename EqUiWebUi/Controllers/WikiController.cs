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
    public class WikiController : Controller
    {
        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: Wiki
        public ActionResult Index()
        {
            return View(db.Wiki.ToList());
        }

        public ActionResult GetWiki(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wiki wiki = db.Wiki.Find(id);
            if (wiki == null)
            {
                Wiki newWiki = new Wiki();
                newWiki.createDate = System.DateTime.Now;
                newWiki.l_user_id = EqUiWebUi.CurrentUser.Getuser.id;
                newWiki.wiki1 = "New page";
                newWiki.isDead = false;
                db.Wiki.Add(newWiki);
                db.SaveChanges();
                wiki = newWiki;
            }
            return PartialView(wiki);
        }

        // GET: Wiki/Edit/5
        public ActionResult Edit(string id)
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

        // POST: Wiki/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Wiki wiki)
        {
            if (ModelState.IsValid)
            {
                //make a new wiki mark the old one as dead 

                db.Entry(wiki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wiki);
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
