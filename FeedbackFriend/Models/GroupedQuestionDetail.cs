using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class GroupedQuestionDetail
    {
        public Question Question { get; set; }
        public Survey Survey { get; set; }
        public List<GroupedQuestions> GroupedQuestions { get; set; }        
               
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string Instructions { get; set; }
        public string Description { get; set; }
    }
}
