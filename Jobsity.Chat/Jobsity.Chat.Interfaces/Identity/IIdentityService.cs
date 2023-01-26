using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;
using System.Security.Claims;

namespace Jobsity.Chat.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task<SaveUserResponse> SaveUser(SaveUserRequest request);
        Task<AuthenticateUserResponse> Authenticate(AuthenticateUserRequest request);
        Task<IUser?> GetLoggedUser(ClaimsPrincipal claims);
        Task<IUser?> GetUserById(string id);
        Task<IUser?> GetUserByEmail(string email);
    }
}
