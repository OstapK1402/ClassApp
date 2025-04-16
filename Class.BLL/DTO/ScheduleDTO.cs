using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace School.BLL.DTO
{
    public class ScheduleDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Клас є обов'язковим.")]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Предмет є обов'язковим.")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Вчитель є обов'язковим.")]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "День тижня є обов'язковим.")]
        public DayOfWeek DayOfWeek { get; set; }

        [Required(ErrorMessage = "Часовий слот є обов'язковим.")]
        public TimeSpan TimeSlot { get; set; }

        [BindNever]
        public UserDTO? Teacher { get; set; }
        [BindNever]
        public Subject? Subject { get; set; }
        [BindNever]
        public Class? Class { get; set; }

        public List<SelectListItem>? Subjects { get; set; }
        public List<SelectListItem>? Teachers { get; set; }
    }
}
