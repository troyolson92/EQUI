using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Tiplife.Models;

namespace EqUiWebUi.Areas.Tiplife.Controllers
{
    public class NgacElectrodeController : Controller
    {
        // GET: Tiplife/NgacElectrode
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
            var startdate = DateTime.Now.Date.AddDays(daysback * -1);
            GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
            string locationroot = Session["LocationRoot"].ToString();
            IQueryable<TipwearBeforeChange> data = from t in gADATAEntities.TipwearBeforeChange
                                                   where t.TipchangeTimestamp > startdate
                                                   && (t.LocationTree ?? "").Contains(locationroot)
                                                   select t;
            return View(data);
        }

        //------------------------------------tabel met ruwe tipdress data.-------------------------------------------------
        [HttpGet]
        public ActionResult TipDressData(int daysback = 360)
        {
            var startdate = DateTime.Now.Date.AddDays(daysback * -1);
            GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
            string locationroot = Session["LocationRoot"].ToString();
            IQueryable<TipDressLogFile> data = from t in gADATAEntities.TipDressLogFile
                                               where t.Date_Time > startdate
                                               && (t.LocationTree ?? "").Contains(locationroot)
                                               select t;
            return View(data);
        }

        //----------------------------------onderhouds plannings tools------------------------------------------------------
        [HttpGet]
        public ActionResult PlanTipChange()
        {
            user_management.Controllers.AreaFiltersController areaFiltersController = new user_management.Controllers.AreaFiltersController();
            string locationroot = Session["LocationRoot"].ToString();
            ViewBag.selectlist = areaFiltersController.getAreaSelectList(locationroot);
            return View();
        }

        //get filterd list of wich need to be change
        public ActionResult _TipsToChange(string locationFilter, int minWear = 0, int minParts = 0, int maxDress = 1000)
        {
            GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
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
            GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
            IQueryable<TipMonitor> data = from tipMonitor in gADATAEntities.TipMonitor
                                          where tipMonitor.pWear > minWear
                                          && tipMonitor.nRcars > minParts
                                          && tipMonitor.LocationTree.Contains(locationFilter)
                                          select tipMonitor;
            return PartialView(data);
        }
    }
}