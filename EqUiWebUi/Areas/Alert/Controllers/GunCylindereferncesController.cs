using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Alert.Models;

namespace EqUiWebUi.Areas.Alert.Controllers
{
    public class GunCylindereferncesController : Controller
    {
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/GunCylinderefernces
        public async Task<ActionResult> Index(int maxdelta = 0)
        {
            var GunCylinderefernce = db.GunCylinderefernce.Include(s => s.c_controller).Where(s => (s.UCL - s.LCL) > maxdelta);
            return View(await GunCylinderefernce.ToListAsync());
        }

        // GET: Alert/GunCylinderefernces/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GunCylinderefernce gunCylinderefernce = await db.GunCylinderefernce.FindAsync(id);
            if (gunCylinderefernce == null)
            {
                return HttpNotFound();
            }
            return View(gunCylinderefernce);
        }

        // GET: Alert/GunCylinderefernces/Create
        public ActionResult Create()
        {
            ViewBag.Controller_id = new SelectList(db.c_controller, "id", "controller_name");
            return View();
        }

        // POST: Alert/GunCylinderefernces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Controller_id,tool_id,SampleStart,nDataPoints,avg,Max,Min,Stdev,UCL,LCL,id")] GunCylinderefernce gunCylinderefernce)
        {
            if (ModelState.IsValid)
            {
                db.GunCylinderefernce.Add(gunCylinderefernce);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Controller_id = new SelectList(db.c_controller, "id", "controller_name", gunCylinderefernce.Controller_id);
            return View(gunCylinderefernce);
        }

        // GET: Alert/GunCylinderefernces/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GunCylinderefernce gunCylinderefernce = await db.GunCylinderefernce.FindAsync(id);
            if (gunCylinderefernce == null)
            {
                return HttpNotFound();
            }
            ViewBag.Controller_id = new SelectList(db.c_controller, "id", "controller_name", gunCylinderefernce.Controller_id);
            return View(gunCylinderefernce);
        }

        // POST: Alert/GunCylinderefernces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Controller_id,tool_id,SampleStart,nDataPoints,avg,Max,Min,Stdev,UCL,LCL,id")] GunCylinderefernce gunCylinderefernce)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gunCylinderefernce).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Controller_id = new SelectList(db.c_controller, "id", "controller_name", gunCylinderefernce.Controller_id);
            return View(gunCylinderefernce);
        }

        // GET: Alert/GunCylinderefernces/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GunCylinderefernce gunCylinderefernce = await db.GunCylinderefernce.FindAsync(id);
            if (gunCylinderefernce == null)
            {
                return HttpNotFound();
            }
            return View(gunCylinderefernce);
        }

        // POST: Alert/GunCylinderefernces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GunCylinderefernce gunCylinderefernce = await db.GunCylinderefernce.FindAsync(id);
            db.GunCylinderefernce.Remove(gunCylinderefernce);
            await db.SaveChangesAsync();
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
