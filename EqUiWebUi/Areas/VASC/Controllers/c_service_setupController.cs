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
    public class c_service_setupController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_service_setup
        //show a list of session configured
        public ActionResult Index()
        {
            return View(db.c_service_setup.Where(c => c.name == "SESSION_NAME").ToList());
        }

        // GET: VASC/c_service_setup/_sessionSetup
        public ActionResult _sessionSetup(int? SessionDataBit)
        {
            //must use like _list with enable mask and get bits !

            List<c_service_setup> list = new List<c_service_setup>();
            list.AddRange(db.c_service_setup.Where(c => c.bit_id == SessionDataBit || c.bit_id == -1).ToList());
            return PartialView(list);
        }

        // GET: VASC/c_service_setup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_service_setup c_service_setup = db.c_service_setup.Find(id);
            if (c_service_setup == null)
            {
                return HttpNotFound();
            }
            return View(c_service_setup);
        }

        // GET: VASC/c_service_setup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VASC/c_service_setup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,bit_id,name,value,description")] c_service_setup c_service_setup)
        {
            if (ModelState.IsValid)
            {
                db.c_service_setup.Add(c_service_setup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_service_setup);
        }

        // GET: VASC/c_service_setup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_service_setup c_service_setup = db.c_service_setup.Find(id);
            if (c_service_setup == null)
            {
                return HttpNotFound();
            }
            return View(c_service_setup);
        }

        // POST: VASC/c_service_setup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,bit_id,name,value,description")] c_service_setup c_service_setup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_service_setup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_service_setup);
        }

        // GET: VASC/c_service_setup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_service_setup c_service_setup = db.c_service_setup.Find(id);
            if (c_service_setup == null)
            {
                return HttpNotFound();
            }
            return View(c_service_setup);
        }

        // POST: VASC/c_service_setup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_service_setup c_service_setup = db.c_service_setup.Find(id);
            db.c_service_setup.Remove(c_service_setup);
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
