using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class AnswerQuestionViewModel
    {
        public int QuestionId { get; set; }
      
        [Range(1, 10, ErrorMessage = "The value must be between 1 and 10")]
        public int? Response { get; set; }

        public string QuestionText { get; set; }
    }
}
