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
        public ActionResult ListControlPlans(string DBname = "default")
        {
            List<string> plans = db.Database.SqlQuery<string>($"SELECT [T_PlansList].[Name] FROM [UL].[T_PlansList] where DBname = '{DBname}'").ToList();
            return View(plans);
        }


        /// <summary>
        /// Get control plan and run to all the control plan pictures
        /// </summary>
        /// <param name="PlanName">planname to be renderd</param>
        /// <param name="RobotName">if RobotName is set build a plan for that robot</param>
        /// <param name="DBname">name of ultralog database to use</param>
        /// <returns></returns>
        public ActionResult GetControlPlan(string PlanName = "", string RobotName = "", string DBname = "default")
        {
            if (PlanName!= "" && RobotName == "") //default get plan by planname
            {
                string qry = $@"SELECT distinct T_Picture.PictureID
                            FROM [UL].[T_PlansList]
                            Left join UL.T_PlanPoints on T_PlanPoints.PlanID = T_PlansList.PlanID  and T_PlanPoints.DBname = T_PlansList.DBname
                            Left join UL.T_PicturePoints on T_PicturePoints.PlanPointID = T_PlanPoints.PlanPointID and T_PicturePoints.DBname = T_PlanPoints.DBname
                            Left join UL.T_Picture on T_Picture.PictureID = T_PicturePoints.PictureID and T_Picture.DBname = T_PicturePoints.DBname
                            where T_Picture.PictureID is not null AND T_PlansList.[Name] = '{PlanName}' and T_PlansList.Dbname = '{DBname}'
                            order by T_Picture.PictureID asc";
                ViewBag.PictureList = db.Database.SqlQuery<int>(qry).ToList();
            }
            else if (PlanName == "" && RobotName != "") //build a plan by robotname (use PJV)
            {
                string qry = $@"SELECT distinct T_Picture.PictureID
			                --Ul part
                            From Ul.T_PointsList
                            Left join UL.T_PlanPoints on T_PlanPoints.PlanPointID = T_PointsList.PointID  and T_PlanPoints.DBname = T_PointsList.DBname
                            Left join UL.T_PicturePoints on T_PicturePoints.PlanPointID = T_PlanPoints.PlanPointID and T_PicturePoints.DBname = T_PlanPoints.DBname
                            Left join UL.T_Picture on T_Picture.PictureID = T_PicturePoints.PictureID and T_Picture.DBname = T_PicturePoints.DBname
			                --pjv part 
			                left join pjv.RobotPoint on RobotPoint.processValue1 = T_PointsList.[Name]
			                left join pjv.RobotRoutine on RobotRoutine.id = RobotPoint.robotRoutineId
			                left join pjv.RobotProgram on RobotProgram.id = RobotRoutine.RobotProgramId
			                left join pjv.RobotDetail on RobotDetail.id = RobotProgram.robotDetailId

                            where T_Picture.PictureID is not null and T_PointsList.Dbname = '{DBname}'
			                and RobotDetail.[name] = '{RobotName}'
			                and RobotPoint.isDead = 0 --only active points
                            order by T_Picture.PictureID asc";
                ViewBag.PictureList = db.Database.SqlQuery<int>(qry).ToList();
            }
            else 
            {
                throw new Exception("invalid input parameters");
            }

            ViewBag.PlanName = PlanName;
            ViewBag.RobotName = RobotName;
            return View();
        }

        /// <summary>
        /// Test view to render single control plan picture
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Spotname">get picture by a specific spotname</param>
        /// <returns></returns>
        public ActionResult GetControlPlanPicture(int? Id, string Spotname)
        {
            ViewBag.ControlPlanPicture = Id;
            ViewBag.Spotname = Spotname;
            return View();
        }

        /// <summary>
        /// Partial view to render control plan picture
        /// </summary>
        /// <param name="Id">id of picture</param>
        /// <param name="Spotname">get picture by a specific spotname</param>
        /// <param name="RobotName">if RobotName is set build a plan for that robot</param>
        /// <returns></returns>
        public ActionResult _GetControlPlanPicture(int? Id, string Spotname, string RobotName)
        {
            ViewBag.ControlPlanPicture = Id;
            ViewBag.Spotname = Spotname;
            ViewBag.RobotName = RobotName;
            return PartialView();
        }

        /// <summary>
        /// Json request to get control plan picture data
        /// </summary>
        /// <param name="Id">id of picture</param>
        /// <param name="Spotname">get picture by a specific spotname</param>
        /// <param name="RobotName">if RobotName is set build a plan for that robot</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult _GetControlPlanPictureData(int? Id, string Spotname, string RobotName, string DBname = "default")
        {
            try { 
                Models.UltralogControlPicture ultralogControlPicture = new Models.UltralogControlPicture();


                if (Id.HasValue && Spotname == "") //default mode get picture by id
                {
                    ultralogControlPicture.Id = Id.GetValueOrDefault();
                }
                else if (!Id.HasValue && Spotname != "" && RobotName == "") //get picture id for specific spotname
                {
                    string qry = $@"
                        select distinct T_PicturePoints.PictureID from ul.T_PointsList 
                        left join ul.T_PlanPoints on T_PlanPoints.PointID = T_PointsList.PointID and T_PlanPoints.DBname = T_PointsList.DBname
                        left join UL.T_PicturePoints on T_PicturePoints.PlanPointID = T_PlanPoints.PlanPointID and T_PicturePoints.DBname = T_PlanPoints.DBname
                        where T_PointsList.[name] = '{Spotname.Trim()}' and T_PointsList.DBname = '{DBname}'";
                    ultralogControlPicture.Id = db.Database.SqlQuery<int>(qry).First();
                }
                else 
                {
                    throw new Exception("No valid input parameters");
                }

                ultralogControlPicture.Picture = db.Database.SqlQuery<string>($@"select(select Picture as '*' for xml path('')) 
                                                                                    from UL.T_Picture where PictureID = {ultralogControlPicture.Id} and DBname = '{DBname}'").First();

                if (RobotName == "") //default mode get by picture ID and or spotname 
                {
                    ultralogControlPicture.picturePoints = db.Database.SqlQuery<Models.PicturePoint>($@"
                                                                        select 
                                                                              T_PicturePoints.PictureID as 'ID' 
                                                                            , T_PicturePoints.PlanPointID
                                                                            , T_PicturePoints.Xpos
                                                                            , T_PicturePoints.Ypos 
                                                                            from UL.T_PicturePoints
                                                                            left join UL.T_PlanPoints on T_PlanPoints.PlanPointID = T_PicturePoints.PlanPointID  
                                                                            and T_PlanPoints.DBname = T_PicturePoints.DBname
	                                                                        left join ul.T_PointsList on T_PlanPoints.PointID = T_PointsList.PointID
	                                                                        and T_PointsList.DBname = T_PlanPoints.DBname
                                                                            where T_PicturePoints.PictureID = {ultralogControlPicture.Id} and T_PicturePoints.DBname = '{DBname}'
	                                                                        and (T_PointsList.[Name] = '{Spotname}' OR '{Spotname}' = '')
                                                                            order by T_PlanPoints.[Sequence] asc").ToList();
                }
                else //get plan points by robotname use PJV
                {
                    ultralogControlPicture.picturePoints = db.Database.SqlQuery<Models.PicturePoint>($@"
                                             select 
                                                  T_PicturePoints.PictureID as 'ID' 
                                                , T_PicturePoints.PlanPointID
                                                , T_PicturePoints.Xpos
                                                , T_PicturePoints.Ypos 
                                                from UL.T_PicturePoints
                                                left join UL.T_PlanPoints on T_PlanPoints.PlanPointID = T_PicturePoints.PlanPointID  
                                                and T_PlanPoints.DBname = T_PicturePoints.DBname
	                                            left join ul.T_PointsList on T_PlanPoints.PointID = T_PointsList.PointID
	                                            and T_PointsList.DBname = T_PlanPoints.DBname
                                              --pjv part 
	                                            left join pjv.RobotPoint on RobotPoint.processValue1 = T_PointsList.[Name]
	                                            left join pjv.RobotRoutine on RobotRoutine.id = RobotPoint.robotRoutineId
	                                            left join pjv.RobotProgram on RobotProgram.id = RobotRoutine.RobotProgramId
	                                            left join pjv.RobotDetail on RobotDetail.id = RobotProgram.robotDetailId
                                                where T_PicturePoints.PictureID = {ultralogControlPicture.Id} and T_PicturePoints.DBname = '{DBname}'
	                                            and RobotDetail.[name] = '{RobotName}'
                                                order by T_PlanPoints.[Sequence] asc").ToList();
                }
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
                Response.StatusDescription = ex.Message;
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}