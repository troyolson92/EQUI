using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.user_management.Models;

namespace EqUiWebUi.Areas.user_management.Controllers
{
    public class userController : Controller
    {
        private Models.GADATAEntitiesUserManagement db = new Models.GADATAEntitiesUserManagement();

        // GET: user_management/user/Details
        public ActionResult _Details()
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name;
            users user = db.L_users.Where(u => u.username == username).First();
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView(user);
        }

        // GET: user_management/user/Edit
        public async Task<ActionResult> Edit()
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name;
            users user = await db.L_users.Where(u => u.username == username).FirstAsync();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: user_management/user/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,username,LocationRoot,AssetRoot,Locked,Blocked")] users user)
        {
            if (ModelState.IsValid && !user.Locked )
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return View(user);
        }

        // Get: user_management/user/getCookie
        //forces reload of the user cookie.
        public ActionResult ResetCookie()
        {
            users user = GetUser(System.Web.HttpContext.Current.User.Identity.Name);
            //set user variables
            Session["Username"] = user.username;
            Session["LocationRoot"] = user.LocationRoot;
            Session["AssetRoot"] = user.AssetRoot;
            return null;
        }

        // Get: user_management/user/SetCookie/key/value
        public ActionResult SetCookie(string key, string value)
        {
            Session[key] = value;
            return null;
        }

        //get user info and make user if not exist
        public users GetUser(string username)
        {
            using (Models.GADATAEntitiesUserManagement db = new Models.GADATAEntitiesUserManagement())
            {
                users user = (from users in db.L_users
                              where users.username == username
                              select users).FirstOrDefault();

                if (user == null)
                {
                    //add new user to database
                    users newUser = new users();
                    newUser.username = username;
                    newUser.LocationRoot = "VCG";
                    newUser.AssetRoot = "U";
                    newUser.Blocked = false;
                    newUser.Locked = false;
                    db.L_users.Add(newUser);
                    db.SaveChanges();
                    //get it back to be sure
                    user = (from users in db.L_users
                            where users.username == username
                            select users).FirstOrDefault();
                    if (user == null)
                    {
                        //error
                    }
                }
                return user;
            }
        }

    }
}