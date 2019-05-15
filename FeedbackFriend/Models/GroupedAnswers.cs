using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class GroupedAnswers
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }

        public int FocusResponse { get; set; }

        public int ResponseSum { get; set; }
        public int QuestionAverage { get; set; }
        
    }
}
