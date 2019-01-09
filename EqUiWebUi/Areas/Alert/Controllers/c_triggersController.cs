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
using System.Text.RegularExpressions;

namespace EqUiWebUi.Areas.Alert.Controllers
{

    public class c_triggersController : Controller
    {
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/c_triggers
        public ActionResult Index()
        {
            return View();
        }

        //gird for Index
        public ActionResult _List(int? c_schedule_id)
        {
            if (c_schedule_id.HasValue)
            {
                return PartialView(db.c_triggers.Where(c => c.c_schedule_id == c_schedule_id).Include(c => c.c_smsSystem).Include(c => c.c_state).AsQueryable());
            }
            else
            {
                return PartialView(db.c_triggers.Include(c => c.c_smsSystem).Include(c => c.c_state).AsQueryable());
            }
        }

        /// <summary>
        /// Legend partial view for alert triggers
        /// </summary>
        /// <returns></returns>
        public ActionResult _Legend()
        {
            //get list of all alerts that have an animation and can be in a report.
            List<c_triggers> triggers = db.c_triggers.Where(c => c.isInReport && c.enabled && c.Animation.Length > 1).OrderBy(c => c.Animation).ToList();
            return PartialView(triggers);
        }

        /// <summary>
        /// Wiki partial view for alert trigger
        /// </summary>
        /// <returns></returns>
        public ActionResult _Wiki(int c_trigger_id)
        {

            c_triggers trigger = db.c_triggers.Where(c => c.id == c_trigger_id).First();
            return PartialView(trigger);
        }

        //run the alert trigger in debug mode (manual triggered no hang-fire)
        [Authorize(Roles = "Administrator, AlertMaster, AlertPowerUser")]
        public void RunAlertTrigger(int triggerID)
        {
            AlertEngine alertEngine = new AlertEngine();
            alertEngine.CheckForalerts(triggerID, "debugRun",null,RunWhenDisabled:true);
        }

        // GET: Alert/c_triggers/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simpler) to create a new trigger pass ID = -1
        [Authorize(Roles = "Administrator, AlertMaster, AlertPowerUser")]
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
            ViewBag.c_datasource_id = new SelectList(db.c_datasource, "Id", "Name", c_triggers.c_datasource_id);
            ViewBag.Animation = new SelectList(getanimationList(), "Value", "Text", c_triggers.Animation);
            ViewBag.c_schedule_id = new SelectList(db.c_schedule, "id", "name", c_triggers.c_schedule_id);
            //
            return View(c_triggers);
        }

        // POST: Alert/c_triggers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, AlertMaster")]
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
                //
                return RedirectToAction("Index");
            }
            ViewBag.smsSystem = new SelectList(db.c_smsSystem, "id", "discription", c_triggers.smsSystem);
            ViewBag.initial_state = new SelectList(db.c_state, "id", "discription", c_triggers.initial_state);
            ViewBag.c_datasource_id = new SelectList(db.c_datasource, "Id", "Name", c_triggers.c_datasource_id);
            ViewBag.Animation = new SelectList(getanimationList(), "Value", "Text", c_triggers.Animation);
            ViewBag.c_schedule_id = new SelectList(db.c_schedule, "id", "name", c_triggers.c_schedule_id);
            return View(c_triggers);
        }

        // GET: Alert/c_triggers/Delete/5
        [Authorize(Roles = "Administrator, AlertMaster")]
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
        [Authorize(Roles = "Administrator, AlertMaster")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //before we delete the trigger we need to delete the alerts.
            ConnectionManager connectionManager = new ConnectionManager();
            //run command against selected database.
            string deleteQry = "delete GADATA.Alerts.h_alert from GADATA.Alerts.h_alert where c_tirgger_id = {0}";
            connectionManager.RunCommand(string.Format(deleteQry, id.ToString()), enblExeptions: true);
            //clear hangfire
            db.c_triggers.Find(id).enabled = false;
            db.SaveChanges();
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

        //helper to make selectlist from animation file
        private List<SelectListItem> getanimationList()
        {
            //make dropdown from styles available in tableRowStyles.css
            string StyleFile = System.IO.File.ReadAllText(Server.MapPath(Url.Content("~/Content/TableRowStyles.css")));
            MatchCollection mt = Regex.Matches(StyleFile, @"\.(.*?)\{", RegexOptions.Multiline);
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "noAnimation", Value = null }); // dummy null item
            for (int i = 0; i < mt.Count; i++)
            {
                string cls = mt[i].Captures[0].ToString().Trim();
                var className = cls.Substring(1, cls.IndexOf("{") - 1).Trim().Replace(":before", "").Replace(":after", "");
                list.Add(new SelectListItem { Text = className, Value = className });
            }
            return list;
        }
    }
}
