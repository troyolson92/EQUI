using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.HanfireArea.Controllers
{
    //helper class 
    public class Job
    {
        [HelpText("give the job a short name")]
        public string name { get; set; }
        [HelpText("command to run in gadata")]
        public string command { get; set; }
        [HelpText("when to run the job")]
        public string cron { get; set; }
    }

    [Authorize(Roles = "Administrator,HangFire")]
    public class JobController : Controller
    {
        // GET: HanfireArea/Job
        public ActionResult Index()
        {
            return View();
        }

        //GEt: hanfireAre/_settings
        public ActionResult _settings()
        {
            return PartialView();
        }

        // GET: hanfireArea/Makejob
        [HttpGet]
        public ActionResult MakeJob()
        {
            return View();
        }

        // POST: hanfireArea/Makejob
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void MakeJob(Job job)
        {
            Jobengine jobengine = new Jobengine();
            jobengine.Makejob(job.name, job.command, job.cron);
            Response.Redirect("~/hangfire/recurring");
        }

        //init background work jobs.
        public void ConfiureBackgroundJobs()
        {
            //old class
            Backgroundwork backgroundwork = new Backgroundwork();
            backgroundwork.configHangfireJobs();
            //gadata jobs 
            Areas.Gadata.BackgroundWork backgroundWorkGADATA = new Areas.Gadata.BackgroundWork();
            backgroundWorkGADATA.configHangfireJobs();

        }

    }
}