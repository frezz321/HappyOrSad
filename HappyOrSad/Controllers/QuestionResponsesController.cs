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
    public class QuestionResponsesController : Controller
    {
        private HappyOrSadContext db = new HappyOrSadContext();

        // GET: Responses
        public ActionResult Index(int? id)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            //var response = db.QuestionResponse.Include(r => r.Question);
            // return View(response.ToList());
            var response = from s in db.QuestionResponse
                             select s;
            response = response.OrderByDescending(s => s.DateSubmitted);

            int totalPages = response.Count() / 10;
            
            int Remainder = response.Count() % 10;
            if (Remainder > 0) {
                totalPages += 1;
            }
            ViewBag.Pages = totalPages;
            
            List<QuestionResponse> responseList = new List<QuestionResponse>();
            int count = 1;
            id = (id != null) ? (int)id : 1;
            int index = ((int)id * 10) - 10;
            while (count <= 10)
            {
                if (response.ToList().Count > index)
                    responseList.Add(response.ToList()[index]);

                ++index;
                ++count;
            }

            return View(responseList);
        }

        // GET: Responses/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (CurrentUser.IsAnonymous)
        //    {
        //        ViewBag.ErrorRole = "Access Denied!";
        //        return View();
        //    }

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    QuestionResponse response = db.QuestionResponse.Find(id);
        //    if (response == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(response);
        //}

        // GET: Responses/Create
        /*public ActionResult Create()
        {
            
            ViewBag.QuestionID = new SelectList(db.Question, "QuestionID", "Text");
            return View();
        }

        // POST: Responses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResponseID,QuestionID,Score,DateSubmitted,KiosKIP")] Response response)
        {
            if (!Authorised())
                return RedirectToAction("Setting", "Cookies");

            if (ModelState.IsValid)
            {
                db.Response.Add(response);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Question, "QuestionID", "Text", response.QuestionID);
            return View(response);
        }

        // GET: Responses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!Authorised())
                return RedirectToAction("Setting", "Cookies");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Response.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Question, "QuestionID", "Text", response.QuestionID);
            return View(response);
        }

        // POST: Responses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResponseID,QuestionID,Score,DateSubmitted,KiosKIP")] Response response)
        {
            if (!Authorised())
                return RedirectToAction("Setting", "Cookies");

            if (ModelState.IsValid)
            {
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Question, "QuestionID", "Text", response.QuestionID);
            return View(response);
        }*/

        // GET: Responses/Delete/5
        //public ActionResult Delete(int? id)
        //{
            
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    QuestionResponse response = db.QuestionResponse.Find(id);
        //    if (response == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(response);
        //}

        //// POST: Responses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
            
        //    QuestionResponse response = db.QuestionResponse.Find(id);
        //    db.QuestionResponse.Remove(response);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
