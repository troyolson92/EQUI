using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Tiplife.Models;

namespace EqUiWebUi.Areas.Tiplife.Controllers
{
    /// <summary>
    /// control handles electrode data SPECIFIC TO VASC
    /// </summary>
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
        /// Page show Tcp_log for each tool
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Tcp_log()
        {
            return View();
        }

        /// <summary>
        /// Partial view that is loaded in Tcp_log contains grid 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="Setup_no"></param>
        /// <returns></returns>
        public ActionResult _Tcp_logGrid(string location = "", int Setup_no = 1)
        {
            GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            IQueryable<TCP_LOG> data = from t in gADATAEntities.TCP_LOG
                                                   where (t.LocationTree ?? "").Contains(LocationRoot)
                                                   && (t.controller_name.Contains(location) && t.SetUp_No == Setup_no)
                                                   select t;
            return PartialView(data);
        }


    }
}