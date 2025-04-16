using System.ComponentModel.DataAnnotations;

namespace School.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Електронна пошта є обов'язковою.")]
        [EmailAddress(ErrorMessage = "Неправильний формат електронної пошти.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль є обов'язковим.")]
        [StringLength(100, ErrorMessage = "Пароль має бути від {2} до {1} символів.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ім'я є обов'язковим.")]
        [StringLength(100, ErrorMessage = "Ім'я не повинно перевищувати 100 символів.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Прізвище є обов'язковим.")]
        [StringLength(100, ErrorMessage = "Прізвище не повинно перевищувати 100 символів.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Дата народження є обов'язковою.")]
        public DateTime DateOfBirth { get; set; }

        public int? ClassId { get; set; }

        public string? FullName => $"{FirstName} {LastName}";
    }
}
