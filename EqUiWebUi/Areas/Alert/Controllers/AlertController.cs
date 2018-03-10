﻿using EqUiWebUi.Areas.Alert.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Alert.Controllers
{
    public class AlertController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Rewrite hangfire configureAtion
        public void ConfigureHangfire()
        {
            log.Info("Hangfire config for alerts has be rewritten");
            AlertEngine alertEngine = new AlertEngine();
            //do cleanup in hangfire for triggers that are not active
            alertEngine.ClearHanfireAlertwork();
            //write confirareation to hangefire.
            alertEngine.ConfigureHangfireAlertWork();
        }

        //stop al alert processing in hangefire
        public void StopHanfire()
        {
            log.Info("Hangfire config for alerts has been stopped!");
            AlertEngine alertEngine = new AlertEngine();
            alertEngine.ClearHanfireAlertwork(true);
        }

        //interface where users can manage the alerts
        private GADATA_AlertModel db = new GADATA_AlertModel();

        //Global Alert interface
        // GET: Listalerts AND filter the alerts based on the users profile
        public async Task<ActionResult> Listalerts()
        {
            //filter alerts basted on user profile!
            string UserLocationroot = Session["LocationRoot"].ToString();
            var h_alert = db.h_alert.Include(h => h.c_state).Include(h => h.c_triggers).Include(h => h.ChangedUser).Include(h => h.CloseUser).Include(h => h.AcceptUser);
            return View(await h_alert.Where(a => a.locationTree.Contains(UserLocationroot)).ToListAsync());
        }

        //specific Alert interface
        //GET: AASPOTAlertList (for SBCU and gun cylinder stuff with quick link toSbcu tool.
        public async Task<ActionResult> AASPOTAlertList()
        {
            //filter alerts basted on user profile!
            string UserLocationroot = Session["LocationRoot"].ToString();
            var h_alert = db.h_alert.Include(h => h.c_state).Include(h => h.c_triggers).Include(h => h.ChangedUser).Include(h => h.CloseUser).Include(h => h.AcceptUser);
            return View(await h_alert.Where(a => a.locationTree.Contains(UserLocationroot) 
                                                &&(
                                                   a.c_triggers.alertType == "SBCUalert" //only allow 
                                                   ||a.c_triggers.alertType == "GUNalert"
                                                   )
                                                ).ToListAsync());
        }

        // GET: Alert/Details partial to get basic info about alert 
        public async Task<ActionResult> _Details (int? id)
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
            return PartialView(h_alert);
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
        public async Task<ActionResult> Edit(h_alert _alert)
        {
            //get original alert (this insures when multible users edit at the same time nothing should get lost)
            h_alert org_alert = await db.h_alert.FindAsync(_alert.id);
            if (org_alert == null)
            {
                return HttpNotFound();
            }
            //if the poseted model state is valid
            if (ModelState.IsValid)
            {
                if (org_alert.state != _alert.state)
                {
                    //do some stuff based on the new state request
                    switch (_alert.state)
                    {
                        case 1: //WGK
                                //nothing to do
                            break;

                        case 2: //OKREQ
                            if (!org_alert.acceptUserID.HasValue)
                            {
                                org_alert.acceptTimestamp = System.DateTime.Now;
                                org_alert.acceptUserID = (int)Session["UserId"];
                            }
                            break;

                        case 3: //COMP 
                        case 4: //VOID
                            if (!org_alert.closeUserID.HasValue)
                            {
                                org_alert.closeTimestamp = System.DateTime.Now;
                                org_alert.closeUserID = (int)Session["UserId"]; ;
                            }
                            break;
                        default:
                            //unhandled state
                            break;
                    }
                    //set the new state 
                    org_alert.state = _alert.state;
                }

                //update last changed user 
                org_alert.lastChangedTimestamp = System.DateTime.Now;
                org_alert.lastChangedUserID = (int)Session["UserId"];

                //append the users new comments (we do this because we don't whant the user to be able to edit previous comments)
                StringBuilder sb = new StringBuilder();
                //add existing 
                sb.AppendLine(org_alert.comments);
                //add break 
                sb.AppendLine("<hr />");
                //add new pannel
                sb.AppendLine("<div class='card card-info'>");
                sb.AppendLine("<div class='card-block'>");
                sb.AppendLine("<h4 class='card-title'>" + Session["Username"].ToString() + "</h4>");
                sb.AppendLine("<h6 class='card-subtitle mb-2 text-muted'>" + org_alert.lastChangedTimestamp + " Previous State: "+ org_alert.c_state.state + "</h6>");
                sb.AppendLine("<p class='card-text'>");
                sb.Append(_alert.comments);
                sb.AppendLine("</p>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
                org_alert.comments = sb.ToString();
                //save it 
                db.Entry(org_alert).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Listalerts");
            }
            //if model not valid return to revalidate
            ViewBag.state = new SelectList(db.c_state, "id", "discription", _alert.state);
            return View(_alert);
        }

        // Create a new shiftbook item this is also implemented in VSTO plugin
        //returns to the default edit alert view 
        public async Task<ActionResult> CreateShiftbookItem(string locationTree, string location, string logtype, string logtext, string refid)
        {
            //check if there is active item !!!!



            h_alert newAlert = new h_alert();
            newAlert.c_triggers = (from trig in db.c_triggers
                                   where trig.id == 13 //shiftbook trigger id 
                                   select trig).FirstOrDefault();

            newAlert.locationTree = locationTree;
            newAlert.location = location;
            newAlert.Classification = "AAOSR";
            newAlert.alarmobject = newAlert.location; //set to same as location 
            newAlert.C_timestamp = System.DateTime.Now;
            newAlert.triggerCount = 1;
            newAlert.lastTriggerd = newAlert.C_timestamp;
            //update last changed user 
            newAlert.lastChangedTimestamp = System.DateTime.Now;
            newAlert.lastChangedUserID = (int)Session["UserId"];

            newAlert.info = string.Format("{0} => {1} refid:{2}",logtype,logtext,refid.ToString());

            newAlert.comments = ""; //add badge with details about the referenced object and a link to the breakdown
            newAlert.state = 4; //inital state set to void

            //commit to database
            db.h_alert.Add(newAlert);
            await db.SaveChangesAsync();

            ViewBag.state = new SelectList(db.c_state, "id", "discription", newAlert.state);
            return View("Edit", newAlert);
        }
    }
}