using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EqUiWebUi.Areas.VWSC.Models;
using static EqUiWebUi.Areas.VWSC.Models.VWSCenums;

namespace EqUiWebUi.Areas.VWSC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class c_timerController : Controller
    {
        private GADATAEntitiesVWSC db = new GADATAEntitiesVWSC();

        // GET: VWSC/c_timer
        public ActionResult Index()
        {
            return View();
        }

        // GET: VWSC/c_timer/_List
        //Will return partial view with a list of the controllers in a controller class.
        //Filterable by enable bit
        public ActionResult _List(int? timerclass, int? timer_id)
        {
            if (timer_id != null)
            {
                return PartialView(db.c_timer.Where(c => c.ID == timer_id));
            }

            if (timerclass is null)
            {
                return PartialView(db.c_timer);
            }
            else
            {
                return PartialView(db.c_timer.Where(c => c.c_timer_class_id == timerclass));
            }
        }

        // GET: VWSC/c_timer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VWSC_c_timer Timer = db.c_timer.Find(id);
            if (Timer == null)
            {
                return HttpNotFound();
            }
            return View(Timer);
        }


        // GET: VWSC/c_timer/Edit/5
        // We will handle the creation of a new trigger also in EDIT. (to make code simplere) to create a new trigger pass ID = -1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VWSC_c_timer Timer;

            if (id == -1) //create new
            {
                Timer = new VWSC_c_timer();
                Timer.enable_bit = (int)Enable_bit.Disabled;
            }
            else //find the existing alert 
            {
                Timer = db.c_timer.Find(id);

                if (Timer == null)
                {
                    return HttpNotFound();
                }
            }
            ViewBag.class_id = new SelectList(db.c_timer_class, "id", "name", Timer.c_timer_class_id);
            ViewBag.NptId = new SelectList(db.c_NPT, "id", "name", Timer.NptId);
            return View(Timer);
        }

        // POST: VWSC/c_timer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VWSC_c_timer Timer)
        {
            if (ModelState.IsValid)
            {
                if (Timer.ID == -1)//add new 
                {
                    db.c_timer.Add(Timer);
                }
                else
                {
                    db.Entry(Timer).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.class_id = new SelectList(db.c_timer_class, "id", "name", Timer.c_timer_class_id);
            ViewBag.NptId = new SelectList(db.c_NPT, "id", "name", Timer.NptId);
            return View(Timer);
        }

        // GET: VWSC/c_timer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VWSC_c_timer Timer = db.c_timer.Find(id);
            if (Timer == null)
            {
                return HttpNotFound();
            }
            return View(Timer);
        }

        // POST: VWSC/c_timer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VWSC_c_timer Timer = db.c_timer.Find(id);
            db.c_timer.Remove(Timer);
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