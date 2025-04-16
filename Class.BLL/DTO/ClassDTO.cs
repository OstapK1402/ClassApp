using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace School.BLL.DTO
{
    public class ClassDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва класу є обов'язковою.")]
        [StringLength(100, ErrorMessage = "Назва класу не повинна перевищувати 100 символів.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вчитель є обов'язковим.")]
        public int TeacherId { get; set; }

        public UserDTO? Teacher { get; set; }

        public IEnumerable<SelectListItem>? Teachers { get; set; }
        public List<UserDTO>? Students { get; set; }
        public List<ClassSubjectDTO>? Lessons { get; set; }
    }
}
