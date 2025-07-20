using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Shared;

namespace ServiceChat.Domain.Interfaces
{
    public interface IChatRepository : IRepositoryEF<Chat>
    {
        Task<Chat?> FindChatAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Chat>> BySearch(Guid userId, PaginationOptions options, CancellationToken cancellationToken);
        Task<Chat?> GetChatByUsersAsync(List<Guid> friendIds, CancellationToken cancellationToken);
    }
}
