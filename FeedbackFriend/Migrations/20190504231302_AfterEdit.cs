using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FeedbackFriend.Migrations
{
    public partial class AfterEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    SurveyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    SurveyName = table.Column<string>(maxLength: 55, nullable: false),
                    Instructions = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.SurveyId);
                    table.ForeignKey(
                        name: "FK_Surveys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSurvey",
                columns: table => new
                {
                    UserSurveyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResponderId = table.Column<int>(nullable: false),
                    SurveyId = table.Column<int>(nullable: false),
                    FocusId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSurvey", x => x.UserSurveyId);
                    table.ForeignKey(
                        name: "FK_UserSurvey_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SurveyId = table.Column<int>(nullable: false),
                    QuestionText = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Response = table.Column<int>(nullable: false),
                    ResponderId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false),
                    UserSurveyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_UserSurvey_UserSurveyId",
                        column: x => x.UserSurveyId,
                        principalTable: "UserSurvey",
                        principalColumn: "UserSurveyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAssignment",
                columns: table => new
                {
                    SurveyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(nullable: false),
                    SurveyId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAssignment", x => x.SurveyId);
                    table.ForeignKey(
                        name: "FK_QuestionAssignment_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAssignment_Surveys_SurveyId1",
                        column: x => x.SurveyId1,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cb6ef119-9551-4b15-a912-2f6c6223a49f", 0, "93eeed31-eacd-47b6-be82-21965529abbc", "admin@admin.com", true, "admin", "admin", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEIM/O/mfnKolzH6ySEYOWdqCt44TdP2hy0l2BzzJ/kAlesQHLJBEhq4Z7kroOqZnlQ==", null, false, "b0cfb7b6-ac85-45b1-89f8-7123b26e7134", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "Surveys",
                columns: new[] { "SurveyId", "Description", "Instructions", "SurveyName", "UserId" },
                values: new object[,]
                {
                    { 1, "The primary objective of this survey is to collect feedback relative to a person's capacity for walking in another's shoes and how others experience their balance of analysis and sympathy.", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree. Consider your experience of this individual relative to the way they balance analysis and sympathy and relative to your experience of their capacity for walking in another's shoes.", "Empathy", "cb6ef119-9551-4b15-a912-2f6c6223a49f" },
                    { 2, "The primary objective of this survey is twofold. 1. To collect feedback relative to a persons' capacity for passive hearing vs active listening. 2. To asses their attunement to the reality that it's not about what we tell people, but what they hear.", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Listening vs hearing", "cb6ef119-9551-4b15-a912-2f6c6223a49f" },
                    { 3, "The primary objective of this survey is to assess flexibility and responsiveness in communication.", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Just stop talking already", "cb6ef119-9551-4b15-a912-2f6c6223a49f" },
                    { 4, "The primary objective of this survey is to assess capacity for navigating emotional safety needs. How did this person balance the need to avoid pain and potential loss of what they value, danger and insecurity with the objective they were committed to?", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Presentation Feedback", "cb6ef119-9551-4b15-a912-2f6c6223a49f" },
                    { 5, "The primary objective of this survey is to assess the balance between approaching problems aggressively vs reflectively. How much does the need to gain control of one's time factor in problem solving?", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Problem Solving", "cb6ef119-9551-4b15-a912-2f6c6223a49f" },
                    { 6, "What is this person's style of influence? Primarily feeling, or fact? Can they move flexibly between them when it's called for? How much does the need to gain approval factor in their style of influence?", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Influence", "cb6ef119-9551-4b15-a912-2f6c6223a49f" },
                    { 7, "The primary objective of this survey is to assess the balance between necessary stability and unnecessary resistance to change - Does this person prefer the certainty of misery or the misery of uncertainty?", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Change", "cb6ef119-9551-4b15-a912-2f6c6223a49f" },
                    { 8, "The primary objective of this survey is to assess caution vs spontaneity in the quest for excellence. How does this person live in the time warp between carefully weighing options and possibly missing opportunities?", "Responses are on a scale of 1 - 10 where 1 is never/little/strongly disagree and 10 is always/much/strongly agree.", "Decision Making", "cb6ef119-9551-4b15-a912-2f6c6223a49f" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "QuestionText", "SurveyId" },
                values: new object[,]
                {
                    { 1, "In your experience, is this person attentive to the schedules of others?", 1 },
                    { 76, "To what extent do you experience this person as calm and introspective?", 6 },
                    { 75, "How likely is this person to say, 'Let's stop and look at all the evidence'?", 6 },
                    { 74, "How likely is this person to say, 'Trust me, it will work great'?", 6 },
                    { 73, "To what extent do you experience this person as an adept negotiator?", 6 },
                    { 72, "To what extent do you experience this person as optimistic and enthusiastic?", 6 },
                    { 71, "To what extent do you experience this person as unreliable?", 6 },
                    { 70, "To what extent do you experience this person as inattentive - Do they miss what others value?", 6 },
                    { 69, "Do you experience this person as more objective than subjective?", 5 },
                    { 68, "In your experience, how likely is this person to say, 'We could solve this problem if you'd do what I say'?", 5 },
                    { 67, "In your experience, does this person miss opportunities?", 5 },
                    { 77, "To what extent do you experience this person as friendly and outgoing?", 6 },
                    { 66, "In your experience, is this person adept at gathering information?", 5 },
                    { 64, "Do you experience this person as indecisive?", 5 },
                    { 63, "Do you experience this person as apt to waste time and resources?", 5 },
                    { 62, "Do you experience this person as direct?", 5 },
                    { 61, "In your experience, does this person listen as slowly as they speak?", 5 },
                    { 60, "In your experience, does this person display irritation when interrupted?", 5 },
                    { 59, "In your experience, does this person become anxious, slow, or withdrawn in the midst of conflict?", 5 },
                    { 58, "In your experience, does this person become intimidating and confrontational in the midst of conflict?", 5 },
                    { 57, "How likely are you to describe this person as considerate, self-controlled, patient and cooperative?", 5 },
                    { 56, "How likely are you to describe this person as a self-starter, bold, determined and tenacious?", 5 },
                    { 55, "In your experience, how likely is this person to say, 'Let's give it some time'?", 5 },
                    { 65, "Do you experience this person as valuing function over relationship - achieving goals over valuing people?", 5 },
                    { 78, "In your experience of them, does this person become impulsive and unrealistic when under stress?", 6 },
                    { 79, "In your experience of them, does this person become pessimistic and introspective when under stress?", 6 },
                    { 80, "In your experience of them, does this person become skeptical and uncommunicative in the midst of conflict?", 6 },
                    { 103, "In your experience, does this person become more controversial and insensitive when under stress?", 8 },
                    { 102, "In your experience, does this person become more exacting and perfectionistic when under stress?", 8 },
                    { 101, "To what degree do you experience this person as independent?", 8 },
                    { 100, "To what degree do you experience this person as accurate?", 8 },
                    { 99, "To what degree do you experience this person as decisive?", 8 },
                    { 98, "To what degree do you experience this person as having high standards?", 8 },
                    { 97, "To what degree do you experience this person as bold?", 8 },
                    { 96, "To what degree do you experience this person as conscientious?", 8 },
                    { 95, "How likely is this person to say, 'Let's go for it'?", 8 },
                    { 94, "How likely is this person to say, 'I'm not sure yet'?", 8 },
                    { 93, "In your experience, does this person become distracted and impulsive in the midst of conflict?", 7 },
                    { 92, "In your experience, does this person tend toward sullen and stubborn in the midst of conflict?", 7 },
                    { 91, "In your experience, does this person become more intense and reckless under stress?", 7 },
                    { 90, "In your experience, does this person become slow-paced and inflexible under stress?", 7 },
                    { 89, "To what degree do you experience them as flexible?", 7 },
                    { 88, "To what degree do you experience them as spontaneous?", 7 },
                    { 87, "To what degree do you experience them as energetic?", 7 },
                    { 86, "To what degree do you experience this person as methodical?", 7 },
                    { 85, "To what degree do you experience this person as a team-player?", 7 },
                    { 84, "How likely is this person to say, 'Let's try something new'?", 7 },
                    { 83, "How likely is this person to say, 'Let's keep things the way they are'?", 7 },
                    { 82, "To what degree do you experience this person as inspiring?", 6 },
                    { 81, "In your experience of them, does this person become a poor listener in the midst of conflict?", 6 },
                    { 54, "In your experience, how likely is this person to say, 'Let's do it now'?", 5 },
                    { 104, "In your experience, does this person become more indecisive and unyielding in the midst of conflict?", 8 },
                    { 53, "In your experience, to what degree were they able to recognize and affirm what others valued?", 4 },
                    { 51, "In your experience, how much did this person seek to understand others before being understood?", 4 },
                    { 23, "In your experience, does this person stop talking when they're becoming repetitive?", 3 },
                    { 22, "In your experience, does this person stop talking after realizing they're interrupting?", 3 },
                    { 21, "In your experience, what is this person's capacity for productive silence?", 2 },
                    { 20, "In your experience, what percentage of this person's time was spent listening? (Where 1 = 10% and 10 = 100%)", 2 },
                    { 19, "In your experience, did this person take time to understand and validate other points of view before offering solutions?", 2 },
                    { 18, "In your experience, was this person attentive to what wasn't being said?", 2 },
                    { 17, "In your experience, did this person show appreciation for the strengths/pros of others' points of view?", 2 },
                    { 16, "In your experience, did this person validate the concerns of others?", 2 },
                    { 15, "In your experience, did this person acknowledge the concerns of others?", 2 },
                    { 14, "In your experience, was this person easily distracted?", 2 },
                    { 24, "In your experience, how likely is this person to pursue the same conversation again and again?", 3 },
                    { 13, "In your experience, how attentively did this person listen?", 2 },
                    { 11, "In your experience, is this person aware of, and responsive to the level of team morale?", 1 },
                    { 10, "In your experience, how responsive is this person to decreases in momentum during meetings?", 1 },
                    { 9, "In your experience, how insistent is this person that others adapt to their problem solving style?", 1 },
                    { 8, "In your experience, how insistent is this person that others adapt to their communication style?", 1 },
                    { 7, "In your experience, how attuned is this person to the communication styles of others?", 1 },
                    { 6, "In your experience, how attuned is this person to the values of others?", 1 },
                    { 5, "In your experience, how attuned is this person to the likely stress points of their co-workers?", 1 },
                    { 4, "In your experience, how informed is this person about the responsibilities and job scope of others?", 1 },
                    { 3, "In your experience, what is this person's level of focus on measuring appreciable progress toward company objectives?", 1 },
                    { 2, "In your experience, what is this person's level of focus on company objectives?", 1 },
                    { 12, "In your experience, what is their capacity for seeing differing opinions as complimentary rather than adversarial?", 1 },
                    { 25, "In your experience, how much attunement does this person show to others' receptivity?", 3 },
                    { 26, "In your experience, is this person likely to stop talking in order to negotiate time to compose a thoughtful response?", 3 },
                    { 27, "In your experience, what is this person's capacity to create a refuge of silence when others are being unreasonable or irrational?", 3 },
                    { 50, "In your experience, what was this person's level of defensiveness relative to questions?", 4 },
                    { 49, "In your experience, did this person fidget and/or have other distracting gestures?", 4 },
                    { 48, "In your experience, was this person's posture open, relaxed and receptive?", 4 },
                    { 47, "In your experience, did this person maintain good eye contact?", 4 },
                    { 46, "In your opinion, was the material organized?", 4 },
                    { 45, "In your opinion, was there mastery of the subject matter?", 4 },
                    { 44, "In your opinion, was preparation for this presentation in evidence?", 4 },
                    { 43, "In your opinion, was the pace of the presentation hurried?", 4 },
                    { 42, "In your opinion, did the presentation wander off course?", 4 },
                    { 41, "In your experience, did this person deal well with distractions?", 4 },
                    { 40, "In your opinion, did the presentation meet its stated objectives?", 4 },
                    { 39, "In your experience, was this person dismissive of other's concerns?", 4 },
                    { 38, "In your experience, did this person maintain a good balance between talking and listening?", 4 },
                    { 37, "How positively did you experience this person's tone of voice?", 4 },
                    { 36, "How positively did you experience this person's nonverbal communication?", 4 },
                    { 35, "In your experience, how successful was this person in communicating expectations for action/response?", 4 },
                    { 34, "In your experience, how successful was this person in communicating context?", 4 },
                    { 33, "In your opinion, how informative was this presentation?", 4 },
                    { 32, "In your experience, how likely is this person to agree with and act on the phrase, 'Delay is the deadliest form of denial'?", 3 },
                    { 31, "In your experience, how likely is this person to substitute conversation for action - suffer from the paralysis of analysis?", 3 },
                    { 30, "In your experience, does this person have a tendency to offer unsolicited critique?", 3 },
                    { 29, "In your experience, does this person have a tendency to offer unsolicited advice?", 3 },
                    { 28, "In your experience, does this person stop talking when the focus of the group shifts from the topic at hand?", 3 },
                    { 52, "In your experience, how flexible was this person in reframing difficult interactions?", 4 },
                    { 105, "In your experience, does this person become more reckless and overconfident when in the midst of conflict?", 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserId",
                table: "Answers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserSurveyId",
                table: "Answers",
                column: "UserSurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAssignment_QuestionId",
                table: "QuestionAssignment",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAssignment_SurveyId1",
                table: "QuestionAssignment",
                column: "SurveyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSurvey_UserId",
                table: "UserSurvey",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "QuestionAssignment");

            migrationBuilder.DropTable(
                name: "UserSurvey");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
