using Jobsity.Chat.Enums;

namespace Jobsity.Chat.Models
{
    public class Chat : JobsityModel
    {
        public Guid? SenderUserId { get; set; }
        public Guid? ReceiverId { get; set; }
        public MessageReceiverType? ReceiverType { get; set; } 

        public string? ReceiverName { get; set; }
        public string? ReceiverEmail { get; set; }

        public virtual ICollection<ChatMessage> Messages { get;set; }
    }
}
