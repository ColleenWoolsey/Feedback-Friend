using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class CreateQuestionWithSurveyIdViewModel
    { 
        public int SurveyId { get; set; }

        [Display(Name = "Survey Name")]        
        public string SurveyName { get; set; }

        public string Instructions { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public int NumQuestions { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
