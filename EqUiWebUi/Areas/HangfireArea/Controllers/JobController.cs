using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.HangfireArea.Controllers
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
        [HelpText("Max time to run the job (second)")]
        [Range(10, 60*10)]
        public int maxExectime { get; set; }
        [HelpText("Max # of retry on fail")]
        [Range(0, 5)]
        public int maxRetry { get; set; }
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
            jobengine.Makejob(job.name, job.command, job.cron, job.maxExectime, job.maxRetry);
            Response.Redirect("~/hangfire/recurring");
        }

        //init background work jobs.
        public void ConfiureBackgroundJobs()
        {
            //maximo jobs
            Areas.Maximo_ui.Backgroundwork backgroundworkMaximo = new Areas.Maximo_ui.Backgroundwork();
            backgroundworkMaximo.configHangfireJobs();
            //gadata jobs 
            Areas.Gadata.BackgroundWork backgroundWorkGADATA = new Areas.Gadata.BackgroundWork();
            backgroundWorkGADATA.configHangfireJobs();
            //tiplife jobs 
            Areas.Tiplife.Backgroundwork backgroundworkTiplife = new Tiplife.Backgroundwork();
            backgroundworkTiplife.configHangfireJobs();
            //Stw040 jobs
            Areas.STW040.BackgroundWork backgroundWorkSTW040 = new STW040.BackgroundWork();
            backgroundWorkSTW040.configHangfireJobs();
            //STO jobs
            Areas.PlcSupervisie.Backgroundwork backgroundWorkSTO = new PlcSupervisie.Backgroundwork();
            backgroundWorkSTO.configHangfireJobs();

        }

    }
}