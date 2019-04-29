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
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]        
        [StringLength(51, ErrorMessage = "Please shorten the survey name to 55 characters")]       
        public string SurveyName { get; set; }

        [StringLength(255)]        
        public string Instructions { get; set; }

        [StringLength(255)]        
        public string Description { get; set; }

        [NotMapped]
        public int NumQuestions { get; set; }

        public ICollection<Question> Questions { get; set; }
        
        // public virtual ICollection<Question> Questions { get; set; } ???
    }
}
