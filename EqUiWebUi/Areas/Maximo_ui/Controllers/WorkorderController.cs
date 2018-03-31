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
        public ActionResult Workorders(string location, string locancestor, bool? b_ciblings, bool? b_preventive, string jpnum, string worktype, string wonum, string status
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
            if (status != null) workorderSelectOptions.status = status;
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
        public ActionResult _workordersOnLocation(string location, string locancestor, bool? b_ciblings, bool? b_preventive, string jpnum, string worktype, string wonum, string status
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
            workorderSelectOptions.status = status;
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
        public ActionResult _workordersOnLocationGrid(string location, string locancestor, bool? b_ciblings, bool? b_preventive, string jpnum, string worktype, string wonum, string status
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
            sbqry.AppendLine(@"
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
                left join MAXIMO.LOCANCESTOR LOCANCESTOR on WORKORDER.LOCATION = LOCANCESTOR.LOCATION AND LOCANCESTOR.SYSTEMID = 'PRODMID'");


            //start where clause
            sbqry.AppendLine("WHERE ((WORKORDER.woclass = 'WORKORDER' or WORKORDER.woclass = 'ACTIVITY') and WORKORDER.historyflag = 0 and WORKORDER.istask = 0)");

            //handel ancestors.
            sbqry.AppendLine(handleParm("LOCANCESTOR.ANCESTOR", locancestor));
            //handle locations.
            sbqry.AppendLine(handleParm("WORKORDER.LOCATION", location));
            //handle preventive
            b_preventive = b_preventive ?? false; //default no preventive
            if (b_preventive == false)
            {
                sbqry.AppendLine("AND WORKORDER.WORKTYPE not in ('PP','PCI','WSCH')");
            }
            //hande worktype
            sbqry.AppendLine(handleParm("WORKORDER.WORKTYPE", worktype));
            //handle JPnum
            sbqry.AppendLine(handleParm("WORKORDER.JPNUM", jpnum));
            //handle wonum
            sbqry.AppendLine(handleParm("WORKORDER.WONUM", wonum));
            //handle status
            sbqry.AppendLine(handleParm("WORKORDER.STATUS", status));
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
            dataTable = maximoComm.Oracle_runQuery(sbqry.ToString(),RealtimeConn:RealtimeConn,maxEXECtime:15,enblExeptions:true); 
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

        //helper procedure for adding parms to query
        string handleParm(string parmname, string value)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(value))
            {
                bool not = false;
                if (value.StartsWith("!"))
                {
                    not = true;
                    value = value.Replace("!", "");
                }
                string[] values = value.Split(';');
                //if only 1 location is passed we allow wildcards (like) 
                if (values.Count() == 1)
                {
                    if (!values[0].Trim().Contains("%"))  //if no wildcard exact match 
                    {
                        if (not)
                        {
                            sb.AppendLine(string.Format("AND {1} <> '{0}'", values[0].Trim(), parmname));
                        }
                        else
                        {
                            sb.AppendLine(string.Format("AND {1} = '{0}'", values[0].Trim(), parmname));
                        }
                    }
                    else //wrap with wildcards. 
                    {
                        if (not)
                        {
                            sb.AppendLine(string.Format("AND {1} NOT LIKE '%{0}%'", values[0].Trim(), parmname));
                        }
                        else
                        {
                            sb.AppendLine(string.Format("AND {1} LIKE '%{0}%'", values[0].Trim(), parmname));
                        }
                    }
                }
                else // if more than one location use in statement
                {
                    if (not)
                    {
                        sb.Append(string.Format("AND {0} NOT IN (", parmname));
                    }
                    else
                    {
                        sb.Append(string.Format("AND {0} IN (", parmname));
                    }
                    foreach (string val in values)
                    {
                        sb.Append("'").Append(val.Trim()).Append("'");
                        if (val != values.Last())
                        {
                            sb.Append(",");
                        }
                    }
                    sb.AppendLine(") ");
                }
            }
            return sb.ToString();
        }

    }
}