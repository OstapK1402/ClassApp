using System.ComponentModel.DataAnnotations;

namespace School.App.Models
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Ім'я є обов'язковим.")]
        [StringLength(100, ErrorMessage = "Ім'я не повинно перевищувати 100 символів.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Прізвище є обов'язковим.")]
        [StringLength(100, ErrorMessage = "Прізвище не повинно перевищувати 100 символів.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Дата народження є обов'язковою.")]
        public DateTime DateOfBirth { get; set; }
    }
}
