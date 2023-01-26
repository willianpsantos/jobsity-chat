using Jobsity.Chat.Interfaces.Identity;
using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Jobsity.Chat.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        
        public UserController(IIdentityService identityService) 
        { 
            _identityService = identityService;
        }

        [HttpPost("save")]
        public async Task<ActionResult<SaveUserResponse>> SaveUser([FromBody] SaveUserRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _identityService.SaveUser(request);

            if(result.Success)
                return Ok(result);

            if (!result.Success)
                return BadRequest(result);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("auth")]
        public async Task<ActionResult<AuthenticateUserResponse>> Authenticate([FromBody] AuthenticateUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _identityService.Authenticate(request);

            if (result.Success)
                return Ok(result);

            return Unauthorized(result);
        }
    }
}
