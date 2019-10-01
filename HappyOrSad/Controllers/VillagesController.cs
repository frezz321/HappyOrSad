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
    public class VillagesController : Controller
    {
        private HappyOrSadContext db = new HappyOrSadContext();

        // GET: Villages
        public ActionResult Index()
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            return View(db.Villages.ToList());
        }

        // GET: Villages/Details/5
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
            Village village = db.Villages.Find(id);
            if (village == null)
            {
                return HttpNotFound();
            }
            return View(village);
        }

        // GET: Villages/Create
        public ActionResult Create()
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            return View();
        }

        // POST: Villages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VillageID,VillageCode,VillageName")] Village village)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Villages.Add(village);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(village);
        }

        // GET: Villages/Edit/5
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
            Village village = db.Villages.Find(id);
            if (village == null)
            {
                return HttpNotFound();
            }
            return View(village);
        }

        // POST: Villages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VillageID,VillageCode,VillageName")] Village village)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Entry(village).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(village);
        }

        // GET: Villages/Delete/5
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
            Village village = db.Villages.Find(id);
            if (village == null)
            {
                return HttpNotFound();
            }
            return View(village);
        }

        // POST: Villages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            Village village = db.Villages.Find(id);
            db.Villages.Remove(village);
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
