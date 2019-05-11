using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class Survey
    {
        [Key]
        public int SurveyId { get; set; }

        [Required]
        [Display(Name = "Created By")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }        

        [Required]
        [Display(Name = "Survey Name")]
        [StringLength(55, ErrorMessage = "Please shorten the survey name to 55 characters")]
        public string SurveyName { get; set; }

        public string Instructions { get; set; }

        public string Description { get; set; }

        public bool Assigned { get; set; }

        [NotMapped]
        public int NumQuestions { get; set; }

        public List<Question> Questions { get; set; }
        
        public List<SurveyAssignment> SurveyAssignments { get; set; }
    }
}
