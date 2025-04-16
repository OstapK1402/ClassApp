namespace School.DAL.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }

        public User Teacher { get; set; }

        public ICollection<User> Students { get; set; }
        public ICollection<ClassSubject> Lessons { get; set; }
    }
}
