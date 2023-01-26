using Jobsity.Chat.Identity.Data;
using Jobsity.Chat.Identity.Services;
using Jobsity.Chat.Interfaces.Identity;
using Jobsity.Chat.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Jobsity.Chat.IoC
{
    public static class ServicesCollectionAuthExtensions
    {
        public const string IDENTITY_DB_CONNECTION_STRING = "Identity";

        public static void AddJobsityChatIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString(IDENTITY_DB_CONNECTION_STRING);
                options.UseSqlServer(connectionString);
            });

            services
                .AddDefaultIdentity<JobsityIdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
        }

        public static void AddJobsityChatJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions));
            var securityKey = jwtOptions.GetSection(nameof(SecurityKey)).Value;
            var symmSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));

            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = jwtOptions[nameof(JwtOptions.Issuer)];
                options.Audience = jwtOptions[nameof(JwtOptions.Audience)];
                options.ExpirationHours = int.Parse(jwtOptions[nameof(JwtOptions.ExpirationHours)]);
                options.SigningCredentials = new SigningCredentials(symmSecurityKey, SecurityAlgorithms.HmacSha512);
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,

                ValidIssuer = jwtOptions[nameof(JwtOptions.Issuer)],
                ValidAudience = jwtOptions[nameof(JwtOptions.Audience)],
                IssuerSigningKey = symmSecurityKey
            };

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                });
        }
    }
}
