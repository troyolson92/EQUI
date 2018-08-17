using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Management;
using System.Net;
using System.ServiceProcess;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.VASC.Models;

namespace EqUiWebUi.Areas.VASC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class c_service_setupController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/c_service_setup
        //show a list of session configured join the controller enable mask.
        public ActionResult Index()
        {
            List<c_service_setup> sessions = db.c_service_setup.Where(c => c.name == "SESSION_NAME").ToList();
            List<Models.winService> servicesOnServer = GetServices();
            List<Models.winService> result = new List<winService>();
            foreach (c_service_setup session in sessions)
            {
                Models.winService service = servicesOnServer.Where(s => s.ServiceName == session.value).FirstOrDefault();
                if (service == null)
                {
                    log.Warn("vasc session: " + session.name + " not found on server");
                    service = new winService();
                    service.ServiceName = "NOT FOUND";
                    service.ServiceDescription = "no service found named " + session.value;
                }
                service.id = session.id;
                service.bit_id = session.bit_id;
                service.SessionName = session.value;
                service.description = session.description;
                result.Add(service);
            }

            return View(result);
        }

        //get list of running services
        public List<Models.winService> GetServices()
        {
            ServiceController[] scServices;
            List<Models.winService> list = new List<Models.winService>();
            try
            {
                scServices = ServiceController.GetServices();

                try
                {

                    foreach (ServiceController scTemp in scServices)
                    {
                            try
                            {
                                Models.winService winService = new Models.winService();
                                winService.ServiceName = scTemp.ServiceName;
                                winService.ServiceDisplayName = scTemp.DisplayName;
                                winService.ServiceStatus = scTemp.Status.ToString() ?? "null";

                                // Query WMI for additional information about this service.
                                ManagementObject wmiService;
                                wmiService = new ManagementObject("Win32_Service.Name='" + scTemp.ServiceName + "'");
                                wmiService.Get();
                                winService.ServiceStartName = wmiService["StartName"].ToString() ?? "null";
                                winService.ServiceDescription = wmiService["Description"].ToString() ?? "null";
                                //
                                list.Add(winService);
                            }
                            catch (Exception ex)
                            {
                                log.Error("Fail to process: " + scTemp.ServiceName, ex);
                            }
                        }

                }
                catch (Exception ex)
                {
                    log.Error("Failed to loop services", ex);
                }
            }
            catch (Exception ex)
            {
                log.Error("Failed to get services", ex);
            }
            return list;
        }

        //change services state
        public void SetServiceState(string ServiceName, int State)
        {
            try
            {



            }
            catch (Exception ex)
            {
                log.Error("ImpresonateError", ex);
            }




            ServiceController controller = new ServiceController(ServiceName);
            try
            {
                switch (State)
                {
                    case 1:
                        controller.Start();
                        controller.WaitForStatus(ServiceControllerStatus.Running);
                        break;

                    case 2:
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped);
                        break;

                    case 3:
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped);
                        controller.Start();
                        controller.WaitForStatus(ServiceControllerStatus.Running);
                        break;

                    default:
                        //not valid state 
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error("Failed to set state for: " + ServiceName, ex);
                throw;
            }


        }

        //
        public ActionResult _sessionSetup(int? enable_mask)
        {
            ViewBag.enable_mask = enable_mask;
            return PartialView();
        }

        // GET: VASC/c_service_setup/_sessionSetup
        public ActionResult _sessionSetupGrid(int? enable_mask)
        {
            List<c_service_setup> list = new List<c_service_setup>();
            if (enable_mask is null)
            {
                list = db.c_service_setup.ToList();
            }
            else
            {
                var setbits = Enumerable.Range(0, 32).Where(x => ((enable_mask >> x) & 1) == 1);

                foreach (int setbit in setbits)
                {
                    list.AddRange(db.c_service_setup.Where(c => c.bit_id == setbit+1 && c.bit_id != 0).ToList());
                }
                //also add everything with bit_id set to -1 because these are global parameters for all sessions.
                list.AddRange(db.c_service_setup.Where(c => c.bit_id == -1).ToList());
            }
            return PartialView(list);
        }


        // GET: VASC/c_service_setup/_sessionSetup
        public ActionResult _sessionDetails(string sessionName)
        {
            ViewBag.sessionName = sessionName;
            return PartialView();
        }

        // GET: VASC/c_service_setup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_service_setup c_service_setup = db.c_service_setup.Find(id);
            if (c_service_setup == null)
            {
                return HttpNotFound();
            }
            return View(c_service_setup);
        }

        // GET: VASC/c_service_setup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VASC/c_service_setup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,bit_id,name,value,description")] c_service_setup c_service_setup)
        {
            if (ModelState.IsValid)
            {
                db.c_service_setup.Add(c_service_setup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_service_setup);
        }

        // GET: VASC/c_service_setup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_service_setup c_service_setup = db.c_service_setup.Find(id);
            if (c_service_setup == null)
            {
                return HttpNotFound();
            }
            return View(c_service_setup);
        }

        // POST: VASC/c_service_setup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,bit_id,name,value,description")] c_service_setup c_service_setup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_service_setup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_service_setup);
        }

        // GET: VASC/c_service_setup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_service_setup c_service_setup = db.c_service_setup.Find(id);
            if (c_service_setup == null)
            {
                return HttpNotFound();
            }
            return View(c_service_setup);
        }

        // POST: VASC/c_service_setup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            c_service_setup c_service_setup = db.c_service_setup.Find(id);
            db.c_service_setup.Remove(c_service_setup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
