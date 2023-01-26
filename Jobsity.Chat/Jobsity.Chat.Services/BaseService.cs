using Jobsity.Chat.Interfaces.Identity;
using Jobsity.Chat.Requests;
using Microsoft.AspNetCore.Http;

namespace Jobsity.Chat.Services
{
    public abstract class BaseService
    {
        protected readonly IIdentityService _identityService;
        protected readonly IHttpContextAccessor _contextAcessor;


        public BaseService(IIdentityService identityService, IHttpContextAccessor contextAcessor)
        {
            _identityService = identityService;
            _contextAcessor = contextAcessor;
        }

        protected async Task<IUser> GetLoggedUser() =>
            await _identityService.GetLoggedUser(_contextAcessor.HttpContext.User);

        protected bool RequestHasId(Request request) =>
            !string.IsNullOrEmpty(request.Id) && !string.IsNullOrWhiteSpace(request.Id);
    }
}
