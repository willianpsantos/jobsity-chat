using Jobsity.Chat.API.UnitOfWorks;
using Jobsity.Chat.DB;
using Jobsity.Chat.Interfaces.Repositories;
using Jobsity.Chat.Interfaces.Services.Domain;
using Jobsity.Chat.Interfaces.Services.Thirds;
using Jobsity.Chat.Interfaces.UnitsOfWOrk;
using Jobsity.Chat.Options;
using Jobsity.Chat.Repositories;
using Jobsity.Chat.Services.Domain;
using Jobsity.Chat.Services.Thirds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jobsity.Chat.IoC
{
    public static class ServicesCollectionExtensions
    {        
        public const string JOBSITY_DB_CONNECTION_STRING = "Jobsity";

        private static void AddJobsityRepositories(this IServiceCollection services)
        {
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatRoomParticipantRepository, ChatRoomParticipantRepository>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
        }

        private static void AddJobsityDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IChatRoomParticipantService, ChatRoomParticipantService>();
            services.AddScoped<IChatRoomService, ChatRoomService>();
            services.AddScoped<IChatMessageService, ChatMessageService>();

            services.AddScoped<IMessageSenderService, MessageSenderService>();
            services.AddScoped<IStockQuoteService, StockQuoteService>();
        }

        public static void AddJobsityConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var socketOptionsSection = configuration.GetSection(nameof(SocketOptions));
            var messageEventsSection = configuration.GetSection(nameof(MessageSendingEventsOptions));
            var stockQuoteSection = configuration.GetSection(nameof(StockQuoteOptions));

            services.Configure<SocketOptions>(socketOptionsSection);
            services.Configure<MessageSendingEventsOptions>(messageEventsSection);
            services.Configure<StockQuoteOptions>(stockQuoteSection);
        }

        public static void AddJobsityUnitsOfWork(this IServiceCollection services)
        {
            services.AddScoped<ISaveAndSendMessageUnitOfWork, SaveAndSendMessageUnitOfWork>();
        }

        public static void AddJobsityChatDependencies(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<JobsityChatDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString(JOBSITY_DB_CONNECTION_STRING);
                options.UseSqlServer(connectionString);
            });

            services.AddJobsityConfigurationOptions(configuration);
            services.AddHttpContextAccessor();
            services.AddJobsityRepositories();
            services.AddJobsityDomainServices();
            services.AddJobsityUnitsOfWork();
        }
    }
}