using System;
using System.Collections.Generic;
using System.Text;
using FeedbackFriend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FeedbackFriend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Colleen",
                LastName = "Woolsey",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);


            // Create seven surveys
            modelBuilder.Entity<Survey>().HasData(
                new Survey()
                {
                    SurveyId = 1,
                    UserId = user.Id,
                    SurveyName = "Empathy",
                    Description = "The primary objective of this survey is to collect feedback relative to a person's capacity for walking in another's shoes and how others experience their balance of analysis and sympathy.",
                    Instructions = "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree. Consider your experience of this individual relative to the way they balance analysis and sympathy and relative to your experience of their capacity for walking in another's shoes."
                },
                new Survey()
                {
                    SurveyId = 2,
                    UserId = user.Id,
                    SurveyName = "Listening vs hearing",
                    Description = "The primary objective of this survey is twofold. 1. To collect feedback relative to a persons' capacity for passive hearing vs active listening. 2. To asses their attunement to the reality that it's not about what we tell people, but what they hear.",
                    Instructions = "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree."
                },
                new Survey()
                {
                    SurveyId = 3,
                    UserId = user.Id,
                    SurveyName = "Just stop talking already",
                    Description = "The primary objective of this survey is to assess flexibility and responsiveness in communication.",
                    Instructions = "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree."
                },
                new Survey()
                {
                    SurveyId = 4,
                    UserId = user.Id,
                    SurveyName = "Presentation Feedback",
                    Description = "The primary objective of this survey is to assess capacity for navigating emotional safety needs. How did this person balance the need to avoid pain and potential loss of what they value, danger and insecurity with the objective they were committed to?",
                    Instructions = "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree."
                },
                new Survey()
                {
                    SurveyId = 5,
                    UserId = user.Id,
                    SurveyName = "Problem Solving",
                    Description = "The primary objective of this survey is to assess the balance between approaching problems aggressively vs reflectively. How much does the need to gain control of one's time factor in problem solving?",
                    Instructions = "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree."
                },
                new Survey()
                {
                    SurveyId = 6,
                    UserId = user.Id,
                    SurveyName = "Influence",
                    Description = "What is this person's style of influence? Primarily feeling, or fact? Can they move flexibly between them when it's called for? How much does the need to gain approval factor in their style of influence?",
                    Instructions = "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree."
                },
                new Survey()
                {
                    SurveyId = 7,
                    UserId = user.Id,
                    SurveyName = "Change",
                    Description = "The primary objective of this survey is to assess the balance between necessary stability and unnecessary resistance to change - Does this person prefer the certainty of misery or the misery of uncertainty?",
                    Instructions = "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree."
                },
                new Survey()
                {
                    SurveyId = 8,
                    UserId = user.Id,
                    SurveyName = "Decision Making",
                    Description = "The primary objective of this survey is to assess caution vs spontaneity in the quest for excellence. How does this person live in the time warp between carefully weighing options and possibly missing opportunities?",
                    Instructions = "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree."
                }
            );

            // Create many questions
            modelBuilder.Entity<Question>().HasData(
                new Question()
                {
                    QuestionId = 1,
                    SurveyId = 1,
                    QuestionText = "In your experience, is this person attentive to the schedules of others?"
                },
                new Question()
                {
                    QuestionId = 2,
                    SurveyId = 1,
                    QuestionText = "In your experience, what is this person's level of focus on company objectives?"
                },
                new Question()
                {
                    QuestionId = 3,
                    SurveyId = 1,
                    QuestionText = "In your experience, what is this person's level of focus on measuring appreciable progress toward company objectives?"
                },
                new Question()
                {
                    QuestionId = 4,
                    SurveyId = 1,
                    QuestionText = "In your experience, how informed is this person about the responsibilities and job scope of others?"
                },
                new Question()
                {
                    QuestionId = 5,
                    SurveyId = 1,
                    QuestionText = "In your experience, how attuned is this person to the likely stress points of their co-workers?"
                },
                new Question()
                {
                    QuestionId = 6,
                    SurveyId = 1,
                    QuestionText = "In your experience, how attuned is this person to the values of others?"
                },
                new Question()
                {
                    QuestionId = 7,
                    SurveyId = 1,
                    QuestionText = "In your experience, how attuned is this person to the communication styles of others?"
                },
                new Question()
                {
                    QuestionId = 8,
                    SurveyId = 1,
                    QuestionText = "In your experience, how insistent is this person that others adapt to their communication style?"
                },
                new Question()
                {
                    QuestionId = 9,
                    SurveyId = 1,
                    QuestionText = "In your experience, how insistent is this person that others adapt to their problem solving style?"
                },
                new Question()
                {
                    QuestionId = 10,
                    SurveyId = 1,
                    QuestionText = "In your experience, how responsive is this person to decreases in momentum during meetings?"
                },
                new Question()
                {
                    QuestionId = 11,
                    SurveyId = 1,
                    QuestionText = "In your experience, is this person aware of, and responsive to the level of team morale?"
                },
                new Question()
                {
                    QuestionId = 12,
                    SurveyId = 1,
                    QuestionText = "In your experience, what is their capacity for seeing differing opinions as complimentary rather than adversarial?"
                },
                new Question()
                {
                    QuestionId = 13,
                    SurveyId = 2,
                    QuestionText = "In your experience, how attentively did this person listen?"
                },
                new Question()
                {
                    QuestionId = 14,
                    SurveyId = 2,
                    QuestionText = "In your experience, was this person easily distracted?"
                },
                new Question()
                {
                    QuestionId = 15,
                    SurveyId = 2,
                    QuestionText = "In your experience, did this person acknowledge the concerns of others?"
                },
                new Question()
                {
                    QuestionId = 16,
                    SurveyId = 2,
                    QuestionText = "In your experience, did this person validate the concerns of others?"
                },
                new Question()
                {
                    QuestionId = 17,
                    SurveyId = 2,
                    QuestionText = "In your experience, did this person show appreciation for the strengths/pros of others' points of view?"
                },
                new Question()
                {
                    QuestionId = 18,
                    SurveyId = 2,
                    QuestionText = "In your experience, was this person attentive to what wasn't being said?"
                },
                new Question()
                {
                    QuestionId = 19,
                    SurveyId = 2,
                    QuestionText = "In your experience, did this person take time to understand and validate other points of view before offering solutions?"
                },
                new Question()
                {
                    QuestionId = 20,
                    SurveyId = 2,
                    QuestionText = "In your experience, what percentage of this person's time was spent listening? (Where 1 = 10% and 10 = 100%)"
                },
                new Question()
                {
                    QuestionId = 21,
                    SurveyId = 2,
                    QuestionText = "In your experience, what is this person's capacity for productive silence?"
                },
                new Question()
                {
                    QuestionId = 22,
                    SurveyId = 3,
                    QuestionText = "In your experience, does this person stop talking after realizing they're interrupting?"
                },
                new Question()
                {
                    QuestionId = 23,
                    SurveyId = 3,
                    QuestionText = "In your experience, does this person stop talking when they're becoming repetitive?"
                },
                new Question()
                {
                    QuestionId = 24,
                    SurveyId = 3,
                    QuestionText = "In your experience, how likely is this person to pursue the same conversation again and again?"
                },
                new Question()
                {
                    QuestionId = 25,
                    SurveyId = 3,
                    QuestionText = "In your experience, how much attunement does this person show to others' receptivity?"
                },
                new Question()
                {
                    QuestionId = 26,
                    SurveyId = 3,
                    QuestionText = "In your experience, is this person likely to stop talking in order to negotiate time to compose a thoughtful response?"
                },
                new Question()
                {
                    QuestionId = 27,
                    SurveyId = 3,
                    QuestionText = "In your experience, what is this person's capacity to create a refuge of silence when others are being unreasonable or irrational?"
                },
                new Question()
                {
                    QuestionId = 28,
                    SurveyId = 3,
                    QuestionText = "In your experience, does this person stop talking when the focus of the group shifts from the topic at hand?"
                },
                new Question()
                {
                    QuestionId = 29,
                    SurveyId = 3,
                    QuestionText = "In your experience, does this person have a tendency to offer unsolicited advice?"
                },
                new Question()
                {
                    QuestionId = 30,
                    SurveyId = 3,
                    QuestionText = "In your experience, does this person have a tendency to offer unsolicited critique?"
                },
                new Question()
                {
                    QuestionId = 31,
                    SurveyId = 3,
                    QuestionText = "In your experience, how likely is this person to substitute conversation for action - suffer from the paralysis of analysis?"
                },
                new Question()
                {
                    QuestionId = 32,
                    SurveyId = 3,
                    QuestionText = "In your experience, how likely is this person to agree with and act on the phrase, 'Delay is the deadliest form of denial'?"
                },
                new Question()
                {
                    QuestionId = 33,
                    SurveyId = 4,
                    QuestionText = "In your opinion, how informative was this presentation?"
                },
                new Question()
                {
                    QuestionId = 34,
                    SurveyId = 4,
                    QuestionText = "In your experience, how successful was this person in communicating context?"
                },
                new Question()
                {
                    QuestionId = 35,
                    SurveyId = 4,
                    QuestionText = "In your experience, how successful was this person in communicating expectations for action/response?"
                },
                new Question()
                {
                    QuestionId = 36,
                    SurveyId = 4,
                    QuestionText = "How positively did you experience this person's nonverbal communication?"
                },
                new Question()
                {
                    QuestionId = 37,
                    SurveyId = 4,
                    QuestionText = "How positively did you experience this person's tone of voice?"
                },
                new Question()
                {
                    QuestionId = 38,
                    SurveyId = 4,
                    QuestionText = "In your experience, did this person maintain a good balance between talking and listening?"
                },
                new Question()
                {
                    QuestionId = 39,
                    SurveyId = 4,
                    QuestionText = "In your experience, was this person dismissive of other's concerns?"
                },
                new Question()
                {
                    QuestionId = 40,
                    SurveyId = 4,
                    QuestionText = "In your opinion, did the presentation meet its stated objectives?"
                },
                new Question()
                {
                    QuestionId = 41,
                    SurveyId = 4,
                    QuestionText = "In your experience, did this person deal well with distractions?"
                },
                new Question()
                {
                    QuestionId = 42,
                    SurveyId = 4,
                    QuestionText = "In your opinion, did the presentation wander off course?"
                },
                new Question()
                {
                    QuestionId = 43,
                    SurveyId = 4,
                    QuestionText = "In your opinion, was the pace of the presentation hurried?"
                },
                new Question()
                {
                    QuestionId = 44,
                    SurveyId = 4,
                    QuestionText = "In your opinion, was preparation for this presentation in evidence?"
                },
                new Question()
                {
                    QuestionId = 45,
                    SurveyId = 4,
                    QuestionText = "In your opinion, was there mastery of the subject matter?"
                },
                new Question()
                {
                    QuestionId = 46,
                    SurveyId = 4,
                    QuestionText = "In your opinion, was the material organized?"
                },
                new Question()
                {
                    QuestionId = 47,
                    SurveyId = 4,
                    QuestionText = "In your experience, did this person maintain good eye contact?"
                },
                new Question()
                {
                    QuestionId = 48,
                    SurveyId = 4,
                    QuestionText = "In your experience, was this person's posture open, relaxed and receptive?"
                },
                new Question()
                {
                    QuestionId = 49,
                    SurveyId = 4,
                    QuestionText = "In your experience, did this person fidget and/or have other distracting gestures?"
                },
                new Question()
                {
                    QuestionId = 50,
                    SurveyId = 4,
                    QuestionText = "In your experience, what was this person's level of defensiveness relative to questions?"
                },
                new Question()
                {
                    QuestionId = 51,
                    SurveyId = 4,
                    QuestionText = "In your experience, how much did this person seek to understand others before being understood?"
                },
                new Question()
                {
                    QuestionId = 52,
                    SurveyId = 4,
                    QuestionText = "In your experience, how flexible was this person in reframing difficult interactions?"
                },
                new Question()
                {
                    QuestionId = 53,
                    SurveyId = 4,
                    QuestionText = "In your experience, to what degree were they able to recognize and affirm what others valued?"
                },
                new Question()
                {
                    QuestionId = 54,
                    SurveyId = 5,
                    QuestionText = "In your experience, how likely is this person to say, 'Let's do it now'?"
                },
                new Question()
                {
                    QuestionId = 55,
                    SurveyId = 5,
                    QuestionText = "In your experience, how likely is this person to say, 'Let's give it some time'?"
                },
                new Question()
                {
                    QuestionId = 56,
                    SurveyId = 5,
                    QuestionText = "How likely are you to describe this person as a self-starter, bold, determined and tenacious?"
                },
                new Question()
                {
                    QuestionId = 57,
                    SurveyId = 5,
                    QuestionText = "How likely are you to describe this person as considerate, self-controlled, patient and cooperative?"
                },
                new Question()
                {
                    QuestionId = 58,
                    SurveyId = 5,
                    QuestionText = "In your experience, does this person become intimidating and confrontational in the midst of conflict?"
                },
                new Question()
                {
                    QuestionId = 59,
                    SurveyId = 5,
                    QuestionText = "In your experience, does this person become anxious, slow, or withdrawn in the midst of conflict?"
                },
                new Question()
                {
                    QuestionId = 60,
                    SurveyId = 5,
                    QuestionText = "In your experience, does this person display irritation when interrupted?"
                },
                new Question()
                {
                    QuestionId = 61,
                    SurveyId = 5,
                    QuestionText = "In your experience, does this person listen as slowly as they speak?"
                },
                new Question()
                {
                    QuestionId = 62,
                    SurveyId = 5,
                    QuestionText = "Do you experience this person as direct?"
                },
                new Question()
                {
                    QuestionId = 63,
                    SurveyId = 5,
                    QuestionText = "Do you experience this person as apt to waste time and resources?"
                },
                new Question()
                {
                    QuestionId = 64,
                    SurveyId = 5,
                    QuestionText = "Do you experience this person as indecisive?"
                },
                new Question()
                {
                    QuestionId = 65,
                    SurveyId = 5,
                    QuestionText = "Do you experience this person as valuing function over relationship - achieving goals over valuing people?"
                },
                new Question()
                {
                    QuestionId = 66,
                    SurveyId = 5,
                    QuestionText = "In your experience, is this person adept at gathering information?"
                },
                new Question()
                {
                    QuestionId = 67,
                    SurveyId = 5,
                    QuestionText = "In your experience, does this person miss opportunities?"
                },
                new Question()
                {
                    QuestionId = 68,
                    SurveyId = 5,
                    QuestionText = "In your experience, how likely is this person to say, 'We could solve this problem if you'd do what I say'?"
                },
                new Question()
                {
                    QuestionId = 69,
                    SurveyId = 5,
                    QuestionText = "Do you experience this person as more objective than subjective?"
                },
                new Question()
                {
                    QuestionId = 70,
                    SurveyId = 6,
                    QuestionText = "To what extent do you experience this person as inattentive - Do they miss what others value?"
                },
                new Question()
                {
                    QuestionId = 71,
                    SurveyId = 6,
                    QuestionText = "To what extent do you experience this person as unreliable?"
                },
                new Question()
                {
                    QuestionId = 72,
                    SurveyId = 6,
                    QuestionText = "To what extent do you experience this person as optimistic and enthusiastic?"
                },
                new Question()
                {
                    QuestionId = 73,
                    SurveyId = 6,
                    QuestionText = "To what extent do you experience this person as an adept negotiator?"
                },
                new Question()
                {
                    QuestionId = 74,
                    SurveyId = 6,
                    QuestionText = "How likely is this person to say, 'Trust me, it will work great'?"
                },
                new Question()
                {
                    QuestionId = 75,
                    SurveyId = 6,
                    QuestionText = "How likely is this person to say, 'Let's stop and look at all the evidence'?"
                },
                new Question()
                {
                    QuestionId = 76,
                    SurveyId = 6,
                    QuestionText = "To what extent do you experience this person as calm and introspective?"
                },
                new Question()
                {
                    QuestionId = 77,
                    SurveyId = 6,
                    QuestionText = "To what extent do you experience this person as friendly and outgoing?"
                },
                new Question()
                {
                    QuestionId = 78,
                    SurveyId = 6,
                    QuestionText = "In your experience of them, does this person become impulsive and unrealistic when under stress?"
                },
                new Question()
                {
                    QuestionId = 79,
                    SurveyId = 6,
                    QuestionText = "In your experience of them, does this person become pessimistic and introspective when under stress?"
                },
                new Question()
                {
                    QuestionId = 80,
                    SurveyId = 6,
                    QuestionText = "In your experience of them, does this person become skeptical and uncommunicative in the midst of conflict?"
                },
                new Question()
                {
                    QuestionId = 81,
                    SurveyId = 6,
                    QuestionText = "In your experience of them, does this person become a poor listener in the midst of conflict?"
                },
                new Question()
                {
                    QuestionId = 82,
                    SurveyId = 6,
                    QuestionText = "To what degree do you experience this person as inspiring?"
                },
                new Question()
                {
                    QuestionId = 83,
                    SurveyId = 7,
                    QuestionText = "How likely is this person to say, 'Let's keep things the way they are'?"
                },
                new Question()
                {
                    QuestionId = 84,
                    SurveyId = 7,
                    QuestionText = "How likely is this person to say, 'Let's try something new'?"
                },
                new Question()
                {
                    QuestionId = 85,
                    SurveyId = 7,
                    QuestionText = "To what degree do you experience this person as a team-player?"
                },
                new Question()
                {
                    QuestionId = 86,
                    SurveyId = 7,
                    QuestionText = "To what degree do you experience this person as methodical?"
                },
                new Question()
                {
                    QuestionId = 87,
                    SurveyId = 7,
                    QuestionText = "To what degree do you experience them as energetic?"
                },
                new Question()
                {
                    QuestionId = 88,
                    SurveyId = 7,
                    QuestionText = "To what degree do you experience them as spontaneous?"
                },
                new Question()
                {
                    QuestionId = 89,
                    SurveyId = 7,
                    QuestionText = "To what degree do you experience them as flexible?"
                },
                new Question()
                {
                    QuestionId = 90,
                    SurveyId = 7,
                    QuestionText = "In your experience, does this person become slow-paced and inflexible under stress?"
                },
                new Question()
                {
                    QuestionId = 91,
                    SurveyId = 7,
                    QuestionText = "In your experience, does this person become more intense and reckless under stress?"
                },
                new Question()
                {
                    QuestionId = 92,
                    SurveyId = 7,
                    QuestionText = "In your experience, does this person tend toward sullen and stubborn in the midst of conflict?"
                },
                new Question()
                {
                    QuestionId = 93,
                    SurveyId = 7,
                    QuestionText = "In your experience, does this person become distracted and impulsive in the midst of conflict?"
                },
                new Question()
                {
                    QuestionId = 94,
                    SurveyId = 8,
                    QuestionText = "How likely is this person to say, 'I'm not sure yet'?"
                },
                new Question()
                {
                    QuestionId = 95,
                    SurveyId = 8,
                    QuestionText = "How likely is this person to say, 'Let's go for it'?"
                },
                new Question()
                {
                    QuestionId = 96,
                    SurveyId = 8,
                    QuestionText = "To what degree do you experience this person as conscientious?"
                },
                new Question()
                {
                    QuestionId = 97,
                    SurveyId = 8,
                    QuestionText = "To what degree do you experience this person as bold?"
                },
                new Question()
                {
                    QuestionId = 98,
                    SurveyId = 8,
                    QuestionText = "To what degree do you experience this person as having high standards?"
                },
                new Question()
                {
                    QuestionId = 99,
                    SurveyId = 8,
                    QuestionText = "To what degree do you experience this person as decisive?"
                },
                new Question()
                {
                    QuestionId = 100,
                    SurveyId = 8,
                    QuestionText = "To what degree do you experience this person as accurate?"
                },
                new Question()
                {
                    QuestionId = 101,
                    SurveyId = 8,
                    QuestionText = "To what degree do you experience this person as independent?"
                },
                new Question()
                {
                    QuestionId = 102,
                    SurveyId = 8,
                    QuestionText = "In your experience, does this person become more exacting and perfectionistic when under stress?"
                },
                new Question()
                {
                    QuestionId = 103,
                    SurveyId = 8,
                    QuestionText = "In your experience, does this person become more controversial and insensitive when under stress?"
                },
                new Question()
                {
                    QuestionId = 104,
                    SurveyId = 8,
                    QuestionText = "In your experience, does this person become more indecisive and unyielding in the midst of conflict?"
                },
                new Question()
                {
                    QuestionId = 105,
                    SurveyId = 8,
                    QuestionText = "In your experience, does this person become more reckless and overconfident when in the midst of conflict?"
                }
            );
        }
                
        public DbSet<FeedbackFriend.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}