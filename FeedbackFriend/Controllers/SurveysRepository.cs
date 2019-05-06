using FeedbackFriend.Data;
using FeedbackFriend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Controllers
{
    public class SurveysRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        
        public SurveyQuestionsListViewModel GetSurveyQuestionsDisplay(Guid surveyid)
        {
            var applicationDbContext = _context.Surveys.Include(s => s.User);            

            if (surveyid != null && surveyid != Guid.Empty)
            {
                using (var context = new ApplicationDbContext())
                {
                    var surveyRepo = new SurveysRepository();
                    var survey = surveyRepo.GetSurveyQuestionsDisplay(surveyid);
                    if (survey != null)
                    {
                        var surveyQuestionsListVm = new SurveyQuestionsListViewModel()
                        {
                            SurveyId = survey.SurveyId,
                            SurveyName = survey.SurveyName,
                            FullName = context.Users.Find(survey.FullName).FullName,
                            Description = survey.Description,
                            Instructions = survey.Instructions
                            // RegionNameEnglish = context.Regions.Find(survey.RegionCode).RegionNameEnglish
                        };

                        List<QuestionDisplayViewModel> questionList = context.Questions.AsNoTracking()
                            .Where(x => x.SurveyId == surveyid).Select(x =>
                           new QuestionDisplayViewModel
                           {
                               SurveyId = x.SurveyId,
                               QuestionId = x.QuestionId,
                               QuestionText = x.QuestionText
                           }).ToList();
                        surveyQuestionsListVm.Questions = questionList;
                        return surveyQuestionsListVm;
                    }
                }
            }
            return null;
        }

        public void SaveQuestions(List<QuestionDisplayViewModel> questions)
        {
            if (questions != null)
            {
                using (var context = new ApplicationDbContext())
                {
                    foreach (var question in questions)
                    {
                        var record = context.Questions.Find(question.QuestionId);
                        if (record != null)
                        {
                            record.QuestionText = question.QuestionText;
                        }
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
