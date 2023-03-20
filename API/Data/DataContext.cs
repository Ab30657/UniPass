using System.Net.Sockets;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        protected readonly IConfiguration _configuration;
        public DataContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_configuration.GetConnectionString("POSTGRESQLCONNSTR_DefaultConnection"));
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<PerformanceIndicator> PerformanceIndicators { get; set; }
        public DbSet<TakeAssignment> TakeAssignments { get; set; }
        public DbSet<TakeQuestion> TakeQuestions { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<PIScore> PIScores { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Takes> Takes { get; set; }
        /////////////
        /// @deprecated includes old relationships 
        /////////////
        /// The following represent relationships not entities
        ///////////// 
        // public DbSet<AnswerAttempt> AnswerAttempt { get; set; }
        // public DbSet<CoursePI> CoursePI { get; set; }
        // public DbSet<QuestionPI> QuestionPI { get; set; }
        // public DbSet<Teaches> Teaches { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
            builder.Entity<AppRole>()
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

            builder.Entity<Student>()
                .HasOne(x => x.AppUser)
                .WithOne(x => x.Student)
                .HasForeignKey<Student>(x => x.AppUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Instructor>()
                .HasOne(x => x.AppUser)
                .WithOne(x => x.Instructor)
                .HasForeignKey<Instructor>(x => x.AppUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Takes>().HasKey(x => new { x.StudentId, x.CourseId, x.SemesterId });
            builder.Entity<Takes>()
                .HasOne(t => t.Student)
                .WithMany(s => s.Takes)
                .HasForeignKey(t => t.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Takes>()
                .HasOne(t => t.Course)
                .WithMany(c => c.Takes)
                .HasForeignKey(t => t.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Takes>()
                .HasOne(t => t.Semester)
                .WithMany(c => c.Takes)
                .HasForeignKey(t => t.SemesterId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Teaches>().HasKey(t => new { t.InstructorId, t.CourseId, t.SemesterId });
            builder.Entity<Teaches>()
                .HasOne(t => t.Instructor)
                .WithMany(i => i.Teaches)
                .HasForeignKey(t => t.InstructorId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Teaches>()
                .HasOne(t => t.Course)
                .WithMany(c => c.Teaches)
                .HasForeignKey(t => t.CourseId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Teaches>()
                .HasOne(t => t.Semester)
                .WithMany(c => c.Teaches)
                .HasForeignKey(t => t.SemesterId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CoursePI>().HasKey(k => new { k.CourseId, k.PerformanceIndicatorId });
            builder.Entity<CoursePI>()
                .HasOne(cp => cp.Course)
                .WithMany(c => c.CoursePIs)
                .HasForeignKey(cp => cp.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<CoursePI>()
                .HasOne(cp => cp.PerformanceIndicator)
                .WithMany(pi => pi.CoursePIs)
                .HasForeignKey(cp => cp.PerformanceIndicatorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<QuestionPI>().HasKey(k => new { k.QuestionId, k.PerformanceIndicatorId });
            builder.Entity<QuestionPI>()
                .HasOne(cp => cp.Question)
                .WithMany(c => c.QuestionPIs)
                .HasForeignKey(cp => cp.QuestionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<QuestionPI>()
                .HasOne(cp => cp.PerformanceIndicator)
                .WithMany(pi => pi.QuestionPIs)
                .HasForeignKey(cp => cp.PerformanceIndicatorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TakeQuestion>()
                .HasOne(t => t.TakeAssignment)
                .WithMany(i => i.TakeQuestions)
                .HasForeignKey(t => t.TakeAssignmentId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TakeQuestion>()
                .HasOne(t => t.Question)
                .WithMany(c => c.TakeQuestions)
                .HasForeignKey(t => t.QuestionId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TakeQuestion>()
                .HasOne(t => t.Answer)
                .WithMany(c => c.TakeQuestions)
                .HasForeignKey(t => t.AnswerId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TakeAssignment>()
                .HasOne(t => t.Student)
                .WithMany(i => i.TakeAssignments)
                .HasForeignKey(t => t.StudentId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<TakeAssignment>()
                .HasOne(t => t.Assignment)
                .WithMany(c => c.TakeAssignments)
                .HasForeignKey(t => t.AssignmentId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PIScore>().HasKey(x => new { x.TakeAssignmentId, x.PerformanceIndicatorId });
            builder.Entity<PIScore>()
                .HasOne(x => x.PerformanceIndicator)
                .WithMany(x => x.PIScores)
                .HasForeignKey(x => x.PerformanceIndicatorId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<PIScore>()
                .HasOne(x => x.TakeAssignment)
                .WithMany(x => x.PIScores)
                .HasForeignKey(x => x.TakeAssignmentId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
