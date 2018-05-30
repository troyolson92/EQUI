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
        //is showisdead = true all is show else only active control limits
        public ActionResult Index(bool showIsDead = false)
        {
            var l_controlLimits = db.l_controlLimits.Include(l => l.c_triggers).Include(l => l.L_ChangeUser).Include(l => l.L_CreateUser).Where(l => l.isdead == false || l.isdead == showIsDead);
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
                l_controlLimits.Createuser = CurrentUser.Getuser.id;
                l_controlLimits.ChangeDate = System.DateTime.Now;
                l_controlLimits.ChangeUser = CurrentUser.Getuser.id;
                l_controlLimits.isdead = false;
                db.l_controlLimits.Add(l_controlLimits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.c_trigger_id = new SelectList(db.c_triggers, "id", "alertType", l_controlLimits.c_trigger_id);
            return View(l_controlLimits);
        }

        // GET: Alert/L_controlLimits/Edit/5
        // It is also possible to edit multible records at the same time. by setting the l_variants_id        
        public ActionResult Edit(int? id, int? l_variants_id)
        {
            if ((id.HasValue && l_variants_id.HasValue) || (id == null && l_variants_id == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            l_controlLimits l_controlLimits = null;
            if (id.HasValue)
            {
                l_controlLimits = db.l_controlLimits.Find(id);
            }
            if (l_variants_id.HasValue)
            {
                l_controlLimits = db.l_controlLimits.Where(l => l.l_variants_id == l_variants_id && l.isdead == false).FirstOrDefault();
                ViewBag.variantCount = db.l_controlLimits.Where(l => l.l_variants_id == l_variants_id && l.isdead == false).Count();
            }

            if (l_controlLimits == null)
            {
                return HttpNotFound();
            }else if(l_controlLimits.isdead == true)
            {
                return new HttpStatusCodeResult(404, "not possible to edit a dead control limit id:" + id.ToString());
            }

            ViewBag.c_trigger_id = new SelectList(db.c_triggers, "id", "alertType", l_controlLimits.c_trigger_id);
            ViewBag.l_variants_id = new SelectList(db.l_variants.Where(l => l.c_trigger_id == l_controlLimits.c_trigger_id), "id", "variantGroup", l_controlLimits.l_variants_id);
            return View(l_controlLimits);
        }

        // POST: Alert/L_controlLimits/Edit/5
        //This edit is special. we never delete a edit a control limit. a new instance get created and the current one is marked as dead.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(l_controlLimits l_controlLimits, int? l_variants_group_id)
        {
  
            if (ModelState.IsValid)
            {
                //in case the l_variants_id is set get all the records that should be changed.
                if (l_variants_group_id.HasValue)
                {
                    List<l_controlLimits> list = db.l_controlLimits.Where(l => l.l_variants_id == l_variants_group_id && l.isdead == false).ToList();
                    foreach(l_controlLimits controllimit in list)
                    {
                        //copy the object, set the create user data and add it to the db
                        l_controlLimits newControlimit = controllimit;
                        newControlimit.CreateDate = System.DateTime.Now;
                        newControlimit.Createuser = CurrentUser.Getuser.id;
                        newControlimit.ChangeDate = System.DateTime.Now;
                        newControlimit.ChangeUser = CurrentUser.Getuser.id;
                        newControlimit.isdead = false;
                        //copy values
                        newControlimit.UpperLimit = l_controlLimits.UpperLimit;
                        newControlimit.LowerLimit = l_controlLimits.LowerLimit;
                        newControlimit.Comment = l_controlLimits.Comment;
                        //add
                        db.l_controlLimits.Add(newControlimit);
                        //get the old control limit set the change user and mark it dead 
                        l_controlLimits oldControlimit = db.l_controlLimits.Where(c => c.id == controllimit.id).First();
                        oldControlimit.ChangeDate = System.DateTime.Now;
                        oldControlimit.ChangeUser = CurrentUser.Getuser.id;
                        oldControlimit.isdead = true;
                        db.Entry(oldControlimit).State = EntityState.Modified;
                    }
                }
                else //just add the one new record
                {
                    //copy the object, set the create user data and add it to the db
                    l_controlLimits newControlimit = l_controlLimits;
                    newControlimit.CreateDate = System.DateTime.Now;
                    newControlimit.Createuser = CurrentUser.Getuser.id;
                    newControlimit.ChangeDate = System.DateTime.Now;
                    newControlimit.ChangeUser = CurrentUser.Getuser.id;
                    newControlimit.isdead = false;
                    //add
                    db.l_controlLimits.Add(newControlimit);
                    //get the old control limit set the change user and mark it dead 
                    l_controlLimits oldControlimit = db.l_controlLimits.Where(c => c.id == l_controlLimits.id).First();
                    oldControlimit.ChangeDate = System.DateTime.Now;
                    oldControlimit.ChangeUser = CurrentUser.Getuser.id;
                    oldControlimit.isdead = true;
                    db.Entry(oldControlimit).State = EntityState.Modified;
                }
                //save 
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.c_trigger_id = new SelectList(db.c_triggers, "id", "alertType", l_controlLimits.c_trigger_id);
            ViewBag.l_variants_id = new SelectList(db.l_variants.Where(l => l.c_trigger_id == l_controlLimits.c_trigger_id), "id", "variantGroup", l_controlLimits.l_variants_id);
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
