using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class ResultsViewModel
    {        
        public string SurveyName { get; set; }       
        public string Description { get; set; }
        public IList<GroupedAnswers> GroupedAnswers { get; set; }
    }
}

