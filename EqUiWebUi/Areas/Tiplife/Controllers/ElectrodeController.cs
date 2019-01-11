﻿using EqUiWebUi.Areas.Tiplife.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Tiplife.Controllers
{
    /// <summary>
    /// general control for Electrode works cross platform. (VASC VCSC)
    /// </summary>
    public class ElectrodeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Tiplife/Electrode
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
                data = new List<NGAC_TipMonitor>();
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
        /// Legend partial view for alert status
        /// </summary>
        /// <returns></returns>
        public ActionResult _Legend()
        {
            return PartialView();
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
        /// <param name="Tipchanger">if this is true all wear parms above are ignored</param>
        /// <returns></returns>
        public ActionResult _TipsToChange(string locationFilter, int minWear = 0, int minParts = 0, int maxDress = 1000, bool Tipchanger = false)
        {
            GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            IEnumerable<NGAC_TipMonitor> data;
            if (!Tipchanger)
            {
               data = from tipMonitor in DataBuffer.Tipstatus
                    where
                    (tipMonitor.pWear > minWear
                    || tipMonitor.nRcars.GetValueOrDefault(1000) < minParts //if no Rcars value available ignore! 
                    || tipMonitor.nDress > maxDress
                    || (tipMonitor.Status != "" && tipMonitor.Status != "NO PREDICTION")  //do not push for no wear in clac 
                    )
                    && tipMonitor.LocationTree.Contains(locationFilter) //apply dropdown filter
                    && tipMonitor.LocationTree.Contains(LocationRoot) //apply user filter
                    select tipMonitor;
            }
            else
            {
                data = from tipMonitor in DataBuffer.Tipstatus
                    where
                    (tipMonitor.Status != "" && tipMonitor.Status != "NO PREDICTION")  //do not push for no wear in clac 
                    && tipMonitor.LocationTree.Contains(locationFilter) //apply dropdown filter
                    && tipMonitor.LocationTree.Contains(LocationRoot) //apply user filter
                    select tipMonitor;
            }

            log.Info($"Plantipchange for: {locationFilter} Filters: minwear: {minWear} minparts: {minParts} maxDress: {maxDress}  |resultCount: {data.Count()}");
            //debug added to store result in log and see if they follow the plan.
            List<NGAC_TipMonitor> results = data.ToList();
            foreach (NGAC_TipMonitor result in results)
            {
                log.Info($"PlantipchangeResult for: {result.Robot} wear: {result.pWear} parts: {result.nRcars} dresses: {result.nDress} status: {result.Status}");
            }

            return PartialView(data);
        }

        /// <summary>
        /// Main interface page for welgunTool (allows user to select a location and tool number)
        /// </summary>
        /// <param name="location"></param>
        /// <param name="tool_nr">From robot</param>
        /// <param name="ElectrodeNo">From timer If set to 0 this means tool_nr = ElectrodeNo</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WeldgunTool(string location = "", int tool_nr = 0, int ElectrodeNo = 0)
        {
            //get location valid for user profile
            Areas.Welding.Models.GADATAEntitiesWelding db = new Areas.Welding.Models.GADATAEntitiesWelding();
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (location != "")//if location is passing only show that one
            {
                SelectList list = new SelectList(db.c_timer.Where(c => c.Robot.Contains(location)).OrderBy(c => c.Name), "Robot", "Robot");
                ViewBag.Locations = list;
                //check if we should auto load. (if only one is possible).
                if (list.Count() == 1 && tool_nr != 0 && ElectrodeNo == 0)
                {
                    ViewBag.autoload = true;
                }
            }
            else if (LocationRoot != "") //else if user has profile filter apply it
            {
                ViewBag.Locations = new SelectList(db.c_timer.Where(c => c.enable_bit != -1 &&  (c.LocationTree ?? "").Contains(LocationRoot)).OrderBy(c => c.Name), "Robot", "Robot");
            }
            else //show all (enabled timers)
            {
                ViewBag.Locations = new SelectList(db.c_timer.Where( c => c.enable_bit != -1).OrderBy(c => c.Name), "Robot", "Robot");
            }

            //pass tool_nr select list 
            if (tool_nr != 0)
            {
                ViewBag.tool_nr = new SelectList(new List<SelectListItem>
                                        {
                                            new SelectListItem { Selected = true,  Text = $"Tool{tool_nr.ToString()}", Value = tool_nr.ToString()},
                                        }, "Value", "Text");
            }
            else //pass all options
            {
                ViewBag.tool_nr = new SelectList(new List<SelectListItem>
                                        {
                                            new SelectListItem { Selected = true,  Text = "Tool1", Value = "1"},
                                            new SelectListItem { Selected = false, Text = "Tool2", Value = "2"},
                                            new SelectListItem { Selected = false, Text = "Tool3", Value = "3"},
                                            new SelectListItem { Selected = false, Text = "Tool4", Value = "4"},
                                            new SelectListItem { Selected = false, Text = "Tool5", Value = "5"}
                                        }, "Value", "Text");
            }

            //pass ElectrodeNo select list 
            if (ElectrodeNo != 0)
            {
                ViewBag.ElectrodeNo = new SelectList(new List<SelectListItem>
                                        {
                                            new SelectListItem { Selected = false,  Text = $"ElectrodeNo{ElectrodeNo.ToString()}", Value = ElectrodeNo.ToString()}
                                        }, "Value", "Text");
            }
            else //pass all options
            {
                ViewBag.ElectrodeNo = new SelectList(new List<SelectListItem>
                                        {
                                            new SelectListItem { Selected = true,  Text = "ElectrodeNo = tool_nr", Value = 0.ToString()},
                                            new SelectListItem { Selected = false, Text = "ElectrodeNo1", Value = "1"},
                                            new SelectListItem { Selected = false, Text = "ElectrodeNo2", Value = "2"},
                                            new SelectListItem { Selected = false, Text = "ElectrodeNo3", Value = "3"},
                                            new SelectListItem { Selected = false, Text = "ElectrodeNo4", Value = "4"},
                                            new SelectListItem { Selected = false, Text = "ElectrodeNo5", Value = "5"}
                                        }, "Value", "Text");
            }
            //
            return View();
        }

        /// <summary>
        /// Partial view that gets loading into WeldgunTool (all chars are rendered in this page)
        /// </summary>
        /// <param name="location"></param>
        /// <param name="tool_nr">From robot</param>
        /// <param name="ElectrodeNo">From timer If set to 0 this means tool_nr = ElectrodeNo</param>
        /// <returns></returns>
        public ActionResult _WeldgunTool(string location, int tool_nr, int ElectrodeNo)
        {
            ViewBag.location = location;
            ViewBag.tool_nr = tool_nr;
            if (ElectrodeNo == 0)
            {
                ElectrodeNo = tool_nr;
            }
            ViewBag.ElectrodeNo = ElectrodeNo;
            ViewBag.daysback = System.DateTime.Now.AddDays(-30);
            //IF NGAC location add 'real wear VS measured wear scatter chart' ID:43
            //This is a hack on the control chart system. Need to look if I can implement this in a clean way.
            Areas.VASC.Models.GADATAEntitiesVASC gADATAEntitiesVASC = new VASC.Models.GADATAEntitiesVASC();
            if (gADATAEntitiesVASC.c_controller.Where(c => c.controller_name == location).Count() != 0)
            {
                ViewBag.NgacDummyAlarmobject = $"{location.Trim()}_gun{tool_nr}";
                ViewBag.NgacDummyTriggerId = 43;
                ViewBag.isNgac = true; //used to pick more info modal (ngac or comau)
            }
            else
            {
                ViewBag.isNgac = false;
            }
            //Add midair scatter chart always (all weld gun tools should have a midair)
            ViewBag.MidAirDummyAlarmobject = $"{location.Trim()}_ElecNo{ElectrodeNo}";
            ViewBag.MidAircDummyTriggerId = 44;

            //get a list of all control charts that need to be rendered to this location / tool_nr combination
            //Make the match on start with location and EndsWith tool_nr. This is a leap of fate and all has to do with how the users sets up the alerts.
            //examples of alarm objects. 81030R26_gun1 /361010R01_Tool1
            //this is a mess and should be fixed in Alert system. (tricky)
            Areas.Alert.Models.GADATA_AlertModel db = new Alert.Models.GADATA_AlertModel();
            ViewBag.controlLimits = db.l_controlLimits.Where(l => l.alarmobject.Trim().ToUpper().StartsWith(location.Trim().ToUpper())
                                                               && l.alarmobject.Trim().EndsWith(tool_nr.ToString())
                                                               && l.isdead == false //only active control limits
                                                                ).ToList();

            return PartialView();
        }

    }
}