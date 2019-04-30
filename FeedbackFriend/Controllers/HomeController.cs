using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeedbackFriend.Models;
using Microsoft.AspNetCore.Identity;
using FeedbackFriend.Data;
using Microsoft.EntityFrameworkCore;

namespace FeedbackFriend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Privacy()
        {
            return View();
        }
        //setting private reference to the I.D.F usermanager
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        //Getting the current user in the system (whoever is logged in)
        public Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public HomeController(ApplicationDbContext context,
                                  UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoggedIn()
        {
            var applicationDbContext = _context.Surveys
               .Include(s => s.Questions)
               .Include(s => s.User)
               .OrderBy(s => s.SurveyName);
               //.Take(20);

            return View(await applicationDbContext.ToListAsync());
        }

        // **********************************************
        // GET: All Question Detail for each Survey
        public async Task<IActionResult> QuestionsForEachSurvey()       
        {
            var model = new SurveysViewModel();

            // Build list of Survey instances for display in view
            // The LINQ statement groups the survey entities by surveyId,
            // calculates the number of questions in each group, 
            // and stores the results in a collection of SurveyQuestionGroup view model objects.

            model.GroupedQuestions = await (
                from s in _context.Surveys
                join q in _context.Questions
                on s.SurveyId equals q.SurveyId                
                group new { s, q } by new { s.SurveyId, s.SurveyName } into grouped
                select new GroupedQuestions
                {
                    SurveyDetailId = grouped.Key.SurveyId,
                    SurveyDetailName = grouped.Key.SurveyName,
                    QuestionCount = grouped.Select(x => x.q.QuestionId).Count(),
                    Questions = grouped.Select(x => x.q)
                }).ToListAsync();

            return View(model);
        }


        // GET: Survey/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Surveys
               .Include(s => s.Questions)
               .FirstOrDefaultAsync(m => m.SurveyId == id);           

            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
