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
    public class c_variable_searchController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_variable_search
        public ActionResult Index()
        {
            return View(db.c_variable_search.ToList());
        }

        // GET: VASC/c_variable_search/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            c_variable_search c_variable_search;

            if (id == -1) //create new alert
            {
                c_variable_search = new c_variable_search();
                //set default
                c_variable_search._Poll_Rate = Poll_rate.ReadOnConnect;
                c_variable_search._Enable_bit = Enable_bit.Disabled;
                c_variable_search._Insert_update = Insert_update.Insert;
                c_variable_search._SymbolTypes = SymbolTypes.None;

            }
            else //find the existing alert 
            {
                c_variable_search = db.c_variable_search.Find(id);

                if (c_variable_search == null)
                {
                    return HttpNotFound();
                }
            }
            return View(c_variable_search);
        }

        // POST: VASC/c_variable_search/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(c_variable_search c_variable_search)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_variable_search).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_variable_search);
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
