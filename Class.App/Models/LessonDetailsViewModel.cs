using School.BLL.DTO;

namespace School.App.Models
{
    public class LessonDetailsViewModel
    {
        public ClassSubjectDTO ClassSubject { get; set; }
        public IEnumerable<HomeworkDTO> Homeworks { get; set; }
    }
}
