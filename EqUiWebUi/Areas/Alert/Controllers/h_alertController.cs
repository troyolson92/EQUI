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
    [Authorize(Roles = "Administrator, AlertMaster")]
    public class h_alertController : Controller
    {
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/h_alert
        public async Task<ActionResult> Index(int? c_trigger_id)
        {
            if (c_trigger_id.HasValue)
            {
                var h_alert = db.h_alert.Include(h => h.c_state).Include(h => h.c_triggers).Include(h => h.ChangedUser).Include(h => h.CloseUser).Include(h => h.AcceptUser).Where(h => h .c_tirgger_id  == c_trigger_id);
                return View(await h_alert.ToListAsync());
            }
            else
            {
                var h_alert = db.h_alert.Include(h => h.c_state).Include(h => h.c_triggers).Include(h => h.ChangedUser).Include(h => h.CloseUser).Include(h => h.AcceptUser);
                return View(await h_alert.ToListAsync());
            }
        }

        // GET: Alert/h_alert/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            h_alert h_alert = await db.h_alert.FindAsync(id);
            if (h_alert == null)
            {
                return HttpNotFound();
            }
            ViewBag.state = new SelectList(db.c_state, "id", "discription", h_alert.state);
            ViewBag.c_tirgger_id = new SelectList(db.c_triggers, "id", "discription", h_alert.c_tirgger_id);
            ViewBag.lastChangedUserID = new SelectList(db.L_users, "id", "username", h_alert.lastChangedUserID);
            ViewBag.closeUserID = new SelectList(db.L_users, "id", "username", h_alert.closeUserID);
            ViewBag.acceptUserID = new SelectList(db.L_users, "id", "username", h_alert.acceptUserID);
            return View(h_alert);
        }

        // POST: Alert/h_alert/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] //to alow posting of raw html data
        public async Task<ActionResult> Edit([Bind(Include = "id,c_tirgger_id,C_timestamp,info,state,location,comments,acceptUserID,acceptTimestamp,closeUserID,closeTimestamp,lastChangedUserID,lastChangedTimestamp,triggerCount,lastTriggerd,locationtree")] h_alert h_alert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(h_alert).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.state = new SelectList(db.c_state, "id", "discription", h_alert.state);
            ViewBag.c_tirgger_id = new SelectList(db.c_triggers, "id", "discription", h_alert.c_tirgger_id);
            ViewBag.lastChangedUserID = new SelectList(db.L_users, "id", "username", h_alert.lastChangedUserID);
            ViewBag.closeUserID = new SelectList(db.L_users, "id", "username", h_alert.closeUserID);
            ViewBag.acceptUserID = new SelectList(db.L_users, "id", "username", h_alert.acceptUserID);
            return View(h_alert);
        }

        // GET: Alert/h_alert/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            h_alert h_alert = await db.h_alert.FindAsync(id);
            if (h_alert == null)
            {
                return HttpNotFound();
            }
            return View(h_alert);
        }

        // POST: Alert/h_alert/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            h_alert h_alert = await db.h_alert.FindAsync(id);
            db.h_alert.Remove(h_alert);
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
