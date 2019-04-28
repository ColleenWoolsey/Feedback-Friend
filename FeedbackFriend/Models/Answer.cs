using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public enum Response
    {
        A, B, C, D, F
    }

    public class Answer    {
        public int AnswerId { get; set; }
        public int ResponderId { get; set; }
        public int UserSurveyId { get; set; }
        public int QuestionId { get; set; }
        public Response? Response { get; set; }

        public Survey Survey { get; set; }
        public Question Question { get; set; }
        public User User { get; set; }
    }    
}
