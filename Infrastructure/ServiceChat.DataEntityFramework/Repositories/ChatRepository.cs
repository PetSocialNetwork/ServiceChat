using Microsoft.EntityFrameworkCore;
using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace ServiceChat.DataEntityFramework.Repositories
{
    public class ChatRepository : EFRepository<Chat>, IChatRepository
    {
        public ChatRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<Chat?> FindChatAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Entities.SingleOrDefaultAsync(it => it.Id == id, cancellationToken);
        }

        public async IAsyncEnumerable<Chat> BySearch(Guid userId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var query = Entities.Where(c => c.UserIds.Any(it => it == userId)).AsQueryable();
            await foreach (var message in query.AsAsyncEnumerable().WithCancellation(cancellationToken))
                yield return message;
        }
    }
}
