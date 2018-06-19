using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
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
            ViewBag.c_ClassificationId = new SelectList(db.c_Classification.OrderBy(c => c.Classification) , "id", "Classification"); 

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

        //to manualy set a classification.
        //this has modes 
        //1 if no RowID is given we run it against the current filterParms (clear = false)
        //2 if a RowID is given only that row is set. (clear = false)
        //3 if no Row is is given we run it against the current filterParms and CLEAR the set classification (clear = true)
        //4 if a Rowid is given only CLEAR thar row. (clear = true)
        public JsonResult SetClass(c_LogClassRules c_LogClassRule, int? RowID, bool Clear = false)
        {
            //get c_logclassRule
            c_logClassSystem c_LogClassSystem = db.c_logClassSystem.Where(c => c.id == c_LogClassRule.c_logClassSystem_id).First();
            string UpdateSkelation = c_LogClassSystem.UpdateStatement;

            //update stament example.. Parameters are MANDATORY!
            /*these pars must be passed
             *  DECLARE @textSearch as varchar(max)
                DECLARE @coderangeStart as int
                DECLARE @coderangeEnd as int
                DECLARE @rowID as int
                DECLARE @Clear as bit
                */

            /*
SELECT L_error.id as 'id'
      ,L_error.[error_number] as 'code'
      ,L_error.error_text as 'text'
      ,L_error.c_RuleId as  'c_logcClassRules_id'
      ,L_error.c_ClassificationId as 'c_Classification_id'
      ,L_error.c_SubgroupId as 'c_Subgroup_id'
  FROM [GADATA].C3G.L_error 
  WHERE  
  --Group update
  (
  L_error.error_text like @textSearch
  AND 
  L_error.[error_number] between @coderangeStart and @coderangeEnd
  AND
  @rowID is null
  )
  --single set 
  OR
  (
  L_error.id = @rowID
  AND
  @rowID is not null 
  )
             * */

            db.Database.ExecuteSqlCommand(UpdateSkelation,
                    new SqlParameter("@textSearch", c_LogClassRule.textSearch),
                    new SqlParameter("@coderangeStart", c_LogClassRule.coderangeStart),
                    new SqlParameter("@coderangeEnd", c_LogClassRule.coderangeEnd),
                    new SqlParameter("@rowID", RowID ?? SqlInt32.Null),
                    new SqlParameter("@Clear", Clear));

            return Json(new { Msg = "job OK" },JsonRequestBehavior.AllowGet);

        }
    }
}

//need to find a good place to pust this
public class JsonHttpStatusResult : JsonResult
{
    private readonly HttpStatusCode _httpStatus;

    public JsonHttpStatusResult(object data, HttpStatusCode httpStatus)
    {
        Data = data;
        _httpStatus = httpStatus;
    }

    public override void ExecuteResult(ControllerContext context)
    {
        context.RequestContext.HttpContext.Response.StatusCode = (int)_httpStatus;
        base.ExecuteResult(context);
    }
}