using Jobsity.Chat.Core.Interfaces;

namespace Jobsity.Chat.Interfaces.Services.Thirds
{
    public interface IMessageSenderService
    {
        Task SendMessageAsync<TMessageData>(TMessageData messageData) where TMessageData : IMessageEventEmitter;
    }
}
