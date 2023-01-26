namespace Jobsity.Chat.Models
{
    public class ChatMessage : JobsityModel
    {
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }

        public Guid? ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }

        public Guid? UserId { get; set; }
        public string Message { get; set; }
        public bool Read { get; set; } = false;
        public DateTimeOffset? ReadAt { get; set; }
    }
}
