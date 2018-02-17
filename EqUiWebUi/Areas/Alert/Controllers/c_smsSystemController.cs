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
    public class c_smsSystemController : Controller
    {
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/c_smsSystem
        public async Task<ActionResult> Index()
        {
            return View(await db.c_smsSystem.ToListAsync());
        }

        // GET: Alert/c_smsSystem/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_smsSystem c_smsSystem = await db.c_smsSystem.FindAsync(id);
            if (c_smsSystem == null)
            {
                return HttpNotFound();
            }
            return View(c_smsSystem);
        }

        // GET: Alert/c_smsSystem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alert/c_smsSystem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,system,Discription")] c_smsSystem c_smsSystem)
        {
            if (ModelState.IsValid)
            {
                db.c_smsSystem.Add(c_smsSystem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(c_smsSystem);
        }

        // GET: Alert/c_smsSystem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_smsSystem c_smsSystem = await db.c_smsSystem.FindAsync(id);
            if (c_smsSystem == null)
            {
                return HttpNotFound();
            }
            return View(c_smsSystem);
        }

        // POST: Alert/c_smsSystem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,system,Discription")] c_smsSystem c_smsSystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_smsSystem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(c_smsSystem);
        }

        // GET: Alert/c_smsSystem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_smsSystem c_smsSystem = await db.c_smsSystem.FindAsync(id);
            if (c_smsSystem == null)
            {
                return HttpNotFound();
            }
            return View(c_smsSystem);
        }

        // POST: Alert/c_smsSystem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            c_smsSystem c_smsSystem = await db.c_smsSystem.FindAsync(id);
            db.c_smsSystem.Remove(c_smsSystem);
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
