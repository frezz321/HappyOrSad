using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HappyOrSad.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using System.Globalization;

namespace HappyOrSad.Controllers
{
    
    public class QuestionDisplayController : Controller
    {
        private HappyOrSadContext db = new HappyOrSadContext();
        // GET: QuestionDisplay
        public ActionResult Display()
        {    
            System.Security.Claims.ClaimsPrincipal current = System.Security.Claims.ClaimsPrincipal.Current;
            string username = (current.Identity.Name == null)? current.Claims.ElementAt(9).Value: current.Identity.Name;
            if(String.IsNullOrEmpty(username) || String.IsNullOrWhiteSpace(username))
            {
                return RedirectToAction("Index","Home");
            }

            var isActivated = (db.Users.Where(u => u.UserName == username).Count() > 0) ? db.Users.Where(u => u.UserName == username).First().isActivated : false;
            if (!isActivated)
            {
                ViewBag.Error = "The user account is not activated! Please activate the account first";
                return View("Error");
            }

            if (!string.IsNullOrEmpty(username))
                ViewBag.UserId = (db.Users.Where(u => u.UserName == username).Count() > 0) ? db.Users.Where(u => u.UserName == username).First().Id : "";


            Question question = null;
            if (db.TimeInterval.Count() > 0)
            {
                TempData["TimeIntervalType"] = db.TimeInterval.ToList().First().TimeIntervalType;
                TempData["TimeIntervalValue"] = db.TimeInterval.ToList().First().Value;
            }
            else
            {
                TempData["TimeIntervalType"] = TimeIntervalType.Day;
                TempData["TimeIntervalValue"] = 1;
            }

            // Get first question with date display is today
            List<Question> questions = db.Question.Where(q => q.DateDisplay != null).ToList();

            foreach (Question q in questions)
            {
                if (q.DateDisplay.Value.ToLocalTime().Equals(DateTime.Now.ToLocalTime()))
                {
                    question = q;
                    // Update date display of the question
                    question.DateDisplay = DateTime.Now.ToLocalTime();
                    db.Entry(question).State = EntityState.Modified;
                    db.SaveChanges();
                    return View(question);
                }
            }

            // Get first question with date display is null
            questions = db.Question.Where(q => q.DateDisplay == null).ToList();
            if (questions.Count() > 0)
            {
                question = questions.First();
            }
            else
            {
                questions = db.Question.Where(q => q.DateDisplay != null).ToList();
                if (questions.Count > 0)
                {
                    Question questionWithMinDate = questions.First();
                    for (int i = 1; i < questions.Count; i++)
                    {
                        // Compare question with min date > question[i] then swap.
                        if (questionWithMinDate.DateDisplay.Value.ToLocalTime().CompareTo(questions[i].DateDisplay.Value.ToLocalTime()) == 1)
                        {
                            questionWithMinDate = questions[i];
                        }
                    }

                    question = questionWithMinDate;
                }
            }

            if (question != null)
            {
                // Update date display of the question
                question.DateDisplay = DateTime.Now.ToLocalTime();
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View(question);
        }

        [HttpGet]
        public JsonResult ReloadQuestions(int? questionId)
        {
            List<Question> questions = db.Question.Where(q => q.DateDisplay == null).ToList();
            if (questions.Count > 0)
            {
                Question q = questions.First();
                q.DateDisplay = DateTime.Now.ToLocalTime();
                db.Entry(q).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { id = q.QuestionID, text = q.Text }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                questions = db.Question.ToList();

                questions.Sort(delegate (Question x, Question y)
                {
                    return x.CompareTo(y);
                });

                Question question = questions.Find(q => q.QuestionID == questionId);
                int index = questions.IndexOf(question);
                Question nextQuestion = null;
                if (index < questions.Count - 1)
                {
                    index += 1;
                }
                else
                {
                    index = 0;
                }

                DateTime date = DateTime.Now.ToLocalTime();
                nextQuestion = questions[index];
                nextQuestion.DateDisplay = date;
                db.Entry(nextQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { id = nextQuestion.QuestionID, text = nextQuestion.Text }, JsonRequestBehavior.AllowGet);
            }

        }

        public string ResponseEmotion(int score, int questionId, string userId, string dateString)
        {
            string[] formats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss",
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm","dd/MM/yyyy hh:mm:ss"};
            
            DateTime dateValue;
            //DateTime now = DateTime.ParseExact(dateString, "M/dd/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
            DateTime.TryParseExact(dateString, formats,
                                    new CultureInfo("en-US"),
                                    DateTimeStyles.None,
                                    out dateValue);
            HappyOrSad.Models.QuestionResponse questionAndResponse = new QuestionResponse()
            {
                Score = score,
                QuestionID = questionId,
                DateSubmitted = dateValue,//DateTime.Now,
                UserId = userId,
            };

            try
            {
                db.QuestionResponse.Add(questionAndResponse);
                db.SaveChanges();
                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}