namespace Jobsity.Chat.Requests
{
    public class SaveChatRoomParticipantRequest : Request
    {
        public string? ChatRoomId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }

        public Guid? ChatRoomUid { get => ParseId(ChatRoomId); }
        public Guid? UserUid { get => ParseId(UserId); }
    }
}
