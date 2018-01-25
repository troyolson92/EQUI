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
    [Authorize(Roles = "Administrator")]
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            L_Screens l_Screens = await db.L_Screens.FindAsync(id);
            db.L_Screens.Remove(l_Screens);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Render selected screen in the screen wrapper
        public ActionResult RenderUserScreen(int screenID)
        {
            //Get screen
          //  string username = System.Web.HttpContext.Current.User.Identity.Name;
            L_Screens Screen = db.L_Screens.Where(s => s.id == screenID).First();
            if (Screen == null)
            {
                return HttpNotFound("Did not find the screen you where looking for");
            }
            //Pass screen lifecycle

            //add refresh rate if needed.
            if (Screen.ResetRate.HasValue)
            {
                Response.AddHeader("Refresh", Screen.ResetRate.ToString());
            }
            //Pass correct layout (empty layout)
            return View("RenderUserScreen", "~/Views/Shared/_MinimalLayout.cshtml", Screen);
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
