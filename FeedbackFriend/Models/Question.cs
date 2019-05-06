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
        public virtual Survey Survey { get; set; }

        public string QuestionText { get; set; }
    }
}
