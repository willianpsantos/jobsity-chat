using Jobsity.Chat.DB;
using Jobsity.Chat.Interfaces.Repositories;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.Repositories
{
    public class ChatRepository: Repository<ChatModels.Chat, IChatRepository>, IChatRepository
    {
        public ChatRepository(JobsityChatDataContext context) : base(context)
        {
            
        }
    }
}
