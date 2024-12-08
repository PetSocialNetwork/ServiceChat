using Microsoft.EntityFrameworkCore;
using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace ServiceChat.DataEntityFramework.Repositories
{
    public class MessageRepository : EFRepository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<Message?> FindMessageAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Entities.SingleOrDefaultAsync(it => it.Id == id, cancellationToken);
        }

        public async IAsyncEnumerable<Message> BySearch(Guid chatId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var query = Entities.Where(c => c.ChatId == chatId).AsQueryable();
            await foreach (var message in query.AsAsyncEnumerable().WithCancellation(cancellationToken))
                yield return message;
        }
    }
}
