using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace School.BLL.DTO
{
    public class HomeworkSubmissionDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Домашнє завдання є обов'язковим.")]
        public int HomeworkId { get; set; }

        [Required(ErrorMessage = "Студент є обов'язковим.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Файл є обов'язковим.")]
        [StringLength(500, ErrorMessage = "Шлях до файлу не повинен перевищувати 500 символів.")]
        public string FilePath { get; set; }

        public DateTime SubmittedAt { get; set; }

        public IFormFile? File { get; set; }

        [BindNever]
        public UserDTO? Student { get; set; }
    }
}
