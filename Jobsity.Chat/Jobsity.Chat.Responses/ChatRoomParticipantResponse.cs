namespace Jobsity.Chat.Responses
{
    public class ChatRoomParticipantResponse : DataResponse
    {
        public Guid? Id { get; set; }
        public Guid? ChatRoomId { get; set; }
        public string? ChatRoomName { get; set; }
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
    }
}
