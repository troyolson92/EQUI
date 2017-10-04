using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EqUiWebUi.Models;
using System.Data;
using EQUICommunictionLib;
using EqUiWebUi.WebGridHelpers;

namespace EqUiWebUi.Controllers
{
	public class GadataController : Controller
	{
		//
		// GET: /Gadata/
		public ActionResult Index()
		{
			return new HttpNotFoundResult("Woeps");
		}

		[HttpGet]
		public ActionResult WebGrid()
		{
			ProductModel model = new ProductModel();
			model.PageSize = 4;

			List<Product> products = Product.GetSampleProducts();

			if (products != null)
			{
				model.TotalCount = products.Count();
				model.Products = products;
			}

			return View(model);
		}

		[HttpGet]
		public ActionResult DynamicWebgrid()
		{
            //set refresh 
            //Response.AddHeader("Refresh", "10");

            DataTable dt = new DataTable();
            if (DataBuffer.Tipstatus != null)
            {
                dt = DataBuffer.Tipstatus;
            }
            //
			WebGridHelpers.WebGridHelper webGridHelper = new WebGridHelper();
			ViewBag.Columns = webGridHelper.getDatatabelCollumns(dt);
			//
			List<dynamic> data = webGridHelper.datatableToDynamic(dt);
			//
			WebGridHelpers.DefaultModel model = new WebGridHelpers.DefaultModel();
			model.PageSize = 30;
			//

			if (data != null)
			{
				model.TotalCount = data.Count();
				model.Data = data;
                model.DataTimestamp =  DataBuffer.TipstatusLastDt.ToString("yyyy-MM-dd HH:mm:ss");
			}
			return View(model);
		}

		[HttpGet]
		public JsonResult checkNewData(String dataTimestamp)
		{
            DateTime date = DateTime.ParseExact(dataTimestamp, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            if (DataBuffer.TipstatusLastDt > date)
			{
			   //issue reload
			   return Json(new object[] { new object() }, JsonRequestBehavior.AllowGet);
			}
			else
			{
				//no reload needed
				return null; 
			}

		}

		[HttpGet]
		public ActionResult PloegRapportWebgrid()
		{
			GadataComm gadataComm = new GadataComm();
			DataTable dt = gadataComm.RunQueryGadata(
				@"USE GADATA EXEC GADATA.EqUi.AAOSR_PloegRaportV2 @daysBack = 1 , @minDowntime = 20 ");
			//
			WebGridHelpers.WebGridHelper webGridHelper = new WebGridHelper();
			ViewBag.Columns = webGridHelper.getDatatabelCollumns(dt);
			//
			List<dynamic> data = webGridHelper.datatableToDynamic(dt);
			//
			WebGridHelpers.DefaultModel model = new WebGridHelpers.DefaultModel();
			model.PageSize = 30;
			//
			if (data != null)
			{
				model.TotalCount = data.Count();
				model.Data = data;
			}
			return View(model);
		}


		[HttpGet]
		public ActionResult jqGrid()
		{
			return View();
		}

		public ActionResult GetProducts(string sidx, string sord, int page, int rows)
		{
			var products = Product.GetSampleProducts();
			int pageIndex = Convert.ToInt32(page) - 1;
			int pageSize = rows;
			int totalRecords = products.Count();
			int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

			var data = products.OrderBy(x => x.Id)
						 .Skip(pageSize * (page - 1))
						 .Take(pageSize).ToList();

			var jsonData = new
			{
				total = totalPages,
				page = page,
				records = totalRecords,
				rows = data
			};

			return Json(jsonData, JsonRequestBehavior.AllowGet);
		}

		public ActionResult GetProductById(int id)
		{
			var products = Product.GetSampleProducts().Where(x => x.Id == id); ;

			if (products != null)
			{
				ProductModel model = new ProductModel();

				foreach (var item in products)
				{
					model.Name = item.Name;
					model.Price = item.Price;
					model.Department = item.Department;
				}

				return PartialView("_GridEditPartial", model);
			}

			return View();
		}

		[HttpPost]
		public ActionResult UpdateProduct(ProductModel model)
		{
			//update database
			return Content("Record updated!!", "text/html");
		}
	}
}