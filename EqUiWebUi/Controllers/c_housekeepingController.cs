using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Models;
using Newtonsoft.Json.Linq;

namespace EqUiWebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class c_housekeepingController : Controller
    {
        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: c_housekeeping
        public ActionResult Index()
        {
            IQueryable<c_housekeeping> c_housekeeping = db.c_housekeeping.Include(c => c.c_datasource).Include(c => c.c_schedule).AsQueryable();
            return View(c_housekeeping);
        }

        // GET: _l_housekeeping
        public ActionResult _L_housekeeping(int? c_houseKeeping_id)
        {
            IQueryable<L_housekeeping> data = db.L_housekeeping.Include(c => c.c_housekeeping).Where(c => c.c_housekeeping_id == c_houseKeeping_id || c_houseKeeping_id == null).AsQueryable();
            return PartialView(data);
        }

        // GET: c_housekeeping/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_housekeeping c_housekeeping = db.c_housekeeping.Find(id);
            if (c_housekeeping == null)
            {
                return HttpNotFound();
            }
            return View(c_housekeeping);
        }

        // GET: c_housekeeping/Create
        public ActionResult Create()
        {
            ViewBag.c_datasource_id = new SelectList(db.c_datasource, "Id", "Name");
            ViewBag.c_schedule_id = new SelectList(db.c_schedule, "id", "name");
            return View();
        }

        // POST: c_housekeeping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,c_schedule_id,c_datasource_id,Ordinal,Name,Description,SchemaName,TableName,nDaysKeepHistory,nDeleteBatchSize,nMaxRunTime,IdColName,DateTimeColName")] c_housekeeping c_housekeeping)
        {
            if (ModelState.IsValid)
            {
                db.c_housekeeping.Add(c_housekeeping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.c_datasource_id = new SelectList(db.c_datasource, "Id", "Name", c_housekeeping.c_datasource_id);
            ViewBag.c_schedule_id = new SelectList(db.c_schedule, "id", "name", c_housekeeping.c_schedule_id);
            return View(c_housekeeping);
        }

        // GET: c_housekeeping/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_housekeeping c_housekeeping = db.c_housekeeping.Find(id);
            if (c_housekeeping == null)
            {
                return HttpNotFound();
            }
            ViewBag.c_datasource_id = new SelectList(db.c_datasource, "Id", "Name", c_housekeeping.c_datasource_id);
            ViewBag.c_schedule_id = new SelectList(db.c_schedule, "id", "name", c_housekeeping.c_schedule_id);
            return View(c_housekeeping);
        }

        // POST: c_housekeeping/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,c_schedule_id,c_datasource_id,Ordinal,Name,Description,SchemaName,TableName,nDaysKeepHistory,nDeleteBatchSize,nMaxRunTime,IdColName,DateTimeColName")] c_housekeeping c_housekeeping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_housekeeping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.c_datasource_id = new SelectList(db.c_datasource, "Id", "Name", c_housekeeping.c_datasource_id);
            ViewBag.c_schedule_id = new SelectList(db.c_schedule, "id", "name", c_housekeeping.c_schedule_id);
            return View(c_housekeeping);
        }

        // GET: c_housekeeping/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_housekeeping c_housekeeping = db.c_housekeeping.Find(id);
            if (c_housekeeping == null)
            {
                return HttpNotFound();
            }
            return View(c_housekeeping);
        }

        // POST: c_housekeeping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_housekeeping c_housekeeping = db.c_housekeeping.Find(id);
            db.c_housekeeping.Remove(c_housekeeping);
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


        //test for in line edit 
        public string UpdateLabel(string id, string value)
        {
            return value;
        }

        public ActionResult saveuser(int id, string propertyName, string value)
        {
            var status = false;
            var message = "";

            //Update data to database 
            /*
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var user = dc.SiteUsers.Find(id);
                if (user != null)
                {
                    dc.Entry(user).Property(propertyName).CurrentValue = value;
                    dc.SaveChanges();
                    status = true;
                }
                else
                {
                    message = "Error!";
                }
            }
            */
            var response = new { value = value, status = status, message = message };
            JObject o = JObject.FromObject(response);
            return Content(o.ToString());
        }

    }
}
