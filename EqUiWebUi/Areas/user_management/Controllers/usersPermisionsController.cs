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
    public class usersPermisionsController : Controller
    {
        private Models.GADATAEntitiesUserManagement db = new Models.GADATAEntitiesUserManagement();

        // GET: user_management/usersPermisions
        public ActionResult Index()
        {
            return View();
        }

        // GET: user_management/_permisions (partial grid list with user permision.)
        //if user id null return ALL 
        public async Task<ActionResult> _permisions(int? id)
        {
            if (id == null)
            {
                var h_usersPermisions = db.h_usersPermisions.Include(u => u.c_userRoles).Include(u => u.L_users).Include(u => u.L_users1);
                return PartialView(await h_usersPermisions.ToListAsync());
            }
            else
            {
                var h_usersPermisions = db.h_usersPermisions.Where(u => u.L_users.id == id).Include(u => u.c_userRoles).Include(u => u.L_users).Include(u => u.L_users1);
                return PartialView(await h_usersPermisions.ToListAsync());
            }
        }

        // GET: user_management/usersPermisions/Create
        //if user id not null block from selecting
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                ViewBag.user_id = new SelectList(db.L_users, "id", "username");
            }
            else
            {
                ViewBag.user_id = new SelectList(db.L_users.Where(u => u.id == id), "id", "username");
            }
            ViewBag.Role = new SelectList(db.c_userRoles, "Role", "Description");
            return View();
        }

        // POST: user_management/usersPermisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,user_id,Role")] usersPermisions usersPermisions)
        {
            //get current user ID 
            using (Models.GADATAEntitiesUserManagement db = new Models.GADATAEntitiesUserManagement())
            {
                int userID = (from users in db.L_users
                              where users.username == HttpContext.User.Identity.Name
                              select users.id).FirstOrDefault();
                usersPermisions.GrantedBy = userID;
            }
            usersPermisions.GrantedAt = System.DateTime.Now;

            if (ModelState.IsValid)
            {
                db.h_usersPermisions.Add(usersPermisions);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "users" , new {id = usersPermisions.user_id});
            }

            ViewBag.Role = new SelectList(db.c_userRoles, "Role", "Description", usersPermisions.Role);
            ViewBag.user_id = new SelectList(db.L_users, "id", "username", usersPermisions.user_id);
            return View(usersPermisions);
        }

        // GET: user_management/usersPermisions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usersPermisions usersPermisions = await db.h_usersPermisions.FindAsync(id);
            if (usersPermisions == null)
            {
                return HttpNotFound();
            }
            return View(usersPermisions);
        }

        // POST: user_management/usersPermisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            usersPermisions usersPermisions = await db.h_usersPermisions.FindAsync(id);
            db.h_usersPermisions.Remove(usersPermisions);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "users", new { id = usersPermisions.user_id });
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
