namespace Jobsity.Chat.Options
{
    public class SocketOptions
    {
        public string? ServerUrl { get; set; }
        public string? ServerPath { get; set; }
        public bool? Reconnection { get; set; }
        public int? ConnectionTimeout { get; set; }
        public string? SendMessageUrl { get; set; }
    }
}
