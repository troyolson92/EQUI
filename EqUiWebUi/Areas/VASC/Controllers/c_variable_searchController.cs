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
            return View();
        }

        // GET: VASC/c_variable_search/_List
        //Will return partial view with a list of the c_variable_search.
        //Filterable by enable mask
        public ActionResult _List(int? enable_mask, int? controller_id)
        {
            ViewBag.controller_id = controller_id;
            List<c_variable_search> list = new List<c_variable_search>();
            if (enable_mask is null)
            {
                list = db.c_variable_search.ToList();
            }
            else
            {
                var setbits = Enumerable.Range(0, 32).Where(x => ((enable_mask >> x) & 1) == 1);

                foreach (int setbit in setbits)
                {
                    list.AddRange(db.c_variable_search.Where(c => c.enable_bit == setbit+1 && c.enable_bit != 0).ToList());
                }
            }
            return PartialView(list);
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
                c_variable_search.rt_table = "[NGAC].[Rt_value_Search]";

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
                if (c_variable_search.id == -1)//add new 
                {
                    db.c_variable_search.Add(c_variable_search);
                }
                else
                {
                    db.Entry(c_variable_search).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_variable_search);
        }

        // GET: VASC/c_variable_search/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_variable_search c_variable_search = db.c_variable_search.Find(id);
            if (c_variable_search == null)
            {
                return HttpNotFound();
            }
            return View(c_variable_search);
        }

        // POST: VASC/c_variable_search/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_variable_search c_variable_search = db.c_variable_search.Find(id);
            db.c_variable_search.Remove(c_variable_search);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //routine to clear rt_value from all data that is no longer configured in c_variable
        public void CleanUpRt_value_Search()
        {
            string qry = @"delete GADATA.NGAC.Rt_value_Search
                        from GADATA.NGAC.Rt_value_Search
                        left join GADATA.NGAC.c_variable_search on c_variable_search.id = Rt_value_Search.c_variable_search_id
                        where c_variable_search.id is null";
            EQUICommunictionLib.GadataComm gadataComm = new EQUICommunictionLib.GadataComm();
            gadataComm.RunCommandGadata(qry, runAsAdmin: true, enblExeptions: true);
        }

        // GET: VASC/c_variable/GetVariableData
        public ActionResult GetVariableData(int c_variable_id, int? controller_id)
        {
            //get variable config by ID

            //build Qry 

            //get data 

            //build object 

            return View();
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
