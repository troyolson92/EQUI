using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;
using EqUiWebUi.Areas.Maximo_ui.Models;

namespace EqUiWebUi.Areas.Maximo_ui.Controllers
{

    public class WorkorderController : Controller
    {
        //default number of days we search back in hystory
        int MaximoWorkordersDaysback = -1000;

        // GET: Maximo_ui/Workorder
        public ActionResult Index()
        {
            return View();
        }

        //full view can be intitiated by using parms or by model. 
        [HttpGet]
        public ActionResult Workorders(string location, string locancestor, bool? b_ciblings, bool? b_preventive, string jpnum, string worktype, string wonum
            , DateTime? startdate, DateTime? enddate, Models.WorkorderSelectOptions workorderSelectOptions
            , bool loadOnInit = false, bool fullscreen = false, int fontSize = 12, bool RealtimeConn = false)
        {
            //if a parm value is appended set it to the model ELSE MODEL IS BOSS
            if (location != null) workorderSelectOptions.location = location;
            if (locancestor != null) workorderSelectOptions.locancestor = locancestor;
            if (b_ciblings != null) workorderSelectOptions.b_ciblings = b_ciblings.GetValueOrDefault();
            if (b_preventive != null) workorderSelectOptions.b_preventive = b_preventive.GetValueOrDefault();
            if (jpnum != null) workorderSelectOptions.jpnum = jpnum;
            if (worktype != null) workorderSelectOptions.worktype = worktype;
            if (wonum != null) workorderSelectOptions.wonum = wonum;
            if (startdate != null)
            {
                workorderSelectOptions.startdate = startdate.GetValueOrDefault(); //if a value is passed (nullcheck)
            }
            else
            {
                workorderSelectOptions.startdate = System.DateTime.Now.AddDays(MaximoWorkordersDaysback); //default value 
            }
            if (enddate != null)
            {
                workorderSelectOptions.enddate = enddate.GetValueOrDefault(); //if a value is passed (nullcheck)
            }
            else
            {
                workorderSelectOptions.enddate = System.DateTime.Now; //default value
            }
            //to be able to override fontsize
            ViewBag.fontSize = fontSize;
            //if this is set we load the workorder directly and fold up the parms pannem
            ViewBag.loadOnInit = loadOnInit;
            //if this is set we hide navbar and render in full screen mode
            ViewBag.fullscreen = fullscreen;
            //if this is set we run on the production server.
            workorderSelectOptions.realtimeConn = RealtimeConn;
            //
            return View(workorderSelectOptions);
        }

        //can be called to be renders as partial in model or something like that...
        [HttpGet]
        public ActionResult _workordersOnLocation(string location, string locancestor, bool? b_ciblings, bool? b_preventive, string jpnum, string worktype, string wonum
            , DateTime? startdate, DateTime? enddate, bool RealtimeConn = false)
        {
            Models.WorkorderSelectOptions workorderSelectOptions = new WorkorderSelectOptions();
            //set models to parms
            workorderSelectOptions.location = location;
            workorderSelectOptions.locancestor = locancestor;
            workorderSelectOptions.b_ciblings = b_ciblings.GetValueOrDefault();
            workorderSelectOptions.b_preventive = b_preventive.GetValueOrDefault();
            workorderSelectOptions.jpnum = jpnum;
            workorderSelectOptions.worktype = worktype;
            workorderSelectOptions.wonum = wonum;
            workorderSelectOptions.startdate = startdate.GetValueOrDefault(System.DateTime.Now.AddDays(MaximoWorkordersDaysback));
            workorderSelectOptions.enddate = enddate.GetValueOrDefault(System.DateTime.Now);
            workorderSelectOptions.realtimeConn = RealtimeConn;
            //
            return PartialView(workorderSelectOptions);
        }

        //form post work Workoders main view.
        [HttpPost]
        public ActionResult _workordersOnLocation(Models.WorkorderSelectOptions workorderSelectOptions)
        {
            return PartialView(workorderSelectOptions);
        }

        //Test render _workordersOnLocation (example of how to call it as a partial)
        [HttpGet]
        public ActionResult ExampleCall_workordersOnLocation()
        {
            return View();
        }

        //gets called by AJAX to render the workorder grid
        [HttpGet]
        public ActionResult _workordersOnLocationGrid(string location, string locancestor, bool? b_ciblings, bool? b_preventive, string jpnum, string worktype, string wonum
            , DateTime? startdate, DateTime? enddate, bool RealtimeConn = false)
        {
            //check if user is allowed to user realtimeConn
            if (RealtimeConn)
            {
                roleProvider roleProvider = new roleProvider();
                if (!roleProvider.IsUserInRole(System.Web.HttpContext.Current.User.Identity.Name, "MAXIMOrealtime"))
                {
                    RealtimeConn = false;
                }
            }
            ViewBag.RealtimeConn = RealtimeConn;

            //set controller timeout! (if somebody does a crazy query)
            System.Web.HttpContext.Current.Server.ScriptTimeout = 10; //seconds

            //build qyr
            StringBuilder sbqry = new StringBuilder();
            sbqry.Append(@"
               SELECT 
                 WORKORDER.WONUM WONUM
                ,WORKORDER.STATUS
                ,WORKORDER.STATUSDATE
                ,WORKORDER.WORKTYPE
                ,WORKORDER.DESCRIPTION
                ,WORKORDER.LOCATION
                ,WORKORDER.REPORTEDBY
                ,WORKORDER.REPORTDATE
                ,WORKORDER.CHANGEDATE
                ,locancestor.ANCESTOR
                FROM MAXIMO.WORKORDER WORKORDER
                JOIN MAXIMO.locancestor locancestor on 
                locancestor.LOCATION = WORKORDER.LOCATION
                and locancestor.ORGID = WORKORDER.ORGID
                and locancestor.ANCESTOR = (
                select ancestor from (select locancestor.ancestor 
                from maximo.locancestor where locancestor.location");

            //handel ancestors.
            if (!string.IsNullOrWhiteSpace(locancestor))
            {
                string[] locancestors = locancestor.Split(';');
                //if only 1 location is passed we allow wildcards (like) 
                if (locancestors.Count() == 1)
                {
                    sbqry.Append(string.Format(" LIKE '%{0}%'", locancestors[0].Trim()));
                }
                else // if more than one location use in statement
                {
                    sbqry.Append(" IN (");
                    foreach (string ancestor in locancestors)
                    {
                        sbqry.Append("'").Append(ancestor.Trim()).Append("'");
                        if (ancestor != locancestors.Last())
                        {
                            sbqry.Append(",");
                        }
                    }
                    sbqry.AppendLine(") ");
                }
            }
            else //if no location is passed set wildcard for everything
            {
                sbqry.Append(" LIKE '%'");
            }
            //append the rest of the locancestor clause
            sbqry.AppendLine().AppendLine(@"and locancestor.ORGID = WORKORDER.ORGID
                and locancestor.location <> locancestor.ancestor 
                order by locancestor.LOCANCESTORID)
                where rownum = 1)");

            //start where clause
            sbqry.AppendLine().Append("WHERE WORKORDER.LOCATION");

            //handle locations.
            if (!string.IsNullOrWhiteSpace(location))
            {
                string[] locations = location.Split(';');
                //if only 1 location is passed we allow wildcards (like) 
                if (locations.Count() == 1)
                {
                    sbqry.Append(string.Format(" LIKE '%{0}%'", locations[0].Trim()));
                }
                else // if more than one location use in statement
                {
                    sbqry.Append(" IN (");
                    foreach (string loc in locations)
                    {
                        sbqry.Append("'").Append(loc.Trim()).Append("'");
                        if (loc != locations.Last())
                        {
                            sbqry.Append(",");
                        }
                    }
                    sbqry.AppendLine(") ");
                }
            }
            else //if no location is passed set wildcard for everything
            {
                sbqry.Append(" LIKE '%'");
            }

            //new line 
            sbqry.AppendLine();

            //handle preventive
            b_preventive = b_preventive ?? false; //default no preventive
            if (b_preventive == false)
            {
                sbqry.AppendLine("AND WORKORDER.WORKTYPE not in ('PP','PCI','WSCH')");
            }

            //hande worktype
            if (!string.IsNullOrWhiteSpace(worktype))
            {
                sbqry.Append("AND WORKORDER.WORKTYPE in (");
                string[] worktypes = worktype.Split(';');
                foreach(string type in worktypes)
                {
                    sbqry.Append("'").Append(type.Trim()).Append("'");
                    if (type != worktypes.Last())
                    {
                        sbqry.Append(",");
                    }                    
                }
                sbqry.AppendLine(") ");
            }

            //handle JPnum
            if (!string.IsNullOrWhiteSpace(jpnum))
            {
                sbqry.Append("AND WORKORDER.JPNUM in (");
                string[] jpnums = jpnum.Split(';');
                foreach (string jp in jpnums)
                {
                    sbqry.Append("'").Append(jp).Append("'");
                    if (jp != jpnums.Last())
                    {
                        sbqry.Append(",");
                    }
                }
                sbqry.AppendLine(") ");
            }

            //handle wonum
            if (!string.IsNullOrWhiteSpace(wonum))
            {
                sbqry.Append("AND WORKORDER.WONUM in (");
                string[] wonums = wonum.Split(';');
                foreach (string wo in wonums)
                {
                    sbqry.Append("'").Append(wo).Append("'");
                    if (wo != wonums.Last())
                    {
                        sbqry.Append(",");
                    }
                }
                sbqry.AppendLine(") ");
            }

            //handle timerange
            if (enddate != null)
            {
                sbqry.AppendLine(string.Format("AND WORKORDER.CHANGEDATE < TO_TIMESTAMP('{0}', 'YYYY/MM/DD HH24:MI:SS') ",enddate.GetValueOrDefault().ToString("yyyy/MM/dd HH:mm:ss")));
            }
            if (startdate != null)
            {
                sbqry.AppendLine(string.Format("AND WORKORDER.CHANGEDATE > TO_TIMESTAMP('{0}', 'YYYY/MM/DD HH24:MI:SS') ", startdate.GetValueOrDefault().ToString("yyyy/MM/dd HH:mm:ss")));
            }

            //add sort 
            sbqry.Append("ORDER BY WORKORDER.STATUSDATE DESC");
            //****************************************************************************************************************
            string TEST = sbqry.ToString();
            //
            //get data from maximo
            EQUICommunictionLib.MaximoComm maximoComm = new MaximoComm();
            DataTable dataTable = new DataTable();
            dataTable = maximoComm.Oracle_runQuery(sbqry.ToString()); 
            //parse datatable to listobject
            List<Models.Workorder> workorders = new List<Models.Workorder>();
            foreach (DataRow row in dataTable.Rows)
            {
                Workorder workorder = new Workorder();
                workorder.WONUM = row.Field<string>("WONUM");
                workorder.STATUS = row.Field<string>("STATUS");
                workorder.STATUSDATE = row.Field<DateTime>("STATUSDATE");
                workorder.WORKTYPE = row.Field<string>("WORKTYPE");
                workorder.DESCRIPTION = row.Field<string>("DESCRIPTION");
                workorder.LOCATION = row.Field<string>("LOCATION");
                workorder.REPORTEDBY = row.Field<string>("REPORTEDBY");
                workorder.REPORTDATE = row.Field<DateTime>("REPORTDATE");
                workorder.ANCESTOR = row.Field<string>("ANCESTOR");
                //
                workorders.Add(workorder);
            }
            return PartialView(workorders);
        }

    }
}