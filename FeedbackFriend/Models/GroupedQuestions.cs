using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class GroupedQuestions
    {       
        public int ID { get; set; }
        public int GroupedSurveyId { get; set; }            
        public string GroupedSurveyName { get; set; }
        public string GroupedSurveyDescription { get; set; }
        public string GroupedSurveyInstructions { get; set; }

        public int GroupedQuestionId { get; set; }
        public string GroupedQuestionText { get; set; }
        public int GroupedQuestionCount { get; set; }

        public int GroupedUserId { get; set; }                
        public string GroupedFirstName { get; set; }                
        public string GroupedLastName { get; set; }        
        public string GroupedFullName
        {
            get
            {
                return $"{GroupedFirstName} {GroupedLastName}";
            }
        }

        public Answer Answer { get; set; }
        public IEnumerable<Question> Questions { get; set; }       
    }
}
