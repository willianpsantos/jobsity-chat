using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.Interfaces.Repositories
{
    public interface IChatRepository : IRepository<ChatModels.Chat, IChatRepository>
    {
    }
}
