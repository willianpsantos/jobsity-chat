using System.ComponentModel.DataAnnotations;

namespace Jobsity.Chat.Requests
{
    public class AuthenticateUserRequest
    {
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage ="{0} is invalid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string? Password { get; set; }
    }
}