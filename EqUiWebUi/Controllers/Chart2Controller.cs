using EQUICommunictionLib;
using EqUiWebUi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace EqUiWebUi.Controllers
{
    public class Chart2Controller : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getLineChartData(string timestampCol, string[] datacolls)
        {
            List<object> iData = new List<object>();
            List<string> labels = new List<string>();
            //First get distinct Month Name for select Year.
            string query1 = @"
select top 100
controller_name
,[Date Time]
,Dress_Num
,Wear_Fixed
,Wear_Move
 from GADATA.NGAC.TipDressLogFile where controller_name like '338030r01' and [Date Time] between GETDATE()-10 and GETDATE()";

            ConnectionManager connectionManager = new ConnectionManager();

            DataTable dt = connectionManager.RunQuery(query1);
            foreach (DataRow drow in dt.Rows)
            {
                labels.Add(drow[timestampCol].ToString());
            }
            iData.Add(labels);


            foreach(string datacol in datacolls)
            {
                List<double> lst_dataItem_x = new List<double>();
                foreach (DataRow dr in dt.Rows)
                {
                    lst_dataItem_x.Add(Convert.ToDouble(dr[datacol].ToString()));
                }
                iData.Add(lst_dataItem_x);
            }

            return Json(iData, JsonRequestBehavior.AllowGet);
        }



    }
}