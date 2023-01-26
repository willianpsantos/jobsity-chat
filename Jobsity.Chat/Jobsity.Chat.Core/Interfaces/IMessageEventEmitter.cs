using Jobsity.Chat.Enums;

namespace Jobsity.Chat.Core.Interfaces
{
    public interface IMessageEventEmitter
    {
        string? EmitEventName { get; set; }
        MessageSendingType SendingType { get; set; }
    }
}
