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
        public int QuestionId { get; set; }

        [Required]
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        [Required]
        [Display(Name = "Add a question OR finalize survey")]
        public string QuestionText { get; set; }
    }
}
