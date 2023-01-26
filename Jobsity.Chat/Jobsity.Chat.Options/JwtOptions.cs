using Microsoft.IdentityModel.Tokens;

namespace Jobsity.Chat.Options
{
    public class JwtOptions
    {
        public int? ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }
}