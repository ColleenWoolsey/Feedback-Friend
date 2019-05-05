using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class SurveyQuestionsEDITViewModel
    {        
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public bool Assigned { get; set; }
    }
}

