using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Maximo_ui.Controllers
{
    public class MaximoController : Controller
    {
        // GET: Maximo_ui/Maximo
        public ActionResult Index()
        {
            return View();
        }

        //lagency support for sharepoint list 
        public ActionResult WoDetails(int? wonum)
        {
            return RedirectToAction("WoDetails", "WorkorderDetails", new {area = "Maximo_ui", wonum = wonum });
        }

        //make tool to help construct where clause
  /*      
((woclass = 'WORKORDER' or woclass = 'ACTIVITY')
and historyflag = 0 and istask = 0 and siteid = 'VCG'
and((exists (select 1 from failureremark where (enterdate > sysdate - 1/4)
and failureremark.wonum = workorder.wonum)) or(exists (select 1 from worklog where (createdate > sysdate - 1/4)
and worklog.recordkey = workorder.wonum )) or(workorder.reportdate > sysdate - 1/4)  )
and(worktype = 'CI' or worktype = 'SA'))
and(exists (select 1 from maximo.multiassetlocci where (exists (select 1 from maximo.locancestor where ((ancestor = 'A'))
and(location = multiassetlocci.location and systemid = (select systemid from locsystem where primarysystem = 1
and siteid = multiassetlocci.siteid)
and siteid = multiassetlocci.siteid)))
and(recordkey= workorder.wonum and recordclass = workorder.woclass and worksiteid = workorder.siteid)))
        */
    }
}