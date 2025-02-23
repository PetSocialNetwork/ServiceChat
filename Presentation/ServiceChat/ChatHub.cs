using Microsoft.AspNetCore.SignalR;
using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Services;

namespace ServiceChat.WebApi
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;

        private readonly MessageService _messageService; 
        public ChatHub(MessageService messageService, ILogger<ChatHub> logger)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _logger = logger;
        }

        public async Task SendMessage
            (string message,
            string userName,
            string userId,
            string chatId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message, nameof(message));
            ArgumentException.ThrowIfNullOrWhiteSpace(userName, nameof(userName));

            var newMessage = new Message(Guid.NewGuid(), Guid.Parse(chatId), Guid.Parse(userId), message, userName)
            {
                DateRecord = DateTime.UtcNow
            };

            try
            {
                await _messageService.AddMessageAsync(newMessage, default);
                await Clients.All.SendAsync("ReceiveMessage", userId, message, userName);
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
