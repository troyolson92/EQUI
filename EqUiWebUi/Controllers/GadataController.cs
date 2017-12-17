﻿using System;
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
            GadataComm gadataComm = new GadataComm();
            MaximoComm maximoComm = new MaximoComm();
            //query all instances of the error 
            string qry = string.Format(
            @"EXEC [EqUi].[GetErrorInfoData] @Location  = '{0}' ,@ERRORNUM = {1} ,@Refid = {2} ,@logtype ='{3}'"
            , location, errornum, refid, logtype);

            //fill dataset
            DataTable dt = gadataComm.RunQueryGadata(qry);
            //check if the result was valid 

            StringBuilder sb = new StringBuilder();
            string newline = "<p></p>";
            if (dt.Rows.Count != 0)
            {
                DataRow myRow = dt.Rows[0];
                foreach (DataColumn dc in myRow.Table.Columns)
                {
                    if (myRow[dc.ColumnName] == DBNull.Value)
                    {
                        myRow[dc.ColumnName] = "null (no data)";
                    }
                    sb.AppendLine(maximoComm.StringToHTML_Table(dc.ColumnName, myRow.Field<string>(dc.ColumnName).ToString())).AppendLine(newline);
                }
            }
            else
            {
                sb.AppendLine("No valid result from query").AppendLine(newline);
            }
            ViewBag.Logdetails = sb.ToString();
            ViewBag.location = location;
            ViewBag.errornum = errornum;
            ViewBag.refid = refid;
            ViewBag.logtype = logtype;
            ViewBag.logtext = logtext;
            //
            return PartialView();
        }
    }
}