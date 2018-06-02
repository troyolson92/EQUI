using EQUICommunictionLib;
using EqUiWebUi.Areas.Gadata.Models;
using EqUiWebUi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
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
            var data = DataBuffer.Ploegreport;
            //in case still null trow error return empty result 
            if (data == null)
            {
                data = new List<AAOSR_PloegRaport_Result>();
            }

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
            //
            return PartialView(data);
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
                data = new List<Supervisie>();
            }

            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (LocationRoot != "")
            {
                data = (from d in data
                        where (d.LocationTree ?? "").Contains(LocationRoot)
                        || d.Logtype == "TIMELINE"
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
            //
            return PartialView(data);
        }
        //-------------------------------------------------------------------------------------------------

        //------------------------------------EQpluginDefaultNGAC-------------------------------------------------
        [HttpGet]
        public ActionResult EQpluginDefaultNGACWebgrid()
        {
            return View();
        }

        [HttpGet]
        public ActionResult _EQpluginDefaultNGAC()
        {
            //
            var data = DataBuffer.EQpluginDefaultNGAC;
            //in case still null trow error return empty result 
            if (data == null)
            {
                data = new List<EQpluginDefaultNGAC_Result>();
            }

            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (LocationRoot != "")
            {
                data = (from d in data
                        where (d.LocationTree ?? "").Contains(LocationRoot)
                        || d.Logtype == "TIMELINE"
                        select d).ToList();
            }
            //
            return PartialView(data);
        }

        //------------------------------------Body track webgrid-------------------------------------------------
        [HttpGet]
        public ActionResult BodyTrackWebgrid()
        {
            GADATAEntities2 gADATAEntities = new GADATAEntities2();
            IQueryable<Bodytracking> data = from bodytracking in gADATAEntities.Bodytracking
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
        public ActionResult MoreInfo(string location, int? errornum, int? refid, string logtype, string logtext)
        {
            LogInfo logInfo = new LogInfo();
            logInfo.location = location;
            logInfo.errornum = errornum.GetValueOrDefault(0);
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
            GadataComm gadataComm = new GadataComm();
            string qry = string.Format(
            @"EXEC [EqUi].[GetErrorInfoData] @Location  = '{0}' ,@ERRORNUM = {1} ,@Refid = {2} ,@logtype ='{3}'"
            , logInfo.location, logInfo.errornum, logInfo.refid, logInfo.logtype);
            DataTable dt = gadataComm.RunQueryGadata(qry);
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