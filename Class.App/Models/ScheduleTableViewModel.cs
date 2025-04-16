namespace School.App.Models
{
    public class ScheduleTableViewModel
    {
        public string ClassName { get; set; }

        // Всі можливі слоти (наприклад: 8:30, 10:10, 11:50)
        public List<TimeSpan> TimeSlots { get; set; }

        // Таблиця: [час][день] = список предметів
        public Dictionary<TimeSpan, Dictionary<DayOfWeek, ScheduleCellViewModel>> Schedule { get; set; }
    }

    public class ScheduleCellViewModel
    {
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
    }
}
