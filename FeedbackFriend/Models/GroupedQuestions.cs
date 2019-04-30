using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class GroupedQuestions
    {
        public int SurveyDetailId { get; set; }
        public string SurveyDetailName { get; set; }
        public int QuestionCount { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}
