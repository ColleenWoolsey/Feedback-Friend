using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeedbackFriend.Data;
using FeedbackFriend.Models;
using Microsoft.AspNetCore.Identity;

namespace FeedbackFriend.Controllers
{
    public class AnswersController : Controller
    {
        private readonly ApplicationDbContext _context;

        //access the currently authenticated user
        private readonly UserManager<ApplicationUser> _userManager;

        //inject the UserManager service in the constructor
        public AnswersController(ApplicationDbContext context,
                                  UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // !!!  Any method that needs to see who the user is can invoke the method
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Answers
                .Include(a => a.User)
                .Include(a => a.Question)
                .ThenInclude(q => q.Survey);
            return View(await applicationDbContext.ToListAsync());
        }


        //------------------------------------------------------------------------------------ CompleteSurvey
        public async Task<IActionResult> ContextAnswer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Answers
                .Include(a => a.Question)
                .Where(q => q.QuestionId == id).ToListAsync();

            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }



        //-------------------------------------------------------------------------------------- ReturnSurvey
        public async Task<IActionResult> ContextSurvey(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Surveys
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(m => m.SurveyId == id);

            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        public async Task<IActionResult> ContextQuestion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Surveys
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(m => m.SurveyId == id);

            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        //------------------------------------------------------------------------------- JoinQuestionsAnswers
        public async Task<IActionResult> JoinQuestionsAnswers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = new SurveysViewModel();

            model.GroupedQuestions = await (
                from q in _context.Questions
                join s in _context.Surveys
                on q.SurveyId equals s.SurveyId
                join a in _context.Answers
                on q.QuestionId equals a.QuestionId
                group new { s, q } by new { s.SurveyId, s.SurveyName } into grouped
                select new GroupedQuestions
                {
                    GroupedSurveyId = grouped.Key.SurveyId,
                    GroupedSurveyName = grouped.Key.SurveyName,
                    GroupedQuestionCount = grouped.Select(x => x.q.QuestionId).Count(),
                    Questions = grouped.Select(x => x.q)
                }).ToListAsync();

            return View(model);
        }

        //--------------------------------------------------------------------------------------------- CREATE
        public async Task<IActionResult> Create(int? id)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return NotFound();
            }

            var surveyQuestionListViewModel = await _context.Surveys.FindAsync(id);

            surveyQuestionListViewModel = await _context.Surveys
                        .Include(i => i.SurveyAssignments).ThenInclude(i => i.Question)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(i => i.SurveyId == id);


            if (surveyQuestionListViewModel == null)
            {
                return NotFound();
            }
            var viewModel = new SurveyQuestionsListViewModel();
            viewModel.SurveyId = surveyQuestionListViewModel.SurveyId;
            viewModel.SurveyName = surveyQuestionListViewModel.SurveyName;
            viewModel.Description = surveyQuestionListViewModel.Description;
            viewModel.Instructions = surveyQuestionListViewModel.Instructions;

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("AnswerId,Response,QuestionId,FocusId,ResponderId")] Answer answer)
        {
            ModelState.Remove("User");
            ModelState.Remove("responderId");
            var user = await GetCurrentUserAsync();
            answer.ResponderId = user.UserId;

            if (ModelState.IsValid)
            {
                _context.Add(answer);
                await _context.SaveChangesAsync();
                return RedirectToAction("CompleteSurvey", new { id = answer.QuestionId });
            }

            return View(answer);
        }

        //        surveyQuestionsListViewModel.SurveyId = id;

        //    if (ModelState.IsValid)
        //    {
        //        var newQuestion = new Question();
        //        newQuestion.SurveyId = surveyQuestionsListViewModel.SurveyId;
        //        newQuestion.QuestionText = surveyQuestionsListViewModel.QuestionText;

        //        _context.Add(newQuestion);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Details", "Surveys", new { id = newQuestion.SurveyId });
        //    }
        //    return View(surveyQuestionsListViewModel);
        //}



        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Question)
                .FirstOrDefaultAsync(m => m.AnswerId == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        //// GET: Answers/Create
        //public IActionResult Create()
        //{
        //    ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionText");

        //    return View();
        //}

        //// POST: Answers/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("AnswerId,Response,ResponderId,QuestionId,FocusId")] Answer answer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(answer);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "QuestionText", answer.QuestionId);
        //    return View(answer);
        //}

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
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswerId,Response,ResponderId,QuestionId,FocusId")] Answer answer)
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
