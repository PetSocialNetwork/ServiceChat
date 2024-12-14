using ServiceChat.Domain.Interfaces;

namespace ServiceChat.Domain.Entities
{
    public class Chat : IEntity
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; set; }
        public List<Guid> UserIds { get; set; } = new List<Guid>();
        public virtual List<Message>? Messages { get; set; } = new List<Message>();

        protected Chat() { }
    }
}
