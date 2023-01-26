using System.Text.Json.Serialization;

namespace Jobsity.Chat.Responses
{
    public class AuthenticateUserResponse : DefaultResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Token { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? ExpireAt { get; set; }

    }
}