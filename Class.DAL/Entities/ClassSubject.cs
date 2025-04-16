namespace School.DAL.Entities
{
    public class ClassSubject
    {
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }

        public Class Class { get; set; }
        public Subject Subject { get; set; }
        public User Teacher { get; set; }
    }
}
