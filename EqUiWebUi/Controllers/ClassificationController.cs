using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Models;

namespace EqUiWebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ClassificationController : Controller
    {

        private GADATAEntitiesEQUI db = new GADATAEntitiesEQUI();

        // GET: Classification
        public ActionResult Index()
        {
            return View();
        }


        //Classification tool interface
        public ActionResult ClassificationTool()
        {

            return View();
        }


        //return a partial view gird based on how the user filterd.
        //we use a dummy logclassRules object to pass the data
        public ActionResult _logSearchResult(c_LogClassRules c_LogClassRule)
        {
            //we need to make a dummy object to query. look at how we did the variable data in vasc 


            return PartialView();
        }


        //need to find a way to sync c_classification without breaking the key.
        public ActionResult UpdateC_Classiciation()
        {
            return View();
        }

        //need to find a way to sync c_subgroup without breaking the key.
        public ActionResult UpdateC_Subgroup()
        {
            return View();
        }
    }
}