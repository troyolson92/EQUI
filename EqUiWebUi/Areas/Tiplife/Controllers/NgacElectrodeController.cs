using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Tiplife.Models;

namespace EqUiWebUi.Areas.Tiplife.Controllers
{
    public class NgacElectrodeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                if (Debugger.IsAttached)
                {
                    log.Warn("Loading tip status in method (debug mode)");
                    //in debug mode get the data here
                    GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
                    DataBuffer.Tipstatus = (from tipstatus in gADATAEntities.TipMonitor
                                            select tipstatus).ToList();
                    data = DataBuffer.Tipstatus;
                }
            }

            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (LocationRoot != "")
            {
                data = (from d in data
                        where (d.LocationTree ?? "").Contains(LocationRoot)
                        select d).ToList();
            }
            //
            return PartialView(data);
        }
        //------------------------------------Tiplife info partial.--------------------------------------------------------
        [HttpGet]
        public ActionResult _Tipinfo(string location, int tool_nr)
        {
            ViewBag.location = location;
            ViewBag.tool_nr = tool_nr;
            return PartialView();
        }

        [HttpGet]
        public ActionResult WeldgunTool(string location ="", int tool_nr = 0)
        {
            //get location valid for user profile
            Areas.Welding.Models.GADATAEntitiesWelding db = new Areas.Welding.Models.GADATAEntitiesWelding();
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (location != "")//if location is passing only show that one
            {
                ViewBag.Locations = new SelectList(db.c_timer.Where(c => c.Robot.Contains(location)).OrderBy(c => c.Name), "Name", "Robot");
            }
            else if (LocationRoot != "") //else if user has profile filter apply it
            {
                ViewBag.Locations = new SelectList(db.c_timer.Where(c => (c.LocationTree ?? "").Contains(LocationRoot)).OrderBy(c => c.Name), "Name", "Robot");
            }
            else //show all 
            {
                ViewBag.Locations = new SelectList(db.c_timer.OrderBy(c => c.Name), "Name", "Robot");
            }
            //pass tool number select list 
            if (tool_nr != 0)
            {
                ViewBag.tool_nr = new SelectList(new List<SelectListItem>
                                        {
                                            new SelectListItem { Selected = true,  Text = $"Tool{tool_nr.ToString()}", Value = tool_nr.ToString()},
                                        }, "Text", "Value");
            }
            else //pass all options
            {
                ViewBag.tool_nr = new SelectList(new List<SelectListItem>
                                        {
                                            new SelectListItem { Selected = true,  Text = "Tool1", Value = "1"},
                                            new SelectListItem { Selected = false, Text = "Tool2", Value = "2"},
                                            new SelectListItem { Selected = false, Text = "Tool3", Value = "3"},
                                            new SelectListItem { Selected = false, Text = "Tool4", Value = "4"},
                                            new SelectListItem { Selected = false, Text = "Tool5", Value = "5"},
                                        }, "Text", "Value");
            }
            //
            return View();
        }

        //------------------------------------Tabel met elektrode wissels.-------------------------------------------------
        [HttpGet]
        public ActionResult TipwearBeforeChange()
        {
            return View();
        }
        [HttpGet]
        public ActionResult _TipwearBeforeChangeGrid(int daysback = 360, string location = "", int tool_nr = 1)
        {
            var startdate = DateTime.Now.Date.AddDays(daysback * -1);
            GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            IQueryable<TipwearBeforeChange> data = from t in gADATAEntities.TipwearBeforeChange
                                                   where t.TipchangeTimestamp > startdate
                                                   && (t.LocationTree ?? "").Contains(LocationRoot)
                                                   && (t.controller_name.Contains(location) && t.Tool_Nr == tool_nr)
                                                   select t;
            return PartialView(data);
        }

        //------------------------------------tabel met ruwe tipdress data.-------------------------------------------------
        [HttpGet]
        public ActionResult TipDressData(int daysback = 360)
        {
            return View();
        }
        public ActionResult _TipDressDataGrid(int daysback = 360, string location = "", int tool_nr = 1)
        {
            var startdate = DateTime.Now.Date.AddDays(daysback * -1);
            GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            IQueryable<TipDressLogFile> data = from t in gADATAEntities.TipDressLogFile
                                               where t.Date_Time > startdate
                                               && (t.LocationTree ?? "").Contains(LocationRoot)
                                               && (t.controller_name.Contains(location) && t.Tool_Nr == tool_nr) 
                                               select t;
            return PartialView(data);
        }

        //------------------------------------tabel met TipLifeExpectations-------------------------------------------------
        [HttpGet]
        public ActionResult TipLifeExpectations()
        {
            return View();
        }
        [HttpGet]
        public ActionResult _TipLifeExpectationsGrid(string location = "", int tool_nr = 1)
        {
            GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            IQueryable<TipLifeExpectations> data = from t in gADATAEntities.TipLifeExpectations
                                                   where (t.LocationTree ?? "").Contains(LocationRoot)
                                                   && (t.controller_name.Contains(location) && t.Tool_Nr == tool_nr)
                                                   select t;
            return PartialView(data);
        }

        //----------------------------------onderhouds plannings tools------------------------------------------------------
        [HttpGet]
        public ActionResult PlanTipChange()
        {
            user_management.Controllers.AreaFiltersController areaFiltersController = new user_management.Controllers.AreaFiltersController();
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            ViewBag.selectlist = areaFiltersController.getAreaSelectList(LocationRoot);
            return View();
        }

        //get filterd list of wich need to be change
        public ActionResult _TipsToChange(string locationFilter, int minWear = 0, int minParts = 0, int maxDress = 1000)
        {
            GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            IEnumerable<TipMonitor> data = from tipMonitor in DataBuffer.Tipstatus
                                          where
                                          (tipMonitor.pWear > minWear
                                           || tipMonitor.nRcars.GetValueOrDefault(1000) < minParts //if no Rcars value available ignore! 
                                           || tipMonitor.nDress > maxDress
                                           || (tipMonitor.Status != "" && tipMonitor.Status != "NWIC")  //do not push for no wear in clac 
                                          )
                                          && tipMonitor.LocationTree.Contains(locationFilter) //apply dropdown filter
                                          && tipMonitor.LocationTree.Contains(LocationRoot) //apply user filter
                                          orderby tipMonitor.nRcars ascending
                                          select tipMonitor;
            log.Info($"Plantipchange for: {locationFilter} Filters: minwear: {minWear} minparts: {minParts} maxDress: {maxDress}  |resultCount: {data.Count()}");
            //debug added to store result in log and see if they follow the plan.
            List<TipMonitor> results = data.ToList();
            foreach (TipMonitor result in results )
            {
                log.Info($"PlantipchangeResult for: {result.Robot} wear: {result.pWear} parts: {result.nRcars} dresses: {result.nDress} status: {result.Status}");
            }

            return PartialView(data);
        }
        //get filterd list of wich need to be change
        public ActionResult _TipsChanged(string locationFilter, int minWear = 0, int minParts = 0, int maxDress = 1000)
        {
            GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            IEnumerable<TipMonitor> data = from tipMonitor in DataBuffer.Tipstatus
                                           where
                                           (tipMonitor.pWear > minWear
                                            || tipMonitor.nRcars.GetValueOrDefault(1000) < minParts //if no Rcars value available ignore! 
                                            || tipMonitor.nDress > maxDress
                                            || (tipMonitor.Status != "" && tipMonitor.Status != "NWIC")  //do not push for no wear in clac 
                                           )
                                          && tipMonitor.LocationTree.Contains(locationFilter) //apply dropdown filter
                                          && tipMonitor.LocationTree.Contains(LocationRoot) //apply user filter
                                           orderby tipMonitor.nRcars ascending
                                           select tipMonitor;
            return PartialView(data);
        }

    }
}