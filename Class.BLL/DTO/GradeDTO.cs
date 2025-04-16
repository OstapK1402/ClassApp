using System.ComponentModel.DataAnnotations;

namespace School.BLL.DTO
{
    public class GradeDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Домашнє завдання є обов'язковим.")]
        public int HomeworkSubmissionId { get; set; }

        [Required(ErrorMessage = "Вчитель є обов'язковим.")]
        public int TeacherId { get; set; }

        [Range(1, 12, ErrorMessage = "Оцінка повинна бути від 1 до 12.")]
        public int GradeValue { get; set; }

        [StringLength(500, ErrorMessage = "Коментар не повинен перевищувати 500 символів.")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Дата є обов'язковою.")]
        public DateTime Date { get; set; }
    }
}