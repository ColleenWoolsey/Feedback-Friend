using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class SurveyQuestionsListViewModel
    {
        public int SurveyId { get; set; }

        public int QuestionId { get; set; }

        [Required]
        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }

        public bool Assigned { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Survey Name")]
        public string SurveyName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Instructions")]
        public string Instructions { get; set; }
    }
}