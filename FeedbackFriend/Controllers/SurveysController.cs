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


        // Created automatically redirects me to SurveysController Index Action - So I will put the view I
        // need returned after initializing survey but before adding questions here for now

        // ********************************************************************************
        // GET: Surveys
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Surveys.Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }


        // ********************************************************************************
        // GET: Surveys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survey = await _context.Surveys
                .Include(a => a.Questions)
                .FirstOrDefaultAsync(m => m.SurveyId == id);
            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }


        // ********************************************************************************
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        public async Task<IActionResult> Create([Bind("UserId,SurveyName,Instructions,Description")] Survey survey)
        {
            ModelState.Remove("User");
            ModelState.Remove("userId");
            var user = await GetCurrentUserAsync();
            survey.UserId = user.Id;

            if (ModelState.IsValid)
            {
                _context.Add(survey);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Questions", new { survey.SurveyId });
            }

            return View(survey);
        }


        // ********************************************************************************
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", survey.UserId);
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurveyId,UserId,SurveyName,Instructions,Description")] Survey survey)
        {
            if (id != survey.SurveyId)
            {
                return NotFound();
            }

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
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", survey.UserId);
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

            var survey = await _context.Surveys
                .Include(s => s.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SurveyId == id);
            if (survey == null)
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
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Surveys.Remove(survey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
    }
}
