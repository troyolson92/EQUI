using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.user_management.Models;

namespace EqUiWebUi.Areas.user_management.Controllers
{
    public class AreaFiltersController : Controller
    {
        private GADATAEntitiesUserManagement db = new GADATAEntitiesUserManagement();

        // GET: user_management/AreaFilters
        public ActionResult Index()
        {
            ViewBag.selectlist = getAreaSelectList();
            return View(db.AreaFilters.ToList());
        }

        //GET: user_management/AreaFilters/_settings
        public ActionResult _settings()
        {
            return PartialView();
        }

        // GET: user_management/AreaFilters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaFilters areaFilters = db.AreaFilters.Find(id);
            if (areaFilters == null)
            {
                return HttpNotFound();
            }
            return View(areaFilters);
        }

        // GET: user_management/AreaFilters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: user_management/AreaFilters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( AreaFilters areaFilters)
        {
            if (ModelState.IsValid)
            {
                db.AreaFilters.Add(areaFilters);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(areaFilters);
        }

        // GET: user_management/AreaFilters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaFilters areaFilters = db.AreaFilters.Find(id);
            if (areaFilters == null)
            {
                return HttpNotFound();
            }
            return View(areaFilters);
        }

        // POST: user_management/AreaFilters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AreaFilters areaFilters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaFilters).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(areaFilters);
        }

        // GET: user_management/AreaFilters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaFilters areaFilters = db.AreaFilters.Find(id);
            if (areaFilters == null)
            {
                return HttpNotFound();
            }
            return View(areaFilters);
        }

        // POST: user_management/AreaFilters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AreaFilters areaFilters = db.AreaFilters.Find(id);
            db.AreaFilters.Remove(areaFilters);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //returns the area (locationtreefilter) select list from the database
        public SelectList getAreaSelectList(string UserLocationRoot= "")
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            List<AreaFilters> areaFilters = db.AreaFilters.ToList();

            List<string> plants = areaFilters.GroupBy(list => list.Plant).Select(grp => grp.First().Plant).ToList();
            foreach(string plant in plants)
            {
                //do we need an option to group by plant?

                List<string> optgroups = areaFilters.Where(list => list.Plant == plant).GroupBy(list => list.Optgroup).Select(grp => grp.First().Optgroup).ToList();
                foreach(string optgroup in optgroups)
                {
                    SelectListGroup selectListGroup =  new SelectListGroup() { Name = optgroup, Disabled = false };
                    List<SelectListItem> optgroupItems = areaFilters.Where(list => list.Plant == plant && list.Optgroup == optgroup 
                            && (list.LocationTreeFilter1.Contains(UserLocationRoot) || UserLocationRoot == "")
                                            ).Select(list => new SelectListItem
                                            {
                                                Value = list.LocationTreeFilter1,
                                                Text = list.Area,
                                                Group = selectListGroup,
                                                Disabled = false
                                            }).ToList();
                    selectList.AddRange(optgroupItems);
                }

            }
            return new SelectList(selectList, "Value","Text","Group.Name",0); 
        }
    }
}
