using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace School.BLL.DTO
{
    public class ClassSubjectDTO
    {
        [Required(ErrorMessage = "Клас є обов'язковим.")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Предмет є обов'язковим.")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Вчитель є обов'язковим.")]
        public int TeacherId { get; set; }

        [BindNever]
        public UserDTO? Teacher { get; set; }
        [BindNever]
        public Subject? Subject { get; set; }
        [BindNever]
        public Class? Class { get; set; }

        public IEnumerable<SelectListItem>? Classes { get; set; }
        public IEnumerable<SelectListItem>? Subjects { get; set; }
        public IEnumerable<SelectListItem>? Teachers { get; set; }
    }
}
