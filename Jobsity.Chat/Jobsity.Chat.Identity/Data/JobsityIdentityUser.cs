using Microsoft.AspNetCore.Identity;

namespace Jobsity.Chat.Identity.Data
{
    public class JobsityIdentityUser : IdentityUser
    {
        public string? Fullname { get; set; }
    }
}
