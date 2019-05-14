using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class AnswerCreateViewModel
    {          
        public int SurveyId { get; set; }

        public string SurveyName { get; set; }

        public string Instructions { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "A Feedback Recipient must be Selected")]
        [Display(Name = "Feedback Recipient")]
        public string FocusUserId { get; set; }
        public string FocusUserName { get; set; }
        public IEnumerable<SelectListItem> Recipients { get; set; }

        public string ResponderUserId { get; set; }
        public string ResponderUserName { get; set; }
        public ApplicationUser ResponderUser { get; set; }
                       
        public string QuestionText { get; set; }

        public DateTime ResponseDate { get; set; }

        public List<AnswerQuestionViewModel> AnswerQuestionViewModels { get; set; }
    }
}
