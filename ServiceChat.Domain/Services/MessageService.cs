using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Exceptions;
using ServiceChat.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace ServiceChat.Domain.Services
{
    public class MessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository
                ?? throw new ArgumentException(nameof(messageRepository));
        }

        public async Task<Message> GetMessageByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                return await _messageRepository.GetById(id, cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new MessageNotFoundException("Сообщение не существует.");
            }
        }

        public async Task<Message> AddMessageAsync(Message message, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(message);
            await _messageRepository.Add(message, cancellationToken);
            return message;
        }

        public async Task DeleteMessageAsync(Guid id, CancellationToken cancellationToken)
        {
            var existedMessage = await _messageRepository.FindMessageAsync(id, cancellationToken);
            if (existedMessage is null)
            {
                throw new MessageNotFoundException("Сообщение не существует.");
            }

            await _messageRepository.Delete(existedMessage, cancellationToken);
        }

        public async Task DeleteAllMessagesByChatIDAsync(Guid chatId, CancellationToken cancellationToken)
        {
            await _messageRepository.DeleteAllMessagesByChatIdAsync(chatId, cancellationToken);
        }

        public async Task UpdateMessageAsync(Guid messageId, string messageText, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(messageText);
            var existedMessage = await _messageRepository.FindMessageAsync(messageId, cancellationToken);
            if (existedMessage is null)
            {
                throw new MessageNotFoundException("Сообщение не существует.");
            }

            existedMessage.MessageText = messageText;

            await _messageRepository.Update(existedMessage, cancellationToken);
        }

        public async IAsyncEnumerable<Message> BySearchAsync(Guid chatId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            await foreach (var comment in _messageRepository.BySearch(chatId, cancellationToken))
                yield return comment;
        }
    }
}
