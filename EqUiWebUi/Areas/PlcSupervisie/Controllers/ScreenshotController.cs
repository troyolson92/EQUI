using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.PlcSupervisie.Controllers
{
    public class ScreenshotController : Controller
    {
        // GET: PlcSupervisie/Screenshot
        public ActionResult Index()
        {
            return View();
        }

        //display a list of jpg pictures (for ub12)
        //ReloadInterval Set reload time for refrehsing full page
        //AutoCarousel rate to change out pictures if -1 no autorotate
        public ActionResult Carousel(int? ReloadInterval, int AutoCarousel = -1)
        {
            if (ReloadInterval.HasValue)
            {
                Response.AddHeader("Refresh", ReloadInterval.ToString());
            }
            ViewBag.AutoCarousel = AutoCarousel;
            return View();
        }

        public ActionResult CarouselBF(int? ReloadInterval, int AutoCarousel = -1)
        {
            if (ReloadInterval.HasValue)
            {
                Response.AddHeader("Refresh", ReloadInterval.ToString());
            }
            ViewBag.AutoCarousel = AutoCarousel;
            return View();
        }

        //display a single JPG
        public ActionResult ShowJpg(int? ReloadInterval, string url)
        {
            if (ReloadInterval.HasValue)
            {
                Response.AddHeader("Refresh", ReloadInterval.ToString());
            }
            ViewBag.url = url;
            return View();
        }

        public ActionResult Screenshots(string name = "")
        {
            List<PlcSupervisie.Models.PlcScreenshot> data = new List<Models.PlcScreenshot> { 
                //greenfield
                new Models.PlcScreenshot{Name="A LIJN331 / A LIJN34",LocationTree="VCG -> A -> A GA1.0 -> A LIJN 33",url="http://webapps.gen.volvocars.net/applications/eng/equipment/SuperVisieCma/VCG_CMA40_GA3SUPERVNCW01__01_UnderBody_1.Pd_.jpg"},
                new Models.PlcScreenshot{Name="A LIJN336 / A LIJN338 / A LIJN339",LocationTree="VCG -> A -> A GA1.0 -> A LIJN 33",url="http://webapps.gen.volvocars.net/applications/eng/equipment/SuperVisieCma/VCG_CMA40_GA3SUPERVNCW01__02_UnderBody_2.Pd_.jpg"},
                new Models.PlcScreenshot{Name="UB12 BackPanel",LocationTree="VCG -> A -> A GA1.0 -> A LIJN 33",url="http://webapps.gen.volvocars.net/applications/eng/equipment/SuperVisieCma/VCG_CMA40_GA3SUPERVNCW01__03_UnderBody_3.Pd_.jpg"},
                new Models.PlcScreenshot{Name="",LocationTree="",url="http://webapps.gen.volvocars.net/applications/eng/equipment/SuperVisieCma/VCG_CMA40_GA3SUPERVNCW01__04_Conveyors.Pd_.jpg"},
                new Models.PlcScreenshot{Name="A LIJN35*",LocationTree="VCG -> A -> A GA1.0 -> A LIJN 35",url="http://webapps.gen.volvocars.net/applications/eng/equipment/SuperVisieCma/VCG_CMA40_GA3SUPERVNCW01__05_SideLine.Pd_.jpg"},
                new Models.PlcScreenshot{Name="A GA4.0",LocationTree="VCG -> A -> A GA4.0",url="http://webapps.gen.volvocars.net/applications/eng/equipment/SuperVisieCma/VCG_CMA40_GA3SUPERVNCW01__06_SubAssemblies.Pd_.jpg"},
                new Models.PlcScreenshot{Name="A LIJN33*",LocationTree="VCG -> A -> A GA1.0 -> A LIJN 33",url="http://webapps.gen.volvocars.net/applications/eng/equipment/SuperVisieCma/VCG_CMA40_GA3SUPERVNCW01__00_Overview_UnderBody_CMA.Pd_.jpg"},
                new Models.PlcScreenshot{Name="A LIJN939",LocationTree="VCG -> A -> A GA1.0 -> A LIJN 33",url="http://webapps.gen.volvocars.net/applications/eng/equipment/SuperVisieCma/VCG_CMA40_GA3SUPERVNCW01__07_Conveyors.Pd_.jpg"},
                //brownfield
                new Models.PlcScreenshot{Name="p1x Floor",LocationTree="VCG -> A -> AAS -> A FLOOR S",url="http://webapps.gen.volvocars.net/applications/eng/equipment/supervisiep1/Floor.jpg"},
                new Models.PlcScreenshot{Name="p1x Bop",LocationTree="VCG -> A -> AAS",url="http://webapps.gen.volvocars.net/applications/eng/equipment/supervisiep1/Bop.jpg"},
                new Models.PlcScreenshot{Name="p1x Sibo",LocationTree="VCG -> A -> AAS",url="http://webapps.gen.volvocars.net/applications/eng/equipment/supervisiep1/Sibo.jpg"},
                new Models.PlcScreenshot{Name="p1x Framing",LocationTree="VCG -> A -> AAS",url="http://webapps.gen.volvocars.net/applications/eng/equipment/supervisiep1/Framing.jpg" },
                new Models.PlcScreenshot{Name="p1x Hop",LocationTree="VCG -> A -> A ASSEMBLY LINES",url="http://webapps.gen.volvocars.net/applications/eng/equipment/supervisiep1/Hop.jpg" },
                new Models.PlcScreenshot{Name="p1x Hop flex",LocationTree="VCG -> A -> A ASSEMBLY LINES",url="http://webapps.gen.volvocars.net/applications/eng/equipment/supervisiep1/Hopsteering.jpg" },
            };

            if (name != "")
            {
                data = (from d in data
                        where d.Name.Contains(name)
                        select d).ToList();
            }
            else
            {
                //apply user filter
                string LocationRoot = CurrentUser.Getuser.LocationRoot;
                data = (from d in data
                        where d.LocationTree.Contains(LocationRoot)
                        select d).ToList();
            }
            return View(data);
        }
    }
}