using EqUiWebUi.WebGridHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;
using EqUiWebUi.Areas.Alert.Models;
using System.Text;

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
            ConnectionManager connectionManager = new ConnectionManager();
            string qry = @"select top 100 id, controller_name from gadata.c3g.c_controller";
            dt = connectionManager.RunQuery(qry, enblExeptions: true);

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
            ConnectionManager connectionManager = new ConnectionManager();
            string qry = @"select top 100 id, controller_name from gadata.c3g.c_controller";
            dt = connectionManager.RunQuery(qry, enblExeptions: true);
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

        //return partial based on a query run against a database.
        public ActionResult _dynamicWebgridRunQueryAgainstDB(string qry, int db = 0)
        {
            DataTable dt = new DataTable();

            //run against database.
            ConnectionManager connectionManager = new ConnectionManager();
            //run command against selected database.
            dt = connectionManager.RunQuery(qry, dbID: db, enblExeptions: true);
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

        //runs an sql statement and check if it returns a certain collection of colums. (for alert trigger statement checks)
        [HttpGet]
        public ActionResult _CheckStatement(string qry, List<string> mandatoryColumns, int db = 0)
        {
            DataTable dt = new DataTable();
            //run against database.
            ConnectionManager connectionManager = new ConnectionManager();
            //run command against selected database.
            dt = connectionManager.RunQuery(qry, dbID: db, enblExeptions: true);

            StringBuilder sb = new StringBuilder();
            foreach (string mandatoryColumn in mandatoryColumns)
            {
                if(dt.Columns.Contains(mandatoryColumn)) //OK
                {
                    sb.AppendLine("<hr />");
                    sb.AppendLine("<div class='alert alert-success'>");
                    sb.AppendLine("<strong>mandatoryColumn: " + mandatoryColumn + " OK</strong>");
                    sb.AppendLine("<div>datatype" + dt.Columns[mandatoryColumn].DataType.Name.ToString() + "</div>");
                    sb.AppendLine("</div>");
                }
                else //NOK
                {
                    sb.AppendLine("<hr />");
                    sb.AppendLine("<div class='alert alert-danger'>");
                    sb.AppendLine("<strong>mandatoryColumn: " + mandatoryColumn + " NOK</strong>");
                    sb.AppendLine("<div>This column is missing!</div>");
                    sb.AppendLine("</div>");
                }
            }

            ViewBag.mandatoryColumnsResult = sb.ToString();

            return PartialView();
        }
    }
}