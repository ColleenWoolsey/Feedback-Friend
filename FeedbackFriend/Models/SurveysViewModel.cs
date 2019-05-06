﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackFriend.Models
{
    public class SurveysViewModel
    {
        // public int Id { get; set; }

        public Question Question { get; set; }
        public Survey Survey { get; set; }
        public List<GroupedQuestions> GroupedQuestions { get; set; }
       
        public int UserId { get; set; }
       
        public int OpenId { get; set; }
        public SurveysViewModel(int openId)
        {
            this.OpenId = openId;
        }

        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }
        public int QuestionId { get; set; }

        public int SurveyId { get; set; }
        [Display(Name = "Survey Name")]
        public string SurveyName { get; set; }
        public string Instructions { get; set; }
        public string Description { get; set; }

        public bool Assigned { get; set; }
       
        public IEnumerable<Question> IEnumQuestions { get; set; }
    }
}
