using Jobsity.Chat.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Jobsity.Chat.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("settings")]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private readonly SocketOptions _socketOptions;
        private readonly MessageSendingEventsOptions _messageEventsOptions;

        public SettingsController(
            IOptions<SocketOptions> socketOptions,
            IOptions<MessageSendingEventsOptions> messageEventsOptions
        ) 
        { 
            _socketOptions = socketOptions.Value;
            _messageEventsOptions = messageEventsOptions.Value;
        }

        [HttpGet("socket")]
        public ActionResult GetSocketSettings()
        {
            //var ServerUrl =
            //    _socketOptions
            //        .ServerUrl
            //        .Replace("http", "ws")
            //        .Replace("https", "ws");

            return Ok(new
            {
                _socketOptions.ServerUrl,
                _socketOptions.ServerPath,
                _socketOptions.Reconnection,

                Events = new
                {
                    _messageEventsOptions.SendMessageSingleUserEventName,
                    _messageEventsOptions.SendMessageChatRoomEventName
                }
            });
        }
    }
}
