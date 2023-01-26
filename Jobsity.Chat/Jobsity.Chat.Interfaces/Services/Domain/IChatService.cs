using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;

namespace Jobsity.Chat.Interfaces.Services.Domain
{
    public interface IChatService : IDomainService<
        SaveChatRequest, 
        ChatResponse, 
        ChatResponse, 
        ChatResponse, 
        ChatResponse, 
        ChatResponse>
    {

    }
}
