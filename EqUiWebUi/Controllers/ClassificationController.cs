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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            Rule.coderangeStart = null;
            Rule.coderangeEnd = null;
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
                                    && (c.code >= c_LogClassRule.coderangeStart.GetValueOrDefault(0) && c.code <= c_LogClassRule.coderangeEnd.GetValueOrDefault(int.MaxValue)) //handle range search
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
        //update statement example.. Parameters are MANDATORY!
        /*
            DECLARE @textSearch as varchar(max)
            DECLARE @coderangeStart as int
            DECLARE @coderangeEnd as int
            DECLARE @rowID as int
            DECLARE @Clear as bit
            */
        /*
            UPDATE GADATA.C3G.l_error
            SET  c_ClassificationID =  CASE  
                                     WHEN @Clear = 0 THEN @c_ClassificationId 
                                     ELSE NULL
                                     END ,
              c_SubgroupId =  CASE  
                                     WHEN @Clear = 0 THEN @c_SubgroupId 
                                     ELSE NULL
                                     END 
            FROM GADATA.C3G.L_error
            WHERE  
            --Group update
            (
            L_error.error_text like @textSearch
            AND 
            L_error.[error_number] between @coderangeStart and @coderangeEnd
            AND
            @rowID = 0
            )
            --single set 
            OR
            (
            L_error.id = @rowID
            AND
            @rowID <> 0 
            )
         * */
        public JsonResult SetClass(c_LogClassRules c_LogClassRule, int RowID, bool Clear = false)
        {
            //get c_logclassRule
            c_logClassSystem c_LogClassSystem = db.c_logClassSystem.Where(c => c.id == c_LogClassRule.c_logClassSystem_id).First();

            db.Database.ExecuteSqlCommand(c_LogClassSystem.UpdateStatement,
                    new SqlParameter("@textSearch", c_LogClassRule.textSearch),
                    new SqlParameter("@coderangeStart", c_LogClassRule.coderangeStart.GetValueOrDefault(0)),
                    new SqlParameter("@coderangeEnd", c_LogClassRule.coderangeEnd.GetValueOrDefault(int.MaxValue)),
                    new SqlParameter("@rowID", RowID),
                    new SqlParameter("@Clear", Clear),
                    new SqlParameter("@c_ClassificationId", c_LogClassRule.c_ClassificationId),
                    new SqlParameter("@c_SubgroupId", c_LogClassRule.c_SubgroupId)
                    );

            return Json(new { Msg = "job OK" },JsonRequestBehavior.AllowGet);
        }

        //to apply clasication rules.
        //this has modes.

        //RunRule statement exaple.. parameter are MANDATORY!
        /*
DECLARE @logClassSystem_id as int --logclassSystemID
DECLARE @RuleId as int --if 0 run all rules if <> 0 run that rule Id only
DECLARE @overrideManualSet as bit --OverRide with rule if manual set
DECLARE @Clear as bit  --Clear if it
DECLARE @UPDATE as bit --reApply if rule already set

set @logClassSystem_id = 0
set @RuleId = 0
set @overrideManualSet = 0
set @Clear = 0
set @UPDATE = 0

         the c_rule ID can have multible meanings.
        --c_RuleID -1 = manual set
        --c_RuleID 0 = processed by auto rule engine
        --c_RuleID NULL = rule engine has not run
        --c_ruleID > 0 = rule was applied
         */

        /*  UPDATE GADATA.c3g.l_error
            SET  c_ClassificationID =  CASE  
                                    WHEN @Clear = 0 THEN r.c_ClassificationId 
                                    ELSE NULL
                                    END ,
                   c_SubgroupId =  CASE  
                                    WHEN @Clear = 0 THEN r.c_SubgroupId 
                                    ELSE NULL
                                    END,
                       c_RuleId =   CASE  
                                    WHEN @Clear = 0 THEN r.id
                                    ELSE NULL
                                    END

            FROM [GADATA].c3g.L_error as L
            left join GADATA.EQUI.c_LogClassRules as r on
            (
            r.c_logClassSystem_id = @logClassSystem_id
            AND
            l.error_text like ISNULL(r.textSearch,'%')
            AND 
            l.[error_number] between ISNULL(r.coderangeStart,0) and ISNULL(r.coderangeEnd,1000000)
            )
            WHERE
            --handle single rule run 
            (
            r.id = @ruleID --single rule
            OR
            @ruleID = 0 --run all
            )
            AND --handle overrideManualSet
            (
            L.c_RuleId <> -1
            OR 
            L.c_RuleId is null 
            OR
            @overrideManualSet = 1
            )
            AND --handle update (reapply rule)
            (
            L.c_RuleId is null
            OR 
            @UPDATE = 1
            )
*/
        public JsonResult RunRule(c_LogClassRules c_LogClassRule, bool overrideManualSet = false, bool Clear = false, bool UPDATE = true)
        {
        c_logClassSystem c_LogClassSystem = db.c_logClassSystem.Where(c => c.id == c_LogClassRule.c_logClassSystem_id).First();

        db.Database.ExecuteSqlCommand(c_LogClassSystem.RunRuleStatement,
                new SqlParameter("@logClassSystem_id", c_LogClassRule.c_logClassSystem_id),
                new SqlParameter("@ruleID", c_LogClassRule.id),
                new SqlParameter("@overrideManualSet", overrideManualSet),
                new SqlParameter("@Clear", Clear),
                new SqlParameter("@UPDATE", UPDATE)
            );
            string debugmsg = string.Format("System: {4}(id{5}) Ruleid: {0}, override: {1}, clear: {2} Update: {3}", c_LogClassRule.id, overrideManualSet, Clear, UPDATE, c_LogClassSystem.Name, c_LogClassSystem.id);
            log.Debug(debugmsg);
            return Json(new { Msg = debugmsg }, JsonRequestBehavior.AllowGet);
        }
    }
}

