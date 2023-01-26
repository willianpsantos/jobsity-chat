using Azure.Core;
using Jobsity.Chat.Identity.Data;
using Jobsity.Chat.Interfaces.Identity;
using Jobsity.Chat.Options;
using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Jobsity.Chat.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<JobsityIdentityUser> _signInManager;
        private readonly UserManager<JobsityIdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(
            SignInManager<JobsityIdentityUser> signInManager, 
            UserManager<JobsityIdentityUser> userManager, 
            IOptions<JwtOptions> jwtOptions
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        private async Task<IList<Claim>> GetClaims(JobsityIdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Fullname));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach(var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            return claims;
        }

        private async Task<AuthenticateUserResponse> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync( email );
            var claims = await GetClaims(user);
            var expireAt = DateTime.Now.AddHours(_jwtOptions.ExpirationHours ?? 3);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expireAt,
                signingCredentials: _jwtOptions.SigningCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken( jwt );

            return new AuthenticateUserResponse
            {
                Id = user.Id,
                Success = true,
                Token = token,
                ExpireAt = expireAt,
            };
        }

        public async Task<AuthenticateUserResponse> Authenticate(AuthenticateUserRequest request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

            if(result.Succeeded) 
                return await GenerateToken(request.Email);

            var response = new AuthenticateUserResponse
            {
                Success = result.Succeeded
            };

            if (result.IsNotAllowed)
                response.AddError($"This user account has no permission to sign in.");
            else if (result.IsLockedOut)
                response.AddError($"This user account is locked out.");
            else if (result.RequiresTwoFactor)
                response.AddError($"You must confirm your login on your email inbox.");
            else
                response.AddError("Email or password incorrects.");

            return response;
        }

        public async Task<SaveUserResponse> SaveUser(SaveUserRequest request)
        {
            JobsityIdentityUser? identityUser = null;
            IdentityResult? result = null;

            if (string.IsNullOrEmpty(request.Id) && string.IsNullOrWhiteSpace(request.Id))
            {
                identityUser = new JobsityIdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = request.Email,
                    Fullname = request.Name,
                    UserName = request.Email,
                    EmailConfirmed = true
                };

                result = await _userManager.CreateAsync(identityUser, request.Password ?? "");
            }
            else
            {
                identityUser = await _userManager.FindByIdAsync(request.Id);
                identityUser.Email = request.Email;
                identityUser.Fullname = request.Name;
                identityUser.UserName = request.Email;
                identityUser.EmailConfirmed = true;

                result = await _userManager.UpdateAsync(identityUser);
            }

            if (result.Succeeded)
                await _userManager.SetLockoutEnabledAsync(identityUser, false);

            var response = new SaveUserResponse
            {                
                Success = result.Succeeded
            };

            if(!result.Succeeded && result.Errors.Count() > 0)
            {
                foreach(var error in result.Errors ) 
                {
                    response.AddError($"[{error.Code}] {error.Description}");
                }
            }

            return response;
        }

        public async Task<IUser?> GetLoggedUser(ClaimsPrincipal claims)
        {
            var identityUser = await _userManager.GetUserAsync(claims);

            if (identityUser is null)
                return null;

            var response = new User
            {
                Id = identityUser.Id,
                Name = identityUser.Fullname,
                Email = identityUser.Email,
            };

            return response;
        }

        public async Task<IUser?> GetUserById(string id)
        {
            var identityUser = await _userManager.FindByIdAsync(id);

            if (identityUser is null)
                return null;

            var response = new User
            {
                Id = identityUser.Id,
                Name = identityUser.Fullname,
                Email = identityUser.Email,
            };

            return response;
        }

        public async Task<IUser?> GetUserByEmail(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);

            if (identityUser is null)
                return null;

            var response = new User
            {
                Id = identityUser.Id,
                Name = identityUser.Fullname,
                Email = identityUser.Email,
            };

            return response;
        }
    }
}
