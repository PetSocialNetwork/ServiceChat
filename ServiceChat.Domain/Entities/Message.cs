#pragma warning disable CS8618 
using ServiceChat.Domain.Interfaces;

namespace ServiceChat.Domain.Entities
{
    public class Message : IEntity
    {
        public Guid Id { get; init; }
        public Guid ChatId { get; init; }
        public Guid UserId { get; set; }
        public string MessageText { get; set; }
        public DateTime DateRecord { get; set; }
        public Chat Chat { get; set; }
        protected Message() { }
    }
}
