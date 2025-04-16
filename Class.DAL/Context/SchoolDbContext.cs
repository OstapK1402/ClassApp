using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School.DAL.Context.EntityConfiguration;
using School.DAL.Entities;

namespace School.DAL.Context
{
    public class SchoolDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Subject> Subjects  { get; set; }
        public virtual DbSet<Schedule> Schedules  { get; set; }
        public virtual DbSet<HomeworkSubmission> HomeworkSubmissions  { get; set; }
        public virtual DbSet<Homework> Homeworks  { get; set; }
        public virtual DbSet<Grade> Grades  { get; set; }
        public virtual DbSet<ClassSubject> ClassSubjects  { get; set; }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
            //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClassConfiguration());
            modelBuilder.ApplyConfiguration(new ClassSubjectConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
            modelBuilder.ApplyConfiguration(new HomeworkConfiguration());
            modelBuilder.ApplyConfiguration(new HomeworkSubmissionConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
