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
            return View();
        }

        [HttpGet]
        public ActionResult _TipStatus()
        {
            //
            var data = DataBuffer.Tipstatus;
            //in case still null trow error return empty result 
            if (data == null)
            {
                data = new List<TipMonitor>();
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

        //------------------------------------tabel met elektrode wissels.-------------------------------------------------
        [HttpGet]
        public ActionResult TipwearBeforeChange(int daysback = 360)
        {
            var startdate = DateTime.Now.Date.AddDays(daysback*-1);
            GADATAEntities gADATAEntities = new GADATAEntities();
            IQueryable<TipwearBeforeChange> data = from tipwearBeforeChange in gADATAEntities.TipwearBeforeChange
                                                   where tipwearBeforeChange.TipchangeTimestamp > startdate
                                                   select tipwearBeforeChange;
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
            IQueryable<TipMonitor> data = from tipMonitor in gADATAEntities.TipMonitor
                                          where
                                          (tipMonitor.pWear > minWear
                                           //  || tipMonitor.nRcars < minParts
                                           || tipMonitor.nDress > maxDress
                                          )
                                          && tipMonitor.LocationTree.Contains(locationFilter)
                                          select tipMonitor;
            return PartialView(data);
        }
        //get filterd list of wich need to be change
        public ActionResult _TipsChanged(string locationFilter, int minWear = 0, int minParts = 0)
        {
            GADATAEntities gADATAEntities = new GADATAEntities();
            IQueryable<TipMonitor> data = from tipMonitor in gADATAEntities.TipMonitor
                                          where tipMonitor.pWear > minWear
                                          && tipMonitor.nRcars > minParts
                                          && tipMonitor.LocationTree.Contains(locationFilter)
                                          select tipMonitor;
            return PartialView(data);
        }
    }
}