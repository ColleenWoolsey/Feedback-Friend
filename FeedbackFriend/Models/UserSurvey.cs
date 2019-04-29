using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class UserSurvey
    {
        [Key]
        public int UserSurveyId { get; set; }


        public int ResponderId { get; set; }


        public int SurveyId { get; set; }


        public int FocusId { get; set; }


        public ApplicationUser User { get; set; }
    }
}
