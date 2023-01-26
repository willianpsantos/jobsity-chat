using Jobsity.Chat.Core;
using Jobsity.Chat.Identity.Data;
using Jobsity.Chat.Core.Interfaces;
using Jobsity.Chat.Interfaces.Identity;
using Jobsity.Chat.Interfaces.Repositories;
using Jobsity.Chat.Interfaces.Services.Domain;
using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;
using Microsoft.AspNetCore.Http;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.Services.Domain
{
    public class ChatService : BaseService, IChatService
    {
        private readonly IChatRepository _repository;


        public ChatService(
            IChatRepository repository, 
            IIdentityService identityService, 
            IHttpContextAccessor contextAccessor
        ) : base(identityService, contextAccessor)
        {
            _repository = repository;
        }


        public async Task<ChatResponse> SaveAsync(SaveChatRequest request)
        {
            var model = new ChatModels.Chat
            {
                Id = request.Uid ?? Guid.Empty,
                ReceiverId = request.ReceiverUId,
                ReceiverEmail = request.ReceiverEmail,
                ReceiverName = request.ReceiverName,
                SenderUserId = request.SenderUserUId,
                ReceiverType = request.ReceiverType
            };

            var user = await GetLoggedUser();
            var userId = Guid.Parse(user.Id);
            ChatModels.Chat saved = null;

            if (!RequestHasId( request))
            {
                model.Id = Guid.NewGuid();
                model.CreatedBy = userId;
                model.CreatedByName = user?.Name;
                model.CreatedAt = DateTimeOffset.Now;

                saved = await _repository.AddAsync(model);
            }
            else
            {
                model.UpdatedBy = userId;
                model.UpdatedByName = user.Name;
                model.UpdatedAt = DateTimeOffset.Now;

                saved = await _repository.UpdateAsync(request.Uid.Value, model);
            }

            var response = new ChatResponse
            {
                Id = saved.Id,
                
                CreatedBy = userId,
                CreatedByName = user?.Name,
                CreatedAt = saved.CreatedAt,

                UpdatedAt = saved.UpdatedAt
            };

            return response;
        }

        public async Task<ChatResponse> DeleteAsync(Guid id)
        {
            var deleted = await _repository.DeleteAsync(id);

            var response = new ChatResponse
            {
                Id = deleted.Id,

                CreatedBy = deleted.CreatedBy,
                CreatedByName = deleted.CreatedByName,
                CreatedAt = deleted.CreatedAt,

                UpdatedAt = deleted.UpdatedAt
            };

            return response;
        }

        public async Task<ChatResponse> FindByIdAsync(Guid id)
        {
            var found = await _repository.FindByIdAsync(id);

            var response = new ChatResponse
            {
                Id = found.Id,
                
                CreatedBy = found.CreatedBy,
                CreatedByName = found.CreatedByName,
                CreatedAt = found.CreatedAt,

                UpdatedAt = found.UpdatedAt
            };

            return response;
        }

        public async Task<IEnumerable<ChatResponse>> GetAsync()
        {
            var results = await _repository.GetAsync();

            var response = 
                results
                    .Select(_ => new ChatResponse
                    {
                        Id = _.Id,

                        CreatedBy = _.CreatedBy,
                        CreatedByName = _.CreatedByName,
                        CreatedAt = _.CreatedAt,

                        UpdatedAt = _.UpdatedAt
                    })
                    .ToArray();

            return response;
        }

        public async Task<IPageableResponse<ChatResponse>> GetPaginatedAsync(int page, int pageSize)
        {
            var results = await _repository.GetPaginatedAsync(page, pageSize);

            var responseData =
                results
                    .Data
                    .Select(_ => new ChatResponse
                    {
                        Id = _.Id,

                        CreatedBy = _.CreatedBy,
                        CreatedByName = _.CreatedByName,
                        CreatedAt = _.CreatedAt,

                        UpdatedAt = _.UpdatedAt
                    })
                    .ToArray();

            var response = new PageableResponse<ChatResponse>
            {
                Data = responseData,
                Count = results.Count,
                Page = page,
                PageSize = pageSize
            };

            return response;
        }
    }
}
