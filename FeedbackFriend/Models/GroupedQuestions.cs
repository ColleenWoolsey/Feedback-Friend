using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class GroupedQuestions
    {
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }

        public int SurveyDetailId { get; set; }
        public string SurveyDetailName { get; set; }
        public string SurveyDetailDescription { get; set; }
        public string SurveyInstructions { get; set; }
        public int QuestionCount { get; set; }

        public Question Question { get; set; }
        public Survey Survey { get; set; }
        public IEnumerable<Question> Questions { get; set; }       
    }
}
