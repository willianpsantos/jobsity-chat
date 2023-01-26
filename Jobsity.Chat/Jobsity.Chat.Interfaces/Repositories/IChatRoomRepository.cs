using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.Interfaces.Repositories
{
    public interface IChatRoomRepository : IRepository<ChatModels.ChatRoom, IChatRoomRepository>
    {
    }
}
