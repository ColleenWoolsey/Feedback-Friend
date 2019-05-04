using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class QuestionAssignment
    {
        [Key]
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public Survey Survey { get; set; }
        public Question Question { get; set; }
    }
}
