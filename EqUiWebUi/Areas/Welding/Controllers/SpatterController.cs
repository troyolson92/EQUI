using EqUiWebUi.Areas.Welding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Welding.Controllers
{
    public class SpatterController : Controller
    {
        GADATAEntitiesWelding db = new GADATAEntitiesWelding();
        // GET: Welding/Spatter

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _SpikesV316()
        {
            IQueryable<ActualSpatter_> data = db.ActualSpatter_.AsQueryable().Where( c => c.Zone >= 41 && c.Zone <= 52) ;
            return PartialView(data);
         
        }
    }
}