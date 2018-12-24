﻿using EqUiWebUi.Controllers;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EqUiWebUi.Areas.user_management.Models;
using System.Threading;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.Web.Configuration;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace EqUiWebUi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            log.Warn("Application_Start");
            //change EF connection strings    
            //WARNING ! make sure that the IIS_IUSRS hare full control on the ISS root folder!! (else we can not save the web.config)
            SqlConnectionStringBuilder EQUIConnectionString = new SqlConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ConnectionString);
            //get the web.config
            var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
            bool configChanged = false;
            //loop all connection strings
            foreach (ConnectionStringSettings c in System.Configuration.ConfigurationManager.ConnectionStrings)
            {
                if (c.Name == "EQUIConnectionString") continue; //main string must not be updated
                if (c.Name == "LocalSqlServer") continue; //local SQL instance mustn ot be updated
                //         
                try
                {
                    EntityConnectionStringBuilder EFConnectionstring = new EntityConnectionStringBuilder(c.ConnectionString);
                    SqlConnectionStringBuilder EFProviderConnectionString = new SqlConnectionStringBuilder(EFConnectionstring.ProviderConnectionString);
                    EFProviderConnectionString.DataSource = EQUIConnectionString.DataSource;
                    EFProviderConnectionString.InitialCatalog = EQUIConnectionString.InitialCatalog;
                    if (EQUIConnectionString.IntegratedSecurity)
                    {
                        EFProviderConnectionString.IntegratedSecurity = true;
                        EFProviderConnectionString.Remove("User ID");
                        EFProviderConnectionString.Remove("Password");
                    }
                    else
                    {
                        EFProviderConnectionString.UserID = EQUIConnectionString.UserID;
                        EFProviderConnectionString.Password = EQUIConnectionString.Password;
                    }
                    EFConnectionstring.ProviderConnectionString = EFProviderConnectionString.ToString();
                    //set the web.config if connectionstring is different.
                    if (section.ConnectionStrings[c.Name].ConnectionString != EFConnectionstring.ToString())
                    {
                        log.Info("update connectionstring: " + c.Name);
                        section.ConnectionStrings[c.Name].ConnectionString = EFConnectionstring.ToString();
                        configChanged = true;
                    }
                }
                catch(Exception ex)
                {
                    log.Error("failed to update connectionstring: " + c.Name, ex);
                }
            }
            try
            {
                if (configChanged)
                {
                    log.Warn("Web.config connection strings have changed");
                    configuration.Save(ConfigurationSaveMode.Modified);
                }   
            }
            catch(Exception ex)
            {
                log.Error("failed to update web.config CHECK IF IIS_IUSRS has RW acces to site root folder!!", ex);
            }
           
            //default mvc web registration
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            //log 4 net 
            log4net.Config.XmlConfigurator.Configure();

        }

        //custom handlers for errors 
        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = ((MvcApplication)sender).Context;
            var currentController = " ";
            var currentAction = " ";
            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            if (currentRouteData != null)
            {
                if (currentRouteData.Values["controller"] != null && !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                {
                    currentController = currentRouteData.Values["controller"].ToString();
                }

                if (currentRouteData.Values["action"] != null && !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                {
                    currentAction = currentRouteData.Values["action"].ToString();
                }
            }

            var ex = Server.GetLastError();
            var controller = new ErrorController();
            var routeData = new RouteData();
            var action = "Index";

            if (ex is HttpException)
            {
                var httpEx = ex as HttpException;

                switch (httpEx.GetHttpCode())
                {
                    case 404:
                        action = "NotFound";
                        break;

                    case 401:
                        action = "AccessDenied";
                        break;

                    case 500:
                        action = "InternalServerError";
                        break;
                }
                log.Error($"HttpException  code:{httpEx.GetHttpCode()} for: {httpContext.User.Identity.Name}", ex);
            }
            else
            {
                log.Error($"Application error for: {httpContext.User.Identity.Name}", ex);
            }

            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            httpContext.Response.TrySkipIisCustomErrors = true;

            routeData.Values["area"] = "";
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = action;

            controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        }


        //keeping track of active sessions..
        private static readonly List<string> _sessions = new List<string>();
        private static readonly object padlock = new object();

        //on session start
        void Session_Start(object sender, EventArgs e)
        {
            lock (padlock)
            {
                _sessions.Add(Session.SessionID);
            }

        }

        //on session start
        void Session_End(object sender, EventArgs e)
        {
            lock (padlock)
            {
                _sessions.Remove(Session.SessionID);
            }

        }

        //on user Autherize.
        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //try init session vars 
            try
            {
                if (Session["InitDone"] == null)
                {
                    Areas.user_management.Controllers.userController userController = new Areas.user_management.Controllers.userController();
                    users user = userController.GetUser(System.Web.HttpContext.Current.User.Identity.Name);
                    //set user variables
                    lock (padlock)
                    {
                        Session["Impersonating"] = "";
                        //parse user object into session
                        Session["user"] = user;
                        // set init done
                        Session["InitDone"] = true;
                    }
                    log.Info(string.Format("Session init done for: {0}  id: {1}", user.username, Session.SessionID.ToString()));
                }

                //set user culture for this request
                string cultrue = CurrentUser.Getuser.Culture;
                if (cultrue == null)
                {
                    cultrue = "en-GB";
                }
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultrue);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultrue);
            }
            catch (Exception ex)
            {
                log.Error("TEMP session state not available", ex);
            }

        }

        //handle UNauthorised
        void Application_EndRequest(object sender, System.EventArgs e)
        {
            // If the user is not authorised to see this page or access this function, send them to the error page.
            if (Response.StatusCode == 401 && Request.RawUrl != "/" && Request.LogonUserIdentity.IsAuthenticated == true)
            {
                log.Error(string.Format("Unauthorised: {0} For page: {1}", Request.LogonUserIdentity.Name, Request.RawUrl));

                //work around to promt credentials when I whant it to promt.
                if (Request.RawUrl == "/user_management/user/LoginPrompt")
                {
                 //do nothing and continue windows will prompt us    
                //but how do I allow the user to continue afther...
                }
                else //normal user unauth
                {
                    Response.ClearContent();
                    //redirect to  page
                    Response.Redirect("~/Error/AccessDenied");
                }
            }
        }

        //returns a list of all active sessions.
        public static List<string> Sessions()
        {
            return _sessions;
        }


    }


    //****************************************************************
    //class to acces the session vars for the current user
    //***************************************************************
    public static class CurrentUser
    {
        //
        public static EqUiWebUi.Areas.user_management.Models.users Getuser
        {
            get
            {
                var user = HttpContext.Current.Session["user"] as users;
                if (null == user)
                {
                    user = new users();
                    HttpContext.Current.Session["user"] = user;
                }
                //fix for whitespaces at begin or start of roots
                user.AssetRoot = user.AssetRoot.Trim();
                user.LocationRoot = user.LocationRoot.Trim();
                return user;
            }

        }
    }
}
