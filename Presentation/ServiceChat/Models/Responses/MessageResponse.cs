#pragma warning disable CS8618  

namespace ServiceChat.WebApi.Models.Responses
{
    public class MessageResponse
    {
        public Guid Id { get; init; }
        public Guid ChatId { get; init; }
        public Guid UserId { get; set; }
        public string? MessageText { get; set; }
        public DateTime DateRecord { get; set; }
    }
}