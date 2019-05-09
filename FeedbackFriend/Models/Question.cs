using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class Question
    {       
        [Key]
        [Display(Name = "Question ID")]
        public int QuestionId { get; set; }

        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }

        [Required]
        public int SurveyId { get; set; }

        public Survey Survey { get; set; }
        public Answer Answer { get; set; }
    }
}
