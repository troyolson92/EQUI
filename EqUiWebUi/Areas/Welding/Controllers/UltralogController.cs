using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Welding.Controllers
{
    public class UltralogController : Controller
    {

        Models.GADATAEntitiesWelding db = new Models.GADATAEntitiesWelding();

        // GET: Welding/Ultralog
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get control plan and run to all the control plan pictures
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetControlPlan(int Id = 659)
        {
            ViewBag.PlanId = Id;
            ViewBag.PlanName = "V316_331060_LHD";
            ViewBag.PictureList = new List<int> {
                 3477
                ,3478
                ,3479
                ,3480
                ,3481
                ,3482
                ,3483
                ,3484
                ,3485
                ,3486
                ,3487 };
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