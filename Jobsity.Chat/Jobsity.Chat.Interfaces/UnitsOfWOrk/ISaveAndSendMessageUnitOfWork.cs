using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;

namespace Jobsity.Chat.Interfaces.UnitsOfWOrk
{
    public interface ISaveAndSendMessageUnitOfWork
    {
        Task<ChatMessageResponse> SaveAndSendAsync(SaveChatMessageRequest request);
    }
}
