using EqUiWebUi.Areas.Welding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Welding.Controllers
{
    public class UpdateController : Controller

    {
        GADATAEntitiesWelding db = new GADATAEntitiesWelding();

        // GET: Welding/Update
        //test for in line edit 1 method for 1 value
        public void UpdateLabel(string id, string value)
        {
            rt_weldfault weldfault = db.rt_weldfault.Find(Int32.Parse(id));
            weldfault.WMComment = value;
            //  db.SaveChanges();
        }
    }
}



