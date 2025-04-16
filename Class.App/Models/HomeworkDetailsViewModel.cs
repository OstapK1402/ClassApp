using School.BLL.DTO;

namespace School.App.Models
{
    public class HomeworkDetailsViewModel
    {
        public HomeworkDTO Homework { get; set; }
        public HomeworkSubmissionDTO? Submission { get; set; }

        public GradeDTO? Grade { get; set; }
    }
}
