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
    public class usersController : Controller
    {
        private Models.GADATAEntitiesUserManagement db = new Models.GADATAEntitiesUserManagement();

        // GET: user_management/users
        public async Task<ActionResult> Index()
        {
            return View(await db.L_users.ToListAsync());
        }

        //GET: user_management/user/_settings
        public ActionResult _settings()
        {
            ViewBag.ActiveSessions = EqUiWebUi.MvcApplication.Sessions().Count;
            return PartialView();
        }

        // GET: user_management/ListSessions
        public ActionResult ListSessions()
        {
            //get the list of active sessionIDs
            List<string> activeSessions = EqUiWebUi.MvcApplication.Sessions();
            //make model object with all session details
            List<userSession> sessionList = new List<userSession>();
            foreach (string sessionID in activeSessions)
            {
                userSession userSession = new userSession();
                userSession.sessionId = sessionID;

                //HOW DO I GET THE SESSIONS???

                sessionList.Add(userSession);
            }
            return View(sessionList);
        }

        // GET: user_management/users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = await db.L_users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: user_management/users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: user_management/users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,username,LocationRoot,AssetRoot,Locked,Blocked")] users users)
        {
            if (ModelState.IsValid)
            {
                db.L_users.Add(users);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: user_management/users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = await db.L_users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: user_management/users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,username,LocationRoot,AssetRoot,Locked,Blocked")] users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: user_management/users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            users users = await db.L_users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: user_management/users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            //must check here if user still has roles. (can not delete a user that still is in a role)
            //it even wors :) if a usr has given roles you can not delete him to...

            users users = await db.L_users.FindAsync(id);
            db.L_users.Remove(users);
            await db.SaveChangesAsync();
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
