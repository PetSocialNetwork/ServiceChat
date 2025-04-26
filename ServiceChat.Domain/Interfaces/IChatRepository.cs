using ServiceChat.Domain.Entities;

namespace ServiceChat.Domain.Interfaces
{
    public interface IChatRepository : IRepositoryEF<Chat>
    {
        Task<Chat?> FindChatAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Chat>> BySearch(Guid userId, int take, int offset, CancellationToken cancellationToken);
        Task<Chat?> GetChatByUsersAsync(List<Guid> friendIds, CancellationToken cancellationToken);
    }
}
