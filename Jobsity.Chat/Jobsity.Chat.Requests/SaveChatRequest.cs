using Jobsity.Chat.Enums;

namespace Jobsity.Chat.Requests
{
    public class SaveChatRequest : Request
    {
        public string? SenderUserId { get; set; }
        public string? ReceiverId { get; set; }
        public MessageReceiverType? ReceiverType { get; set; }

        public string? ReceiverName { get; set; }
        public string? ReceiverEmail { get; set; }

        public Guid? SenderUserUId { get => ParseId(SenderUserId); }
        public Guid? ReceiverUId { get => ParseId(ReceiverId); }
    }
}
