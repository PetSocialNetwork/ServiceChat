#pragma warning disable CS8618  

namespace ServiceChat.WebApi.Models.Responses
{
    public class ChatResponse
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; set; }
        public List<Guid> FriendIds { get; set; }
    }
}
