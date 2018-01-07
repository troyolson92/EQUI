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
        private Models.userCookie db = new Models.userCookie();

        // GET: user_management/user/Edit
        public async Task<ActionResult> _Edit()
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name;
            users user = await db.L_users.Where(u => u.username == username).FirstAsync();
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView(user);
        }

        // POST: user_management/user/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> _Edit([Bind(Include = "id,username,LocationRoot,AssetRoot,Locked,Blocked")] users user)
        {
            if (ModelState.IsValid && !user.Locked )
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return PartialView(user);
        }

    }
}