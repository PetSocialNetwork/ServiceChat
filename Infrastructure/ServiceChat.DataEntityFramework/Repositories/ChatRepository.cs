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

        public async Task<List<Chat>> BySearch(Guid userId, CancellationToken cancellationToken)
        {
            return await Entities.Where(c => c.UserId == userId).ToListAsync(cancellationToken);         
        }

        public async Task<Chat?> GetChatByUsersAsync(Guid userId, Guid friendId, CancellationToken cancellationToken)
        {
            var chat = await Entities
                .FirstOrDefaultAsync(
                    c => c.UserId == userId && c.FriendIds.Contains(friendId) ||
                         c.UserId == friendId && c.FriendIds.Contains(userId),
                    cancellationToken);

            return chat;
        }
    }
}
