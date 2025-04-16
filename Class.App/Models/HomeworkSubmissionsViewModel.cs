using School.BLL.DTO;

namespace School.App.Models
{
    public class HomeworkSubmissionsViewModel
    {
        public HomeworkDTO Homework { get; set; }
        public List<StudentSubmissionStatus> Students { get; set; }
    }
}
