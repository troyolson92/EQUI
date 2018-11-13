using EqUiWebUi.Areas.Alert.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Alert.Controllers
{
    public class AlertController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Get Index
        public ActionResult Index()
        {
            return View();
        }

        //Get Partial settings
        public ActionResult _settings()
        {
            return PartialView();
        }

        //interface where users can manage the alerts
        private GADATA_AlertModel db = new GADATA_AlertModel();

        //Global Alert interface
        // GET: Listalerts AND filter the alerts based on the users profile
        public ActionResult Listalerts(int? c_trigger_id, int? id, string Location = "")
        {           
            var h_alert = db.h_alert.Include(h => h.c_state).Where(h =>
                ((h.id == (id ?? 0)) || (id == null))
                && ((h.c_tirgger_id == (c_trigger_id ?? 0)) || (c_trigger_id == null))
                && ((h.location.StartsWith(Location))||(Location == ""))
                ).Include(h => h.c_triggers).Include(h => h.ChangedUser).Include(h => h.CloseUser).Include(h => h.AcceptUser);

            //only apply location filter if no parms are passed 
            if (c_trigger_id.HasValue || id.HasValue || Location != "")
            {
                return View(h_alert);
            }
            else
            {
                //filter alerts basted on user profile!
                string UserLocationroot = CurrentUser.Getuser.LocationRoot;
                return View(h_alert.Where(a => a.locationTree.Contains(UserLocationroot)));
            }
        }

        //specific Alert interface for AASPOT
        //Get AASPOTIndex
        public ActionResult AASPOTIndex()
        {
            return View();
        }

        //specific Alert interface
        //GET: AASPOTAlertList (for SBCU and gun cylinder stuff with quick link toSbcu tool.
        //allow to filter on single location
        //allow to filter on open items
        //
        public ActionResult AASPOTAlertList(string LocationFilter, bool ActiveAlertOnly = false)
        {
            //filter alerts basted on user profile!
            string UserLocationroot = CurrentUser.Getuser.LocationRoot;
            var h_alert = db.h_alert.Include(h => h.c_state).Include(h => h.c_triggers).Include(h => h.ChangedUser).Include(h => h.CloseUser).Include(h => h.AcceptUser);
            var result = h_alert.Where(a => a.locationTree.Contains(UserLocationroot)
                                            &&
                                               a.c_triggers.enabled == true
                                            && (
                                               a.c_triggers.alertType == "SBCUalert" //only allow this trigger type
                                               || a.c_triggers.alertType == "GUNalert"
                                            )
                                            && (
                                              ActiveAlertOnly == false //Allow filtering on active items only
                                              || (a.state != (int)alertState.COMP && a.state != (int)alertState.TECHCOMP && a.state != (int)alertState.VOID) //not a closed item
                                            )
                                            && (
                                              LocationFilter == null //Allow filtering on a location
                                              || a.location.Contains(LocationFilter) //filter out specific location
                                            ));

            //count the total number of record to display as 'total triggers'
            ViewBag.LocationFilter = LocationFilter;
            ViewBag.AlertCount = result.Count();
            return View(result);
        }

        // GET: Alert/Details partial to get basic info about alert
        public ActionResult _Details(int? id, bool AddTrendChart = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            h_alert h_alert = db.h_alert.Find(id);
            if (h_alert == null)
            {
                return HttpNotFound();
            }

            //bug 18w36d4 URI to long...
            //if you use EDIT it does work. strange...

            ViewBag.AddTrendChart = AddTrendChart;
            return PartialView(h_alert);
        }

        // GET: Alert/_ControlChart partial. (control charts and hystory of limis)
        public ActionResult _ControlChart(h_alert h_Alert, int? l_controllimit_id)
        {
            if (l_controllimit_id.HasValue)
            {
                l_controlLimits controlLimit = db.l_controlLimits.Find(l_controllimit_id);
                h_Alert.c_tirgger_id = controlLimit.c_triggers.id;
                h_Alert.C_timestamp = System.DateTime.Now;
                h_Alert.lastTriggerd = System.DateTime.Now;
                h_Alert.alarmobject = controlLimit.alarmobject;
            }

            return PartialView(h_Alert);
        }

        // GET: Alert/Edit/5
        public async Task<ActionResult> Edit(int? id, bool CloseOnSaveSuccess = false)
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
            //pass option to close after succesful save
            ViewBag.CloseOnSaveSucces = CloseOnSaveSuccess;
            //pass the previous url in the viewbag so we can return on save action
            ViewBag.returnURL = System.Web.HttpContext.Current.Request.UrlReferrer;
            return View(h_alert);
        }

        // POST: Alert/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] //to alow posting of raw html data
        public async Task<ActionResult> Edit(h_alert _alert, string returnURL = "Listalerts")
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
                org_alert.lastChangedUserID = CurrentUser.Getuser.id;

                //append the users new comments (we do this because we don't whant the user to be able to edit previous comments)
                StringBuilder sb = new StringBuilder();
                //add existing
                sb.AppendLine(org_alert.comments);
                //add break
                sb.AppendLine("<hr />");
                //add new pannel
                sb.AppendLine("<div class='card card-info'>");
                sb.AppendLine("<div class='card-block'>");
                sb.AppendLine("<h4 class='card-title'>" + CurrentUser.Getuser.username + "</h4>");
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
                //because this edit form gets called from both ListAlerts and AASPOTalert list we must redirect to the right page.
                return Redirect(returnURL);
            }
            //if model not valid return to revalidate
            ViewBag.state = new SelectList(db.c_state, "id", "discription", _alert.state);
            return View(_alert);
        }

        //change the state of an alert
        private h_alert ChangeState(h_alert alert, int newstate)
        {
            //store current state
            string Oldstate = alert.c_state.state;
            string test = System.Enum.GetName(typeof(alertState), 3);
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
                        alert.acceptUserID = CurrentUser.Getuser.id;
                    }
                    break;

                case (int)alertState.COMP:
                case (int)alertState.VOID:
                    if (!alert.closeUserID.HasValue)
                    {
                        alert.closeTimestamp = System.DateTime.Now;
                        alert.closeUserID = CurrentUser.Getuser.id;
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
            sb.AppendLine("<h6 class='card-subtitle mb-2 text-muted'>" + CurrentUser.Getuser.username + " " + System.DateTime.Now + "</h6>");
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
            alert.lastChangedUserID = CurrentUser.Getuser.id;
            //
            db.SaveChanges();
        }
    }
}