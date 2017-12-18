using EQUICommunictionLib;
using EqUiWebUi.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    public class DifftableController : Controller
    {
        //index for controller
        [HttpGet]
        public ActionResult Index()
        {
            DiffQuery diffQuery = new DiffQuery();
            return View(diffQuery);
        }

        //called by html form commit
        [HttpPost]
        public ActionResult Index(DiffQuery diff_Query)
        {
            if(string.IsNullOrEmpty(diff_Query.SelectedQuery))
            {

            }
            return RedirectToAction("_DiffWebgrid", new
            {
                id1 = diff_Query.SelectedSnapshotID1,
                id2 = diff_Query.SelectedSnapshotID2
            });
        }

        //called by cascading listbox 
        public JsonResult getSnapshots(int id)
        {
            DiffQuery diffQuery = new DiffQuery();
            IEnumerable<SelectListItem> snapshots = diffQuery.h_querySnapshots.Where(h => h.queryId == id).ToList()
                .Select(x => new SelectListItem {
                    Value = x.id.ToString(),
                    Text = x.timestamp.ToString()
                                                });
            return Json(new SelectList(snapshots, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        //show diggerance between 2 views
        [HttpGet]
        public ActionResult _diffWebgrid(int id1, int id2)
        {
            GadataComm gadataComm = new GadataComm();

            string cmd = string.Format(
@"SELECT * FROM (SELECT TOP 1 [id],[htmlresult] FROM [GADATA].[EqUi].[L_querySnapshots] WHERE snapshotid = {0} order by id desc ) as x 
  UNION 
  SELECT * FROM (SELECT TOP 1 [id],[htmlresult] FROM [GADATA].[EqUi].[L_querySnapshots] WHERE snapshotid = {1} order by id desc ) as x "
, id1, id2);
            DataTable dt = gadataComm.RunQueryGadata(cmd);
            //
            HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(dt.Rows[0].Field<string>("htmlresult"), dt.Rows[1].Field<string>("htmlresult"));
            diffHelper.AddBlockExpression(new Regex(@"[0-9]:[0-9]{1,2}:[0-9]{1,2}", RegexOptions.IgnoreCase)); //time
            diffHelper.AddBlockExpression(new Regex(@"[0-9]/[0-9]{1,2}/20[0-9]{1,2}", RegexOptions.IgnoreCase)); //date
            string diffOutput = diffHelper.Build();
            //
            ViewBag.myData = diffOutput;
            return View();
        }

        public static string FormatHTMLTable(string input)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<html>");
            builder.Append(@"<style>
                            ins { 
                                background-color: #cfc; 
                                text-decoration: none; } 
                            del { 
                                color: #999; 
                                background-color:#FEC8C8; } 
                            table {
                                border-collapse: separate;
                                width: 100%;
                                border: 2px solid black;
                                table-layout: auto;
                                white-space:nowrap;}
                             tr, td { width: 100%;
                                    border: 1px solid black
}
                            table > thead > tr > th { width: auto; }
                            </style>");
            builder.Append("<body>");
            builder.Append(input);
            builder.Append("</body>");
            builder.Append("</html>");
            return builder.ToString();
        }
    }
}