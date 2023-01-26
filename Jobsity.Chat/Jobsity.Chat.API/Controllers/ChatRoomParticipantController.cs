using Jobsity.Chat.Interfaces.Services.Domain;
using Jobsity.Chat.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobsity.Chat.API.Controllers
{
    [Route("chat/room/participant")]
    [ApiVersion("1.0")]
    [Authorize]
    public class ChatRoomParticipantController : ControllerBase
    {
        private readonly IChatRoomParticipantService _service;

        public ChatRoomParticipantController(IChatRoomParticipantService service)
        {
            _service = service;
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

        [HttpGet("{id}/room")]
        public async Task<ActionResult> GetParticipantsOfChatRoom(string id)
        {
            try
            {
                object? result = null;

                result = await _service.GetParticipantsOfChatRoom(id);

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
        public async Task<ActionResult> Save([FromBody] SaveChatRoomParticipantRequest request)
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
