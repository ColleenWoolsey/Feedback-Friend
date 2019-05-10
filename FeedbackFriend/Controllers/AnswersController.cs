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


        // ************************************************************************  INDEX
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Answers.Include(a => a.Question);
            return View(await applicationDbContext.ToListAsync());
        }


        // ************************************************************************  COMPLETE
        public async Task<IActionResult> Complete(int? id)
        {
            var model = new AnswerCreateViewModel();

            var surveyDB = await _context.Surveys.FirstOrDefaultAsync(m => m.SurveyId == id);                    

            var user = await GetCurrentUserAsync();
            var userDB = await _context.Users.ToListAsync();
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");

            var questionDB = await _context.Questions.Where(q => q.SurveyId == id).ToListAsync();

            if (model == null)
            {
                return NotFound();
            }

            var vmitem = new List<AnswerQuestionViewModel>();

            foreach (var question in questionDB)
            {
                AnswerQuestionViewModel taco = new AnswerQuestionViewModel();
                {
                    taco.QuestionId = question.QuestionId;
                    taco.QuestionText = question.QuestionText;
                    //taco.Response = question.Answer.Response;
                };                

                vmitem.Add(taco);
            }

            var viewModel = new AnswerCreateViewModel();
            viewModel.SurveyId = surveyDB.SurveyId;
            viewModel.SurveyName = surveyDB.SurveyName;
            viewModel.Description = surveyDB.Description;
            viewModel.Instructions = surveyDB.Instructions;
            viewModel.AnswerQuestionViewModels = model.AnswerQuestionViewModels;

            ViewData["ResponderId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CompletePost(AnswerCreateViewModel ViewModel)
        {
            var user = await GetCurrentUserAsync();
            // for (int i = 0; i < ViewModel.AnswerQuestionViewModel.Count; i++)
            {
                Answer answer = new Answer
                {
                    ResponderId = user.UserId,
                    QuestionId = ViewModel.AnswerQuestionViewModels[i].QuestionId,
                    // Response = ViewModel.AnswerQuestionViewModels.Response[i],
                };
                _context.Add(answer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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
            ModelState.Remove("ResponderId");
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
