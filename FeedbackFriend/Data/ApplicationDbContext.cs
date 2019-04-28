using System;
using System.Collections.Generic;
using System.Text;
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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                StreetAddress = "123 Infinity Way",
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

            // Create two cohorts
            modelBuilder.Entity<Cohort>().HasData(
                new Cohort()
                {
                    CohortId = 1,
                    Name = "Day Cohort 10"
                },
                new Cohort()
                {
                    CohortId = 2,
                    Name = "Day Cohort 11"
                }
            );

            // Create two students
            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    StudentId = 1,
                    FirstName = "Jakob"
    
                    LastName = "Wildman",
                    CohortId = 2
                },
                new Student()
                {
                    StudentId = 2,
                    FirstName = "Susan"
    
                    LastName = "MacAfee",
                    CohortId = 1
                }
            );
        }
    }