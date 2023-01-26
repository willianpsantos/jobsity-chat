namespace Jobsity.Chat.Models
{
    public class ChatRoomParticipant : JobsityModel
    {
        public Guid ChatRoomId { get; set; }
        public ChatRoom? ChatRoom { get; set; }

        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
    }
}
