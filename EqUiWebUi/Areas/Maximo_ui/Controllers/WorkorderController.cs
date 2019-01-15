using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;
using EqUiWebUi.Areas.Maximo_ui.Models;

namespace EqUiWebUi.Areas.Maximo_ui.Controllers
{

    public class WorkorderController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //default number of days we search back in history
        int MaximoWorkordersDaysback = -1000;

        // GET: Maximo_ui/Workorder
        public ActionResult Index()
        {
            return View();
        }

        //full view can be intitiated by using parms or by model. 
        [HttpGet]
        public ActionResult Workorders(string location, string locancestor, bool? b_ciblings, bool? b_preventive, string jpnum, string worktype, string wonum, string status, string ownergroup
            , DateTime? startdate, DateTime? enddate, Models.WorkorderSelectOptions workorderSelectOptions
            , bool loadOnInit = false, bool fullscreen = false, int fontSize = 12, bool RealtimeConn = true)
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
            if (ownergroup != null) workorderSelectOptions.ownergroup = ownergroup;
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
            //
            return View(workorderSelectOptions);
        }

        //can be called to be renders as partial in model or something like that...
        [HttpGet]
        public ActionResult _workordersOnLocation(string location, string locancestor, bool? b_ciblings, bool? b_preventive, string jpnum, string worktype, string wonum, string status, string ownergroup
            , DateTime? startdate, DateTime? enddate)
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
            workorderSelectOptions.ownergroup = ownergroup;
            workorderSelectOptions.startdate = startdate.GetValueOrDefault(System.DateTime.Now.AddDays(MaximoWorkordersDaysback));
            workorderSelectOptions.enddate = enddate.GetValueOrDefault(System.DateTime.Now);
            //
            return PartialView(workorderSelectOptions);
        }

        //form post work Workoders main view.
        [HttpPost]
        public ActionResult _workordersOnLocation(Models.WorkorderSelectOptions workorderSelectOptions)
        {
            return PartialView(workorderSelectOptions);
        }

        //gets called by AJAX to render the workorder grid
        [HttpGet]
        public async Task<ActionResult> _workordersOnLocationGrid(string location, string locancestor, bool? b_ciblings, bool? b_preventive, string jpnum, string worktype, string wonum, string status, string ownergroup
            , DateTime? startdate, DateTime? enddate)
        {
            //check if user is allowed to user realtimeConn
            string MaximoDbName = "MAXIMOrt";
            int CommandTimeout = 30;
            //build query
            StringBuilder sbqry = new StringBuilder();
            sbqry.AppendLine(string.Format(@"
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
                ,WORKORDER.OWNERGROUP
                ,LOCANCESTOR.ANCESTOR
                FROM MAXIMO.WORKORDER WORKORDER
                left join MAXIMO.LOCANCESTOR LOCANCESTOR on WORKORDER.LOCATION = LOCANCESTOR.LOCATION AND LOCANCESTOR.SYSTEMID = '{0}'"
                , System.Configuration.ConfigurationManager.AppSettings["Maximo_LOCATION_SYSTEMID"].ToString()));

            //start where clause
            sbqry.AppendLine("WHERE ((WORKORDER.woclass = 'WORKORDER' or WORKORDER.woclass = 'ACTIVITY') and WORKORDER.historyflag = 0 and WORKORDER.istask = 0)");
            //SDB bugfix case no ancestor 
            if (string.IsNullOrWhiteSpace(locancestor)) //to prevent dups 
            {
                sbqry.AppendLine("AND LOCANCESTOR.ANCESTOR = WORKORDER.LOCATION");
            }
            else
            {
                //handel ancestors.
                sbqry.AppendLine(handleParm("LOCANCESTOR.ANCESTOR", locancestor));
            }
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
            //hanlde ownergroup
            sbqry.AppendLine(handleParm("WORKORDER.OWNERGROUP", ownergroup));

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
            ViewBag.Qry = sbqry.ToString();
            //
            //get data from Maximo
            EQUICommunictionLib.ConnectionManager ConnectionManager = new ConnectionManager();
            DataTable dataTable = new DataTable();
            await Task.Run(() => dataTable = ConnectionManager.RunQuery(sbqry.ToString(), dbName: MaximoDbName, maxEXECtime: CommandTimeout, enblExeptions: true));

            //parse data table to list object
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
                workorder.OWNERGROUP = row.Field<string>("OWNERGROUP");
                //
                workorders.Add(workorder);
            }
            //
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