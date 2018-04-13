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
    public class c_variableController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_variable
        public ActionResult Index()
        {
            return View();
        }

        // GET: VASC/c_variable/_List
        //Will return partial view with a list of the c_variable.
        //Filterable by enable mask
        public ActionResult _List(int? enable_mask)
        {
            List<c_variable> list = new List<c_variable>();
            if (enable_mask is null)
            {
                list = db.c_variable.ToList();
            }
            else
            {
                var setbits = Enumerable.Range(0, 32).Where(x => ((enable_mask + 1 >> x) & 1) == 1);

                foreach (int setbit in setbits)
                {
                    list.AddRange(db.c_variable.Where(c => c.enable_bit == setbit && c.enable_bit != 0).ToList());
                }
            }
            return PartialView(list);
        }

        // GET: VASC/c_variable/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simplere) to create a new trigger pass ID = -1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            c_variable c_variable;

            if (id == -1) //create new alert
            {
                c_variable = new c_variable();
                //set default
                c_variable._Poll_Rate = Poll_rate.ReadOnConnect;
                c_variable._Enable_bit = Enable_bit.Disabled;
                c_variable._Event_code = Event_code.noEvent;

            }
            else //find the existing alert 
            {
                c_variable = db.c_variable.Find(id);

                if (c_variable == null)
                {
                    return HttpNotFound();
                }
            }
            return View(c_variable);
        }

        // POST: VASC/c_variable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( c_variable c_variable)
        {
            if (ModelState.IsValid)
            {
                if (c_variable.id == -1)//add new 
                {
                    db.c_variable.Add(c_variable);
                }
                else
                {
                    db.Entry(c_variable).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_variable);
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
