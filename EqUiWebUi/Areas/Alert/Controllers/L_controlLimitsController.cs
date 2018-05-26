using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Alert.Models;

namespace EqUiWebUi.Areas.Alert.Controllers
{
    public class L_controlLimitsController : Controller
    {
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/L_controlLimits
        public ActionResult Index(bool showIsDead = false)
        {
            var l_controlLimits = db.l_controlLimits.Include(l => l.c_triggers).Include(l => l.L_users).Include(l => l.L_users1).Where(l => l.isdead == showIsDead);
            return View(l_controlLimits.ToList());
        }

        // GET: Alert/L_controlLimits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            l_controlLimits l_controlLimits = db.l_controlLimits.Find(id);
            if (l_controlLimits == null)
            {
                return HttpNotFound();
            }
            return View(l_controlLimits);
        }

        // GET: Alert/L_controlLimits/Create
        public ActionResult Create()
        {
            ViewBag.c_trigger_id = new SelectList(db.c_triggers, "id", "discription");
            return View();
        }

        // POST: Alert/L_controlLimits/Create
        //on create we must check if there is another control limit for this alert id and alarm object. if so give error. (or redirect to edit)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(l_controlLimits l_controlLimits)
        {
            if (ModelState.IsValid)
            {
                l_controlLimits.CreateDate = System.DateTime.Now;
                l_controlLimits.Createuser = (int)Session["UserId"];
                l_controlLimits.ChangeDate = System.DateTime.Now;
                l_controlLimits.ChangeUser = (int)Session["UserId"];
                l_controlLimits.isdead = false;
                db.l_controlLimits.Add(l_controlLimits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.c_trigger_id = new SelectList(db.c_triggers, "id", "discription", l_controlLimits.c_trigger_id);
            return View(l_controlLimits);
        }

        // GET: Alert/L_controlLimits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            l_controlLimits l_controlLimits = db.l_controlLimits.Find(id);
            if (l_controlLimits == null)
            {
                return HttpNotFound();
            }
            ViewBag.c_trigger_id = new SelectList(db.c_triggers, "id", "discription", l_controlLimits.c_trigger_id);
            return View(l_controlLimits);
        }

        // POST: Alert/L_controlLimits/Edit/5
        //This edit is special. we never delete a edit a control limit. a new instance get created and the current one is marked as dead.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(l_controlLimits l_controlLimits)
        {
            //only a record wat isdead false can be edited
            if (ModelState.IsValid && l_controlLimits.isdead == false)
            {
                //copy the object, set the create user data and add it to the db
                l_controlLimits newControlimit = l_controlLimits;
                newControlimit.CreateDate = System.DateTime.Now;
                newControlimit.Createuser = (int)Session["UserId"];
                newControlimit.ChangeDate = System.DateTime.Now;
                newControlimit.ChangeUser = (int)Session["UserId"];
                newControlimit.isdead = false;
                db.l_controlLimits.Add(newControlimit);
                //get the old control limit set the change user and mark it dead 
                l_controlLimits oldControlimit = db.l_controlLimits.Where(c => c.id == l_controlLimits.id).First();
                oldControlimit.ChangeDate = System.DateTime.Now;
                oldControlimit.ChangeUser = (int)Session["UserId"];
                oldControlimit.isdead = true;
                db.Entry(oldControlimit).State = EntityState.Modified;
                //save 
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.c_trigger_id = new SelectList(db.c_triggers, "id", "discription", l_controlLimits.c_trigger_id);
            return View(l_controlLimits);
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
