namespace Jobsity.Chat.Requests
{
    public class Request
    {
        public string? Id { get; set; }
        public Guid? Uid { get => ParseId(Id); }

        protected Guid? ParseId(string? id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
                return null;

            return Guid.Parse(id);
        }
    }
}
