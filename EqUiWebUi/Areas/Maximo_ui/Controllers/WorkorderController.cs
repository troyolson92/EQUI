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

        // GET: Maximo_ui/Workorder
        public ActionResult Index()
        {
            return View();
        }

        //full view can be intitiated by using parms or by model. 
        [HttpGet]
        public ActionResult Workorders(Models.WorkorderSelectOptions workorderSelectOptions, bool loadOnInit = false)
        {
            //if this is set we load the workorder directly and fold up the parms pannem
            ViewBag.loadOnInit = loadOnInit;
            return View(workorderSelectOptions);
        }


        //form post work Workoders main view.
      
        public ActionResult _workordersOnLocation(Models.WorkorderSelectOptions workorderSelectOptions)
        {
            return PartialView(workorderSelectOptions);
        }

        //gets called by AJAX to render the workorder grid
        [HttpGet]
        public async Task<ActionResult> _workordersOnLocationGrid(Models.WorkorderSelectOptions workorderSelectOptions)
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
            sbqry.AppendLine($"WHERE ((WORKORDER.woclass = 'WORKORDER' or WORKORDER.woclass = 'ACTIVITY') and WORKORDER.istask = 0) and WORKORDER.SiteID = '{System.Configuration.ConfigurationManager.AppSettings["Maximo_SiteID"].ToString()}'");
            //SDB bugfix case no ancestor 
            if (string.IsNullOrWhiteSpace(workorderSelectOptions.locancestor)) //to prevent dups 
            {
                sbqry.AppendLine("AND LOCANCESTOR.ANCESTOR = WORKORDER.LOCATION");
            }
            else
            {
                //handel ancestors.
                sbqry.AppendLine(handleParm("LOCANCESTOR.ANCESTOR", workorderSelectOptions.locancestor));
            }
            //handle locations.
            sbqry.AppendLine(handleParm("WORKORDER.LOCATION", workorderSelectOptions.location));
            //handle preventive
            if (workorderSelectOptions.b_preventive == false)
            {
                sbqry.AppendLine("AND WORKORDER.WORKTYPE not in ('PP','PCI','WSCH')");
            }
            //hande worktype
            sbqry.AppendLine(handleParm("WORKORDER.WORKTYPE", workorderSelectOptions.worktype));
            //handle JPnum
            sbqry.AppendLine(handleParm("WORKORDER.JPNUM", workorderSelectOptions.jpnum));
            //handle wonum
            sbqry.AppendLine(handleParm("WORKORDER.WONUM", workorderSelectOptions.wonum));
            //handle status
            sbqry.AppendLine(handleParm("WORKORDER.STATUS", workorderSelectOptions.status));
            //hanlde ownergroup
            sbqry.AppendLine(handleParm("WORKORDER.OWNERGROUP", workorderSelectOptions.ownergroup));

            //handle timerange
            if (workorderSelectOptions.enddate != workorderSelectOptions.startdate)
            {
                sbqry.AppendLine(string.Format("AND WORKORDER.CHANGEDATE < TO_TIMESTAMP('{0}', 'YYYY/MM/DD HH24:MI:SS') ", workorderSelectOptions.enddate.GetValueOrDefault().ToString("yyyy/MM/dd HH:mm:ss")));
                sbqry.AppendLine(string.Format("AND WORKORDER.CHANGEDATE > TO_TIMESTAMP('{0}', 'YYYY/MM/DD HH24:MI:SS') ", workorderSelectOptions.startdate.GetValueOrDefault().ToString("yyyy/MM/dd HH:mm:ss")));
            }

            //add sort 
            sbqry.Append("ORDER BY WORKORDER.REPORTDATE DESC");
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