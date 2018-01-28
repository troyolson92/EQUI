using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.user_management.Models;

namespace EqUiWebUi.Areas.user_management.Controllers
{
    public class UserScreensController : Controller
    {
        private GADATAEntitiesUserManagement db = new GADATAEntitiesUserManagement();

        // GET: user_management/UserScreens
        public async Task<ActionResult> Index()
        {
            var l_Screens = db.L_Screens.Include(l => l.L_users);
            return View(await l_Screens.ToListAsync());
        }

        // GET: user_management/UserScreens/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            L_Screens l_Screens = await db.L_Screens.FindAsync(id);
            if (l_Screens == null)
            {
                return HttpNotFound();
            }
            return View(l_Screens);
        }

        // GET: user_management/UserScreens/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.User_id = new SelectList(db.L_users, "id", "username");
            return View();
        }

        // POST: user_management/UserScreens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Create([Bind(Include = "id,Screen_num,Discription,User_id,ScreenUrl,StartDisplayTime,StopDisplayTime,ResetRate")] L_Screens l_Screens)
        {
            if (ModelState.IsValid)
            {
                db.L_Screens.Add(l_Screens);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.User_id = new SelectList(db.L_users, "id", "username", l_Screens.User_id);
            return View(l_Screens);
        }

        // GET: user_management/UserScreens/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            L_Screens l_Screens = await db.L_Screens.FindAsync(id);
            if (l_Screens == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_id = new SelectList(db.L_users, "id", "username", l_Screens.User_id);
            return View(l_Screens);
        }

        // POST: user_management/UserScreens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit([Bind(Include = "id,Screen_num,Discription,User_id,ScreenUrl,StartDisplayTime,StopDisplayTime,ResetRate")] L_Screens l_Screens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(l_Screens).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.User_id = new SelectList(db.L_users, "id", "username", l_Screens.User_id);
            return View(l_Screens);
        }

        // GET: user_management/UserScreens/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            L_Screens l_Screens = await db.L_Screens.FindAsync(id);
            if (l_Screens == null)
            {
                return HttpNotFound();
            }
            return View(l_Screens);
        }

        // POST: user_management/UserScreens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            L_Screens l_Screens = await db.L_Screens.FindAsync(id);
            db.L_Screens.Remove(l_Screens);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


 //*****************************************************RENDER SCREENS*****************************************************************************


        // GET: Render selected screen in the screen wrapper (single screen)
        public ActionResult RenderUserScreen(int screenID)
        {
            //Get screen
            L_Screens Screen = db.L_Screens.Where(s => s.id == screenID).First();
            if (Screen == null)
            {
                return HttpNotFound("Did not find the screen you where looking for");
            }
            //get render user session
            //Must render screen using the provided user session credentials.
            Areas.user_management.Controllers.userController userController = new Areas.user_management.Controllers.userController();
            users user = userController.GetUser(Screen.L_users.username);
            //change session data to view an url as a specifc user
            Session["LocationRoot"] = user.LocationRoot;
            Session["AssetRoot"] = user.AssetRoot;
            Session["Impersonating"] = user.username;

            //Pass screen lifecycle


            //add refresh rate if needed.
            if (Screen.ResetRate.HasValue)
            {
                Response.AddHeader("Refresh", Screen.ResetRate.ToString());
            }
            //Pass correct layout (empty layout)
            return View("RenderUserScreen", "~/Views/Shared/_MinimalLayout.cshtml", Screen);
        }

        // GET: Render selected screen in the screen wrapper (multible screens MAX =4)
        public ActionResult RenderUserScreens(int? screenID1, int? screenID2, int? screenID3, int? screenID4)
        {
            //Get screen1
            L_Screens Screen1 = db.L_Screens.Where(s => s.id == screenID1).FirstOrDefault();
            if (Screen1 == null)
            {
                Screen1 = new L_Screens();
                Screen1.ScreenUrl = "HTTP://EQUI";
                Screen1.Discription = "Did not find the screen you where looking for";
            }
            //Get screen2
            L_Screens Screen2 = db.L_Screens.Where(s => s.id == screenID2).FirstOrDefault();
            if (Screen2 == null)
            {
                Screen2 = new L_Screens();
                Screen2.ScreenUrl = "HTTP://EQUI";
                Screen2.Discription = "Did not find the screen you where looking for";
            }
            //Get screen3
            L_Screens Screen3 = db.L_Screens.Where(s => s.id == screenID3).FirstOrDefault();
            if (Screen3 == null)
            {
                Screen3 = new L_Screens();
                Screen3.ScreenUrl = "HTTP://EQUI";
                Screen3.Discription = "Did not find the screen you where looking for";
            }
            //Get screen4
            L_Screens Screen4 = db.L_Screens.Where(s => s.id == screenID4).FirstOrDefault();
            if (Screen4 == null)
            {
                Screen4 = new L_Screens();
                Screen4.ScreenUrl = "HTTP://EQUI";
                Screen4.Discription = "Did not find the screen you where looking for";
            }

            //add all in one object
            List<L_Screens> ScreenS = new List<L_Screens>();
            ScreenS.Add(Screen1);
            ScreenS.Add(Screen2);
            ScreenS.Add(Screen3);
            ScreenS.Add(Screen4);

            //Pass screen lifecycle


            //add refresh rate if needed.
            if (Screen1.ResetRate.HasValue)
            {
                Response.AddHeader("Refresh", Screen1.ResetRate.ToString());
            }
            //Pass correct layout (empty layout)
            return View("RenderUserScreens", "~/Views/Shared/_MinimalLayout.cshtml", ScreenS);
        }

        //*****************************************************Inpersonate*****************************************************************************


        // GET: View an url like some other users. (for admin testing)
        [Authorize(Roles = "Administrator")]
        public ActionResult ViewAsUser(string username, string url)
        {
            Areas.user_management.Controllers.userController userController = new Areas.user_management.Controllers.userController();
            users user = userController.GetUser(username);

            //change session data to view an url as a specifc user
                Session["LocationRoot"] = user.LocationRoot;
                Session["AssetRoot"] = user.AssetRoot;
                Session["Impersonating"] = user.username;

            //Pass correct layout (empty layout)
            return View("ViewAsUser", "~/Views/Shared/_MinimalLayout.cshtml", url);
        }

        public ActionResult TEST()
        {
            return View();
        }
        //************************************************************************************************************************************************

        //cleanup Dispose of controller
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
