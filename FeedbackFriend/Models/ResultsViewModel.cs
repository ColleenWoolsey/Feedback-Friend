using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class ResultsViewModel
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string Instructions { get; set; }
        public string Description { get; set; }

        public string FocusUserId { get; set; }
        public string FocusUserName { get; set; }

        public int Response { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }

        public int QuestionAverage {get; set;}
    }
}

