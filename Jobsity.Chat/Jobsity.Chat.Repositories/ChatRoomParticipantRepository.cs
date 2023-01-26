using Jobsity.Chat.DB;
using Jobsity.Chat.Interfaces.Repositories;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.Repositories
{
    public class ChatRoomParticipantRepository : Repository<ChatModels.ChatRoomParticipant, IChatRoomParticipantRepository>, IChatRoomParticipantRepository
    {
        public ChatRoomParticipantRepository(JobsityChatDataContext context) : base(context)
        {
            
        }
    }
}
