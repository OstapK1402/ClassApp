namespace School.App.Models
{
    public class StudentSubmissionStatus
    {
        public int StudentId { get; set; }
        public string StudentFullName { get; set; }
        public bool HasSubmitted { get; set; }
        public int? SubmissionId { get; set; }
        public DateTime? SubmittedAt { get; set; }

        public int? GradeValue { get; set; }
    }
}
