using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class SurveysViewModel
    {
        public Question Question { get; set; }
        public Survey Survey { get; set; }
        public List<GroupedQuestions> GroupedQuestions { get; set; }

        public string QuestionText { get; set; }
        public int QuestionId { get; set; }

        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string Instructions { get; set; }
        public string Description { get; set; }

        public bool Assigned { get; set; }

       //  public List<Question> QuestionList { get; set; }
        public IEnumerable<Question> IEnumQuestions { get; set; }

        // public ICollection<Question> Questions { get; set; }

    }
}
