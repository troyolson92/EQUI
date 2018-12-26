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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Page show current status for all electrode gets refresh by singalR
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CurrentTipstatus()
        {
            return View();
        }

        /// <summary>
        /// Partial view that gets loaded in CurrentTipstatus (grid)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Partial view that gets loaded in CurrentTipstatus when users clicks row. Show all kind of extra info about electrode
        /// </summary>
        /// <param name="location"></param>
        /// <param name="tool_nr"></param>
        /// <returns></returns>
        public ActionResult _Tipinfo(string location, int tool_nr)
        {
            ViewBag.location = location;
            ViewBag.tool_nr = tool_nr;
            return PartialView();
        }

        /// <summary>
        /// Page show all tip dresses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TipDressData()
        {
            return View();
        }

        /// <summary>
        /// Partial view that is loaded in TipDressData contains grid
        /// </summary>
        /// <param name="daysback"></param>
        /// <param name="location"></param>
        /// <param name="tool_nr"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Page shows all tip changes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TipwearBeforeChange()
        {
            return View();
        }

        /// <summary>
        /// Partial view that is loaded in TipwearBeforeChange contains grid
        /// </summary>
        /// <param name="daysback"></param>
        /// <param name="location"></param>
        /// <param name="tool_nr"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Page show TipLifeExpectations for each electrode
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TipLifeExpectations()
        {
            return View();
        }

        /// <summary>
        /// Partial view that is loaded in TipLifeExpectations contains grid 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="tool_nr"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Main page to plan electrode changes. User can set cars to build / Max electrode life and gets returned list of electrodes that needs to be changes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PlanTipChange()
        {
            user_management.Controllers.AreaFiltersController areaFiltersController = new user_management.Controllers.AreaFiltersController();
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            ViewBag.selectlist = areaFiltersController.getAreaSelectList(LocationRoot);
            return View();
        }

        /// <summary>
        /// Partial view that show witch electrode need to be changed (grid)
        /// </summary>
        /// <param name="locationFilter"></param>
        /// <param name="minWear"></param>
        /// <param name="minParts"></param>
        /// <param name="maxDress"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Main interface page for welgunTool (allows user to select a location and tool number)
        /// </summary>
        /// <param name="location"></param>
        /// <param name="tool_nr"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WeldgunTool(string location = "", int tool_nr = 0)
        {
            //get location valid for user profile
            Areas.Welding.Models.GADATAEntitiesWelding db = new Areas.Welding.Models.GADATAEntitiesWelding();
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (location != "")//if location is passing only show that one
            {
                SelectList list = new SelectList(db.c_timer.Where(c => c.Robot.Contains(location)).OrderBy(c => c.Name), "Name", "Robot");
                ViewBag.Locations = list;
                //check if we should auto load. (if only one is possible).
                if (list.Count() == 1 && tool_nr != 0)
                {
                    ViewBag.autoload = true;
                }
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

        /// <summary>
        /// Partial view that gets loading into WeldgunTool (all chars are rendered in this page)
        /// </summary>
        /// <param name="location"></param>
        /// <param name="tool_nr"></param>
        /// <returns></returns>
        public ActionResult _WeldgunTool(string location, int tool_nr)
        {
            ViewBag.location = location;
            ViewBag.tool_nr = tool_nr;
            ViewBag.daysback = System.DateTime.Now.AddDays(-30);
            //IF NGAC location add 'real wear VS measured wear scatter chart' ID:43
            //This is a hack on the control chart system. Need to look if I can implement this in a clean way.
            Areas.VASC.Models.GADATAEntitiesVASC gADATAEntitiesVASC = new VASC.Models.GADATAEntitiesVASC();
            if(gADATAEntitiesVASC.c_controller.Where(c => c.controller_name == location).Count() !=0)
            {
                ViewBag.NgacDummyAlarmobject = $"{location.Trim()}_gun{tool_nr}";
                ViewBag.NgacDummyTriggerId = 43;
            }
            //Add midair scatter chart always (all weld gun tools should have a midair)

            //get a list of all control charts that need to be rendered to this location / tool_nr combination
            //Make the match on start with location and EndsWith tool_nr. This is a leap of fate and all has to do with how the users sets up the alerts.
            //examples of alarm objects. 81030R26_gun1 /361010R01_Tool1
            //this is a mess and should be fixed in Alert system. (tricky)
            Areas.Alert.Models.GADATA_AlertModel db = new Alert.Models.GADATA_AlertModel();
            ViewBag.controlLimits =  db.l_controlLimits.Where(l => l.alarmobject.Trim().ToUpper().StartsWith(location.Trim().ToUpper())
                                                                && l.alarmobject.Trim().EndsWith(tool_nr.ToString())
                                                                && l.isdead == false //only active control limits
                                                                ).ToList();

            return PartialView();
        }


    }
}