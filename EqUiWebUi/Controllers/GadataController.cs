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
			return new HttpNotFoundResult("Woeps");
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

        [HttpGet]
        public ActionResult PloegRapportInfo(int id)
        {
            return new HttpNotFoundResult("Woeps das nog ni klaar");
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

        //------------------------------------Supervisie brownfield-------------------------------------------------
        [HttpGet]
        public ActionResult StoWebgrid()
        {
            //refresh every 10 minutes anyway ! 
            Response.AddHeader("Refresh", "600");
            var data = DataBuffer.StoBreakdown;
            //in case hangfire is taking a day off
            if (data == null)
            {
                return new HttpNotFoundResult("give hangfire some time");
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

        //------------------------------------Supervisie Greenfield-------------------------------------------------
        [HttpGet]
        public ActionResult NgacSupervisieWebgrid()
        {
            GADATAEntities gADATAEntities = new GADATAEntities();
            List<ActiveState> data = (from activeState in gADATAEntities.ActiveState
                                         select activeState).ToList();

            return View(data);
        }

        //------------------------------------maximo-------------------------------------------------
        [HttpGet]
        public ActionResult BatchRuns()
        {
            DataTable dt = new DataTable();

            //get data
            GadataComm gadataComm = new GadataComm();
            string qry = @"
SELECT TOP (50) 
        [Location] +  ' ' 
       +[Classification] + ' <' 
	   +RTRIM(CONVERT(char(19),[timestamp], 108)) + '> ' + char(10) + char(13) 
       +[Logtext] as 'foutinfo'
  FROM [GADATA].[Tableau].[ApplFaults] as a 
  where 
  a.Location LIKE '334%'
  AND
  a.timestamp BETWEEN '2017-10-26 09:33:01' AND '2017-10-26 10:56:00'
  order by timestamp asc 
";
            dt = gadataComm.RunQueryGadata(qry);

            //
            WebGridHelpers.WebGridHelper webGridHelper = new WebGridHelper();
            ViewBag.Columns = webGridHelper.getDatatabelCollumns(dt);
            //
            List<dynamic> data = webGridHelper.datatableToDynamic(dt);
            //
            DefaultModel model = new WebGridHelpers.DefaultModel();
            model.PageSize = 30;
            //

            if (data != null)
            {
                model.TotalCount = data.Count();
                model.Data = data;
                model.DataTimestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return View(model);
        }

        //-------------------------------------------------------------------------------------------------

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