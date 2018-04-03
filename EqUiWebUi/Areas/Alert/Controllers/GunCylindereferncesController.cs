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

        // GET: Alert/GunCylinderefernces/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GunCylinderefernce gunCylinderefernce;

            if (id == -1) //create new alert
            {
                gunCylinderefernce = new GunCylinderefernce();
            }
            else //find the existing alert 
            {
                gunCylinderefernce = await db.GunCylinderefernce.FindAsync(id);
                if (gunCylinderefernce == null)
                {
                    return HttpNotFound();
                }
            }

            ViewBag.Controller_id = new SelectList(db.c_controller, "id", "controller_name", gunCylinderefernce.Controller_id);
            return View(gunCylinderefernce);
        }

        // POST: Alert/GunCylinderefernces/Edit/5
        //special EDIT. "We don't EDIT" control limits. We make a new one each time we save and use the Active bit to set the active set. 
        // if ChangeForVariant set limits for all using this variant.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GunCylinderefernce gunCylinderefernce, bool ChangeForVariant = false)
        {
            if (ModelState.IsValid)
            {
                if (ChangeForVariant && gunCylinderefernce.Variant != null)
                {

                }


                db.Entry(gunCylinderefernce).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Controller_id = new SelectList(db.c_controller, "id", "controller_name", gunCylinderefernce.Controller_id);
            return View(gunCylinderefernce);
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
