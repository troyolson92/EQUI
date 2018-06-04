using EQUICommunictionLib;
using EqUiWebUi.WebGridHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Maximo_ui.Controllers
{
    public class WorkorderDetailsController : Controller
    {
        // GET: Maximo_ui/WorkorderDetails
        public ActionResult Index()
        {
            return View();
        }

        //Standard details view about 1 workorder 
        [HttpGet]
        public ActionResult WoDetails(string wonum, bool RealtimeConn = false)
        {
            ViewBag.wonum = wonum;
            ViewBag.RealtimeConn = RealtimeConn;
            return View();
        }

        //standard details partial about 1 workorder (long text, failure, labor)
        [HttpGet]
        public ActionResult _woDetails(string wonum, bool RealtimeConn = false)
        {
            //check if user is allowed to user realtimeConn
            if (RealtimeConn)
            {
                roleProvider roleProvider = new roleProvider();
                if (!roleProvider.IsUserInRole(System.Web.HttpContext.Current.User.Identity.Name, "MAXIMOrealtime"))
                {
                    RealtimeConn = false;
                }
            }
            ViewBag.RealtimeConn = RealtimeConn;

            if (wonum == null) wonum = "NoWonum";
            if (wonum == "") wonum = "NoWonum";
            #region querys
            string cmdWORKORDER = "select * from MAXIMO.WORKORDER WORKORDER WHERE WORKORDER.WONUM = '{0}'";
            cmdWORKORDER = string.Format(cmdWORKORDER, wonum);

            string cmdFAILUREREMARKdescription = "select* from MAXIMO.FAILUREREMARK FAILUREREMARK WHERE FAILUREREMARK.WONUM = '{0}'";
            cmdFAILUREREMARKdescription = string.Format(cmdFAILUREREMARKdescription, wonum);

            string cmdFAILUREREMARK = (@"
                select  NVL2(LD.LDTEXT, LD.LDTEXT, '') LDTEXT
                from MAXIMO.FAILUREREMARK FM 
                left join MAXIMO.LONGDESCRIPTION LD on LD.LDKEY = FM.FAILUREREMARKID AND LD.LDOWNERTABLE = 'FAILUREREMARK'
                where fm.wonum = '{0}'
            ");
            cmdFAILUREREMARK = string.Format(cmdFAILUREREMARK, wonum);
            //
            string cmdLONGDESCRIPTION = (@"
                select NVL2(LD.LDTEXT, LD.LDTEXT, '') LDTEXT
                from MAXIMO.WORKORDER WO 
                left join MAXIMO.LONGDESCRIPTION LD on LD.LDKEY = WO.WORKORDERID AND LD.LDOWNERTABLE = 'WORKORDER'
                where WO.wonum = '{0}'
            ");
            cmdLONGDESCRIPTION = string.Format(cmdLONGDESCRIPTION, wonum);
            //
            string cmdLabor = (@"
            select 
             PERSON.DISPLAYNAME
            ,CRAFT
            ,PERSON.SUPERVISOR
            ,LABTRANS.ENTERDATE
            ,ROUND(REGULARHRS,2) REGULARHRS
            from MAXIMO.LABTRANS  LABTRANS 
            left join MAXIMO.PERSON ON PERSON.PERSONID = LABTRANS.LABORCODE
            where LABTRANS.REFWO  = '{0}'
            ");
            cmdLabor = string.Format(cmdLabor, wonum);
            //
            string cmdWorkLog = (@"
            select 
            wl.logtype
            ,wl.CREATEBY
            ,wl.CREATEDATE
            ,wl.CLIENTVIEWABLE
            ,wl.DESCRIPTION
            ,ld.LDTEXT 
            from maximo.worklog wl
            left join maximo.longdescription ld  on 
            ld.ldownertable = 'WORKLOG'  
            AND  ld.ldownercol = 'DESCRIPTION'
            AND  ld.LDKEY = wl.WORKLOGID
            where
            wl.RECORDKEY = '{0}'
            ");
            cmdWorkLog = string.Format(cmdWorkLog, wonum);
            #endregion
            //
            EQUICommunictionLib.MaximoComm maximoComm = new MaximoComm();
            DataTable WORKORDER = maximoComm.Oracle_runQuery(cmdWORKORDER, RealtimeConn: RealtimeConn);
            DataTable FAILUREREMARKdesc = maximoComm.Oracle_runQuery(cmdFAILUREREMARKdescription, RealtimeConn: RealtimeConn);
            string LONGDESCRIPTION = maximoComm.GetClobMaximo7(cmdLONGDESCRIPTION,RealtimeConn:RealtimeConn);
            string FAILUREREMARK = maximoComm.GetClobMaximo7(cmdFAILUREREMARK, RealtimeConn: RealtimeConn);
            DataTable LABOR = maximoComm.Oracle_runQuery(cmdLabor, RealtimeConn: RealtimeConn);
            //   DataTable WORKLOG = maximoComm.oracle_runQuery(cmdWorkLog, RealtimeConn:RealtimeConn);
            if (WORKORDER.Rows.Count != 0)
            {
                ViewBag.DESCRIPTION = WORKORDER.Rows[0].Field<string>("DESCRIPTION");
                ViewBag.REPORTEDBY = WORKORDER.Rows[0].Field<string>("REPORTEDBY");
            }
            if (FAILUREREMARKdesc.Rows.Count != 0)
            {
                ViewBag.REMARKDESC = FAILUREREMARKdesc.Rows[0].Field<string>("DESCRIPTION");
            }        
            ViewBag.LongDescription = LONGDESCRIPTION;
            ViewBag.FailureRemark = FAILUREREMARK;
            //parsing datatables using the dynamic system
            WebGridHelpers.WebGridHelper webGridHelper = new WebGridHelper();
            ViewBag.LABORColumns = webGridHelper.getDatatabelCollumns(LABOR);
            List<dynamic> LABORdata = webGridHelper.datatableToDynamic(LABOR);
            DefaultModel LABORmodel = new WebGridHelpers.DefaultModel();
            LABORmodel.PageSize = 5;
            LABORmodel.Data = LABORdata;
            //
            return PartialView(LABORmodel);
        }


        //details view about 1 main workorder and his childeren 
        [HttpGet]
        public ActionResult WoParentDetails(string parentwonum, bool RealtimeConn = false)
        {
            ViewBag.parentwonum = parentwonum;
            ViewBag.RealtimeConn = RealtimeConn;
            return View();
        }

        //partial that gets details in 1 main workorder and loops all the childeren.
        [HttpGet]
        public ActionResult _woParentDetails(string parentwonum, bool RealtimeConn = false)
        {
            //check if user is allowed to user realtimeConn
            if (RealtimeConn)
            {
                roleProvider roleProvider = new roleProvider();
                if (!roleProvider.IsUserInRole(System.Web.HttpContext.Current.User.Identity.Name, "MAXIMOrealtime"))
                {
                    RealtimeConn = false;
                }
            }
            ViewBag.RealtimeConn = RealtimeConn;
            //add the parent to the viewbag.
            ViewBag.parentwonum = parentwonum;
            //Get a list of all the childeren WO for this parent
            if (parentwonum == null) parentwonum = "NoWonum";
            if (parentwonum == "") parentwonum = "NoWonum";
            string qrySubWO = "SELECT * FROM MAXIMO.WORKORDER WORKORDER WHERE WORKORDER.PARENT = '{0}' ORDER BY WORKORDER.LOCATION";
            qrySubWO = string.Format(qrySubWO, parentwonum);
            EQUICommunictionLib.MaximoComm maximoComm = new MaximoComm();
            DataTable SubWo = maximoComm.Oracle_runQuery(qrySubWO, RealtimeConn: RealtimeConn);
            //make this into a list object
            List<Models.Workorder> SubWoList = new List<Models.Workorder>();
            foreach (DataRow row in SubWo.Rows)
            {
                Models.Workorder workorder = new Models.Workorder();
                workorder.WONUM = row.Field<string>("WONUM");
                workorder.LOCATION = row.Field<string>("LOCATION");
                workorder.STATUS = row.Field<string>("STATUS"); 
                workorder.OWNERGROUP = row.Field<string>("OWNERGROUP");
                SubWoList.Add(workorder);
            }
            return PartialView(SubWoList);
        }
    }
}