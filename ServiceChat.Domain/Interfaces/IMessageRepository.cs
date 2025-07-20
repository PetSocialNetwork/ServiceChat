using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Shared;

namespace ServiceChat.Domain.Interfaces
{
    public interface IMessageRepository : IRepositoryEF<Message>
    {
        Task<Message?> FindMessageAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Message>> BySearch(Guid chatId, PaginationOptions options, CancellationToken cancellationToken);
        Task DeleteAllMessagesByChatIdAsync(Guid chatId, CancellationToken cancellationToken);
        Task<Message?> GetLastMessageByChatIdAsync(Guid chatId, CancellationToken cancellationToken);
    }
}
