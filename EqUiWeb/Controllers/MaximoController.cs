using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;
using EquiWebUi.Models;

namespace EquiWebUi.Controllers
{
    public class MaximoController : Controller
    {
        EQUICommunictionLib.MaximoComm lMxCom = new EQUICommunictionLib.MaximoComm();

        //
        // GET: /Maximo/

        public ActionResult Index()
        {
            return HttpNotFound();
        }

        public ActionResult WoDetails(string wonum)
        {
            var workorder = new MaximoWorkorder() { sWoNum = wonum, sWoDetails = lMxCom.getMaximoDetails(wonum) };
            //return View(workorder);
            workorder.sWoDetails =  workorder.sWoDetails.Replace("font-size: small","font-size: xx-small");
            return Content(workorder.sWoDetails);
        }


    }
}
