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
    public class c_CPT600Controller : Controller
    {
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/c_CPT600
        public async Task<ActionResult> Index()
        {
            return View(await db.c_CPT600.ToListAsync());
        }

        // GET: Alert/c_CPT600/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_CPT600 c_CPT600 = await db.c_CPT600.FindAsync(id);
            if (c_CPT600 == null)
            {
                return HttpNotFound();
            }
            return View(c_CPT600);
        }

        // GET: Alert/c_CPT600/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alert/c_CPT600/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Discription,System,LocationTree,AssetRoot,ActivePloeg,StartTime,EndTime,SMSlimit,SMSsend")] c_CPT600 c_CPT600)
        {
            if (ModelState.IsValid)
            {
                db.c_CPT600.Add(c_CPT600);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(c_CPT600);
        }

        // GET: Alert/c_CPT600/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_CPT600 c_CPT600 = await db.c_CPT600.FindAsync(id);
            if (c_CPT600 == null)
            {
                return HttpNotFound();
            }
            return View(c_CPT600);
        }

        // POST: Alert/c_CPT600/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Discription,System,LocationTree,AssetRoot,ActivePloeg,StartTime,EndTime,SMSlimit,SMSsend")] c_CPT600 c_CPT600)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_CPT600).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(c_CPT600);
        }

        // GET: Alert/c_CPT600/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_CPT600 c_CPT600 = await db.c_CPT600.FindAsync(id);
            if (c_CPT600 == null)
            {
                return HttpNotFound();
            }
            return View(c_CPT600);
        }

        // POST: Alert/c_CPT600/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            c_CPT600 c_CPT600 = await db.c_CPT600.FindAsync(id);
            db.c_CPT600.Remove(c_CPT600);
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
