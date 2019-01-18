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
        public ActionResult _List(int? enable_mask, int? controller_id)
        {
            ViewBag.controller_id = controller_id;
            List<c_variable> list = new List<c_variable>();
            if (enable_mask is null)
            {
                list = db.c_variable.ToList();
            }
            else
            {
                var setbits = Enumerable.Range(0, 32).Where(x => ((enable_mask >> x) & 1) == 1);

                foreach (int setbit in setbits)
                {
                    list.AddRange(db.c_variable.Where(c => c.enable_bit == setbit+1 && c.enable_bit != 0).ToList());
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
                c_variable.rt_table = "[rt_value]";

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
                return RedirectToAction("Close", "Home", new { area = "" });
            }
            return View(c_variable);
        }

        // GET: VASC/c_variable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_variable c_variable = db.c_variable.Find(id);
            if (c_variable == null)
            {
                return HttpNotFound();
            }
            return View(c_variable);
        }

        // POST: VASC/c_variable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_variable c_variable = db.c_variable.Find(id);
            //remove the data
            string qry = @"delete NGAC.{0}
                        from NGAC.{0}
                        left join NGAC.c_variable on c_variable.id = {0}.c_variable_id
                        where c_variable.id = {1}";
            EQUICommunictionLib.ConnectionManager connectionManager = new EQUICommunictionLib.ConnectionManager();
            connectionManager.RunCommand(string.Format(qry,c_variable.rt_table,c_variable.id), enblExeptions: true);
            //remove the c_variable
            db.c_variable.Remove(c_variable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //routine to clear rt_value from all data that is no longer configured in c_variable
        public void CleanUpRt_value()
        {
            string qry = @"delete NGAC.rt_value
                        from NGAC.rt_value
                        left join NGAC.c_variable on c_variable.id = rt_value.c_variable_id
                        where c_variable.id is null";
            EQUICommunictionLib.ConnectionManager connectionManager = new EQUICommunictionLib.ConnectionManager();
            connectionManager.RunCommand(qry, enblExeptions: true);
        }

        // GET: VASC/c_variable/GetVariableData
        public ActionResult GetVariableData(int c_variable_id, int? controller_id)
        {
            //get config for this variable_id
            EqUiWebUi.Areas.VASC.Models.c_variable c_Variable = db.c_variable.Where(c => c.id == c_variable_id).FirstOrDefault();

            //base qry for c_variable system table name is injected
            string Qry = string.Format(@"
                            SELECT
                            [rt_value].[id] AS [id], 
                            [rt_value].[c_variable_id] AS [c_variable_id], 
                            [rt_value].[c_controller_id] AS [c_controller_id], 
                            [rt_value].[_timestamp] AS [C_timestamp], 
                            [rt_value].[value] AS [value], 
                            [rt_value].[isEvent] AS [isEvent], 
                            [rt_value].[abbDateTime] AS [abbDateTime]
                            FROM [NGAC].{0} AS [rt_value]",c_Variable.rt_table);

            ViewBag.SourceTable = c_Variable.rt_table;

            //IEnumerable does not work as nice like Iquery because the pager does not inset to fetch next... we need to do this here.
            IEnumerable<EqUiWebUi.Areas.VASC.Models.rt_value> result;

            if (controller_id != null)
            {
                result = db.rt_value.SqlQuery(Qry).Where(c => c.c_variable_id == c_variable_id && c.c_controller_id == controller_id);
            }
            else
            {
                result = db.rt_value.SqlQuery(Qry).Where(c => c.c_variable_id == c_variable_id);
            }

            return View(result.AsQueryable());
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
