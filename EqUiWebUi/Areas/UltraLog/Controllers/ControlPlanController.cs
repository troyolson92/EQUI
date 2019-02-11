using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.UltraLog.Controllers
{
    public class ControlPlanController : Controller
    {

        Models.UltraLogEntities db = new Models.UltraLogEntities();
        // GET: Welding/Ultralog
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get list of all control plans.
        /// </summary>
        /// <returns></returns>
        public ActionResult ListControlPlans()
        {
            List<string> plans = db.Database.SqlQuery<string>("SELECT [T_PlansList].[Name] FROM [UL].[T_PlansList]").ToList();
            return View(plans);
        }

        /// <summary>
        /// Make a control plan to check a single robot or timer
        /// </summary>
        /// <param name="Timername"></param>
        /// <returns></returns>
        public ActionResult MakeControlPlan(string Timername)
        {
            string qry = @"
                 SELECT
                 [T_PlansList].[Name]
                ,T_PlanPoints.PlanID
                ,T_PointsList.[Name] as 'SpotName'
                ,T_PointsList.[Sequence]
                ,T_PointsList.Diameter
                ,c_timer.[Name] as 'Timername'
                FROM[UL].[T_PlansList]
                Left join UL.T_PlanPoints on T_PlanPoints.PlanID = T_PlansList.PlanID
                Left join UL.T_PointsList on T_PointsList.PointID = T_PlanPoints.PointID
                Left join WELDING2.rt_spottable on rt_spottable.SpotName = T_PointsList.[Name] and rt_spottable.isDead = 0
                Left join WELDING2.c_timer on c_timer.id = rt_spottable.[timerId]
                WHERE T_PlansList.[Name] = 'V316_331060_LHD'";
            return View(qry);
        }

        /// <summary>
        /// Get control plan and run to all the control plan pictures
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetControlPlan(string PlanName = "V316_331060_LHD")
        {
            ViewBag.PlanName = PlanName;
            string qry = @"SELECT distinct T_Picture.PictureID
                            FROM [UL].[T_PlansList]
                            Left join UL.T_PlanPoints on T_PlanPoints.PlanID = T_PlansList.PlanID
                            Left join UL.T_PicturePoints on T_PicturePoints.PlanPointID = T_PlanPoints.PlanPointID
                            Left join UL.T_Picture on T_Picture.PictureID = T_PicturePoints.PictureID
                            where T_Picture.PictureID is not null AND T_PlansList.[Name] = '{0}'
                            order by T_Picture.PictureID asc";
            ViewBag.PictureList = db.Database.SqlQuery<int>(string.Format(qry, PlanName)).ToList();
            return View();
        }

        /// <summary>
        /// Test view to render single control plan picture
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetControlPlanPicture(int Id = 3477)
        {
            ViewBag.ControlPlanPicture = Id;
            return View();
        }

        /// <summary>
        /// Partial view to render control plan picture
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult _GetControlPlanPicture(int Id)
        {
            ViewBag.ControlPlanPicture = Id;
            return PartialView();
        }

        /// <summary>
        /// Json request to get control plan picture data
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult _GetControlPlanPictureData(int Id)
        {
            try { 
                //make test data set 
                Models.UltralogControlPicture ultralogControlPicture = new Models.UltralogControlPicture();
                ultralogControlPicture.Id = Id;
                ultralogControlPicture.Picture = db.Database.SqlQuery<string>($"select(select Picture as '*' for xml path('')) from UL.T_Picture where PictureID = {Id}").First(); ;
                ultralogControlPicture.picturePoints = db.Database.SqlQuery<Models.PicturePoint>($"select PictureID as 'ID' , PlanPointID, Xpos, Ypos from UL.T_PicturePoints where PictureID = {Id}").ToList();

                //to order we have col sequenece in t_planpoints 

                //t_nodelist uploaden voor boom van plannen.

                //moet verschillende ultralog databases kunnen uploaden. dus in elke tabel een extra collom met db ID 


                //get image size
                byte[] image = Convert.FromBase64String(ultralogControlPicture.Picture);
                using (var ms = new MemoryStream(image))
                {
                    Image img = Image.FromStream(ms);
                    ultralogControlPicture.Height = img.Height;
                    ultralogControlPicture.Width = img.Width;
                }

                //manipulate data in way chartjs likes
                object ValueData = from e in ultralogControlPicture.picturePoints
                                    select new
                                    {
                                        x = e.Xpos,
                                        y = (e.Ypos * -1) + (ultralogControlPicture.Height * 15), //flip Y axis and move Y axis origin from top left to bottom left 
                                    };

                //combine all data in 1 list object
                List<object> data = new List<object>();
                data.Add(ultralogControlPicture);
                data.Add(ValueData);
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.StatusDescription = ex.Message;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}