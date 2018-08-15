using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
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

            AreaFiltersController areaFiltersController = new EqUiWebUi.Areas.user_management.Controllers.AreaFiltersController();
            ViewBag.AreaSelectlist = areaFiltersController.getAreaSelectList();
            c_ownershipController ownership = new EqUiWebUi.Areas.user_management.Controllers.c_ownershipController();
            ViewBag.OwnershipSelectlist = ownership.getOwnershipSelectList();
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

            AreaFiltersController areaFiltersController = new EqUiWebUi.Areas.user_management.Controllers.AreaFiltersController();
            ViewBag.AreaSelectlist = areaFiltersController.getAreaSelectList();
            c_ownershipController ownership = new EqUiWebUi.Areas.user_management.Controllers.c_ownershipController();
            ViewBag.OwnershipSelectlist = ownership.getOwnershipSelectList();
            ViewBag.cultrueSelectlist = cultures.CultureList();

            return View(user);
        }

        // POST: user_management/user/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(users user)
        {
            if (ModelState.IsValid && !user.Locked )
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //if all is oke wit the save reset user and go to homepage
                return new RedirectToRouteResult(
              new RouteValueDictionary(
                  new
                  {
                      area = "user_management",
                      controller = "user",
                      action = "ResetCookie"
                  }
              )
              );
            }

            AreaFiltersController areaFiltersController = new EqUiWebUi.Areas.user_management.Controllers.AreaFiltersController();
            ViewBag.AreaSelectlist = areaFiltersController.getAreaSelectList();
            c_ownershipController ownership = new EqUiWebUi.Areas.user_management.Controllers.c_ownershipController();
            ViewBag.OwnershipSelectlist = ownership.getOwnershipSelectList();
            ViewBag.cultrueSelectlist = cultures.CultureList();

            return View(user);
        }

        // Get: user_management/user/getCookie
        //forces reload of the user cookie.
        public ActionResult ResetCookie()
        {
            users user = GetUser(System.Web.HttpContext.Current.User.Identity.Name);
            //set user variables
            Session["user"] = user;
            Session["Impersonating"] = "";
            //redirect to home page
            return new RedirectToRouteResult(
               new RouteValueDictionary(
                   new
                   {
                       area = "",
                       controller = "Home",
                       action = "Index"
                   }
               )
           );
        }

        // Get: user_management/user/SetCookie/key/value
        public ActionResult SetCookie(string key, string value)
        {
            if (key == "LocationRoot")
            {
                users user = CurrentUser.Getuser;
                user.LocationRoot = value;
                Session["user"] = user;
            }
            else if (key == "AssetRoot")
            {
                users user = CurrentUser.Getuser;
                user.AssetRoot = value;
                Session["user"] = user;
            }
            else
            {
                //error
            }
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
                    newUser.LocationRoot = System.Configuration.ConfigurationManager.AppSettings["Maximo_SiteID"].ToString(); //take maximo site ID (top level of the location tree)
                    newUser.AssetRoot = ""; //by default no filter on asset levels
                    newUser.Blocked = false;
                    newUser.Locked = false;
                    newUser.Culture = "En-GB";
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

        // to test not auth request
        [Authorize(Roles = "RoleDoesNotExist")]
        public ActionResult ForbiddenMethod()
        {
            return View();
        }

        //force login prompt
        [Authorize(Roles = "RoleDoesNotExist")]
        public ActionResult LoginPrompt()
        {
            return View();
        }

    }
}