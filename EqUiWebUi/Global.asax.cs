﻿using EqUiWebUi.Controllers;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EqUiWebUi.Areas.user_management.Models;

namespace EqUiWebUi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Log.Warn("Site startup");
            //
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //log 4 net 
            log4net.Config.XmlConfigurator.Configure();
        }

        //custom handelers for errors 
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

            Log.Error("Application error for: " + httpContext.User.Identity.Name, ex);

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
            }

            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            httpContext.Response.TrySkipIisCustomErrors = true;

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
                        Session["Username"] = user.username;
                        Session["LocationRoot"] = user.LocationRoot;
                        Session["AssetRoot"] = user.AssetRoot;
                        // set init done
                        Session["InitDone"] = true;
                    }
                    Log.Info(string.Format("Session init done for: {0}  id: {1}", user.username, Session.SessionID.ToString()));
                }
            }
            catch (Exception ex)
            {
                Log.Error("TEMP session state not available", ex);
            }
        }

        //returns a list of all active sessions.
        public List<string> Sessions()
        {
                return _sessions;
        }

    }
}
