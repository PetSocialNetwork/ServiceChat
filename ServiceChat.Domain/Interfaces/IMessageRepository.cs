using ServiceChat.Domain.Entities;

namespace ServiceChat.Domain.Interfaces
{
    public interface IMessageRepository : IRepositoryEF<Message>
    {
        Task<Message?> FindMessageAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Message>> BySearch(Guid chatId, int take, int offset, CancellationToken cancellationToken);
        Task DeleteAllMessagesByChatIdAsync(Guid chatId, CancellationToken cancellationToken);
        Task<Message?> GetLastMessageByChatIdAsync(Guid chatId, CancellationToken cancellationToken);
    }
}
