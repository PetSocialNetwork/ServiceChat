using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Exceptions;
using ServiceChat.Domain.Interfaces;

namespace ServiceChat.Domain.Services
{
    public class ChatService
    {
        private readonly IChatRepository _chatRepository;
        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository
                ?? throw new ArgumentNullException(nameof(chatRepository));
        }

        public async Task<Chat> GetChatByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                return await _chatRepository.GetById(id, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new ChatNotFoundException("Чат не существует.");
            }
        }

        public async Task<Chat> AddChatAsync(Chat chat, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(chat);
            await _chatRepository.Add(chat, cancellationToken);
            return chat;
        }

        public async Task DeleteChatAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedChat = await _chatRepository.FindChatAsync(id, cancellationToken);
            if (existedChat is null)
            {
                throw new ChatNotFoundException("Чат не существует.");
            }

            await _chatRepository.Delete(existedChat, cancellationToken);
        }

        public async Task<List<Chat>> BySearchAsync(Guid chatId, CancellationToken cancellationToken)
        {
            return await _chatRepository.BySearch(chatId, cancellationToken);
        }      
    }
}
