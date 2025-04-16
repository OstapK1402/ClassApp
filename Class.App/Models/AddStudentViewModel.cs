using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace School.App.Models
{
    public class AddStudentViewModel
    {
        [Required]
        public int ClassId { get; set; }
        public List<SelectListItem>? Students { get; set; }

        [Required]
        public int StudentId { get; set; }

        public List<SelectListItem>? Classes { get; set; }
    }
}
