using ServiceChat.Domain.Entities;

namespace ServiceChat.Domain.Interfaces
{
    public interface IMessageRepository : IRepositoryEF<Message>
    {
        Task<Message?> FindMessageAsync(Guid id, CancellationToken cancellationToken);
        IAsyncEnumerable<Message> BySearch(Guid chatId, CancellationToken cancellationToken);
    }
}
