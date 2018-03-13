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
    public class SBCUreferncesController : Controller
    {
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/SBCUrefernces
        //has the option to filter on max delta
        public async Task<ActionResult> Index(int maxdelta = 0)
        {
            var sBCUrefernce = db.SBCUrefernce.Include(s => s.c_controller).Where(s => (s.UCL-s.LCL) > maxdelta);
            return View(await sBCUrefernce.ToListAsync());
        }

        // GET: Alert/SBCUrefernces/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBCUrefernce sBCUrefernce = await db.SBCUrefernce.FindAsync(id);
            if (sBCUrefernce == null)
            {
                return HttpNotFound();
            }
            return View(sBCUrefernce);
        }

        // GET: Alert/SBCUrefernces/Create
        public ActionResult Create()
        {
            ViewBag.Controller_id = new SelectList(db.c_controller, "id", "controller_name");
            return View();
        }

        // POST: Alert/SBCUrefernces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Controller_id,tool_id,Longcheck,SampleStart,nDataPoints,avg,Max,Min,Stdev,UCL,LCL,id")] SBCUrefernce sBCUrefernce)
        {
            if (ModelState.IsValid)
            {
                db.SBCUrefernce.Add(sBCUrefernce);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Controller_id = new SelectList(db.c_controller, "id", "controller_name", sBCUrefernce.Controller_id);
            return View(sBCUrefernce);
        }

        // GET: Alert/SBCUrefernces/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBCUrefernce sBCUrefernce = await db.SBCUrefernce.FindAsync(id);
            if (sBCUrefernce == null)
            {
                return HttpNotFound();
            }
            ViewBag.Controller_id = new SelectList(db.c_controller, "id", "controller_name", sBCUrefernce.Controller_id);
            return View(sBCUrefernce);
        }

        // POST: Alert/SBCUrefernces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Controller_id,tool_id,Longcheck,SampleStart,nDataPoints,avg,Max,Min,Stdev,UCL,LCL,id")] SBCUrefernce sBCUrefernce)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sBCUrefernce).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Controller_id = new SelectList(db.c_controller, "id", "controller_name", sBCUrefernce.Controller_id);
            return View(sBCUrefernce);
        }

        // GET: Alert/SBCUrefernces/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SBCUrefernce sBCUrefernce = await db.SBCUrefernce.FindAsync(id);
            if (sBCUrefernce == null)
            {
                return HttpNotFound();
            }
            return View(sBCUrefernce);
        }

        // POST: Alert/SBCUrefernces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SBCUrefernce sBCUrefernce = await db.SBCUrefernce.FindAsync(id);
            db.SBCUrefernce.Remove(sBCUrefernce);
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
