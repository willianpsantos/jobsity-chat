using System.ComponentModel.DataAnnotations;

namespace Jobsity.Chat.Requests
{
    public class SaveUserRequest
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "{0} must have from {2} to {1} characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0} must have from {2} to {1} characters.")]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Password confirmation is different from Password")]
        public string? PasswordConfirm { get; set; }
    }
}
