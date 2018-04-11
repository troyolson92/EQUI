using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.VASC.Models;

namespace EqUiWebUi.Areas.VASC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class c_device_infoController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_device_info
        public ActionResult Index()
        {
            return View(db.c_device_info.ToList());
        }

        // GET: VASC/c_device_info/_List
        //Will return partial view with a list of the c_device_info.
        //Filterable by enable bit
        public ActionResult _List(Enable_bit_MASK enable_Bit_MASK)
        {
            //make new extension method like HasValue but for HasBit

            //this enable bit is not nullable the others are ?
            return PartialView(db.c_device_info.Where(c => c.enable_bit == 1));
        }

        // GET: VASC/c_device_info/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_device_info c_device_info = db.c_device_info.Find(id);
            if (c_device_info == null)
            {
                return HttpNotFound();
            }
            return View(c_device_info);
        }

        // POST: VASC/c_device_info/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(c_device_info c_device_info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_device_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_device_info);
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
