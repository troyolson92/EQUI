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
    public class c_ownershipController : Controller
    {
        private GADATAEntitiesUserManagement db = new GADATAEntitiesUserManagement();

        // GET: user_management/c_ownership
        public ActionResult Index()
        {
            ViewBag.selectlist = getOwnershipSelectList();
            return View(db.c_ownership.ToList());
        }

        // GET: user_management/c_ownership/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_ownership c_ownership = db.c_ownership.Find(id);
            if (c_ownership == null)
            {
                return HttpNotFound();
            }
            return View(c_ownership);
        }

        // GET: user_management/c_ownership/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View(new c_ownership());
        }

        // POST: user_management/c_ownership/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(c_ownership c_ownership)
        {
            if (ModelState.IsValid)
            {
                db.c_ownership.Add(c_ownership);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c_ownership);
        }

        // GET: user_management/c_ownership/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_ownership c_ownership = db.c_ownership.Find(id);
            if (c_ownership == null)
            {
                return HttpNotFound();
            }
            return View(c_ownership);
        }

        // POST: user_management/c_ownership/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(c_ownership c_ownership)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c_ownership).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c_ownership);
        }

        // GET: user_management/c_ownership/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            c_ownership c_ownership = db.c_ownership.Find(id);
            if (c_ownership == null)
            {
                return HttpNotFound();
            }
            return View(c_ownership);
        }

        // POST: user_management/c_ownership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            c_ownership c_ownership = db.c_ownership.Find(id);
            db.c_ownership.Remove(c_ownership);
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

        //returns the ownership select list from the database
        public SelectList getOwnershipSelectList(string UserLocationRoot = "")
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            List<c_ownership> ownershipFilters = db.c_ownership.ToList();

            List<string> plants = ownershipFilters.GroupBy(list => list.Plant).Select(grp => grp.First().Plant).ToList();
            foreach (string plant in plants)
            {
                //do we need an option to group by plant?

                List<string> optgroups = ownershipFilters.Where(list => list.Plant == plant).GroupBy(list => list.Optgroup).Select(grp => grp.First().Optgroup).ToList();
                foreach (string optgroup in optgroups)
                {
                    SelectListGroup selectListGroup = new SelectListGroup() { Name = optgroup, Disabled = false };
                    List<SelectListItem> optgroupItems = ownershipFilters.Where(list => list.Plant == plant && list.Optgroup == optgroup
                            && (list.LocationTree.Contains(UserLocationRoot) || UserLocationRoot == "")
                                            ).GroupBy(items => items.Ownership).Select(item => new SelectListItem
                                            {
                                                Value = item.First().Ownership,
                                                Text = item.First().Ownership,
                                                Group = selectListGroup,
                                                Disabled = false
                                            }).ToList();
                    selectList.AddRange(optgroupItems);
                }

            }
            return new SelectList(selectList, "Value", "Text", "Group.Name", 0);
        }

        //fire a hangfire job to relink assers 
        [Authorize(Roles = "Administrator")]
        public void LinkMaximoAssetsToGadata()
        {
            EqUiWebUi.Areas.Maximo_ui.Backgroundwork backgroundWork = new Maximo_ui.Backgroundwork();
            backgroundWork.LinkMaximoAssetsToGadata();
        }
    }
}
