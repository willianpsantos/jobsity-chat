namespace Jobsity.Chat.Models
{
    public class ChatRoom : JobsityModel
    {
        public string Name { get; set; }

        public virtual ICollection<ChatRoomParticipant> Participants { get; set; }
        public virtual ICollection<ChatMessage> Messages { get; set; }
    }
}