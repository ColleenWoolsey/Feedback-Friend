using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public enum Response
    {
        A, B, C, D, F
    }

    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        [Required]
        [Display(Name = "Feedback")]
        [Range(1, 10, ErrorMessage = "The value must be between 1 and 10")]
        public int Response { get; set; }

        [Required]
        public int ResponderId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public Question Question { get; set; }        

        [Required]
        public int UserSurveyId { get; set; }
        public UserSurvey UserSurvey { get; set; }
    }    
}
