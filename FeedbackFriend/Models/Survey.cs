using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class Survey
    {
        [Key]
        public int SurveyId { get; set; }

        [Required]
        public int CreatedById { get; set; }

        [Required]
        public string SurveyName { get; set; }


        public string Instructions { get; set; }


        public string Description { get; set; }


        public ICollection<Question> Questions { get; set; }
    }
}
