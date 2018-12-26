using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.HangfireArea.Controllers
{
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

        //init background work jobs.
        public void ConfiureBackgroundJobs()
        {
            //maximo jobs
            Areas.Maximo_ui.Backgroundwork backgroundworkMaximo = new Areas.Maximo_ui.Backgroundwork();
            backgroundworkMaximo.configHangfireJobs();
            //gadata jobs 
            Areas.Supervision.BackgroundWork backgroundWorkGADATA = new Areas.Supervision.BackgroundWork();
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
            //hangfire jobs
            Areas.HangfireArea.Jobengine Jobengine = new Jobengine();
            Jobengine.configure_schedules();
            //cylcetime
            Areas.CycleTime.BackgroundWork backgroundWorkCyletime = new CycleTime.BackgroundWork();
            backgroundWorkCyletime.configHangfireJobs();
        }

    }
}