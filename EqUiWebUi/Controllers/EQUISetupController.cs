using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;

namespace EqUiWebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class EQUISetupController : Controller
    {
        // GET: EQUISetup
        public ActionResult Index()
        {
            return View();
        }

        // Get partial settings section.
        public ActionResult _settings()
        {

            return PartialView();
        }

        // Test all configured database.
        public void RunDbTest()
        {
            ConnectionManager connectionManager = new ConnectionManager();
            connectionManager.TestAllDb();
        }

        //Test change password
        public void ChangePWTest()
        {
            ConnectionManager connectionManager = new ConnectionManager();
            connectionManager.PWCheck(dbName: "MAXIMOrt", ChangeIfExpired: true, newPW: "NEWPWTEST");
        }
    }
}