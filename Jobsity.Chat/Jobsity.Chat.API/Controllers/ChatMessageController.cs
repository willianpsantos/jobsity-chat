using Jobsity.Chat.Interfaces.Services.Domain;
using Jobsity.Chat.Interfaces.UnitsOfWOrk;
using Jobsity.Chat.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobsity.Chat.API.Controllers
{
    [Route("chat/message")]
    [ApiVersion("1.0")]
    [Authorize]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageService _service;
        private readonly ISaveAndSendMessageUnitOfWork _saveAndSendMessageUoW;

        public ChatMessageController(
            IChatMessageService service,
            ISaveAndSendMessageUnitOfWork saveAndSendMessageUoW
        )
        {
            _service = service;
            _saveAndSendMessageUoW = saveAndSendMessageUoW;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetAll([FromQuery] int page = 0, [FromQuery] int pageSize = 0)
        {
            try
            {
                object? result = null;

                if (page > 0 && pageSize > 0)
                    result = await _service.GetPaginatedAsync(page, pageSize);
                else
                    result = await _service.GetAsync();

                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("lastest")]
        public async Task<ActionResult> GetLastestMessages([FromQuery] GetLastestMessagesRequest request)
        {
            try
            {
                object? result = null;

                result = await _service.GetLastestMessages(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            try
            {
                object? result = null;
                var uid = Guid.Parse(id);
                
                result = await _service.FindByIdAsync(uid);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult> Save([FromBody] SaveChatMessageRequest request)
        {
            try
            {
                object? result = null;

                result = await _service.SaveAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("send")]
        public async Task<ActionResult> SaveAndSend([FromBody] SaveChatMessageRequest request)
        {
            try
            {
                var response = await _saveAndSendMessageUoW.SaveAndSendAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                object? result = null;
                var uid = Guid.Parse(id);

                result = await _service.DeleteAsync(uid);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
