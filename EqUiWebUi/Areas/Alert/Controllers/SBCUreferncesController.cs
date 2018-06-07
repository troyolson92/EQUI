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
            var sBCUrefernce = db.SBCUrefernce.Include(s => s.c_controller).Where(s => (s.UCL-s.LCL) >= maxdelta);
            return View(await sBCUrefernce.ToListAsync());
        }

        // GET: Alert/SBCUrefernces/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SBCUrefernce sBCUrefernce;

            if (id == -1) //create new alert
            {
                sBCUrefernce = new SBCUrefernce();
            }
            else //find the existing alert 
            {
                sBCUrefernce = await db.SBCUrefernce.FindAsync(id);
                if (sBCUrefernce == null)
                {
                    return HttpNotFound();
                }
            }

            ViewBag.Controller_id = new SelectList(db.c_controller.OrderBy(c => c.controller_name), "id", "controller_name", sBCUrefernce.Controller_id);
            return View(sBCUrefernce);
        }

        // POST: Alert/SBCUrefernces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( SBCUrefernce sBCUrefernce)
        {
            if (ModelState.IsValid)
            {
                if (sBCUrefernce.id == -1)//add new trigger
                {
                    db.SBCUrefernce.Add(sBCUrefernce);
                }
                else //update existing
                {
                    db.Entry(sBCUrefernce).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Controller_id = new SelectList(db.c_controller.OrderBy(c => c.controller_name), "id", "controller_name", sBCUrefernce.Controller_id);
            return View(sBCUrefernce);
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
