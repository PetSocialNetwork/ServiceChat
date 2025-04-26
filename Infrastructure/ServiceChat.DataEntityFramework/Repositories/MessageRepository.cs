using Microsoft.EntityFrameworkCore;
using ServiceChat.Domain.Entities;
using ServiceChat.Domain.Interfaces;
using System;
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

        public async Task<Message?> GetLastMessageByChatIdAsync(Guid chatId, CancellationToken cancellationToken)
        {
            return await Entities
                .Where(it => it.ChatId == chatId)
                .OrderByDescending(it => it.DateRecord) 
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Message>> BySearch(Guid chatId, int take, int offset, CancellationToken cancellationToken)
        {
            return await Entities
                .Where(c => c.ChatId == chatId)
                .Skip(offset * take)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteAllMessagesByChatIdAsync(Guid chatId, CancellationToken cancellationToken)
        {
            var messagesToDelete = await Entities.Where(it => it.ChatId == chatId)
                                       .ToListAsync(cancellationToken);

            Entities.RemoveRange(messagesToDelete);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
