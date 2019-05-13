using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{    
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        [Required]
        [Display(Name = "Feedback")]
        [Range(1, 10, ErrorMessage = "The value must be between 1 and 10")]
        public int? Response { get; set; }

        [Required]
        [Display(Name = "Feedback Provider")]
        public string ResponderId { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        [Required]
        [Display(Name = "Feedback Recipient")]
        public string FocusId { get; set; } 
        
        public DateTime ResponseDate { get; set; }

        public ApplicationUser User { get; set; }       
    }    
}
