using Jobsity.Chat.Enums;
using Jobsity.Chat.Interfaces.Identity;
using Jobsity.Chat.Interfaces.Services.Domain;
using Jobsity.Chat.Interfaces.Services.Thirds;
using Jobsity.Chat.Interfaces.UnitsOfWOrk;
using Jobsity.Chat.Requests;
using Jobsity.Chat.Responses;

namespace Jobsity.Chat.API.UnitOfWorks
{
    public class SaveAndSendMessageUnitOfWork : ISaveAndSendMessageUnitOfWork
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly IChatService _chatService;
        private readonly IChatRoomService _chatRoomService;
        private readonly IMessageSenderService _messageSenderService;
        private readonly IIdentityService _identityService;
        private readonly IStockQuoteService _stockQuoteService;


        public SaveAndSendMessageUnitOfWork(
            IChatService chatService,
            IChatMessageService chatMessageService,
            IChatRoomService chatRoomService,
            IMessageSenderService senderService,
            IIdentityService identityService,
            IStockQuoteService stockQuoteService
        ) 
        {
            _chatMessageService = chatMessageService;
            _chatService = chatService;
            _messageSenderService= senderService;
            _identityService = identityService;
            _chatRoomService = chatRoomService;
            _stockQuoteService = stockQuoteService;
        }


        private MessageReceiverType GetReceiverType(SaveChatMessageRequest request) 
        {
            var receiverType =
                !string.IsNullOrEmpty(request.UserId) && !string.IsNullOrWhiteSpace(request.UserId)
                  ? MessageReceiverType.User
                  : MessageReceiverType.ChatRoom;

            return receiverType;
        }

        private string GetReceiverId(MessageReceiverType receiverType, SaveChatMessageRequest request)
        {
            string? receiverId = null;

            switch (receiverType)
            {
                case MessageReceiverType.User:
                    receiverId = request.UserId;
                    break;
                case MessageReceiverType.ChatRoom:
                    receiverId = request.ChatRoomId;
                    break;
            }

            return receiverId ?? string.Empty;
        }

        private async Task<ChatResponse> SaveChat(
            SaveChatMessageRequest request,
            MessageReceiverType receiverType,
            string? receiverId,
            IUser? user, 
            ChatRoomResponse chatRoom
        )
        {
            var chatRequest = new SaveChatRequest
            {
                ReceiverId = receiverId,
                ReceiverEmail = user?.Email,
                ReceiverName = user?.Name ?? chatRoom?.Name,
                ReceiverType = receiverType,
                SenderUserId = request.SenderUserId
            };

            var response = await _chatService.SaveAsync(chatRequest);

            return response;
        }

        private async Task<ChatMessageResponse> SaveMessageAndReturnResponse(SaveChatMessageRequest request)
        {
            var receiverType = GetReceiverType(request);
            string? receiverId = GetReceiverId(receiverType, request);

            IUser? receiverUser = null;            
            ChatRoomResponse? receiverChatRoom = null;
            ChatResponse? chatResponse = null;
            MessageSendingType sendingType = MessageSendingType.ToSingleUser;

            switch (receiverType)
            {
                case MessageReceiverType.User:
                    receiverUser = await _identityService.GetUserById(receiverId);
                    sendingType = MessageSendingType.ToSingleUser;
                    break;
                case MessageReceiverType.ChatRoom:                    
                    receiverChatRoom = await _chatRoomService.FindByIdAsync(request.ChatRoomUid.Value);
                    sendingType = MessageSendingType.ToChatRoom;
                    break;
            }

            if (string.IsNullOrEmpty(request.ChatId) || string.IsNullOrWhiteSpace(request.ChatId))
            {
                chatResponse = await SaveChat(request, receiverType, receiverId, receiverUser, receiverChatRoom);
                request.ChatId = chatResponse.Id.ToString();
            }
            else
            {
                chatResponse = await _chatService.FindByIdAsync(request.ChatUid.Value);
            }

            var response = await _chatMessageService.SaveAsync(request);

            response.Chat = chatResponse;
            response.SendingType = sendingType;
            response.StockQuote = false;

            return response;
        }


        public async Task<ChatMessageResponse> SaveAndSendAsync(SaveChatMessageRequest request)
        {
            ChatMessageResponse messageResponse = null;

            if (_stockQuoteService.IsAStockQuoteCommand(request.Message))
            {
                var code = _stockQuoteService.GetStockCode(request.Message);
                var stock = await _stockQuoteService.GetStockQuoteAsync(code);
                var stockUser = await _stockQuoteService.GetStockUserAsync();

                messageResponse = new ChatMessageResponse
                {
                    Id = null,
                    ChatId = request.ChatUid,
                    ChatRoomId = request.ChatRoomUid,
                    UserId = request.UserUid,
                    Message = stock.ToString(),
                    Read = null,
                    ReadAt = null,

                    CreatedAt = DateTimeOffset.Now,
                    CreatedBy = Guid.Parse(stockUser?.Id),
                    CreatedByName = stockUser?.Name,
                    StockQuote = true
                };
            }
            else
            {
                messageResponse = await SaveMessageAndReturnResponse(request);
            }

            await _messageSenderService.SendMessageAsync(messageResponse);

            return messageResponse;
        }
    }
}
