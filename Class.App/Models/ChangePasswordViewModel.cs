using System.ComponentModel.DataAnnotations;

namespace School.App.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Enter your current password.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Enter your new password.")]
        [StringLength(100, ErrorMessage = "Password must contain at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
