using Jobsity.Chat.DB;
using Jobsity.Chat.Interfaces.Repositories;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.Repositories
{
    public class ChatMessageRepository : Repository<ChatModels.ChatMessage, IChatMessageRepository>, IChatMessageRepository
    {
        public ChatMessageRepository(JobsityChatDataContext context) : base(context)
        {
            
        }
    }
}
