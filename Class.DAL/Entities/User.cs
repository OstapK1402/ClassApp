using Microsoft.AspNetCore.Identity;

namespace School.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public int? ClassId { get; set; }
        public Class Class { get; set; }

        public ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }

        public ICollection<Class> ClassesAsTeacher { get; set; }
        public ICollection<ClassSubject> ClassSubjectsAsTeacher { get; set; }
        public ICollection<Homework> HomeworksCreated { get; set; }
        public ICollection<Grade> GradesGiven { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
