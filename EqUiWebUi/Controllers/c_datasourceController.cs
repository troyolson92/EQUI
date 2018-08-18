using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;
using EqUiWebUi.Models;

namespace EqUiWebUi.Controllers
{
    public class c_datasourceController : Controller
    {
        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: c_datasource
        public ActionResult Index()
        {
            return View(db.c_datasource.ToList());
        }

        // GET: c_datasource/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: c_datasource/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(c_datasource c_datasource)
        {
            if (ModelState.IsValid)
            {
                db.c_datasource.Add(c_datasource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_datasource);
        }

        // GET: c_datasource/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_datasource c_datasource = db.c_datasource.Find(id);
            if (c_datasource == null)
            {
                return HttpNotFound();
            }
            return View(c_datasource);
        }

        // POST: c_datasource/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(c_datasource c_datasource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_datasource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_datasource);
        }

        // GET: c_datasource/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_datasource c_datasource = db.c_datasource.Find(id);
            if (c_datasource == null)
            {
                return HttpNotFound();
            }
            return View(c_datasource);
        }

        // POST: c_datasource/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_datasource c_datasource = db.c_datasource.Find(id);
            db.c_datasource.Remove(c_datasource);
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


        //Test a db 
        // Test all configured database. if id is null
        public JsonResult RunDbTest(int? id)
        {
            ConnectionManager connectionManager = new ConnectionManager();
            try
            {
                if (id.HasValue)
                {
                    connectionManager.TestDb(id.GetValueOrDefault());
                    return Json(new { Msg = "RunDbTest id: " + id + " OK" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List <EQUICommunictionLib.Database> list = connectionManager.GetDB();
                    foreach(EQUICommunictionLib.Database db in list)
                    {
                        connectionManager.TestDb(db.Id);
                    }
                    return Json(new { Msg = "RunDbTest (all db) OK" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.StatusDescription = "DB: " +  id + " FAIL: " + ex.Message;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        //Test change password
        public void ChangePWTest()
        {
            ConnectionManager connectionManager = new ConnectionManager();
            connectionManager.PWCheck(dbName: "MAXIMOrt", ChangeIfExpired: true, newPW: "NEWPWTEST");
        }
    }
}
