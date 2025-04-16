using System.ComponentModel.DataAnnotations;

namespace School.BLL.DTO
{
    public class SubjectDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва предмету є обов'язковою.")]
        [StringLength(100, ErrorMessage = "Назва предмету не повинна перевищувати 100 символів.")]
        public string Name { get; set; }
    }
}
