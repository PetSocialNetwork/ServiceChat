using ServiceChat.Domain.Entities;

namespace ServiceChat.Domain.Interfaces
{
    public interface IChatRepository : IRepositoryEF<Chat>
    {
        Task<Chat?> FindChatAsync(Guid id, CancellationToken cancellationToken);
        IAsyncEnumerable<Chat> BySearch(Guid chatId, CancellationToken cancellationToken);
    }
}
