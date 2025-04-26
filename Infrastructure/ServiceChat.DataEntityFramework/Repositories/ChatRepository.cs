using Microsoft.EntityFrameworkCore;
using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Interfaces;

namespace ServiceChat.DataEntityFramework.Repositories
{
    public class ChatRepository : EFRepository<Chat>, IChatRepository
    {
        public ChatRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<Chat?> FindChatAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Entities.SingleOrDefaultAsync(it => it.Id == id, cancellationToken);
        }

        public async Task<List<Chat>> BySearch(Guid userId, int take, int offset, CancellationToken cancellationToken)
        {
            return await Entities
                    .Where(c => c.FriendIds != null && c.FriendIds.Contains(userId))
                    .Skip(offset * take)
                    .Take(take)
                    .ToListAsync(cancellationToken);
        }

        public async Task<Chat?> GetChatByUsersAsync(List<Guid> friendIds, CancellationToken cancellationToken)
        {
            var chat = await Entities
                    .Where(c => c.FriendIds.Count == friendIds.Count && friendIds.All(friendId => c.FriendIds.Contains(friendId)))
                    .FirstOrDefaultAsync(cancellationToken);

            return chat;
        }
    }
}
