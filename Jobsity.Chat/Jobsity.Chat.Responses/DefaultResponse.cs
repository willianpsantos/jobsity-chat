namespace Jobsity.Chat.Responses
{
    public class DefaultResponse
    {
        public bool Success { get; set; } = false;
        public ICollection<string> Errors { get; set; }

        public DefaultResponse() => Errors = new HashSet<string>();

        public void AddError(string error) => Errors.Add(error);
    }
}
