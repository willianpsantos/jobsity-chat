using Jobsity.Chat.Enums;

namespace Jobsity.Chat.Requests
{
    public class GetLastestMessagesRequest
    {
        public string ReceiverId { get; set; }
        public MessageReceiverType ReceiverType { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; } = 1;
    }
}
