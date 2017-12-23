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
            //
            var data = DataBuffer.Ploegreport;
            //in case hangfire is taking a day off
            if (data == null)
            {
                Backgroundwork backgroundwork = new Backgroundwork();
                backgroundwork.UpdatePloegreport();
                data = DataBuffer.Ploegreport;
                ViewBag.DataTimestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else //add tracking timestamp for hangfire sync
            {
                ViewBag.DataTimestamp = DataBuffer.SupervisieLastDt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return View(data);
        }

        [HttpGet]
        public JsonResult PloegRapportcheckNewData(String dataTimestamp)
        {
            DateTime date = DateTime.ParseExact(dataTimestamp, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            date = date.AddSeconds(1);

            if (DataBuffer.PloegreportLastDt > date)
            {
                //issue reload
                return Json(new object[] { new object() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //no reload needed
                return null;
            }

        }

        //-------------------------------------------------------------------------------------------------

        //------------------------------------Supervisie-------------------------------------------------
        [HttpGet]
        public ActionResult SupervisieWebgrid()
        {
            //refresh every 10 minutes anyway ! 
            Response.AddHeader("Refresh", "600");
            var data = DataBuffer.Supervisie;
            //in case hangfire is taking a day off
            if (data == null)
            {
                Backgroundwork backgroundwork = new Backgroundwork();
                backgroundwork.UpdateSupervisie();
                data = DataBuffer.Supervisie;
                ViewBag.DataTimestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else //add tracking timestamp for hangfire sync
            {
                ViewBag.DataTimestamp = DataBuffer.SupervisieLastDt.ToString("yyyy-MM-dd HH:mm:ss");
            }

            return View(data);
        }

        [HttpGet]
        public JsonResult SupervisiecheckNewData(String dataTimestamp)
        {
            DateTime date = DateTime.ParseExact(dataTimestamp, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            date = date.AddSeconds(1);

            if (DataBuffer.SupervisieLastDt > date)
            {
                //issue reload
                return Json(new object[] { new object() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //no reload needed
                return null;
            }

        }

        //------------------------------------Supervisie STO-------------------------------------------------
        [HttpGet]
        public ActionResult StoWebgrid()
        {
            return new HttpNotFoundResult("Data loading disabled in hangfire");

            //refresh every 10 minutes anyway ! 
            Response.AddHeader("Refresh", "600");
            var data = DataBuffer.StoBreakdown;
            //in case hangfire is taking a day off
            if (data == null)
            {
                Backgroundwork backgroundwork = new Backgroundwork();
                //backgroundwork.updataSTO();
                data = DataBuffer.StoBreakdown;
                ViewBag.DataTimestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else //add tracking timestamp for hangfire sync
            {
                ViewBag.DataTimestamp = DataBuffer.StoBreakdownLastDt.ToString("yyyy-MM-dd HH:mm:ss");
            }

            return View(data);
        }

        [HttpGet]
        public JsonResult StocheckNewData(String dataTimestamp)
        {
            DateTime date = DateTime.ParseExact(dataTimestamp, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            date = date.AddSeconds(1);

            if (DataBuffer.StoBreakdownLastDt > date)
            {
                //issue reload
                return Json(new object[] { new object() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //no reload needed
                return null;
            }

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