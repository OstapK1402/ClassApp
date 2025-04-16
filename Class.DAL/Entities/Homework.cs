namespace School.DAL.Entities
{
    public class Homework
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Class Class { get; set; }
        public Subject Subject { get; set; }
        public User Teacher { get; set; }
    }
}
