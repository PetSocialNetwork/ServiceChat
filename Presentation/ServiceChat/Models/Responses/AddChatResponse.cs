#pragma warning disable CS8618  
using ServiceChat.Domain.Entities;

namespace ServiceChat.WebApi.Models.Responses
{
    public class AddChatResponse
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; set; }
        public List<Guid> FriendIds { get; set; } 
        public virtual List<Message>? Messages { get; set; }
    }
}
