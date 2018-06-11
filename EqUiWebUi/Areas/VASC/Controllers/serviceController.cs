using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceProcess;
using System.Management;
using System.Diagnostics;

namespace EqUiWebUi.Areas.VASC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class serviceController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: VASC/service
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetServices()
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
                        if (scTemp.ServiceName.Contains("Remote"))
                        {
                            Debug.WriteLine("hit");
                        }

                        if (scTemp.ServiceName.ToUpper().Contains("VASC") || scTemp.ServiceName.ToUpper().Contains("ABB") || scTemp.ServiceName.ToUpper().Contains("VCSC"))
                        {
                            try
                            {
                                Models.winService winService = new Models.winService();
                                winService.ServiceName = scTemp.ServiceName;
                                winService.DisplayName = scTemp.DisplayName;
                                winService.Status = scTemp.Status.ToString() ?? "null";

                                // Query WMI for additional information about this service.
                                ManagementObject wmiService;
                                wmiService = new ManagementObject("Win32_Service.Name='" + scTemp.ServiceName + "'");
                                wmiService.Get();
                                winService.StartName = wmiService["StartName"].ToString() ?? "null";
                                winService.Description = wmiService["Description"].ToString() ?? "null";
                                //
                                list.Add(winService);
                            }
                            catch (Exception ex)
                            {
                                log.Error("Fail to process: " + scTemp.ServiceName, ex);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Failed to loop services",ex);
                }
            }
            catch (Exception ex)
            {
                log.Error("Failed to get services", ex);
            }

            return View(list);
        }


        public void SetServiceState(string ServiceName, int State)
        {
            try
            {
        


            }
            catch (Exception ex )
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
            catch(Exception ex)
            {
                log.Error("Failed to set state for: " + ServiceName, ex);
                throw;
            }


        }
    }
}