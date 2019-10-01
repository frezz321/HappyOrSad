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
    public class QuestionsController : Controller
    {
        private HappyOrSadContext db = new HappyOrSadContext();

        

        // GET: Questions
        public ActionResult Index()
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            List<Question> questions = db.Question.ToList();
            questions.Sort(delegate (Question x, Question y)
            {
                
                if (x.DateDisplay == null || x.DateDisplay.ToString() == "" || x.DateDisplay.Value.ToLocalTime().Equals(DateTime.Now.ToLocalTime()))
                    return 1;
                if (y.DateDisplay == null || y.DateDisplay.ToString() == "" || y.DateDisplay.Value.ToLocalTime().Equals(DateTime.Now.ToLocalTime()))
                    return 1;
                else if (x.DateDisplay.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                    return -1;
                else if (y.DateDisplay.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                    return 1;
                else
                {
                    return x.CompareTo(y);
                }

            });
            return View(questions);
            
           
        }

        // GET: Questions/Details/5
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
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionID,Text,DateDisplay,Frequency")] Question question)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Question.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Questions/Edit/5
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
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionID,Text")] Question question)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Delete/5
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
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (CurrentUser.IsAnonymous)
            {
                ViewBag.ErrorRole = "Access Denied!";
                return View();
            }

            Question question = db.Question.Find(id);
            db.Question.Remove(question);
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
