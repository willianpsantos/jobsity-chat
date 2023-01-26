using Jobsity.Chat.Core;
using Jobsity.Chat.Core.Interfaces;
using Jobsity.Chat.Interfaces.Identity;
using Jobsity.Chat.Interfaces.Services.Thirds;
using Jobsity.Chat.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Jobsity.Chat.Services.Thirds
{
    public class MessageSenderService : BaseService, IMessageSenderService
    {
        private readonly SocketOptions _socketOptions;
        private readonly MessageSendingEventsOptions _messageEventsOptions;

        public MessageSenderService(
           IIdentityService identityService,
           IHttpContextAccessor contextAccessor,
           IOptions<SocketOptions> socketOptions,
           IOptions<MessageSendingEventsOptions> messageEventsOptions
        ) : base(identityService, contextAccessor)
        {
            _socketOptions = socketOptions.Value;
            _messageEventsOptions = messageEventsOptions.Value;
        }

        private string? GetProperlyEventName(IMessageEventEmitter request)
        {
            string? eventName = "";

            switch(request.SendingType) 
            {
                case Enums.MessageSendingType.ToSingleUser:
                    eventName = _messageEventsOptions.SendMessageSingleUserEventName?.ProcessTemplate(request);
                    break;
                case Enums.MessageSendingType.ToChatRoom:
                default:
                    eventName = _messageEventsOptions.SendMessageChatRoomEventName?.ProcessTemplate(request);                
                    break;
            }

            return eventName;
        }

        public async Task SendMessageAsync<TMessageData>(TMessageData messageData) where TMessageData : IMessageEventEmitter
        {
            messageData.EmitEventName = GetProperlyEventName(messageData);

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_socketOptions.ServerUrl);

                var response = await client.PostAsJsonAsync<TMessageData>(_socketOptions.SendMessageUrl, messageData);
                var content = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
