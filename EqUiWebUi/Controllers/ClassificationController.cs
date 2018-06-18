using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Models;

namespace EqUiWebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ClassificationController : Controller
    {

        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: Classification
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        //Classification tool interface
        [HttpGet]
        public ActionResult ClassificationTool()
        {
            c_LogClassRules Rule = new c_LogClassRules();
            //init values to everything
            Rule.coderangeStart = 0;
            Rule.coderangeEnd = 1000000;
            Rule.textSearch = "%";

            ViewBag.c_logClassSystem_id = new SelectList(db.c_logClassSystem, "id", "Name");
            ViewBag.c_SubgroupId = new SelectList(db.c_Subgroup, "id", "Subgroup");
            ViewBag.c_ClassificationId = new SelectList(db.c_Classification, "id", "Discription"); 

            return View(Rule);
        }


        //return a partial view gird based on how the user filterd.
        //we use a dummy logclassRules object to pass the data
        //we need to handle 2 modes. 
        //1 user searches by locode range or text
        //2 users searches for all revord in class / subgroup 
        [HttpGet]
        public ActionResult _logSearchResult(c_LogClassRules c_LogClassRule, bool SearchByClassification = false)
        {
            //get c_logclassRule
            c_logClassSystem c_LogClassSystem = db.c_logClassSystem.Where(c => c.id == c_LogClassRule.c_logClassSystem_id).First();
            if (c_LogClassSystem == null)
            {
                //in case we doent get a result back
                throw new NotSupportedException();
            }

            //we need to make a dummy object to query. look at how we did the variable data in vasc 
            if (!SearchByClassification)
            {

                IQueryable<l_dummyLogClassResult> result = db.l_dummyLogClassResult.SqlQuery(c_LogClassSystem.SelectStatement).Where(c =>
                                    c.text.Like(c_LogClassRule.textSearch) //hanlde text like statement
                                    && (c.code >= c_LogClassRule.coderangeStart && c.code <= c_LogClassRule.coderangeEnd) //handle range search
                                    ).AsQueryable();
                return PartialView(result);
            }
            else
            {
                IQueryable<l_dummyLogClassResult> result = db.l_dummyLogClassResult.SqlQuery(c_LogClassSystem.SelectStatement).Where(c =>
                //handle search by class and subgroup 
                //Classid and subgroupid van be 0 of not set. (then allow all)
                                     ((c.c_Classification_id == c_LogClassRule.c_ClassificationId) || c_LogClassRule.c_ClassificationId == 0 )
                                     &&
                                     ((c.c_Subgroup_id == c_LogClassRule.c_SubgroupId) || (c_LogClassRule.c_SubgroupId == 0))
                                    ).AsQueryable();
                return PartialView(result);
            }
        }


        //need to find a way to sync c_classification without breaking the key. (data from maximp)
        public ActionResult UpdateC_Classiciation()
        {
            return View();
        }

        //need to find a way to sync c_subgroup without breaking the key. (data manage by user... make stand alone edit add controller)
        public ActionResult UpdateC_Subgroup()
        {
            return View();
        }
    }
}