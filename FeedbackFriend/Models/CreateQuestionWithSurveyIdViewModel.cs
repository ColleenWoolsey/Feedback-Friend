using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class CreateQuestionWithSurveyIdViewModel
    { 
        public int Id { get; set; }

        public int SurveyId { get; set; }

        public Survey Survey { get; set; }
              
        public int QuestionId { get; set; }
        
        [Display(Name = "Add a question OR finalize survey")]
        public string QuestionText { get; set; }
    }
}
