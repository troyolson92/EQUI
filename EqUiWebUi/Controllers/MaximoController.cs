using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;
using EqUiWebUi.Models;

namespace EqUiWebUi.Controllers
{
    public class MaximoController : Controller
    {
        EQUICommunictionLib.MaximoComm lmaxCom = new MaximoComm();

        //
        // GET: /Maximo/

        public ActionResult Index()
        {
            return HttpNotFound();
        }

        public ActionResult WoDetails(string wonum)
        {
            var workorder = new MaximoWorkorder() { sWoNum = wonum, sWoDetails = lmaxCom.getMaximoDetails(wonum) };
            //return View(workorder);
            workorder.sWoDetails =  workorder.sWoDetails.Replace("font-size: small","font-size: xx-small");
            return Content(workorder.sWoDetails);
        }


        [HttpGet]
        public ActionResult WojqGrid()
        {
            return View();
        }

    }
}
