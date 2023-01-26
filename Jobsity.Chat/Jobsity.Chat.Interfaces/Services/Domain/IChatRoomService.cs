using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;

namespace Jobsity.Chat.Interfaces.Services.Domain
{
    public interface IChatRoomService : IDomainService<
        SaveChatRoomRequest, 
        ChatRoomResponse, 
        ChatRoomResponse, 
        ChatRoomResponse, 
        ChatRoomResponse, 
        ChatRoomResponse>
    {
        Task<IEnumerable<ChatRoomResponse>?> GetAllRoomsOfLoggedUser();
    }
}
