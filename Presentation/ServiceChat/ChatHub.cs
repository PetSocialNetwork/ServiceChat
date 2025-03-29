using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Services;
using ServiceChat.WebApi.Models.Responses;

namespace ServiceChat.WebApi
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IMapper _mapper;
        private readonly MessageService _messageService; 
        public ChatHub(MessageService messageService,
            IMapper mapper,
            ILogger<ChatHub> logger)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _logger = logger;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task SendMessage
            (string messageText,
            string userName,
            string userId,
            string chatId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(messageText, nameof(messageText));
            ArgumentException.ThrowIfNullOrWhiteSpace(userName, nameof(userName));

            var newMessage = new Message(Guid.NewGuid(), Guid.Parse(chatId), Guid.Parse(userId), messageText, userName)
            {
                DateRecord = DateTime.UtcNow
            };

            try
            {
                var message = await _messageService.AddMessageAsync(newMessage, default);
                var messageResponse = _mapper.Map<MessageResponse>(message);
                await Clients.All.SendAsync("ReceiveMessage", messageResponse);
                //await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", userId, message, cancellationToken: cancellationToken);              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SendMessage method");
            }
        }

        public async Task JoinChat(Guid chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task LeaveChat(Guid chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }
}
