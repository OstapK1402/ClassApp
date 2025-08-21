namespace School.DAL.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int HomeworkSubmissionId { get; set; }
        public int TeacherId { get; set; }
        public int GradeValue { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }

        public HomeworkSubmission HomeworkSubmission { get; set; }
        public User Teacher { get; set; }
    }
}
