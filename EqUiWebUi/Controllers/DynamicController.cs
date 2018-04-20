using EqUiWebUi.WebGridHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;
using EqUiWebUi.Areas.Alert.Models;

namespace EqUiWebUi.Controllers
{
    public class DynamicController : Controller
    {
        // GET: Dynamic
        public ActionResult Index()
        {
            return new HttpNotFoundResult("Woeps there seems to bo nothing here");
        }

        //test to show how to build da datatable without using entityframework
        [HttpGet]
        public ActionResult DynamicWebgrid()
        {
            DataTable dt = new DataTable();

            //get data
            GadataComm gadataComm = new GadataComm();
            string qry = @"select top 100 id, controller_name from gadata.c3g.c_controller";
            dt = gadataComm.RunQueryGadata(qry);

            //
            WebGridHelpers.WebGridHelper webGridHelper = new WebGridHelper();
            ViewBag.Columns = webGridHelper.getDatatabelCollumns(dt);
            //
            List<dynamic> data = webGridHelper.datatableToDynamic(dt);
            //
            DefaultModel model = new WebGridHelpers.DefaultModel();
            model.PageSize = 30;
            //

            if (data != null)
            {
                model.TotalCount = data.Count();
                model.Data = data;
                model.DataTimestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return View(model);
        }

        //test to show how to build da datatable without using entityframework
        [HttpGet]
        public ActionResult DynamicWebgrid2()
        {
            DataTable dt = new DataTable();

            //get data
            GadataComm gadataComm = new GadataComm();
            string qry = @"select top 100 id, controller_name from gadata.c3g.c_controller";
            dt = gadataComm.RunQueryGadata(qry);

            //
            WebGridHelpers.WebGridHelper webGridHelper = new WebGridHelper();
            ViewBag.Columns = webGridHelper.getDatatabelCollumns(dt);
            //
            List<dynamic> data = webGridHelper.datatableToDynamic(dt);





            //
            DefaultModel model = new WebGridHelpers.DefaultModel();
            model.PageSize = 30;
            //

            if (data != null)
            {
                model.TotalCount = data.Count();
                model.Data = data;
                model.DataTimestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return View(model);
        }

        //return partion based on a query run against a database.
        [HttpGet]
        public ActionResult _dynamicWebgridRunQueryAgainstDB(string qry, int db = 0)
        {
            DataTable dt = new DataTable();

            //run against database.
            StoComm stoComm = new StoComm();
            MaximoComm maximoComm = new MaximoComm();
            GadataComm gadataComm = new GadataComm();
            //run command against selected database.

            switch (db)
            {
                case (int)SmsDatabases.GADATA:
                    dt = gadataComm.RunQueryGadata(qry, enblExeptions: true);
                    break;

                case (int)SmsDatabases.STO:
                    dt = stoComm.oracle_runQuery(qry); //exeptions enabled by default
                    break;

                case (int)SmsDatabases.MAXIMOrt:
                    dt = maximoComm.Oracle_runQuery(qry, RealtimeConn: true, enblExeptions: true);
                    break;

                case (int)SmsDatabases.MAXIMOrep:
                    dt = maximoComm.Oracle_runQuery(qry, RealtimeConn: false, enblExeptions: true);
                    break;

                default:
                    throw new System.ArgumentException("Database not defined", "Alertengine");
            }

            //
            WebGridHelpers.WebGridHelper webGridHelper = new WebGridHelper();
            ViewBag.Columns = webGridHelper.getDatatabelCollumns(dt);
            //
            List<dynamic> data = webGridHelper.datatableToDynamic(dt);
            //
            DefaultModel model = new WebGridHelpers.DefaultModel();
            model.PageSize = 100;
            //

            if (data != null)
            {
                model.TotalCount = data.Count();
                model.Data = data;
            }
            else
            {
                return new HttpNotFoundResult("Woeps there seems to bo nothing here");
            }
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult DynamicDatatable(DataTable dataTable)
        {
            //
            WebGridHelpers.WebGridHelper webGridHelper = new WebGridHelper();
            ViewBag.Columns = webGridHelper.getDatatabelCollumns(dataTable);
            //
            List<dynamic> data = webGridHelper.datatableToDynamic(dataTable);
            //
            DefaultModel model = new WebGridHelpers.DefaultModel();
            model.PageSize = 5;
            //
            model.Data = data;
            //
            return View(model);
        }
    }
}