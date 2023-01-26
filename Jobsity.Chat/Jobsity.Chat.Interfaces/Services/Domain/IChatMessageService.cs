using Jobsity.Chat.Enums;
using Jobsity.Chat.Core.Interfaces;
using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;

namespace Jobsity.Chat.Interfaces.Services.Domain
{
    public interface IChatMessageService : IDomainService<
        SaveChatMessageRequest,
        ChatMessageResponse,
        ChatMessageResponse,
        ChatMessageResponse,
        ChatMessageResponse,
        ChatMessageResponse>
    {
        Task<IPageableResponse<ChatMessageResponse>> GetLastestMessages(GetLastestMessagesRequest request);
    }
}
