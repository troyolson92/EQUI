using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Welding.Models;

namespace EqUiWebUi.Areas.Welding.Controllers
{
    public class ToDoListsController : Controller
    {
        private GADATAEntitiesWelding db = new GADATAEntitiesWelding();

        // GET: Welding/ToDoLists
        public ActionResult Index()
        {
            return PartialView(db.ToDoList.ToList());
        }

        // GET: Welding/ToDoLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList toDoList = db.ToDoList.Find(id);
            if (toDoList == null)
            {
                return HttpNotFound();
            }
            return PartialView(toDoList);
        }

        // GET: Welding/ToDoLists/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Welding/ToDoLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ToDoItem,object,ProjectModel,ControleFrequence")] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                db.ToDoList.Add(toDoList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView(toDoList);
        }

        // GET: Welding/ToDoLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList toDoList = db.ToDoList.Find(id);
            if (toDoList == null)
            {
                return HttpNotFound();
            }
            return PartialView(toDoList);
        }

        // POST: Welding/ToDoLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ToDoItem,object,ProjectModel,ControleFrequence")] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDoList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView(toDoList);
        }

        // GET: Welding/ToDoLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList toDoList = db.ToDoList.Find(id);
            if (toDoList == null)
            {
                return HttpNotFound();
            }
            return PartialView(toDoList);
        }

        // POST: Welding/ToDoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDoList toDoList = db.ToDoList.Find(id);
            db.ToDoList.Remove(toDoList);
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
    }
}
