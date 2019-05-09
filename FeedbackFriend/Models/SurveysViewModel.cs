using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class SurveysViewModel
    {
        [Key]
        public int SurveyId { get; set; }
        public Answer Answer { get; set; }
        public Question Question { get; set; }
        public Survey Survey { get; set; }
        public ApplicationUser User { get; set; }
        public IList<GroupedQuestions> GroupedQuestions { get; set; }
    }
}
