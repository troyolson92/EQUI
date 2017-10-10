﻿using EqUiWebUi.WebGridHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;

namespace EqUiWebUi.Controllers
{
    public class DynamicController : Controller
    {
        // GET: Dynamic
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DynamicWebgrid()
        {
            DataTable dt = new DataTable();

            //get data
            GadataComm gadataComm = new GadataComm();
            string qry = @"select top 100 id, controller_name from gadata.c3g.c_controller";
            dt = gadataComm.RunQueryGadata(qry);

            //
            WebGridHelpers.WebGridHelper webGridHelper = new WebGridHelper();
            ViewBag.Columns = webGridHelper.getDatatabelCollumns(dt);
            //
            List<dynamic> data = webGridHelper.datatableToDynamic(dt);
            //
            DefaultModel model = new WebGridHelpers.DefaultModel();
            model.PageSize = 30;
            //

            if (data != null)
            {
                model.TotalCount = data.Count();
                model.Data = data;
                model.DataTimestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return View(model);
        }
    }
}