using Jobsity.Chat.Core;
using Jobsity.Chat.Core.Interfaces;
using Jobsity.Chat.Interfaces.Identity;
using Jobsity.Chat.Interfaces.Repositories;
using Jobsity.Chat.Interfaces.Services.Domain;
using Jobsity.Chat.Models;
using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;
using Microsoft.AspNetCore.Http;
using System;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.Services.Domain
{
    public class ChatRoomService : BaseService, IChatRoomService
    {
        private readonly IChatRoomRepository _repository;


        public ChatRoomService(
            IChatRoomRepository repository,
            IIdentityService identityService,
            IHttpContextAccessor contextAccessor
        ) : base(identityService, contextAccessor)
        {
            _repository = repository;
        }


        public async Task<ChatRoomResponse> SaveAsync(SaveChatRoomRequest request)
        {
            var model = new ChatModels.ChatRoom
            {
                Id = request.Uid ?? Guid.Empty,
                Name = request.Name
            };

            var user = await GetLoggedUser();
            var userId = Guid.Parse(user.Id);
            ChatModels.ChatRoom saved = null;

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

            var response = new ChatRoomResponse
            {
                Id = saved.Id,
                Name = saved.Name,
                
                CreatedAt = saved.CreatedAt,
                CreatedBy = saved.CreatedBy,
                CreatedByName = saved.CreatedByName,

                UpdatedAt = saved.UpdatedAt
            };

            return response;
        }

        public async Task<ChatRoomResponse> DeleteAsync(Guid id)
        {
            var deleted = await _repository.DeleteAsync(id);

            var response = new ChatRoomResponse
            {
                Id = deleted.Id,
                Name =  deleted.Name,

                CreatedAt = deleted.CreatedAt,
                CreatedBy = deleted.CreatedBy,
                CreatedByName = deleted.CreatedByName,

                UpdatedAt = deleted.UpdatedAt
            };

            return response;
        }

        public async Task<ChatRoomResponse> FindByIdAsync(Guid id)
        {
            var found = await _repository.FindByIdAsync(id);

            var response = new ChatRoomResponse
            {
                Id = found.Id,
                Name = found.Name,

                CreatedAt = found.CreatedAt,
                CreatedBy = found.CreatedBy,
                CreatedByName = found.CreatedByName,

                UpdatedAt = found.UpdatedAt
            };

            return response;
        }

        public async Task<IEnumerable<ChatRoomResponse>> GetAsync()
        {
            var results = await _repository.GetAsync();

            var response =
                results
                    .Select(_ => new ChatRoomResponse
                    {
                        Id = _.Id,
                        Name = _.Name,

                        CreatedAt = _.CreatedAt,                        
                        CreatedBy = _.CreatedBy,
                        CreatedByName = _.CreatedByName,

                        UpdatedAt = _.UpdatedAt
                    })
                    .ToArray();

            return response;
        }

        public async Task<IPageableResponse<ChatRoomResponse>> GetPaginatedAsync(int page, int pageSize)
        {
            var results = await _repository.GetPaginatedAsync(page, pageSize);

            var responseData =
                results
                    .Data
                    .Select(_ => new ChatRoomResponse
                    {
                        Id = _.Id,
                        Name = _.Name,

                        CreatedAt = _.CreatedAt,
                        CreatedBy = _.CreatedBy,
                        CreatedByName = _.CreatedByName,

                        UpdatedAt = _.UpdatedAt
                    })
                    .ToArray();

            var response = new PageableResponse<ChatRoomResponse>
            {
                Data = responseData,
                Count = results.Count,
                Page = page,
                PageSize = pageSize
            };

            return response;
        }

        public async Task<IEnumerable<ChatRoomResponse>?> GetAllRoomsOfLoggedUser()
        {
            var user = await GetLoggedUser();

            if(user is null)
            {
                return null;
            }

            var userId = Guid.Parse(user.Id);

            var result = await
                _repository
                    .Where(r => r.Participants.Any(p => p.UserId == userId))
                    .GetAsync();

            var response = result.Select(_ => new ChatRoomResponse
            {
                Id = _.Id,
                Name = _.Name,

                CreatedAt = _.CreatedAt,
                CreatedBy = _.CreatedBy,
                CreatedByName = _.CreatedByName,

                UpdatedAt = _.UpdatedAt
            });

            return response;
        }
    }
}
