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
using EQUICommunictionLib;

namespace EqUiWebUi.Areas.Alert.Controllers
{
    [Authorize(Roles = "Administrator, AlertMaster")]
    public class c_triggersController : Controller
    {
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/c_triggers
        public async Task<ActionResult> Index()
        {
            var c_triggers = db.c_triggers.Include(c => c.c_smsSystem).Include(c => c.c_state);
            return View(await c_triggers.ToListAsync());
        }

        //run the alert trigger in debug mode (manual triggerd no hangfire
        public void RunAlertTrigger(int triggerID)
        {
            AlertEngine alertEngine = new AlertEngine();
            alertEngine.CheckForalerts(triggerID, "debugRun");
        }

        // GET: Alert/c_triggers/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simplere) to create a new trigger pass ID = -1
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            c_triggers c_triggers;

            if (id == -1) //create new alert
            {
                c_triggers = new c_triggers();
            }
            else //find the existing alert 
            {
                c_triggers = await db.c_triggers.FindAsync(id);
                if (c_triggers == null)
                {
                    return HttpNotFound();
                }
            }

            ViewBag.smsSystem = new SelectList(db.c_smsSystem, "id", "discription", c_triggers.smsSystem);
            ViewBag.initial_state = new SelectList(db.c_state, "id", "discription", c_triggers.initial_state);
            return View(c_triggers);
        }

        // POST: Alert/c_triggers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] //to alow posting of raw html data
        public async Task<ActionResult> Edit(c_triggers c_triggers)
        {
            if (ModelState.IsValid)
            {
                if (c_triggers.id == -1)//add new trigger
                {
                    db.c_triggers.Add(c_triggers);
                }
                else //update existing
                {
                    db.Entry(c_triggers).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();
                //if safe if Oke commit the alert to hangfire
                AlertEngine alertEngine = new AlertEngine();
                if (c_triggers.enabled)
                {
                    alertEngine.ConfigureHangfireAlertWork(c_triggers.id);
                }
                else
                {
                    alertEngine.ClearHanfireAlertwork();
                }

                //
                return RedirectToAction("Index");
            }
            ViewBag.smsSystem = new SelectList(db.c_smsSystem, "id", "discription", c_triggers.smsSystem);
            ViewBag.initial_state = new SelectList(db.c_state, "id", "discription", c_triggers.initial_state);
            return View(c_triggers);
        }

        // GET: Alert/c_triggers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_triggers c_triggers = await db.c_triggers.FindAsync(id);
            if (c_triggers == null)
            {
                return HttpNotFound();
            }
            return View(c_triggers);
        }

        // POST: Alert/c_triggers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //before we delete the trigger we need to delete the alerts.
            ConnectionManager connectionManager = new ConnectionManager();
            //run command against selected database.
            string deleteQry = "delete GADATA.Alerts.h_alert from GADATA.Alerts.h_alert where c_tirgger_id = {0}";
            connectionManager.RunCommand(string.Format(deleteQry, id.ToString()), enblExeptions: true);

            //delete the trigger 
            c_triggers c_triggers = await db.c_triggers.FindAsync(id);
            db.c_triggers.Remove(c_triggers);
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
