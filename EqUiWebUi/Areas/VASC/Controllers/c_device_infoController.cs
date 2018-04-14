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
            return View();
        }

        // GET: VASC/c_device_info/_List
        //Will return partial view with a list of the c_device_info.
        //Filterable by enable bit
        public ActionResult _List(int? enable_mask, int? controller_id)
        {
            ViewBag.controller_id = controller_id;
            List<c_device_info> list = new List<c_device_info>();
            if (enable_mask is null)
            {
                list = db.c_device_info.ToList();
            }
            else
            {
                var setbits = Enumerable.Range(0, 32).Where(x => ((enable_mask >> x) & 1) == 1);

                foreach (int setbit in setbits)
                {
                    list.AddRange(db.c_device_info.Where(c => c.enable_bit == setbit+1 && c.enable_bit != 0).ToList());
                }
            }
            return PartialView(list);
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

        // GET: VASC/c_device_info/_getDeviceInfoData
        public ActionResult GetDeviceInfoData(int c_device_info_id, int? controller_id)
        {
            if (controller_id != null)
            {
                return View(db.rt_device_info.Where(c => c.c_device_info_id == c_device_info_id && c.c_controller_id == controller_id));
            }
            else
            {
                return View(db.rt_device_info.Where(c => c.c_device_info_id == c_device_info_id));
            }
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
