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
using Microsoft.AspNetCore.Routing;

namespace FeedbackFriend.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public SurveysController(ApplicationDbContext context,
                                  UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        //any method that needs to see who the user is can invoke the method
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // ******************************************************************************** INDEX
        // GET: Surveys
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Surveys.Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: IQUERYABLE is a way to list stuff !!!!!
        public async Task<IActionResult> ListStuff()
        {
            IQueryable<GroupedQuestions> data =
                from question in _context.Questions
                group question by question.SurveyId into questionGroup
                select new GroupedQuestions()
                {
                    GroupedSurveyId = questionGroup.Key,
                    GroupedQuestionCount = questionGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        // ********************************************************************************LoggedIn
        public async Task<IActionResult> LoggedIn()
        {
            var applicationDbContext = _context.Surveys
               .Include(s => s.Questions)
               .Include(s => s.User)
               .OrderBy(s => s.SurveyName);
            //.Take(20);

            return View(await applicationDbContext.ToListAsync());
        }



        // ******************************************************************************** DETAILS
        // GET: Surveys/Details/5
        public async Task<IActionResult> Details(int? id)
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



        // ******************************************************************************** CREATE
        // GET: Surveys/Create

        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SurveyId,UserId,SurveyName,Instructions,Description")] Survey survey)
        {
            ModelState.Remove("User");
            ModelState.Remove("userId");
            var user = await GetCurrentUserAsync();
            survey.UserId = user.Id;

            if (ModelState.IsValid)
            {
                _context.Add(survey);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Questions", new { id = survey.SurveyId });
            }

            return View(survey);
        }



        // ********************************************************************************  DELETE
        // GET: Surveys/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await GetCurrentUserAsync();

            var survey = await _context.Surveys
            .Include(s => s.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.SurveyId == id);

            if (survey == null)
            {
                ViewData["ErrorMessage"] =
                    "You may only delete surveys you created";
                // return NotFound();
            }

            if (user.Id != survey.UserId)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }
            return View(survey);
        }
        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var survey = await _context.Surveys.FindAsync(id);
            if (survey == null)
            {
                return RedirectToAction(nameof(LoggedIn));
            }

            try
            {
                _context.Surveys.Remove(survey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(LoggedIn));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        private bool SurveyExists(int id)
        {
            return _context.Surveys.Any(e => e.SurveyId == id);
        }



        // ****************************************************************************************  EDIT
        // GET: Surveys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        // POST: Surveys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurveyId,UserId,SurveyName,Description,Instructions")] Survey survey)
        {
            if (id != survey.SurveyId)
            {
                return NotFound();
            }
            ModelState.Remove("User");
            ModelState.Remove("UserId");

            ApplicationUser user = await GetCurrentUserAsync();

            survey.User = user;
            survey.UserId = user.Id;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(survey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyExists(survey.SurveyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(LoggedIn));
            }
            return View(survey);
        }
    }
}




//        // GET: Surveys/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var survey = await _context.Surveys.FindAsync(id);
//            //.Include(i => i.QuestionAssignments).ThenInclude(i => i.Question)
//            //.AsNoTracking()
//            //.FirstOrDefaultAsync(i => i.SurveyId == id);

//            if (survey == null)
//            {
//                return NotFound();
//            }

//            //PopulateAssignedQuestionData(survey);
//            return View(survey);
//        }

//        private void PopulateAssignedQuestionData(Survey survey)
//        {
//            var allQuestions = _context.Questions.Where(q => q.SurveyId == survey.SurveyId);
//            var surveyQuestions = new HashSet<int>(survey.QuestionAssignments.Select(q => q.QuestionId));
//            var viewModel = new List<SurveyQuestionsListViewModel>();
//            foreach (var question in allQuestions)
//            {
//                viewModel.Add(new SurveyQuestionsListViewModel
//                {
//                    QuestionId = question.QuestionId,
//                    QuestionText = question.QuestionText,
//                    Assigned = surveyQuestions.Contains(question.QuestionId)
//                });
//            }
//            ViewData["Questions"] = viewModel;
//        }
//        // POST: Surveys/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("SurveyName,Description,Instructions")] Survey survey)
//        {
//            if (id != survey.SurveyId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(survey);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!SurveyExists(survey.SurveyId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(LoggedIn));
//            }

//            // PopulateAssignedQuestionData(survey)
//            return View(survey);
//        }
//    }
//}
// POST: Surveys/Edit/5
//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> Edit(int? id, string[] selectedQuestions)
//{
//    if (id == null)
//    {
//        return NotFound();
//    }

//    var surveyToUpdate = await _context.Surveys
//        .Include(i => i.QuestionAssignments)
//            .ThenInclude(i => i.Question)
//        .FirstOrDefaultAsync(m => m.SurveyId == id);

//    if (await TryUpdateModelAsync<Survey>(
//        surveyToUpdate,
//        "",
//        i => i.Description, i => i.Instructions, i => i.SurveyName))
//    {

//        UpdateSurveyQuestions(selectedQuestions, surveyToUpdate);
//        try
//        {
//            await _context.SaveChangesAsync();
//        }
//        catch (DbUpdateException /* ex */)
//        {
//            //Log the error (uncomment ex variable name and write a log.)
//            ModelState.AddModelError("", "Unable to save changes. " +
//                "Try again, and if the problem persists, " +
//                "see your system administrator.");
//        }
//        return RedirectToAction(nameof(Index));
//    }
//    UpdateSurveyQuestions(selectedQuestions, surveyToUpdate);
//    PopulateAssignedQuestionData(surveyToUpdate);
//    return View(surveyToUpdate);
//}

//private void UpdateSurveyQuestions(string[] selectedQuestions, Survey surveyToUpdate)
//{
//    if (selectedQuestions == null)
//    {
//        surveyToUpdate.QuestionAssignments = new List<QuestionAssignment>();
//        return;
//    }

//    var selectedQuestionsHS = new HashSet<string>(selectedQuestions);
//    var surveyQuestions = new HashSet<int>
//        (surveyToUpdate.QuestionAssignments.Select(c => c.Question.QuestionId));
//    foreach (var question in _context.Questions)
//    {
//        if (selectedQuestionsHS.Contains(question.QuestionId.ToString()))
//        {
//            if (!surveyQuestions.Contains(question.QuestionId))
//            {
//                surveyToUpdate.QuestionAssignments.Add(new QuestionAssignment { SurveyId = surveyToUpdate.SurveyId, QuestionId = question.QuestionId });
//            }
//        }
//        else
//        {

//            if (surveyQuestions.Contains(question.QuestionId))
//            {
//                QuestionAssignment questionToRemove = surveyToUpdate.QuestionAssignments.FirstOrDefault(i => i.QuestionId == question.QuestionId);
//                _context.Remove(questionToRemove);
//            }
//        }
//    }
//}

