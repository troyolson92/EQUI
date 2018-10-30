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
    public class ToDoList_RemarkController : Controller
    {
        private GADATAEntitiesWelding db = new GADATAEntitiesWelding();

        // GET: Welding/ToDoList_Remark
        public ActionResult Index()
        {
            var toDoList_Remark = db.ToDoList_Remark.Include(t => t.ToDoList);
            return PartialView(toDoList_Remark.ToList());
        }

        // GET: Welding/ToDoList_Remark/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList_Remark toDoList_Remark = db.ToDoList_Remark.Find(id);
            if (toDoList_Remark == null)
            {
                return HttpNotFound();
            }
            return PartialView(toDoList_Remark);
        }

        // GET: Welding/ToDoList_Remark/Create
        public ActionResult Create()
        {
            ViewBag.TodoID = new SelectList(db.ToDoList, "id", "ToDoItem");
            return PartialView();
        }

        // POST: Welding/ToDoList_Remark/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,TodoID,Timestamp,Remark,UserID")] ToDoList_Remark toDoList_Remark)
        {
            if (ModelState.IsValid)
            {
                db.ToDoList_Remark.Add(toDoList_Remark);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TodoID = new SelectList(db.ToDoList, "id", "ToDoItem", toDoList_Remark.TodoID);
            return PartialView(toDoList_Remark);
        }

        // GET: Welding/ToDoList_Remark/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList_Remark toDoList_Remark = db.ToDoList_Remark.Find(id);
            if (toDoList_Remark == null)
            {
                return HttpNotFound();
            }
            ViewBag.TodoID = new SelectList(db.ToDoList, "id", "ToDoItem", toDoList_Remark.TodoID);
            return PartialView(toDoList_Remark);
        }

        // POST: Welding/ToDoList_Remark/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,TodoID,Timestamp,Remark,UserID")] ToDoList_Remark toDoList_Remark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDoList_Remark).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TodoID = new SelectList(db.ToDoList, "id", "ToDoItem", toDoList_Remark.TodoID);
            return PartialView(toDoList_Remark);
        }

        // GET: Welding/ToDoList_Remark/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList_Remark toDoList_Remark = db.ToDoList_Remark.Find(id);
            if (toDoList_Remark == null)
            {
                return HttpNotFound();
            }
            return PartialView(toDoList_Remark);
        }

        // POST: Welding/ToDoList_Remark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDoList_Remark toDoList_Remark = db.ToDoList_Remark.Find(id);
            db.ToDoList_Remark.Remove(toDoList_Remark);
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
