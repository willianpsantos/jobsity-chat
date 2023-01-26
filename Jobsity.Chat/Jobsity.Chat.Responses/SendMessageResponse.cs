using Jobsity.Chat.Enums;

namespace Jobsity.Chat.Responses
{
    public class SendMessageResponse
    {
        public string? ChatId { get; set; }
        public string? MessageId { get; set; }
        public string? ReceiverUserId { get; set; }
        public string? ReceiverChatRoomId { get; set; }
        public MessageSendingType SendingType { get; set; }

        public string? UserSenderId { get; set; }
        public string? UserSenderName { get; set; }
        public string? UserSenderEmail { get; set; }

        public DateTimeOffset? SentAt { get; set; }
    }
}
