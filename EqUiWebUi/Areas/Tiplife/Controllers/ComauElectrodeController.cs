using EqUiWebUi.Areas.Tiplife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Tiplife.Controllers
{
    /// <summary>
    /// control handles electrode data SPECIFC to Comau
    /// </summary>
    public class ComauElectrodeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        GADATAEntitiesTiplife db = new GADATAEntitiesTiplife();

        // GET: Tiplife/ComauElectrode
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
            ViewBag.location = location.Trim();
            ViewBag.tool_nr = tool_nr;
            return PartialView();
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
        /// <param name="tool_id"></param>
        /// <returns></returns>
        public ActionResult _Tcp_logGrid(string location = "", int tool_id = 1)
        {

            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            IQueryable<C3G_SBCUData> data = from t in db.SBCUData
                                            where (t.LocationTree ?? "").Contains(LocationRoot)
                                            && (t.RobotName.Contains(location) && t.gunnum == tool_id)
                                            select t;
            return PartialView(data);
        }
    }
}