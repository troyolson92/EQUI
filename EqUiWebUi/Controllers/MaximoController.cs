﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;
using EqUiWebUi.Models;
using System.Data;
using EqUiWebUi.WebGridHelpers;
using System.Text;

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

        //used in tableau to get workorder list. 
        [HttpGet]
        public ActionResult workorders(string location, string station, bool b_ciblings = false, bool b_preventive = false, int page = 1, int fontSize = 12)
        {
            //check the selected locations. 
            if (location == null) location = "NoLocation";
            if (location.Contains(','))
            {
                ViewBag.locationInfo = " !... ";
                //multi locations are selected for now just take first.
                location = location.Split(',')[0];
            }

            //check the selected stations.
            if (station == null) station = "NoStation";
            if (station.Contains(','))
            {
                ViewBag.stationInfo = " !... ";
                //multi stations are selected ... do ? ??
                station = station.Split(',')[0];
            }

            ViewBag.location = location;
            ViewBag.station = station;
            ViewBag.fontSize = fontSize;
            ViewBag.b_ciblings = b_ciblings;
            ViewBag.b_preventive = b_preventive;
            ViewBag.page = page; 

            return View();
        }


        //get workorders for a specific location. can search by station or by location.
        /*http://localhost:64061/Maximo/workorders?location=53100R01
         *http://localhost:64061/Maximo/workorders?location=53100R01&station=A%20STN53100
         *http://localhost:64061/Maximo/workorders?location=*&station=A%20STN53100
         *http://localhost:64061/Maximo/workorders?location=*&station=*
         */
        [HttpGet]
        public ActionResult workordersOnLocation(string location, string station, bool b_ciblings = false, bool b_preventive = false, int fontSize = 12)
        {
            //pas info for fontsize
            ViewBag.fontSize = fontSize;
            //build qyr
            StringBuilder sbqry = new StringBuilder();
            sbqry.Append(@"
                SELECT 
                 WORKORDER.WONUM WONUM
                ,WORKORDER.STATUS
                ,WORKORDER.STATUSDATE
                ,WORKORDER.WORKTYPE
                ,WORKORDER.DESCRIPTION
                ,WORKORDER.LOCATION
                ,WORKORDER.REPORTEDBY
                ,WORKORDER.REPORTDATE
                ,locancestor.ANCESTOR
                from MAXIMO.WORKORDER WORKORDER
                join MAXIMO.locancestor locancestor on 
                locancestor.LOCATION = WORKORDER.LOCATION
                AND locancestor.ORGID = 'VCCBE'");

            string targetlocation;
            //if no location passed by tableau try use station
            if (location == "*")
            {
                //if no station passed by tableau trow error
                if (station == "*")
                {
                    //trow error
                    ViewBag.error = "No valid target found";
                    return View(new List<Workorder>());
                }
                else
                {
                    targetlocation = station;
                }
            }
            else //default
            {
                targetlocation = location;
            }

            sbqry.Append(string.Format(@" 
                    AND locancestor.ANCESTOR = (
                    select ancestor from(select locancestor.ancestor
                    from maximo.locancestor where locancestor.location like '{0}'
                    and locancestor.ORGID = 'VCCBE'
                    and locancestor.location <> locancestor.ancestor
                    order by locancestor.LOCANCESTORID)
                    where rownum = 1)", targetlocation));

            //handle ciblings
            if (!b_ciblings)
            {
                sbqry.Append(string.Format(" AND locancestor.LOCATION = '{0}'", targetlocation));
            }

            //handle preventive
            if (b_preventive == false)
            {
                sbqry.Append(" AND WORKTYPE not in ('PP','PCI','WSCH')");
            }
            sbqry.Append(" ORDER BY WORKORDER.STATUSDATE DESC");


            string test = sbqry.ToString();

            //get data from maximo
            EQUICommunictionLib.MaximoComm maximoComm = new MaximoComm();
            DataTable dataTable = new DataTable();
            dataTable = maximoComm.oracle_runQuery(sbqry.ToString());
            //parse datatable to listobject
            List<Workorder> workorders = new List<Workorder>();
            foreach (DataRow row in dataTable.Rows)
            {
                Workorder workorder = new Workorder();
                workorder.WONUM = row.Field<string>("WONUM");
                workorder.STATUS = row.Field<string>("STATUS");
                workorder.STATUSDATE = row.Field<DateTime>("STATUSDATE");
                workorder.WORKTYPE = row.Field<string>("WORKTYPE");
                workorder.DESCRIPTION = row.Field<string>("DESCRIPTION");
                workorder.LOCATION = row.Field<string>("LOCATION");
                workorder.REPORTEDBY = row.Field<string>("REPORTEDBY");
                workorder.REPORTDATE = row.Field<DateTime>("REPORTDATE");
                workorder.ANCESTOR = row.Field<string>("ANCESTOR");
                //
                workorders.Add(workorder);
            }

            return View(workorders);
        }

        //get workorder details (long text, failure, labor)
        [HttpGet]
        public ActionResult WoDetails(string wonum)
        {
            if (wonum == null) wonum = "NoWonum";
            if (wonum == "") wonum = "NoWonum";
            #region querys
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
            string LONGDESCRIPTION = maximoComm.GetClobMaximo7(cmdLONGDESCRIPTION);
            string FAILUREREMARK = maximoComm.GetClobMaximo7(cmdFAILUREREMARK);
            DataTable LABOR = maximoComm.oracle_runQuery(cmdLabor);
            //   DataTable WORKLOG = maximoComm.oracle_runQuery(cmdWorkLog);

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
            return View(LABORmodel);
        }
    }
}
