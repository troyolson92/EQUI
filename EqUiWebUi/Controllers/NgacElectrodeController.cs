using EqUiWebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    public class NgacElectrodeController : Controller
    {
        // GET: NgacElectrode
        public ActionResult Index()
        {
            return View();
        }


        //------------------------------------huidige tip status voor al de robots-------------------------------------------------
        [HttpGet]
        public ActionResult CurrentTipstatus(bool showAll = false)
        {
            //refresh every 10 minutes anyway ! 
            Response.AddHeader("Refresh", "600");
            //
            var data = DataBuffer.Tipstatus;
            //in case hangfire is taking a day off
            if (data == null)
            {
                Backgroundwork backgroundwork = new Backgroundwork();
                backgroundwork.UpdateTipstatus();
                ViewBag.DataTimestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else //add tracking timestamp for hangfire sync
            {
                ViewBag.DataTimestamp = DataBuffer.TipstatusLastDt.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (showAll)
            {
                ViewBag.RowsPerPage = 1000;
            }
            else
            {
                ViewBag.RowsPerPage = 20;
            }
            return View(data);
        }

        [HttpGet]
        public JsonResult TipwearCheckNewData(String dataTimestamp)
        {
            //direct query hangfire is not up
            if (dataTimestamp == "")
            {
                //issue reload
                return Json(new object[] { new object() }, JsonRequestBehavior.AllowGet);
            }
            DateTime date = DateTime.ParseExact(dataTimestamp, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            date = date.AddSeconds(1);

            if (DataBuffer.TipstatusLastDt > date)
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

        //------------------------------------tabel met elektrode wissels.-------------------------------------------------
        [HttpGet]
        public ActionResult TipwearBeforeChange()
        {
            GADATAEntities gADATAEntities = new GADATAEntities();
            List<TipwearBeforeChange> data = (from tipwearBeforeChange in gADATAEntities.TipwearBeforeChange
                                              orderby tipwearBeforeChange.TipchangeTimestamp descending
                                              select tipwearBeforeChange
                                              ).ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult TipChangeList()
        {
            var data = DataBuffer.Tipstatus;
            //in case hangfire is taking a day off
            if (data == null)
            {
                Backgroundwork backgroundwork = new Backgroundwork();
                backgroundwork.UpdateTipstatus();
            }
            else //add tracking timestamp for hangfire sync
            {
                ViewBag.DataTimestamp = DataBuffer.TipstatusLastDt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return View(data);
        }
    }
}