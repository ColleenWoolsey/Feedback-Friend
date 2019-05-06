using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class QuestionDisplayViewModel
    {
        [Key]
        public int ID { get; set; }

        public int SurveyId { get; set; }

        [Display(Name = "Question ID")]
        public int QuestionId { get; set; }

        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }

        public bool Assigned { get; set; }
    }
}
