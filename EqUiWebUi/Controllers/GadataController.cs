using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EqUiWebUi.Models;
using System.Data;
using EQUICommunictionLib;
using EqUiWebUi.WebGridHelpers;
using System.Text;

namespace EqUiWebUi.Controllers
{
	public class GadataController : Controller
	{
		//
		// GET: /Gadata/
		public ActionResult Index()
		{
			return new HttpNotFoundResult("Woeps there seems to bo nothing here");
		}

        //------------------------------------PloegRapport-------------------------------------------------
        [HttpGet]
		public ActionResult PloegRapportWebgrid()
		{
            //refresh every 10 minutes anyway ! 
            Response.AddHeader("Refresh", "600");
            return View();
        }

        [HttpGet]
        public ActionResult _ploegRapport()
        {
            //
            var data = DataBuffer.Ploegreport;
            //in case hangfire is taking a day off
            if (data == null)
            {
                Backgroundwork backgroundwork = new Backgroundwork();
                backgroundwork.UpdatePloegreport();
                data = DataBuffer.Ploegreport;
            }
            return PartialView(data);
        }

        [HttpGet]
        public JsonResult PloegRapportcheckNewData(DateTime dataTimestamp)
        {
            Boolean breload = false;
            if (DataBuffer.PloegreportLastDt > dataTimestamp.AddSeconds(1))
            {
                breload = true;
            }
            //
            return new JsonResult()
            {
                Data = new { doReload = breload, dataTimestamp =  DataBuffer.PloegreportLastDt.ToString("yyyy-MM-dd HH:mm:ss"), lastCheck = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //-------------------------------------------------------------------------------------------------

        //------------------------------------Supervisie-------------------------------------------------
        [HttpGet]
        public ActionResult SupervisieWebgrid()
        {
            //refresh every 10 minutes anyway ! 
            Response.AddHeader("Refresh", "600");
            return View();
        }

        [HttpGet]
        public ActionResult _supervisie()
        {
            //
            var data = DataBuffer.Supervisie;
            //in case hangfire is taking a day off
            if (data == null)
            {
                Backgroundwork backgroundwork = new Backgroundwork();
                backgroundwork.UpdateSupervisie();
                data = DataBuffer.Supervisie;
            }
            return PartialView(data);
        }

        [HttpGet]
        public JsonResult SupervisiecheckNewData(DateTime dataTimestamp)
        {
            Boolean breload = false;
            if (DataBuffer.SupervisieLastDt > dataTimestamp.AddSeconds(1))
            {
                breload = true;
            }
            //
            return new JsonResult()
            {
                Data = new { doReload = breload, dataTimestamp = DataBuffer.SupervisieLastDt.ToString("yyyy-MM-dd HH:mm:ss"), lastCheck = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //------------------------------------Supervisie STO-------------------------------------------------
        [HttpGet]
        public ActionResult StoWebgrid()
        {
            return new HttpNotFoundResult("Data loading disabled in hangfire");
        }

        [HttpGet]
        public JsonResult StocheckNewData(DateTime dataTimestamp)
        {
            Boolean breload = false;
            if (DataBuffer.StoBreakdownLastDt > dataTimestamp.AddSeconds(1))
            {
                breload = true;
            }
            //
            return new JsonResult()
            {
                Data = new { doReload = breload, dataTimestamp = DataBuffer.StoBreakdownLastDt.ToString("yyyy-MM-dd HH:mm:ss"), lastCheck = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //Partial view for more info modal-------------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult _Moreinfo(string location, int errornum, int refid, string logtype, string logtext)
        {
            //build up the model
            LogInfo logInfo = new LogInfo();
            logInfo.location = location;
            logInfo.errornum = errornum;
            logInfo.refid = refid;
            logInfo.logtype = logtype;
            logInfo.logtext = logtext;
            //query all instances of the error 
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
                foreach(DataRow row in dt.Rows)
                {
                    foreach(DataColumn col in row.Table.Columns)
                    {
                        //only add if there is something.
                        if (row.Field<string>(col.ColumnName).ToString().Length > 1)
                        {
                            sb.AppendLine("<div class=\"panel panel-default\">").AppendLine("<div class=\"panel-heading\">");
                            sb.AppendLine(col.ColumnName);
                            sb.AppendLine("</div>").AppendLine("<div class=\"panel-body\">");
                            sb.AppendLine(row.Field<string>(col.ColumnName).ToString());
                            sb.AppendLine("</div>").AppendLine("</div>");
                        }
                    }
                }
            }
            else
            {
                sb.AppendLine("<h4>No valid result from query!</h4>");
            }
            logInfo.logDetails = sb.ToString();
            //
            return PartialView(logInfo);
        }
    }
}