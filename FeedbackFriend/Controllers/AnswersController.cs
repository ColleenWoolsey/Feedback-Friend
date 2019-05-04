using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeedbackFriend.Data;
using FeedbackFriend.Models;

namespace FeedbackFriend.Controllers
{
    public class AnswersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnswersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Answers.Include(a => a.Question).Include(a => a.UserSurvey);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Question)
                .Include(a => a.UserSurvey)
                .FirstOrDefaultAsync(m => m.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionText");
            ViewData["UserSurveyId"] = new SelectList(_context.Set<UserSurvey>(), "UserSurveyId", "UserSurveyId");
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswerId,Response,ResponderId,QuestionId,UserSurveyId")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionText", answer.QuestionId);
            ViewData["UserSurveyId"] = new SelectList(_context.Set<UserSurvey>(), "UserSurveyId", "UserSurveyId", answer.UserSurveyId);
            return View(answer);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionText", answer.QuestionId);
            ViewData["UserSurveyId"] = new SelectList(_context.Set<UserSurvey>(), "UserSurveyId", "UserSurveyId", answer.UserSurveyId);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswerId,Response,ResponderId,QuestionId,UserSurveyId")] Answer answer)
        {
            if (id != answer.AnswerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.AnswerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionText", answer.QuestionId);
            ViewData["UserSurveyId"] = new SelectList(_context.Set<UserSurvey>(), "UserSurveyId", "UserSurveyId", answer.UserSurveyId);
            return View(answer);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Question)
                .Include(a => a.UserSurvey)
                .FirstOrDefaultAsync(m => m.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.AnswerId == id);
        }
    }
}


//.jumbotron {
//    padding: 2rem 1rem;
//    margin-bottom: 2rem;
//    /*background-image: linear-gradient(to right, #8085ff,#8094ff,#80a3ff,#80b3ff,#80c2ff,#80d1ff,#80e0ff);*/
//    background-image: radial-gradient(white 5%, #ecf4ff 15%, #80b3ff 60%);
//    /*background-image: radial-gradient(circle, #ffcc80, #ecf4ff, #80b3ff);*/
//    /*background-image: repeating-radial-gradient(#ecf4ff, #80b3ff 10%, #b6deff 15%);*/
//    /*background-image: linear-gradient(#80b3ff, ecf4ff);*/
//    /*background-color: #80b3ff;*/
//    /*background-image: url("~/Images/presents.png");*/
//    border-radius: 0.3rem;
//}