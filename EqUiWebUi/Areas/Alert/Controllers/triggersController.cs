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
    public class triggersController : Controller
    {
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/triggers
        public async Task<ActionResult> Index()
        {
            var c_triggers = db.c_triggers.Include(c => c.c_smsSystem).Include(c => c.c_state);
            return View(await c_triggers.ToListAsync());
        }

        // GET: Alert/triggers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_triggers c_triggers = await db.c_triggers.FindAsync(id);
            if (c_triggers == null)
            {
                return HttpNotFound();
            }
            return View(c_triggers);
        }

        // GET: Alert/triggers/Create
        public ActionResult Create()
        {
            ViewBag.smsSystem = new SelectList(db.c_smsSystem, "id", "discription");
            ViewBag.initial_state = new SelectList(db.c_state, "id", "discription");
            return View();
        }

        // POST: Alert/triggers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,enabled,discription,sqlStqStatement,locationThreeMask,smsSystem,smsActivePloeg,smsActiveStartTime,smsActiveEndTime,smsLimit,smsSend,initial_state,Pollrate,alertType")] c_triggers c_triggers)
        {
            if (ModelState.IsValid)
            {
                db.c_triggers.Add(c_triggers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.smsSystem = new SelectList(db.c_smsSystem, "id", "discription", c_triggers.smsSystem);
            ViewBag.initial_state = new SelectList(db.c_state, "id", "discription", c_triggers.initial_state);
            return View(c_triggers);
        }

        // GET: Alert/triggers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_triggers c_triggers = await db.c_triggers.FindAsync(id);
            if (c_triggers == null)
            {
                return HttpNotFound();
            }
            ViewBag.smsSystem = new SelectList(db.c_smsSystem, "id", "discription", c_triggers.smsSystem);
            ViewBag.initial_state = new SelectList(db.c_state, "id", "discription", c_triggers.initial_state);
            return View(c_triggers);
        }

        // POST: Alert/triggers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,enabled,discription,sqlStqStatement,locationThreeMask,smsSystem,smsActivePloeg,smsActiveStartTime,smsActiveEndTime,smsLimit,smsSend,initial_state,Pollrate,alertType")] c_triggers c_triggers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_triggers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.smsSystem = new SelectList(db.c_smsSystem, "id", "discription", c_triggers.smsSystem);
            ViewBag.initial_state = new SelectList(db.c_state, "id", "discription", c_triggers.initial_state);
            return View(c_triggers);
        }

        // GET: Alert/triggers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_triggers c_triggers = await db.c_triggers.FindAsync(id);
            if (c_triggers == null)
            {
                return HttpNotFound();
            }
            return View(c_triggers);
        }

        // POST: Alert/triggers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            c_triggers c_triggers = await db.c_triggers.FindAsync(id);
            db.c_triggers.Remove(c_triggers);
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
