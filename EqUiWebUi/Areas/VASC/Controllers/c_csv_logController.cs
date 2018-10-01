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
    public class c_csv_logController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_csv_log
        public ActionResult Index()
        {
            return View();
        }

        // GET: VASC/c_csv_log/_List
        //Will return partial view with a list of the c_csv_log.
        //Filterable by enable bit
        public ActionResult _List(int? enable_mask, int? controller_id)
        {
            ViewBag.controller_id = controller_id;
            List<c_csv_log> list = new List<c_csv_log>();
            if (enable_mask is null)
            {
                list = db.c_csv_log.ToList();
            }
            else
            {
                var setbits = Enumerable.Range(0, 32).Where(x => ((enable_mask >> x) & 1) == 1);
                foreach (int setbit in setbits)
                {
                    list.AddRange(db.c_csv_log.Where(c => c.enable_bit == setbit+1 && c.enable_bit != 0).ToList());
                }
            }
            return PartialView(list);
        }

        // GET: VASC/c_csv_log/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simplere) to create a new trigger pass ID = -1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            c_csv_log c_csv_log;

            if (id == -1) //create new alert
            {
                c_csv_log = new c_csv_log();
                //set default
                c_csv_log._Enable_bit = Enable_bit.Disabled;
                c_csv_log._Poll_Rate = Poll_rate.ReadOnConnect;
                c_csv_log._Csv_log_Flags = Csv_log_Flags.obtained_PCSDK;
            }
            else //find the existing alert 
            {
                c_csv_log = db.c_csv_log.Find(id);
                if (c_csv_log == null)
                {
                    return HttpNotFound();
                }
            }

            return View(c_csv_log);
        }

        // POST: VASC/c_csv_log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(c_csv_log c_csv_log)
        {
            if (ModelState.IsValid)
            {
                if (c_csv_log.id == -1)//add new 
                {
                    db.c_csv_log.Add(c_csv_log);
                }
                else
                {
                    db.Entry(c_csv_log).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_csv_log);
        }

        // GET: VASC/c_csv_log/GetCsvFileData
        public ActionResult GetCsvFileData(int c_csv_log_id, int? controller_id)
        {
            if (controller_id != null)
            {
                return View(db.rt_csv_file.Where(c => c.c_csv_log_id == c_csv_log_id && c.c_controller_id == controller_id));
            }
            else
            {
                return View(db.rt_csv_file.Where(c => c.c_csv_log_id == c_csv_log_id));
            }
        }

        // GET: VASC/c_csv_log/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_csv_log c_csv_log = db.c_csv_log.Find(id);
            if (c_csv_log == null)
            {
                return HttpNotFound();
            }
            return View(c_csv_log);
        }

        // POST: VASC/c_csv_log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_csv_log c_csv_log = db.c_csv_log.Find(id);
            //remove the data
            //must still change to DELETE
            string DATADELETEqry = @"select * from GADATA.NGAC.c_csv_log as c 
                                    left join GADATA.NGAC.rt_csv_file as csvFile on csvFile.c_csv_log_id = c.id
                                    left join {0} as csvData on csvData.[rt_csv_file_id] = csvfile.id
                                    where c.id = {1}";
            string DATADELETEcmd = string.Format(DATADELETEqry, c_csv_log.rt_table, c_csv_log.id);
            EQUICommunictionLib.ConnectionManager connectionManager = new EQUICommunictionLib.ConnectionManager();
            connectionManager.RunCommand(string.Format(DATADELETEcmd, c_csv_log.rt_table, c_csv_log.id), enblExeptions: true);
            
            //remove th rt_file

            
            //remove the c_variable
            db.c_csv_log.Remove(c_csv_log);
            db.SaveChanges();
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
