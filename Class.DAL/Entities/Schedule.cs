namespace School.DAL.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan TimeSlot { get; set; }

        public Class Class { get; set; }
        public Subject Subject { get; set; }
        public User Teacher { get; set; }
    }
}
