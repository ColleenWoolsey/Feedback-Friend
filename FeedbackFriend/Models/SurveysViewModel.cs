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
    }
}
