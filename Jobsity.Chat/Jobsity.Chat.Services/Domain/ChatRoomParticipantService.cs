using Jobsity.Chat.Core;
using Jobsity.Chat.Identity.Data;
using Jobsity.Chat.Core.Interfaces;
using Jobsity.Chat.Interfaces.Identity;
using Jobsity.Chat.Interfaces.Repositories;
using Jobsity.Chat.Interfaces.Services.Domain;
using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;
using Microsoft.AspNetCore.Http;
using System;
using ChatModels = Jobsity.Chat.Models;

namespace Jobsity.Chat.Services.Domain
{
    public class ChatRoomParticipantService : BaseService, IChatRoomParticipantService
    {
        private readonly IChatRoomParticipantRepository _repository;


        public ChatRoomParticipantService(
            IChatRoomParticipantRepository repository,
            IIdentityService identityService,
            IHttpContextAccessor contextAccessor
        ) : base(identityService, contextAccessor)
        {
            _repository = repository;
        }


        public async Task<ChatRoomParticipantResponse> SaveAsync(SaveChatRoomParticipantRequest request)
        {
            var model = new ChatModels.ChatRoomParticipant
            {
                Id = request.Uid ?? Guid.Empty,
                ChatRoomId = request.ChatRoomUid ?? Guid.Empty,
                UserId = request.UserUid ?? Guid.Empty,
                UserName = request.UserName,
                UserEmail = request.UserEmail
            };

            var user = await GetLoggedUser();
            var userId = Guid.Parse(user.Id);
            ChatModels.ChatRoomParticipant saved = null;

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

            var response = new ChatRoomParticipantResponse
            {
                Id = saved.Id,
                ChatRoomId = saved.ChatRoomId,
                UserId = saved.UserId,
                UserName = saved.UserName,
                UserEmail = saved.UserEmail,

                CreatedAt = saved.CreatedAt,
                CreatedBy = userId,
                CreatedByName = user?.Name,

                UpdatedAt = saved.UpdatedAt
            };

            return response;
        }

        public async Task<ChatRoomParticipantResponse> DeleteAsync(Guid id)
        {
            var deleted = await _repository.DeleteAsync(id);

            var response = new ChatRoomParticipantResponse
            {
                Id = deleted.Id,
                ChatRoomId = deleted.ChatRoomId,
                UserId = deleted.UserId,
                UserName = deleted.UserName,
                UserEmail = deleted.UserEmail,

                CreatedAt = deleted.CreatedAt,
                CreatedBy = deleted.CreatedBy,
                CreatedByName = deleted.CreatedByName,

                UpdatedAt = deleted.UpdatedAt
            };

            return response;
        }

        public async Task<ChatRoomParticipantResponse> FindByIdAsync(Guid id)
        {
            var found = await _repository.FindByIdAsync(id);

            var response = new ChatRoomParticipantResponse
            {
                Id = found.Id,
                ChatRoomId = found.ChatRoomId,
                UserId = found.UserId,
                UserName = found.UserName,
                UserEmail = found.UserEmail,

                CreatedBy = found.CreatedBy,
                CreatedByName = found.CreatedByName,
                CreatedAt = found.CreatedAt,

                UpdatedAt = found.UpdatedAt
            };

            return response;
        }

        public async Task<IEnumerable<ChatRoomParticipantResponse>> GetAsync()
        {
            var results = await _repository.GetAsync();

            var response =
                results
                    .Select(_ => new ChatRoomParticipantResponse
                    {
                        Id = _.Id,
                        ChatRoomId = _.ChatRoomId,
                        UserId = _.UserId,
                        UserName = _.UserName,
                        UserEmail = _.UserEmail,

                        CreatedAt = _.CreatedAt,
                        CreatedBy = _.CreatedBy,
                        CreatedByName = _.CreatedByName,

                        UpdatedAt = _.UpdatedAt
                    })
                    .ToArray();

            return response;
        }

        public async Task<IPageableResponse<ChatRoomParticipantResponse>> GetPaginatedAsync(int page, int pageSize)
        {
            var results = await _repository.GetPaginatedAsync(page, pageSize);

            var responseData =
                results
                    .Data
                    .Select(_ => new ChatRoomParticipantResponse
                    {
                        Id = _.Id,
                        ChatRoomId = _.ChatRoomId,
                        UserId = _.UserId,
                        UserName = _.UserName,
                        UserEmail = _.UserEmail,

                        CreatedAt = _.CreatedAt,
                        CreatedBy = _.CreatedBy,
                        CreatedByName = _.CreatedByName,                        

                        UpdatedAt = _.UpdatedAt
                    })
                    .ToArray();

            var response = new PageableResponse<ChatRoomParticipantResponse>
            {
                Data = responseData,
                Count = results.Count,
                Page = page,
                PageSize = pageSize
            };

            return response;
        }

        public async Task<IEnumerable<ChatRoomParticipantResponse>> GetParticipantsOfChatRoom(string roomId)
        {
            var guid = Guid.Parse(roomId);
            var user = await GetLoggedUser();
            var userId = Guid.Parse(user.Id);

            var result = await
                _repository
                    .Include(x => x.ChatRoom)
                    .Where(x => x.ChatRoomId == guid && x.UserId != userId)
                    .GetAsync();

            var response = result.Select(_ => new ChatRoomParticipantResponse
            {
                Id = _.Id,
                ChatRoomId = _.ChatRoomId,
                ChatRoomName = _.ChatRoom?.Name,
                UserId = _.UserId,
                UserEmail= _.UserEmail,
                UserName= _.UserName,

                CreatedAt = _.CreatedAt,
                CreatedBy = _.CreatedBy,
                CreatedByName = _.CreatedByName,

                UpdatedAt = _.UpdatedAt
            })
            .ToArray();

            return response;
        }
    }
}
