namespace Jobsity.Chat.Requests
{
    public class SaveChatMessageRequest : Request
    {
        public string? SenderUserId { get; set; }
        public string? ChatId { get; set; }
        public string? ChatRoomId { get;set; }
        public string? UserId { get; set; }
        public string? Message { get; set; }
        public bool? Read { get; set; }
        public DateTimeOffset? ReadAt { get; set; }

        public Guid? ChatUid { get => ParseId(ChatId); }
        public Guid? ChatRoomUid { get => ParseId(ChatRoomId); }
        public Guid? UserUid { get => ParseId(UserId); }
        public Guid? SenderUserUid {  get => ParseId(SenderUserId); }
    }
}
