using Jobsity.Chat.Core.Interfaces;
using Jobsity.Chat.Enums;

namespace Jobsity.Chat.Responses
{
    public class ChatMessageResponse : DataResponse, IMessageEventEmitter
    {
        public Guid? Id { get; set; }
        public Guid? ChatId { get; set; }
        public ChatResponse? Chat { get; set; }
        public Guid? ChatRoomId { get;set; }
        public Guid? UserId { get; set; }
        public string? Message { get; set; }
        public bool? Read { get; set; }
        public DateTimeOffset? ReadAt { get; set; }

        public string? EmitEventName { get; set; }
        public MessageSendingType SendingType { get; set; }
        public bool? StockQuote { get; set; } = false;
    }
}
