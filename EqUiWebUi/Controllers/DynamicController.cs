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

       

    }
}