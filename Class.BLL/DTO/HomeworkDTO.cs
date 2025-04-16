using Microsoft.AspNetCore.Mvc.ModelBinding;
using School.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace School.BLL.DTO
{
    public class HomeworkDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Клас є обов'язковим.")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Предмет є обов'язковим.")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Вчитель є обов'язковим.")]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Назва завдання є обов'язковою.")]
        [StringLength(200, ErrorMessage = "Назва завдання не повинна перевищувати 200 символів.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Опис є обов'язковим.")]
        [StringLength(2000, ErrorMessage = "Опис не повинен перевищувати 2000 символів.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Дата здачі є обов'язковою.")]
        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; }


        [BindNever]
        public UserDTO? Teacher { get; set; }
        [BindNever]
        public Subject? Subject { get; set; }
        [BindNever]
        public Class? Class { get; set; }
    }
}
