using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Management;
using System.Net;
using System.ServiceProcess;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.VWSC.Models;

namespace EqUiWebUi.Areas.VWSC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class c_service_setupController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private GADATAEntitiesVWSC db = new GADATAEntitiesVWSC();

        // GET: VWSC/c_service_setup
        //show a list of session configured join the controller enable mask.
        public ActionResult Index()
        {
            List<VWSC_c_service_setup> sessions = db.c_service_setup.Where(c => c.name == "SESSION_NAME").ToList();
            List<Models.winService> servicesOnServer = GetServices();
            List<Models.winService> result = new List<winService>();
            foreach (VWSC_c_service_setup session in sessions)
            {
                //check if session is of master type.
                if(db.c_service_setup.Where(c => c.name == "SESSION_TYPE" && c.value == 1.ToString() && c.bit_id == session.bit_id).Count() != 0)
                {
                    Models.winService service = servicesOnServer.Where(s => s.ServiceName.Contains(session.value)).FirstOrDefault();
                    if (service == null)
                    {
                        log.Warn("vwsc MASTER session: " + session.name + " not found on server");
                        service = new winService();
                        service.ServiceName = "NOT FOUND";
                        service.ServiceDescription = "no service found named " + session.value;
                    }
                    service._SessionType = VWSCenums.VWSCSessionType.Master;
                    service.id = session.id;
                    service.bit_id = session.bit_id;
                    service.SessionName = session.value;
                    service.description = session.description;
                    result.Add(service);
                }
                else
                {
                    log.Info($"session= {session.name} is not a master type session");
                    log.Warn("vwsc SLAVE session: " + session.name + " not found on server");
                    winService service = new winService();
                    service.ServiceName = "Slave runs on the npt!";
                    service.ServiceDescription = "";
                    service._SessionType = VWSCenums.VWSCSessionType.Slave;
                    service.id = session.id;
                    service.bit_id = session.bit_id;
                    service.SessionName = session.value;
                    service.description = session.description;
                    result.Add(service);
            }
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
        public JsonResult SetServiceState(string ServiceName, int State)
        {
            //IIS user must have acces to start and stop services!
            //need to test if this works! 
            //https://social.technet.microsoft.com/wiki/contents/articles/5752.how-to-grant-users-rights-to-manage-services-start-stop-etc.aspx
            ServiceController controller = new ServiceController(ServiceName);
            try
            {
                switch (State)
                {
                    case 1:
                        controller.Start();
                        controller.WaitForStatus(ServiceControllerStatus.Running);
                        return Json(new { Msg = "service started: " + ServiceName }, JsonRequestBehavior.AllowGet);

                    case 2:
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped);
                        return Json(new { Msg = "service stopped: " + ServiceName }, JsonRequestBehavior.AllowGet);

                    case 3:
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped);
                        controller.Start();
                        controller.WaitForStatus(ServiceControllerStatus.Running);
                        return Json(new { Msg = "restart OK: " + ServiceName }, JsonRequestBehavior.AllowGet);

                    default:
                        return Json(new { Msg = "state not valid: " + ServiceName }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                log.Error("Failed to set state for: " + ServiceName, ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.StatusDescription = ex.Message;
                return Json(null, JsonRequestBehavior.AllowGet);

            }
        }

        //
        public ActionResult _sessionSetup(long? enable_mask)
        {
            ViewBag.enable_mask = enable_mask;
            return PartialView();
        }

        // GET: VASC/c_service_setup/_sessionSetup
        public ActionResult _sessionSetupGrid(long? enable_mask)
        {
            List<VWSC_c_service_setup> list = new List<VWSC_c_service_setup>();
            if (enable_mask is null)
            {
                list = db.c_service_setup.ToList();
            }
            else
            {
                var setbits = Enumerable.Range(0, 64).Where(x => ((enable_mask >> x) & 1) == 1);

                foreach (int setbit in setbits)
                {
                    list.AddRange(db.c_service_setup.Where(c => c.bit_id == setbit + 1 && c.bit_id != 0).ToList());
                }
                //also add everything with bit_id set to -1 because these are global parameters for all sessions.
                list.AddRange(db.c_service_setup.Where(c => c.bit_id == -1).ToList());
            }
            return PartialView(list);
        }


        // GET: VWSC/c_service_setup/_sessionSetup
        public ActionResult _sessionDetails(string sessionName)
        {
            ViewBag.sessionName = sessionName;
            return PartialView();
        }

        // GET: VWSC/c_service_setup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VWSC_c_service_setup c_service_setup = db.c_service_setup.Find(id);
            if (c_service_setup == null)
            {
                return HttpNotFound();
            }
            return View(c_service_setup);
        }

        // GET: VWSC/c_service_setup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VWSC/c_service_setup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,bit_id,name,value,description")] VWSC_c_service_setup c_service_setup)
        {
            if (ModelState.IsValid)
            {
                db.c_service_setup.Add(c_service_setup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_service_setup);
        }

        // GET: VWSC/c_service_setup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VWSC_c_service_setup c_service_setup = db.c_service_setup.Find(id);
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
        public ActionResult Edit([Bind(Include = "id,bit_id,name,value,description")] VWSC_c_service_setup c_service_setup)
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
            VWSC_c_service_setup c_service_setup = db.c_service_setup.Find(id);
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
            VWSC_c_service_setup c_service_setup = db.c_service_setup.Find(id);
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