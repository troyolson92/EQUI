using EqUiWebUi.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class c_housekeepingController : Controller
    {

        /*check for tables with no housekeeping.
         * SELECT 
            t.TABLE_SCHEMA
            ,t.TABLE_NAME
            ,c.id
            ,c.nDaysKeepHistory
            FROM INFORMATION_SCHEMA.TABLES as t
            left join equi.c_housekeeping as c on t.TABLE_NAME = c.TableName and t.TABLE_SCHEMA = c.SchemaName 
            WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='GADATA' 
            AND
            (
            t.TABLE_NAME like 'h%' OR t.TABLE_NAME like 'rt%'
         
             */


        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: c_housekeeping
        public ActionResult Index()
        {
            return View();
        }

        //gird for Index
        public ActionResult _List(int? c_schedule_id)
        {
            if (c_schedule_id.HasValue)
            {
                return PartialView(db.c_housekeeping.Where(c => c.c_schedule_id == c_schedule_id).Include(c => c.c_datasource).AsQueryable());
            }
            else
            {
                return PartialView(db.c_housekeeping.Include(c => c.c_datasource).AsQueryable());
            }
        }

        // GET: _l_housekeeping
        public ActionResult _L_housekeeping(int? c_houseKeeping_id)
        {
            return PartialView(db.L_housekeeping.Include(c => c.c_housekeeping).Where(c => c.c_housekeeping_id == c_houseKeeping_id || c_houseKeeping_id == null).AsQueryable());
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
        public ActionResult Create(c_housekeeping c_housekeeping)
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
        public ActionResult Edit( c_housekeeping c_housekeeping)
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
            //remove linked logs
            var l_Housekeeping = db.L_housekeeping.Where(c => c.c_housekeeping_id == id);
            db.L_housekeeping.RemoveRange(l_Housekeeping);
            //remove config
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

    }
}