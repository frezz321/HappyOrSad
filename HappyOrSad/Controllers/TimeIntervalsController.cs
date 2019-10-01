using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HappyOrSad.Models;

namespace HappyOrSad.Controllers
{
    [Authorize]
    public class TimeIntervalsController : Controller
    {
        private HappyOrSadContext db = new HappyOrSadContext();

        // GET: ConfAdmins
        public ActionResult Index()
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }
            return View(db.TimeInterval.ToList());
        }

        // GET: ConfAdmins/Details/5
        public ActionResult Details(int? id)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeInterval confAdmin = db.TimeInterval.Find(id);
            if (confAdmin == null)
            {
                return HttpNotFound();
            }
            return View(confAdmin);
        }

        // GET: ConfAdmins/Create
        public ActionResult Create()
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            if (db.TimeInterval.Count() > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        // POST: ConfAdmins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeIntervalID,TimeIntervalType,Value")] TimeInterval timeInterval)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            if (ModelState.IsValid)
            {
                db.TimeInterval.Add(timeInterval);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timeInterval);
        }

        // GET: ConfAdmins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeInterval confAdmin = db.TimeInterval.Find(id);
            if (confAdmin == null)
            {
                return HttpNotFound();
            }
            return View(confAdmin);
        }

        // POST: ConfAdmins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeIntervalID,TimeIntervalType,Value")] TimeInterval timeInterval)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Entry(timeInterval).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timeInterval);
        }

        // GET: ConfAdmins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeInterval confAdmin = db.TimeInterval.Find(id);
            if (confAdmin == null)
            {
                return HttpNotFound();
            }
            return View(confAdmin);
        }

        // POST: ConfAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            TimeInterval confAdmin = db.TimeInterval.Find(id);
            db.TimeInterval.Remove(confAdmin);
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
