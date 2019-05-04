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
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // ******************************************************************************** INDEX
        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Questions.Include(q => q.Survey);
            return View(await applicationDbContext.ToListAsync());
        }


        // ******************************************************************************** DETAILS
        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .Include(q => q.Survey)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // ******************************************************************************** CREATE
        // GET: Questions/Create

        //public async Task<IActionResult> Create(int? surveyId)
        //{
        //    var applicationDbContext = _context.Surveys;

        //    var SurveysViewModel = await _context.Surveys
        //        .Include(s => s.Questions)
        //        .FirstOrDefaultAsync(m => m.SurveyId == surveyId);

        //    //if (SurveysViewModel == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    var model = new SurveysViewModel();

        //    model.GroupedQuestions = await (
        //        from s in _context.Surveys
        //        join q in _context.Questions
        //        on s.SurveyId equals q.SurveyId
        //        group new { s, q } by new { s.SurveyId, s.SurveyName, s.Description, s.Instructions } into grouped
        //        select new GroupedQuestions
        //        {
        //            SurveyDetailId = grouped.Key.SurveyId,
        //            SurveyDetailName = grouped.Key.SurveyName,
        //            SurveyDetailDescription = grouped.Key.Description,
        //            SurveyDetailInstructions = grouped.Key.Instructions,

        //            QuestionCount = grouped.Select(x => x.q.QuestionId).Count(),
        //            Questions = grouped.Select(x => x.q)
        //        }).ToListAsync();

        //    return View(model);
        //}
        // --------------------------------------------------
        //public async Task<IActionResult> Create(int? surveyId)
        //{
        //    var applicationDbContext = _context.Surveys;
            
        //    var surveysViewModel = await _context.Surveys
        //        .Include(s => s.Questions)
        //        .FirstOrDefaultAsync(m => m.SurveyId == surveyId);

        //    if (surveysViewModel == null)
        //    {
        //        return NotFound();
        //    }
        //    var viewModel = new SurveysViewModel();
        //    viewModel.SurveyId = surveysViewModel.SurveyId;
        //    viewModel.SurveyName = surveysViewModel.SurveyName;
        //    viewModel.Description = surveysViewModel.Description;
        //    viewModel.Instructions = surveysViewModel.Instructions;
            
        //    return View(viewModel);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.       

        //public async Task<IActionResult> Create([Bind("SurveyId,QuestionText")] SurveysViewModel surveysViewModel, int surveyId)
        //{
        //    var applicationDbContext = _context.Questions.Include(a => a.Survey);
        //    try
        //    {
        //        var SurveysViewModel = await _context.Questions
        //        .Include(q => q.Survey)
        //        .FirstOrDefaultAsync(m => m.SurveyId == surveyId);

        //        //if (SurveysViewModel == null)
        //        //{
        //        //    return NotFound();
        //        //}              

        //        if (ModelState.IsValid)
        //        {
        //            var viewModel = new SurveysViewModel();
        //            {
        //                viewModel.SurveyId = surveyId;
        //                viewModel.QuestionText = surveysViewModel.QuestionText;
        //            };

                    
                   
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        // PopulateDepartmentsDropDownList(question.DepartmentId);
        //        return View(surveysViewModel);
        //        //ViewData.Add("SurveyId", value: "surveyId");
        //        //     ViewData.Add("QuestionText", value: "Questiontext");
        //        //    await _context.SaveChangesAsync();
        //        //    return RedirectToAction(nameof(Create));
                
        //    }
        //    catch (DbUpdateException /* ex */)
        //    {
        //        //Log the error (uncomment ex variable name and write a log.
        //        ModelState.AddModelError("", "Unable to save changes. " +
        //            "Try again, and if the problem persists " +
        //            "see your system administrator.");
        //    }
        //    return View(surveysViewModel);
        //}

        // TEMPLATE: return RedirectToAction("{controller=Home}/{action=Index}/{id?}");
        // return RedirectToAction("Create", "Questions", "question.SurveyId");   



        // ****************************************************************************************** EDIT
        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "SurveyName", question.SurveyId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionId,SurveyId,QuestionText")] Question question)
        {
            if (id != question.QuestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.QuestionId))
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
            ViewData["SurveyId"] = new SelectList(_context.Surveys, "SurveyId", "SurveyName", question.SurveyId);
            return View(question);
        }


        // ****************************************************************************************** DELETE
        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Survey)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }

        //-----------------------------------------------------------------------------------------------------

        // GET: Surveys with Questions/Create

        public async Task<IActionResult> Create(int? surveyId)
        {
            var applicationDbContext = _context.Surveys;

            var surveysViewModel = await _context.Surveys
                .Include(s => s.Questions)
                .FirstOrDefaultAsync(m => m.SurveyId == surveyId);

            if (surveysViewModel == null)
            {
                return NotFound();
            }
            var viewModel = new SurveysViewModel();
            viewModel.SurveyId = surveysViewModel.SurveyId;
            viewModel.SurveyName = surveysViewModel.SurveyName;
            viewModel.Description = surveysViewModel.Description;
            viewModel.Instructions = surveysViewModel.Instructions;
            viewModel.IEnumQuestions = surveysViewModel.Questions;
                        
            return View(viewModel);
        }
        //public async Task<IActionResult> Create(int? surveyId, SurveysViewModel surveysViewModel)
        //{
        //    var applicationDbContext = _context.Surveys;

            //    var survey = await _context.Surveys
            //        .Include(s => s.Questions)
            //        .FirstOrDefaultAsync(m => m.SurveyId == surveyId);

            //    if (surveysViewModel == null)
            //    {
            //        return NotFound();
            //    }
            //    var viewModel = new SurveysViewModel();

            //    if (surveyId == null)
            //    {
            //        return NotFound();
            //    }

            //    //var survey = await _context.Surveys
            //    //    .Include(s => s.Questions)
            //    //    .AsNoTracking()
            //    //    .FirstOrDefaultAsync(m => m.SurveyId == surveyId);
            //    //if (survey == null)
            //    //{
            //    //    return NotFound();
            //    //}

            //    survey.Questions = new List<Question>();
            //    // PopulateAssignedQuestionData(surveysViewModel);
            //    return View(surveysViewModel);
            //}

            // POST: Surveys/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionId,SurveyId,QuestionText")] SurveysViewModel surveysViewModel, int surveyId)
        {
            var applicationDbContext = _context.Questions;

            surveysViewModel.SurveyId = surveyId;

            var questionvar = await _context.Questions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.SurveyId == surveyId);

            if (questionvar == null)
            {
                return NotFound();
            }

            if (surveysViewModel != null)
            {
                surveysViewModel.IEnumQuestions = new List<Question>();
                foreach (var question in surveysViewModel.IEnumQuestions)
                {
                    var questionVar = new Question
                    {
                        // QuestionId = surveysViewModel.QuestionId,
                        SurveyId = surveysViewModel.SurveyId,
                        QuestionText = surveysViewModel.QuestionText,
                    };
                    //surveysViewModel.IEnumQuestions.Add(questionToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                var questionVar = new Question
                {
                    // QuestionId = surveysViewModel.QuestionId,
                    SurveyId = surveysViewModel.SurveyId,
                    QuestionText = surveysViewModel.QuestionText,
                };

                _context.Add(questionVar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            ViewData["SurveyId"] = surveyId;
            ViewData["QuestionText"] = surveysViewModel.QuestionText;            

            // PopulateAssignedQuestionData(surveysViewModel);

            return View(surveysViewModel);
        }

        //private void PopulateAssignedQuestionData(SurveysViewModel surveysViewModel)
        //{
        //    var allQuestions = _context.Questions;
        //    var surveyQuestions = new HashSet<int>(surveysViewModel.Questions.Select(q => q.QuestionId));
        //    var viewModel = new List<SurveysViewModel>();
        //    foreach (var question in allQuestions)
        //    {
        //        viewModel.Add(new SurveysViewModel
        //        {
        //            QuestionId = question.QuestionId,
        //            SurveyId = question.SurveyId,
        //            QuestionText = question.QuestionText,
        //            Assigned = surveyQuestions.Contains(question.QuestionId)
        //        });
        //    }
        //    ViewData["Questions"] = viewModel;
        //}



    }
}
