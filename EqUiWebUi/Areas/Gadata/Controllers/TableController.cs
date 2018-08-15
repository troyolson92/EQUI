using EQUICommunictionLib;
using EqUiWebUi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Gadata.Controllers
{
    public class TableController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Gadata/Tabel
        public ActionResult Index()
        {
            return new HttpNotFoundResult("Woeps there seems to be nothing here");
        }


        //------------------------------------PloegRapport-------------------------------------------------
        [HttpGet]
        public ActionResult PloegRapportWebgrid()
        {
            return View();
        }

        [HttpGet]
        public ActionResult _ploegRapport()
        {
            var data = DataBuffer.Supervisie;
            //in case still null trow error return empty result 
            if (data == null)
            {
                data = new List<EqUiWebUi.Areas.Gadata.SupervisieDummy>();
            }

            //apply user filters
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (LocationRoot != "")
            {
                data = (from d in data
                        where (d.LocationTree ?? "").Contains(LocationRoot) //apply user locationroot
                        || d.Logtype == "TIMELINE" //always allowtimeline
                            select d).ToList();
            }

            string AssetRoot = CurrentUser.Getuser.AssetRoot;
            if (AssetRoot != "")
            {
                data = (from d in data
                        where (d.Classification ?? "").Contains(AssetRoot) //apply user assetroot
                            || (d.Classification ?? "") == "Undefined*" //or allow assets thet are undedind
                            || (d.Classification ?? "") == "" //or allow assets that are null
                            || d.Logtype == "TIMELINE" //always allowtimeline
                            select d).ToList();
            }

            //order data to make sure First and Last works
            data = (from d in data orderby d.timestamp descending select d).ToList();

            //group data by logtype location into new object 
            var Ploegrap = data.GroupBy( s => new
                {
                     s.Location
                    ,s.LocationTree
                    
                    ,s.Logtype
                    ,s.Subgroup
                    ,s.Classification

                    ,s.Vyear
                    ,s.Vweek
                    ,s.Vday
                    ,s.shift
                }).Select (p => new  EqUiWebUi.Areas.Gadata.PloegRaport_dummy() {
                     Location= p.Key.Location
                    ,logtext = p.First().logtext
                    ,Response_min_ = p.Sum (x => x.RT)
                    ,Downtime_min_ = p.Sum (x => x.DT)
                    ,time = p.First().time
                    ,Count = p.Count()
                    ,Classification = p.Key.Classification
                    ,Subgroup = p.Key.Subgroup
                    ,Logcode = p.First().Logcode
                    ,Logtype = p.Key.Logtype
                    ,refId = p.First().refId
                    ,timestamp = p.First().timestamp
                    ,LocationTree = p.Key.LocationTree
                }).Where(
                 p => 
                 (//EXCLUDE
                 (p.Downtime_min_ > 20 || p.Count > 4)  //longer than 20 min or more than 4 times 
                 && p.Logtype != "LIVE"  //EXCLUDE
                 )
                 || p.Logtype == "ALERT" //INCLUDE
                 || p.Logtype == "TIMELINE" //INCLUDE
                ).ToList();

            //
            return PartialView(Ploegrap);
        }
        //

        //------------------------------------Supervisie-------------------------------------------------
        [HttpGet]
        public ActionResult SupervisieWebgrid()
        {
            return View();
        }

        [HttpGet]
        public ActionResult _supervisie()
        {
            // 
            var data = DataBuffer.Supervisie;
            //in case still null trow error return empty result 
            if (data == null)
            {
                data = new List<EqUiWebUi.Areas.Gadata.SupervisieDummy>();
            }
            //apply location root filter
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (LocationRoot != "")
            {
                data = (from d in data
                        where (d.LocationTree ?? "").Contains(LocationRoot)
                        || d.Logtype == "TIMELINE"
                        select d).ToList();
            }
            //apply asset root filter
            string AssetRoot = CurrentUser.Getuser.AssetRoot;
            if (AssetRoot != "")
            {
                data = (from d in data
                        where (d.Classification ?? "").Contains(AssetRoot) //apply user assetroot
                            || (d.Classification ?? "") == "Undefined*" //or allow assets thet are undedind
                            || (d.Classification ?? "") == "" //or allow assets that are null
                            || d.Logtype == "TIMELINE" //always allowtimeline
                        select d).ToList();
            }
            //apply filter for "Operational"
            data = (from d in data
                    where (d.Subgroup ?? "").Contains("Operational") == false //not Operational
                    orderby d.timestamp descending
                    select d).ToList();

            //
            return PartialView(data);;
        }
        //-------------------------------------------------------------------------------------------------

        //------------------------------------Body track webgrid-------------------------------------------------
        [HttpGet]
        public ActionResult BodyTrackWebgrid()
        {
            VASC.Models.GADATAEntitiesVASC GADATAEntitiesVASC = new VASC.Models.GADATAEntitiesVASC();
            IQueryable<VASC.Models.Bodytracking> data = from bodytracking in GADATAEntitiesVASC.Bodytracking
                                            select bodytracking;

            return View(data);
        }




        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------




        //full view for more info ---------------------------------------------------------------------------------------------------------------------
        //contains _getErrorTrend _loginfo 
        //called by vsto plugin
        [HttpGet]
        public ActionResult MoreInfo(string location, string errornum, int? refid, string logtype, string logtext)
        {
            LogInfo logInfo = new LogInfo();
            logInfo.location = location;
            logInfo.errornum = errornum;
            logInfo.refid = refid.GetValueOrDefault(0);
            logInfo.logtype = logtype;
            logInfo.logtext = logtext;
            return View(logInfo);
        }

        //Partial view for more info modal with model-------------------------------------------------------------------------------------------------
        //contains _getErrorTrend _loginfo 
        //called for the info modals in the site
        [HttpGet]
        public ActionResult _Moreinfo(LogInfo logInfo)
        {
            return PartialView(logInfo);
        }

        //full view for loginfo----------------------------------------------------------------------------------------------------------------------
        //called by vsto plugin
        [HttpGet]
        public ActionResult Loginfo(LogInfo logInfo)
        {
            return View(logInfo);
        }

        //partial view for loginfo 
        [HttpGet]
        public ActionResult _loginfo(LogInfo logInfo)
        {
            //query all instances of the logtype via equi.getErrorInfoData 
            ConnectionManager connectionManager = new ConnectionManager();
            string qry = string.Format(
            @"EXEC [EqUi].[GetErrorInfoData] @Location  = '{0}' ,@ERRORNUM = '{1}' ,@Refid = {2} ,@logtype ='{3}'"
            , logInfo.location, logInfo.errornum, logInfo.refid, logInfo.logtype);
            DataTable dt = connectionManager.RunQuery(qry);
            //build an html respone with the log info.
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