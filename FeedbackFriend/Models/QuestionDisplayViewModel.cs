using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class QuestionDisplayViewModel
    {
        public Guid SurveyId { get; set; }

        [Display(Name = "Question ID")]
        public Guid QuestionId { get; set; }

        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }
    }
}
