using EQUICommunictionLib;
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

        //standard details partial about 1 work order (long text, failure, labor)
        public ActionResult _woDetails(string wonum, bool RealtimeConn = false, bool RenderSubwo = true)
        {
            //check if user is allowed to user realtimeConn
            string MaximoDbName = "MAXIMO7rep";
            int CommandTimeout = 30;
            if (RealtimeConn)
            {
                MaximoDbName = "MAXIMOrt";
                CommandTimeout = 5;
            }
     
            if (wonum == null) wonum = "NoWonum";
            if (wonum == "") wonum = "NoWonum";
            ViewBag.wonum = wonum;
            ViewBag.RenderSubwo = RenderSubwo;
            #region querys
            string cmdWORKORDER = "select * from MAXIMO.WORKORDER WORKORDER WHERE WORKORDER.WONUM = '{0}'";
            cmdWORKORDER = string.Format(cmdWORKORDER, wonum);

            string cmdFAILUREREMARKdescription = "select * from MAXIMO.FAILUREREMARK FAILUREREMARK WHERE FAILUREREMARK.WONUM = '{0}'";
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
            EQUICommunictionLib.ConnectionManager connectionManager = new ConnectionManager();
            DataTable WORKORDER;
            DataTable FAILUREREMARKdesc;
            string LONGDESCRIPTION;
            string FAILUREREMARK;
            DataTable LABOR;

            WORKORDER = connectionManager.RunQuery(cmdWORKORDER, dbName: MaximoDbName, maxEXECtime: CommandTimeout, enblExeptions: true);
            FAILUREREMARKdesc = connectionManager.RunQuery(cmdFAILUREREMARKdescription, dbName: MaximoDbName, maxEXECtime: CommandTimeout, enblExeptions: true);
            LONGDESCRIPTION = connectionManager.GetCLOB(cmdLONGDESCRIPTION, dbName: MaximoDbName, enblExeptions: true);
            FAILUREREMARK = connectionManager.GetCLOB(cmdFAILUREREMARK, dbName: MaximoDbName, enblExeptions: true);
            LABOR = connectionManager.RunQuery(cmdLabor, dbName: MaximoDbName, maxEXECtime: CommandTimeout, enblExeptions: true);
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

            //make labor into a list object
            List<Models.Labor> LaborList = new List<Models.Labor>();
            foreach (DataRow row in LABOR.Rows)
            {
                Models.Labor labor = new Models.Labor();
                labor.DisplayName = row.Field<string>("DISPLAYNAME");
                labor.Craft = row.Field<string>("CRAFT");
                labor.Supervisor = row.Field<string>("SUPERVISOR");
                labor.EnterDate = row.Field<DateTime>("ENTERDATE");
                labor.REGULARHRS = row.Field<decimal>("REGULARHRS");
                LaborList.Add(labor);
            }
            //
            ViewBag.RealtimeConn = RealtimeConn;
            return PartialView(LaborList);
        }


        //details view about all children from given work order
        [HttpGet]
        public ActionResult SubWoDetails(string parentwonum, bool RealtimeConn = false)
        {
            ViewBag.parentwonum = parentwonum;
            ViewBag.RealtimeConn = RealtimeConn;
            return View();
        }

        //partial that gets details of all the children of the given work order.
        public ActionResult _SubWoDetails(string parentwonum, bool RealtimeConn = false)
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
            //add the parent to the view bag.
            ViewBag.parentwonum = parentwonum;
            //Get a list of all the children WO for this parent
            if (parentwonum == null) parentwonum = "NoWonum";
            if (parentwonum == "") parentwonum = "NoWonum";
            string qrySubWO = "SELECT * FROM MAXIMO.WORKORDER WORKORDER WHERE WORKORDER.PARENT = '{0}' ORDER BY WORKORDER.LOCATION";
            qrySubWO = string.Format(qrySubWO, parentwonum);

            EQUICommunictionLib.ConnectionManager connectionManager = new ConnectionManager();
            DataTable SubWo;
            if (RealtimeConn)
            {
                SubWo = connectionManager.RunQuery(qrySubWO, dbName: "MAXIMOrt", maxEXECtime: 15, enblExeptions: true);
            }
            else
            {
                SubWo = connectionManager.RunQuery(qrySubWO, dbName: "MAXIMO7rep", maxEXECtime: 15, enblExeptions: true);
            }
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