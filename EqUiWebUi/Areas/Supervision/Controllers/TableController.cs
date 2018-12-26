using EQUICommunictionLib;
using EqUiWebUi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Supervision.Controllers
{
    public class TableController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Gadata/Table
        public ActionResult Index()
        {
            return View();
        }

        //------------------------------------PloegRapport-------------------------------------------------
        /// <summary>
        /// PloegRapport groups supervision records per shift using some filter parameters
        /// </summary>
        /// <param name="minSumOfDownTime"></param>
        /// <param name="minCountOfDownTime"></param>
        /// <param name="ApplyResponsibleArea"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PloegRapport(int minSumOfDownTime = 20, int minCountOfDownTime = 4, bool ApplyResponsibleArea = false)
        {
            ViewBag.minSumOfDownTime = minSumOfDownTime;
            ViewBag.minCountOfDownTime = minCountOfDownTime;
            ViewBag.ApplyResponsibleArea = ApplyResponsibleArea;
            return View();
        }

        /// <summary>
        /// Grid component for PloegRapport
        /// </summary>
        /// <param name="minSumOfDownTime"></param>
        /// <param name="minCountOfDownTime"></param>
        /// <param name="ApplyResponsibleArea"></param>
        /// <returns></returns>
        public ActionResult _ploegRapportGrid(int minSumOfDownTime = 20, int minCountOfDownTime = 4, bool ApplyResponsibleArea = false)
        {
            var data = DataBuffer.Supervisie;
            //in case still null trow error return empty result 
            if (data == null)
            {
                data = new List<EqUiWebUi.Areas.Supervision.SupervisieDummy>();
            }

            //apply user filters
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (LocationRoot != "" && ApplyResponsibleArea == false)
            {
                data = (from d in data
                        where (d.LocationTree ?? "").Contains(LocationRoot) //apply user locationroot
                        || d.Logtype == "TIMELINE" //always allow timeline
                            select d).ToList();
            }

            List<string> ResponsibleAreaLocations = CurrentUser.Getuser.ResponsibleAreaLocations;
            if (ResponsibleAreaLocations != null && ApplyResponsibleArea == true)
            {
                data = (from d in data
                        where (d.LocationTree ?? "").ListContaints(ResponsibleAreaLocations) //apply user ResponsibleArea
                        || d.Logtype == "TIMELINE" //always allow timeline
                        select d).ToList();
            }

            string AssetRoot = CurrentUser.Getuser.AssetRoot;
            if (AssetRoot != "")
            {
                data = (from d in data
                        where (d.Classification ?? "").Contains(AssetRoot) //apply user assetroot
                            || (d.Classification ?? "") == "" //or allow assets that are null
                            || d.Logtype == "TIMELINE" //always allow timeline
                            select d).ToList();
            }
           
            //THIS SHOULD NOT STAY!!!! CLEAN THIS OUT YOU LAZy F
            //apply filter for "Operational"
            data = (from d in data
                    where (d.Subgroup ?? "").Contains("Operational") == false //not Operational
                    && (d.Subgroup ?? "").Contains("junk") == false // no brekadowns from junk table. 
                    orderby d.timestamp descending
                    select d).ToList();

            //order data to make sure First and Last works
            data = (from d in data orderby d.timestamp descending select d).ToList();

            //group data by logtype location into new object 
            var Ploegrap = data.GroupBy( s => new
                {
                     s.Location
                    ,s.LocationTree               
                    ,s.Logtype
                    //,s.Subgroup
                    ,s.Classification
                    ,s.Vyear
                    ,s.Vweek
                    ,s.Vday
                    ,s.shift
                }).Select (p => new  EqUiWebUi.Areas.Supervision.PloegRaport_dummy() {
                     Location= p.Key.Location
                    ,logtext = p.First().logtext
                    ,Response_min_ = p.Sum (x => x.RT)
                    ,Downtime_min_ = p.Sum (x => x.DT)
                    ,Count = p.Count()
                    ,Classification = p.Key.Classification
                    ,Subgroup = p.First().Subgroup
                    ,Logcode = p.First().Logcode
                    ,Logtype = p.Key.Logtype
                    ,refId = p.First().refId
                    ,timestamp = p.First().timestamp
                    ,LocationTree = p.Key.LocationTree
                    ,animation = p.First().animation
                }).Where(
                 p => 
                 (//EXCLUDE
                 (p.Downtime_min_ > minSumOfDownTime * 60 || p.Count > minCountOfDownTime)  //longer than 20 min or more than 4 times 
                 && p.Logtype != "LIVE"  //EXCLUDE
                 )
                 || p.animation == "ALERT" //INCLUDE (this is on the animation because when the animation of an alert is "Alert" this alert is not in WGK 
                 || p.Logtype == "TIMELINE" //INCLUDE
                ).ToList();

            //
            return PartialView(Ploegrap);
        }


        //------------------------------------Supervision-------------------------------------------------
        /// <summary>
        /// Supervision 
        /// </summary>
        /// <param name="ApplyResponsibleArea"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Supervisie(bool ApplyResponsibleArea = false)
        {
            ViewBag.ApplyResponsibleArea = ApplyResponsibleArea;
            return View();
        }

        /// <summary>
        /// Grid component for Supervision
        /// </summary>
        /// <param name="locationRootFilter"></param>
        /// <param name="ApplyResponsibleArea"></param>
        /// <returns></returns>
        public ActionResult _supervisieGrid(string locationRootFilter = "", bool ApplyResponsibleArea = false)
        {
            // 
            var data = DataBuffer.Supervisie;
            //in case still null trow error return empty result 
            if (data == null)
            {
                data = new List<EqUiWebUi.Areas.Supervision.SupervisieDummy>();
            }
            //apply user filters
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            //if the locationRootFilter is passed as arument allow this to override USER location filter. (is used in ploegrapport)
            if (locationRootFilter != "") LocationRoot = locationRootFilter;
            if (LocationRoot != "" && ApplyResponsibleArea == false)
            {
                data = (from d in data
                        where (d.LocationTree ?? "").Contains(LocationRoot)
                        || d.Logtype == "TIMELINE"
                        select d).ToList();
            }

            List<string> ResponsibleAreaLocations = CurrentUser.Getuser.ResponsibleAreaLocations;
            if (ResponsibleAreaLocations != null && ApplyResponsibleArea == true)
            {
                data = (from d in data
                        where (d.LocationTree ?? "").ListContaints(ResponsibleAreaLocations) //apply user ResponsibleArea
                        || d.Logtype == "TIMELINE" //always allow timeline
                        select d).ToList();
            }

            string AssetRoot = CurrentUser.Getuser.AssetRoot;
            if (AssetRoot != "")
            {
                data = (from d in data
                        where (d.Classification ?? "").Contains(AssetRoot) //apply user assetroot
                            || (d.Classification ?? "") == "" //always allow assets that are null
                            || (d.Classification ?? "") == "VASC" //always allow VASC messages
                            || d.Logtype == "TIMELINE" //always always allowtimeline
                        select d).ToList();
            }
                        
            //THIS SHOULD NOT STAY!!!! CLEAN THIS OUT YOU LAZy F
            //apply filter for "Operational"
            data = (from d in data
                    where (d.Subgroup ?? "").Contains("Operational") == false //not Operational
                    && (d.Subgroup ?? "").Contains("junk") == false // no brekadowns from junk table. 
                    orderby d.timestamp descending
                    select d).ToList();

            //
            return PartialView(data);;
        }

        /// <summary>
        /// Legend partial view for supervision and ploegrapport
        /// </summary>
        /// <returns></returns>
        public ActionResult _Legend()
        {
            return PartialView();
        }

        //------------------------------------Extra info pages-------------------------------------------------
        /// <summary>
        /// Full view that calls partial view _Moreinfo
        /// Gets called by VSTO plug in!
        /// </summary>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoreInfo(LogInfo logInfo)
        {
            return View(logInfo);
        }

        /// <summary>
        /// Partial view that contains more info about ... contains a trend chart / loginfo / maximo partial
        /// called by modal in Supervision and PloegRapport
        /// </summary>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        public ActionResult _Moreinfo(LogInfo logInfo)
        {
            return PartialView(logInfo);
        }

        /// <summary>
        /// Full view that calls partial _loginfo
        /// </summary>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Loginfo(LogInfo logInfo)
        {
            return View(logInfo);
        }

        /// <summary>
        /// Partial view containing more info about a log record.
        /// Called by _Moreinfo
        /// </summary>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        public ActionResult _loginfo(LogInfo logInfo)
        {
            //query all instances of the log type via equi.getErrorInfoData 
            ConnectionManager connectionManager = new ConnectionManager();
            string qry = string.Format(
            @"EXEC [EqUi].[GetErrorInfoData] @Location  = '{0}' ,@ERRORNUM = '{1}' ,@Refid = {2} ,@logtype ='{3}'"
            , logInfo.location, logInfo.errornum, logInfo.refid, logInfo.logtype);
            DataTable dt = connectionManager.RunQuery(qry);
            //build an html response with the log info.
            StringBuilder sb = new StringBuilder();
            //check if the result was valid 
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in row.Table.Columns)
                    {
                        //only add if there is something.
                        if (row.Field<string>(col.ColumnName) != null)
                        {
                            if (!string.IsNullOrEmpty(row.Field<string>(col.ColumnName).ToString()))
                            {
                                sb.AppendLine("<div class=\"card\">").AppendLine("<div class=\"card-header\">");
                                sb.AppendLine(col.ColumnName);
                                sb.AppendLine("</div>").AppendLine("<div class=\"card-body\">");
                                sb.AppendLine(row.Field<string>(col.ColumnName).ToString());
                                sb.AppendLine("</div>").AppendLine("</div>");
                            }
                        }
                    }
                }
            }
            else
            {
                sb.AppendLine("<div class='card bg-danger card-header'>");
                sb.AppendLine("<strong>Triggerd: No valid result from query! </strong>");
                sb.AppendLine("<div> Ran:" + qry + "</div>");
                sb.AppendLine("</div>");
            }
            logInfo.logDetails = sb.ToString();
            return PartialView(logInfo);
        }

    }
}