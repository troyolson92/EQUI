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
            userCookie userCookie = new userCookie();
            Response.Cookies.Add(userCookie.Cookie(System.Web.HttpContext.Current.User.Identity.Name));
            return null;
        }

        // Get: user_management/user/SetCookie/key/value
        public ActionResult SetCookie(string key, string value)
        {
            userCookie userCookie = new userCookie();
            var cookie = Request.Cookies[userCookie.name];
            //if it does no exist build it.
            if (cookie == null)
            {
                Response.Cookies.Add(userCookie.Cookie(System.Web.HttpContext.Current.User.Identity.Name));
            }

            //here we can check if the user is allow to change this value and block it...

            //change value 
            cookie[key] = value;
            //post cookie back 
            Response.Cookies.Add(cookie);
            //I know i should return something to check if it worked by i'm lazy 
            return null;
        }

    }
}