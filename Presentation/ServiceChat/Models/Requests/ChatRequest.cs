#pragma warning disable CS8618
namespace ServiceChat.WebApi.Models.Requests
{
    public class ChatRequest
    {
        public Guid UserId { get; set; }
        public PaginationRequest Options { get; set; }
    }
}
