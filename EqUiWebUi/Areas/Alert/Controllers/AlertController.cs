using EqUiWebUi.Areas.Alert.Models;
using System.Collections.Generic;
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

        //specific Alert Interface
        //GET: AAOSRAlertList (for shiftboek stuff)
        public async Task<ActionResult> AAOSRAlertList(string location)
        {
            var h_alert = db.h_alert.Include(h => h.c_state).Include(h => h.c_triggers).Include(h => h.ChangedUser).Include(h => h.CloseUser).Include(h => h.AcceptUser);
            return View(await h_alert.Where(a => a.c_triggers.alertType == "Shiftbook" //only allow 
                                                && ((a.location.Trim() == location.Trim()) || (location == null)) 
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
                    org_alert = ChangeState(org_alert, _alert.state);
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
                sb.AppendLine("<h6 class='card-subtitle mb-2 text-muted'>" + org_alert.lastChangedTimestamp + "</h6>");
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
            List<h_alert> activeAlerts = (from a in db.h_alert
                                               where a.location == location
                                               select a).ToList();
            //if there is 1 active item on this loaction open it.

            //if more than 1 acitve return list 


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
            newAlert.state = 4; //inital state set to void
            //update last changed user 
            newAlert.lastChangedTimestamp = System.DateTime.Now;
            newAlert.lastChangedUserID = (int)Session["UserId"];

            newAlert.info = string.Format("{0} => {1} refid:{2}",logtype,logtext,refid.ToString());

            //adde badge to comment 
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(newAlert.comments);
            sb.AppendLine("<hr />");
            sb.AppendLine("<div class='alert alert-danger'>");
            sb.AppendLine("<strong>Triggerd: " + newAlert.C_timestamp + "</strong>");

            sb.AppendLine("<div id='logtype'>");
            sb.AppendLine("Logtype: " + logtype);
            sb.AppendLine("</div>");

            sb.AppendLine("<div id='refid'>");
            sb.AppendLine("Refid: " + refid.ToString());
            sb.AppendLine("</div>");

            sb.AppendLine("</div>");
            newAlert.comments = sb.ToString();

            //commit to database
            db.h_alert.Add(newAlert);
            await db.SaveChangesAsync();

            ViewBag.state = new SelectList(db.c_state, "id", "discription", newAlert.state);
            return View("Edit", newAlert);
        }

        //change the state of an alert
        private h_alert ChangeState (h_alert alert, int newstate)
        {
            //store current state 
            string Oldstate = alert.c_state.state;

            //do some stuff based on the new state request
            switch (newstate)
            {
                case (int)alertState.WGK: 
                        //nothing to do
                    break;

                case (int)alertState.OKREQ:
                    if (!alert.acceptUserID.HasValue)
                    {
                        alert.acceptTimestamp = System.DateTime.Now;
                        alert.acceptUserID = (int)Session["UserId"];
                    }
                    break;

                case (int)alertState.COMP:
                case (int)alertState.VOID:
                    if (!alert.closeUserID.HasValue)
                    {
                        alert.closeTimestamp = System.DateTime.Now;
                        alert.closeUserID = (int)Session["UserId"]; ;
                    }
                    break;
                default:
                    //unhandled state
                    break;
            }
            //set the new state 
            alert.state = newstate;
            //add badgje for the statechange
            StringBuilder sb = new StringBuilder();
            //add existing 
            sb.AppendLine(alert.comments);
            //add break 
            sb.AppendLine("<hr />");
            sb.AppendLine("<div class='card card-warning'>");
            sb.AppendLine("<div class='card-block'>");
            sb.AppendLine("<h4 class='card-title'>State changed</h4>");
            sb.AppendLine("<h6 class='card-subtitle mb-2 text-muted'>" + Session["Username"].ToString() + " " + System.DateTime.Now + "</h6>");
            sb.AppendLine("<p class='card-text'>");
            sb.Append("Previous state " + Oldstate);
            sb.AppendLine("</p>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            alert.comments = sb.ToString();
            //
            return alert;
        }

        // Change the state of an alert. 
        // used by quick acces dropdown 
        // GET: alert status 
        [HttpGet]
        public void SetState(int id, int newstate)
        {
            h_alert alert = (from a in db.h_alert
                              where a.id == id
                              select a).ToList().First();
            //set the new state
            alert = ChangeState(alert, newstate);
            //update last changed user 
            alert.lastChangedTimestamp = System.DateTime.Now;
            alert.lastChangedUserID = (int)Session["UserId"];
            //
            db.SaveChanges();
        }
    }
}