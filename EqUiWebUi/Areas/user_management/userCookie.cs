using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EqUiWebUi.Areas.user_management.Models;

namespace EqUiWebUi.Areas.user_management
{
    public class userCookie
    {
        public string name { get { return "equi_user";} }

        public users GetUser(string username)
        {
            using (Models.userCookie db = new Models.userCookie())
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

       public HttpCookie Cookie(string username)
        {
            users user = GetUser(username);
            HttpCookie cookie = new HttpCookie(name);
            cookie["Username"] = user.username;
            cookie["LocationRoot"] = user.LocationRoot;
            cookie["AssetRoot"] = user.AssetRoot;
            return cookie;
        }
    }
}