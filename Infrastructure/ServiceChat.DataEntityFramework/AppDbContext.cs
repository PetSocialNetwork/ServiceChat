using Microsoft.EntityFrameworkCore;
using ServiceChat.Domain.Entities;

namespace ServiceChat.DataEntityFramework
{
    public class AppDbContext : DbContext
    {
        DbSet<Message> Messages=> Set<Message>();
        DbSet<Chat> Chats => Set<Chat>();
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

    }
}
