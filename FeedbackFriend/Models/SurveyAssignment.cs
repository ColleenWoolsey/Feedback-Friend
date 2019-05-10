using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class SurveyAssignment
    {
        [Key]
        public int SurveyAssignmentId { get; set; }
        public int SurveyId { get; set; }       
        public string UserId { get; set; }
        public string FocusId { get; set; }
        public bool Assigned { get; set; }

        public Survey Survey { get; set; }
        public Question Question { get; set; }
        public ApplicationUser User { get; set; }
        public List<GroupedQuestions> GroupedQuestions { get; set; }
    }
}
