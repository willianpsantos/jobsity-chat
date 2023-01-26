namespace Jobsity.Chat.Responses
{
    public class DataResponse
    {
        public DateTimeOffset? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
