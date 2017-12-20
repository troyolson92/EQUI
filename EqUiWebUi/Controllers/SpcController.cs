using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Models;
using System.Text;

namespace EqUiWebUi.Controllers
{
    public class SpcController : Controller
    {
        GADATAEntities gADATAEntities = new GADATAEntities();

        // GET: Spc index
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: alerts
        [HttpGet]
        public ActionResult GetAlerts()
        {
            List<ia_Alert> data = (from ia_Alert in gADATAEntities.ia_Alert
                                   select ia_Alert
                                              ).ToList();
            return View(data);
        }

        // GET: alert details Spc/Details/5
        [HttpGet]
        public ActionResult _details(int? id)
        {
            ia_Alert alert = (from ia_Alert in gADATAEntities.ia_Alert
                              where ia_Alert.id == id
                              select ia_Alert
                                              ).ToList().First();
            return PartialView(alert);
        }

        // POST: Add comment to alert 
        [HttpPost]
        public object _addComment(ia_Alert alert, string newComment = "default")
        {
            StringBuilder sb = new StringBuilder();
            //add original
            sb.Append(alert.User_Comment).AppendLine();
            //add new 
            sb.Append(User.Identity.Name).Append("  @").AppendLine(System.DateTime.Now.ToString()).AppendLine("----------------------------------------");
            sb.AppendLine(newComment);
            //set alert and save 
            alert.User_Comment = sb.ToString();
            gADATAEntities.SaveChanges();

            return PartialView("_details", alert);
        }
        
        // GET: alert status 
        [HttpGet]
        public ActionResult _setState(int? id, int? newstate)
        {
            ia_Alert alert = (from ia_Alert in gADATAEntities.ia_Alert
                              where ia_Alert.id == id
                              select ia_Alert
                                              ).ToList().First();

            //do some stuff based on the new state request
            switch (newstate)
            {
                case 1: //OKREQ
                    if (alert.AlertStatus == 0)
                    {
                        alert.Accept_timestamp = System.DateTime.Now;
                        alert.Accept_user = User.Identity.Name;
                        alert.AlertStatus = 1;
                    }
                    break;

                case 2: //COMP
                    if (alert.AlertStatus >= 1)
                    {
                        if (alert.Fix_timestamp is null)
                        {
                            alert.Fix_timestamp = System.DateTime.Now;
                            alert.Fix_user = User.Identity.Name;
                        }
                        alert.AlertStatus = 2;
                    }

                    break;

                case 3: //VOID
                    if (alert.AlertStatus >= 1)
                    {
                        if (alert.Close_timestamp is null)
                        {
                            alert.Close_timestamp = System.DateTime.Now;
                            alert.Close_user = User.Identity.Name;
                        }
                        alert.AlertStatus = 3;
                    }

                    break;
                default:
                        //invalid state
                    break;
            }
            //commit the data   
            gADATAEntities.SaveChanges();
            //
            return PartialView(alert);
        }
    }
}

