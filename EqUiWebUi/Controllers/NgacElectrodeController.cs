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
        public ActionResult CurrentTipstatus()
        {
            //refresh every 10 minutes anyway ! 
            Response.AddHeader("Refresh", "600");
            return View();
        }

        [HttpGet]
        public ActionResult _TipStatus()
        {
            //
            var data = DataBuffer.Tipstatus;
            //in case hangfire is taking a day off
            if (data == null)
            {
                Backgroundwork backgroundwork = new Backgroundwork();
                backgroundwork.UpdateTipstatus();
                data = DataBuffer.Tipstatus;
            }
            //in case still null trow error 
            if (data == null)
            {
                return new HttpNotFoundResult("Woeps there seems to be an error");
            }

            if (Session["LocationRoot"].ToString() != "")
                {
                    data = (from d in data
                            where (d.LocationTree ?? "").Contains(Session["LocationRoot"].ToString())
                            select d).ToList();
                }
            //
            return PartialView(data);
        }

        [HttpGet]
        public JsonResult TipwearCheckNewData(DateTime dataTimestamp)
        {
            Boolean breload = false;
            if (DataBuffer.TipstatusLastDt > dataTimestamp.AddSeconds(1))
            {
                breload = true;
            }
            //
            return new JsonResult()
            {
                Data = new { doReload = breload, dataTimestamp = DataBuffer.TipstatusLastDt.ToString("yyyy-MM-dd HH:mm:ss"), lastCheck = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //------------------------------------tabel met elektrode wissels.-------------------------------------------------
        [HttpGet]
        public ActionResult TipwearBeforeChange(int daysback = 30)
        {
            var startdate = DateTime.Now.Date.AddDays(daysback*-1);
            GADATAEntities gADATAEntities = new GADATAEntities();
            List<TipwearBeforeChange> data = (from tipwearBeforeChange in gADATAEntities.TipwearBeforeChange
                                              where tipwearBeforeChange.TipchangeTimestamp > startdate
                                              select tipwearBeforeChange
                                              ).ToList();
            return View(data);
        }

        //----------------------------------onderhouds plannings tools------------------------------------------------------
        [HttpGet]
        public ActionResult PlanTipChange()
        {

            return View();
        }

        //get filterd list of wich need to be change
        public ActionResult _TipsToChange(string locationFilter, int minWear = 0, int minParts = 0, int maxDress = 1000)
        {
            GADATAEntities gADATAEntities = new GADATAEntities();
            List<TipMonitor> data = (from tipMonitor in gADATAEntities.TipMonitor
                                     where 
                                     (   tipMonitor.pWear > minWear
                                    //  || tipMonitor.nRcars < minParts
                                      || tipMonitor.nDress > maxDress
                                     )
                                     && tipMonitor.LocationTree.Contains(locationFilter)
                                              select tipMonitor
                                              ).ToList();
            return PartialView(data);
        }
        //get filterd list of wich need to be change
        public ActionResult _TipsChanged(string locationFilter, int minWear = 0, int minParts = 0)
        {
            GADATAEntities gADATAEntities = new GADATAEntities();
            List<TipMonitor> data = (from tipMonitor in gADATAEntities.TipMonitor
                                     where tipMonitor.pWear > minWear
                                     && tipMonitor.nRcars > minParts
                                     && tipMonitor.LocationTree.Contains(locationFilter)
                                     select tipMonitor
                                              ).ToList();
            return PartialView(data);
        }
    }
}