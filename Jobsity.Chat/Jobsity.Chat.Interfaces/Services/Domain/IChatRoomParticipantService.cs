using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;

namespace Jobsity.Chat.Interfaces.Services.Domain
{
    public interface IChatRoomParticipantService : IDomainService<
        SaveChatRoomParticipantRequest,
        ChatRoomParticipantResponse,
        ChatRoomParticipantResponse,
        ChatRoomParticipantResponse,
        ChatRoomParticipantResponse,
        ChatRoomParticipantResponse>
    {
        Task<IEnumerable<ChatRoomParticipantResponse>> GetParticipantsOfChatRoom(string roomId);
    }
}
