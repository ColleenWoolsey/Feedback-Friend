using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class SurveyQuestionsListViewModel
    {
        //[Key]
        //public int ID { get; set; }

        public int SurveyId { get; set; }

        public int QuestionId { get; set; }

        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }

        public bool Assigned { get; set; }        

        public int UserId { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Survey Name")]
        public string SurveyName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Instructions")]
        public string Instructions { get; set; }

        public List<QuestionDisplayViewModel> Questions { get; set; }
    }
}
