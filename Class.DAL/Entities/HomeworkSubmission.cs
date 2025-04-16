namespace School.DAL.Entities
{
    public class HomeworkSubmission
    {
        public int Id { get; set; }
        public int HomeworkId { get; set; }
        public int StudentId { get; set; }
        public string FilePath { get; set; }
        public DateTime SubmittedAt { get; set; }

        public Homework? Homework { get; set; }
        public User? Student { get; set; }
    }
}
