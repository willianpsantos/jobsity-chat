using Jobsity.Chat.DB;
using Jobsity.Chat.Interfaces.Repositories;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.Repositories
{
    public class ChatRoomRepository : Repository<ChatModels.ChatRoom, IChatRoomRepository>, IChatRoomRepository
    {
        public ChatRoomRepository(JobsityChatDataContext context) : base(context)
        {
            
        }
    }
}
