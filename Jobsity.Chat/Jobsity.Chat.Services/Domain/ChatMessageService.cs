using Jobsity.Chat.Core;
using Jobsity.Chat.Core.Interfaces;
using Jobsity.Chat.Enums;
using Jobsity.Chat.Interfaces.Identity;
using Jobsity.Chat.Interfaces.Repositories;
using Jobsity.Chat.Interfaces.Services.Domain;
using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;
using Microsoft.AspNetCore.Http;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.Services.Domain
{
    public class ChatMessageService : BaseService, IChatMessageService
    {
        private readonly IChatMessageRepository _repository;


        public ChatMessageService(
            IChatMessageRepository repository,
            IIdentityService identityService,
            IHttpContextAccessor contextAccessor
        ) : base(identityService, contextAccessor)
        {
            _repository = repository;
        }


        public async Task<ChatMessageResponse> SaveAsync(SaveChatMessageRequest request)
        {
            var model = new ChatModels.ChatMessage
            {
                Id = request.Uid ?? Guid.Empty,
                ChatId = request.ChatUid ?? Guid.Empty,
                ChatRoomId = request.ChatRoomUid,
                UserId = request.UserUid,
                Message = request.Message,
                Read = request.Read ?? false,
                ReadAt = request.ReadAt,
            };

            var user = await GetLoggedUser();
            var userId = Guid.Parse(user.Id);
            ChatModels.ChatMessage saved = null;

            if (!RequestHasId(request))
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
                model.UpdatedByName = user?.Name;
                model.UpdatedAt = DateTimeOffset.Now;

                saved = await _repository.UpdateAsync(request.Uid.Value, model);
            }

            var response = new ChatMessageResponse
            {
                Id = saved.Id,
                ChatId = saved.ChatId,
                ChatRoomId = saved.ChatRoomId,
                UserId = saved.UserId,
                Message = saved.Message,
                Read = saved.Read,
                ReadAt = saved.ReadAt,
                
                CreatedAt = saved.CreatedAt,
                CreatedBy = userId,
                CreatedByName = user?.Name,

                UpdatedAt = saved.UpdatedAt
            };

            return response;
        }

        public async Task<ChatMessageResponse> DeleteAsync(Guid id)
        {
            var deleted = await _repository.DeleteAsync(id);

            var response = new ChatMessageResponse
            {
                Id = deleted.Id,
                ChatId = deleted.Id,
                ChatRoomId = deleted.ChatRoomId,
                UserId = deleted.UserId,
                Message = deleted.Message,
                Read = deleted.Read,
                ReadAt = deleted.ReadAt,

                CreatedAt = deleted.CreatedAt,
                UpdatedAt = deleted.UpdatedAt
            };

            return response;
        }

        public async Task<ChatMessageResponse> FindByIdAsync(Guid id)
        {
            var found = await _repository.FindByIdAsync(id);

            var response = new ChatMessageResponse
            {
                Id = found.Id,
                ChatId = found.Id,
                ChatRoomId = found.ChatRoomId,
                UserId = found.UserId,
                Message = found.Message,
                Read = found.Read,
                ReadAt = found.ReadAt,

                CreatedAt = found.CreatedAt,
                UpdatedAt = found.UpdatedAt
            };

            return response;
        }

        public async Task<IEnumerable<ChatMessageResponse>> GetAsync()
        {
            var results = await _repository.GetAsync();

            var response =
                results
                    .Select(_ => new ChatMessageResponse
                    {
                        Id = _.Id,
                        ChatId = _.Id,
                        ChatRoomId = _.ChatRoomId,
                        UserId = _.UserId,
                        Message = _.Message,
                        Read = _.Read,
                        ReadAt = _.ReadAt,

                        CreatedAt = _.CreatedAt,
                        UpdatedAt = _.UpdatedAt
                    })
                    .ToArray();

            return response;
        }

        public async Task<IPageableResponse<ChatMessageResponse>> GetPaginatedAsync(int page, int pageSize)
        {
            var results = await _repository.GetPaginatedAsync(page, pageSize);

            var responseData =
                results
                    .Data
                    .Select(_ => new ChatMessageResponse
                    {
                        Id = _.Id,
                        ChatId = _.Id,
                        ChatRoomId = _.ChatRoomId,
                        UserId = _.UserId,
                        Message = _.Message,
                        Read = _.Read,
                        ReadAt = _.ReadAt,

                        CreatedAt = _.CreatedAt,
                        CreatedBy = _.CreatedBy,
                        CreatedByName= _.CreatedByName,

                        UpdatedAt = _.UpdatedAt
                    })
                    .ToArray();

            var response = new PageableResponse<ChatMessageResponse>
            {
                Data = responseData,
                Count = results.Count,
                Page = page,
                PageSize = pageSize
            };

            return response;
        }

        public async Task<IPageableResponse<ChatMessageResponse>> GetLastestMessages(GetLastestMessagesRequest request)
        {
            var guid = Guid.Parse(request.ReceiverId);

            var results = await 
                _repository
                    .Include(m => m.Chat)
                    .Where(
                        m => (request.ReceiverType == MessageReceiverType.ChatRoom && m.ChatRoomId == guid) ||
                             (request.ReceiverType == MessageReceiverType.User && m.UserId == guid)
                     )
                    .OrderBy(o => o.CreatedAt)
                    .GetPaginatedAsync(request.Page, request.PageSize);

            var responseData =
                results
                    .Data
                    .Select(_ => new ChatMessageResponse
                    {
                        Id = _.Id,
                        ChatId = _.Id,
                        ChatRoomId = _.ChatRoomId,
                        UserId = _.UserId,
                        Message = _.Message,
                        Read = _.Read,
                        ReadAt = _.ReadAt,

                        CreatedBy= _.CreatedBy,
                        CreatedByName = _.CreatedByName,
                        CreatedAt = _.CreatedAt,

                        UpdatedAt = _.UpdatedAt
                    })
                    .ToArray();

            var response = new PageableResponse<ChatMessageResponse>
            {
                Data = responseData,
                Count = results.Count,
                Page = request.Page,
                PageSize = request.PageSize
            };

            return response;
        }
    }
}
