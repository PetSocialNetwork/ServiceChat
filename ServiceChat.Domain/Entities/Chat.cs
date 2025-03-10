using ServiceChat.Domain.Interfaces;

namespace ServiceChat.Domain.Entities
{
    public class Chat : IEntity
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; set; }
        public List<Guid> FriendIds { get; set; } = [];
        public virtual List<Message>? Messages { get; set; } = [];

        protected Chat() { }
    }
}
