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
    [Authorize(Roles = "Administrator, AlertMaster, AlertPowerUser")]
    public class c_SMSconfigController : Controller
    {
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/c_SMSconfig
        public async Task<ActionResult> Index(int? SystemID)
        {
            if (SystemID.HasValue)
            {
                var c_SMSconfig = db.c_SMSconfig.Where(c => c.c_smsSystem.id == SystemID.GetValueOrDefault(0)).Include(c => c.c_CPT600).Include(c => c.c_smsSystem);
                return View(await c_SMSconfig.ToListAsync());
            }
            else
            {
                var c_SMSconfig = db.c_SMSconfig.Include(c => c.c_CPT600).Include(c => c.c_smsSystem);
                return View(await c_SMSconfig.ToListAsync());
            }

        }

        // GET: Alert/c_SMSconfig/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_SMSconfig c_SMSconfig = await db.c_SMSconfig.FindAsync(id);
            if (c_SMSconfig == null)
            {
                return HttpNotFound();
            }
            return View(c_SMSconfig);
        }

        // GET: Alert/c_SMSconfig/Create
        public ActionResult Create()
        {
            ViewBag.c_CPT600_id = new SelectList(db.c_CPT600, "id", "Discription");
            ViewBag.c_smsSystem_id = new SelectList(db.c_smsSystem, "id", "system");
            return View();
        }

        // POST: Alert/c_SMSconfig/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,c_smsSystem_id,c_CPT600_id")] c_SMSconfig c_SMSconfig)
        {
            if (ModelState.IsValid)
            {
                db.c_SMSconfig.Add(c_SMSconfig);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.c_CPT600_id = new SelectList(db.c_CPT600, "id", "Discription", c_SMSconfig.c_CPT600_id);
            ViewBag.c_smsSystem_id = new SelectList(db.c_smsSystem, "id", "system", c_SMSconfig.c_smsSystem_id);
            return View(c_SMSconfig);
        }

        // GET: Alert/c_SMSconfig/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_SMSconfig c_SMSconfig = await db.c_SMSconfig.FindAsync(id);
            if (c_SMSconfig == null)
            {
                return HttpNotFound();
            }
            ViewBag.c_CPT600_id = new SelectList(db.c_CPT600, "id", "Discription", c_SMSconfig.c_CPT600_id);
            ViewBag.c_smsSystem_id = new SelectList(db.c_smsSystem, "id", "system", c_SMSconfig.c_smsSystem_id);
            return View(c_SMSconfig);
        }

        // POST: Alert/c_SMSconfig/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,c_smsSystem_id,c_CPT600_id")] c_SMSconfig c_SMSconfig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_SMSconfig).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.c_CPT600_id = new SelectList(db.c_CPT600, "id", "Discription", c_SMSconfig.c_CPT600_id);
            ViewBag.c_smsSystem_id = new SelectList(db.c_smsSystem, "id", "system", c_SMSconfig.c_smsSystem_id);
            return View(c_SMSconfig);
        }

        // GET: Alert/c_SMSconfig/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_SMSconfig c_SMSconfig = await db.c_SMSconfig.FindAsync(id);
            if (c_SMSconfig == null)
            {
                return HttpNotFound();
            }
            return View(c_SMSconfig);
        }

        // POST: Alert/c_SMSconfig/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            c_SMSconfig c_SMSconfig = await db.c_SMSconfig.FindAsync(id);
            db.c_SMSconfig.Remove(c_SMSconfig);
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
