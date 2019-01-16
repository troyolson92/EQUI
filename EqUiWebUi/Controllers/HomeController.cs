using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            //pass some site information 
            ViewBag.hostname = System.Environment.MachineName;

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ToString());
            ViewBag.sqlserver = $"{builder.DataSource}/{builder.InitialCatalog}"; 

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            ViewBag.build = version.Build;

            return View();
        }

        //render an external site in our content (embedded)
        public ActionResult Rendersite(string url)
        {
            ViewBag.redirectURL = url;
            return View();
        }

        //show settings page
        public ActionResult Settings()
        {
            return View();
        }

        //this page is a redirect page for when i want to close a window
        public ActionResult Close()
        {
            return View();
        }

        //html test page
        public ActionResult TestPage()
        {
            return View();
        }

    }
}