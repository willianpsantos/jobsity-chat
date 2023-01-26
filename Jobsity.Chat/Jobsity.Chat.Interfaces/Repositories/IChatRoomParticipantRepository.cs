using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.Interfaces.Repositories
{
    public interface IChatRoomParticipantRepository : IRepository<ChatModels.ChatRoomParticipant, IChatRoomParticipantRepository>
    {
    }
}
