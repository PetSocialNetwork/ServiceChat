#pragma warning disable CS8618
namespace ServiceChat.WebApi.Models.Requests
{
    public class MessageRequest 
    {
        public Guid ChatId { get; set; }
        public PaginationRequest Options { get; set; }
    }
}
