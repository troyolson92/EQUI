using System;
using System.Linq;
using System.Web.Mvc;
using EqUiWebUi.Models;
using System.Data;
using EQUICommunictionLib;
using System.Text;
using System.Web;
using System.Collections.Generic;

namespace EqUiWebUi.Controllers
{
	public class GadataController : Controller
	{
		//
		// GET: /Gadata/
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
            //in case hangfire is taking a day off
            if (data == null)
            {
                Log.Info("_ploegRapport local data fetch");
                Backgroundwork backgroundwork = new Backgroundwork();
                backgroundwork.UpdatePloegreport();
                data = DataBuffer.Ploegreport;
            }

            //in case still null trow error 
            if (data ==null)
            {
                Log.Error("_ploegRapport Failed to fetch data");
                return new HttpNotFoundResult("Woeps there seems to be an error");
            }

            if (Session["LocationRoot"].ToString() != "")
                {
                   data = (from d in data
                          where (d.LocationTree ??"").Contains(Session["LocationRoot"].ToString())
                          || d.Logtype == "TIMELINE"
                          select d).ToList();
                }
            //
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
                Log.Info("_supervisie local data fetch");
                Backgroundwork backgroundwork = new Backgroundwork();
                backgroundwork.UpdateSupervisie();
                data = DataBuffer.Supervisie;
            }           
            //in case still null trow error 
            if (data == null)
            {
                Log.Error("_supervisie Failed to fetch data");
                return new HttpNotFoundResult("Woeps there seems to be an error");
            }

            if (Session["LocationRoot"].ToString() != "")
                {
                    data = (from d in data
                            where (d.LocationTree ?? "").Contains(Session["LocationRoot"].ToString())
                            || d.Logtype == "TIMELINE"
                            select d).ToList();
                }
            //
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
            //in case hangfire is taking a day off
            if (data == null)
            {
                Log.Info("_EQpluginDefaultNGAC local data fetch");
                Backgroundwork backgroundwork = new Backgroundwork();
                backgroundwork.UpdateEQpluginDefaultNGAC();
                data = DataBuffer.EQpluginDefaultNGAC;
            }
            //in case still null trow error 
            if (data == null)
            {
                Log.Error("_EQpluginDefaultNGAC Failed to fetch data");
                return new HttpNotFoundResult("Woeps there seems to be an error");
            }

            
            if (Session["LocationRoot"].ToString() != "")
            {
                data = (from d in data
                        where (d.LocationTree ?? "").Contains(Session["LocationRoot"].ToString())
                        || d.Logtype == "TIMELINE"
                        select d).ToList();
            }
            //
            return PartialView(data);
        }

        [HttpGet]
        public JsonResult EQpluginDefaultNGACcheckNewData(DateTime dataTimestamp)
        {
            Boolean breload = false;
            if (DataBuffer.EQpluginDefaultNGAC_DT > dataTimestamp.AddSeconds(1))
            {
                breload = true;
            }
            //
            return new JsonResult()
            {
                Data = new { doReload = breload, dataTimestamp = DataBuffer.EQpluginDefaultNGAC_DT.ToString("yyyy-MM-dd HH:mm:ss"), lastCheck = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
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


        //------------------------------------Body track webgrid-------------------------------------------------
        [HttpGet]
        public ActionResult BodyTrackWebgrid()
        {
            GADATAEntities gADATAEntities = new GADATAEntities();
            List<Bodytracking> data = (from bodytracking in gADATAEntities.Bodytracking
                                       select bodytracking
                                       ).ToList();
            return View(data);
        }



        //Partial view for more info modal-------------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult _Moreinfo(string location, int? errornum, int? refid, string logtype, string logtext)
        {
            //build up the model
            LogInfo logInfo = new LogInfo();
            logInfo.location = location;
            logInfo.errornum = errornum.GetValueOrDefault(0);
            logInfo.refid = refid.GetValueOrDefault(0);
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
                        if (row.Field<string>(col.ColumnName) != null)
                        {
                            if (!string.IsNullOrEmpty(row.Field<string>(col.ColumnName).ToString()))
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