using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.Interfaces.Repositories
{
    public interface IChatMessageRepository : IRepository<ChatModels.ChatMessage, IChatMessageRepository>
    {
    }
}
