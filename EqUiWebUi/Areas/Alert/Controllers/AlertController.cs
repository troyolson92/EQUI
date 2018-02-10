using EqUiWebUi.Areas.Alert.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Alert.Controllers
{
    public class AlertController : Controller
    {
        //Rewrite hangfire configureAtion
        public void configureHangfire()
        {
            Log.Info("Hangfire config for alerts has be rewritten");
            AlertEngine alertEngine = new AlertEngine();
            //do cleanup in hangfire for triggers that are not active
            alertEngine.ClearHanfireAlertwork();
            //write confirareation to hangefire.
            alertEngine.ConfigureHangfireAlertWork();
        }

        //stop al alert processing in hangefire
        public void stopHanfire()
        {
            Log.Info("Hangfire config for alerts has been stopped!");
            AlertEngine alertEngine = new AlertEngine();
            alertEngine.ClearHanfireAlertwork(true);
        }

        //interface where users can manage the alerts
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Listalerts AND filter the alerts based on the users profile
        public async Task<ActionResult> Listalerts()
        {
            //filter alerts basted on user profile!
            string UserLocationroot = Session["LocationRoot"].ToString();
            var h_alert = db.h_alert.Include(h => h.c_state).Include(h => h.c_triggers).Include(h => h.ChangedUser).Include(h => h.CloseUser).Include(h => h.AcceptUser);
            return View(await h_alert.Where(a => a.location.Contains(UserLocationroot)).ToListAsync());
        }

        // GET: Alert/Edit/5
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
            return View(h_alert);
        }

        // POST: Alert/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] //to alow posting of raw html data
        public async Task<ActionResult> Edit(h_alert alert)
        {
            //get current users ID 
            int ThisUsersID = 1; //to be implemented! 

            if (ModelState.IsValid)
            {
                //do some stuff based on the new state request
                switch (alert.state)
                {
                    case 1: //WGK
                        //nothing to do
                        break;

                    case 2: //OKREQ
                        if (!alert.acceptUserID.HasValue)
                        {
                            alert.acceptTimestamp = System.DateTime.Now;
                            alert.acceptUserID = ThisUsersID;
                        }
                        break;

                    case 3: //COMP 
                    case 4: //VOID
                        if (!alert.closeUserID.HasValue)
                        {
                            alert.closeTimestamp = System.DateTime.Now;
                            alert.closeUserID = ThisUsersID;
                        }
                        break;
                    default:
                        //unhandled state
                        break;
                }

                //update last changed user 
                alert.lastChangedTimestamp = System.DateTime.Now;
                alert.lastChangedUserID = ThisUsersID;

                //append on each save to comments section. (include state user and datetime.)
                string commentHeading = string.Format("=>entry for: {0}, state: {1}, dt: {2}", alert.ChangedUser,alert.c_state.state, alert.lastChangedTimestamp);
                //append the users new comments (we do this because we don't whant the user to be able to edit previous comments)

                //to be implemnted !

                //
                db.Entry(alert).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Listalerts");
            }
            //if model not valid return to revalidate
            ViewBag.state = new SelectList(db.c_state, "id", "discription", alert.state);
            return View(alert);
        }

    }
}