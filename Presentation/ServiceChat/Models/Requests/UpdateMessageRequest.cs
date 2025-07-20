#pragma warning disable CS8618 

namespace ServiceChat.WebApi.Models.Requests
{
    public class UpdateMessageRequest
    {
        public Guid Id { get; init; }
        public string MessageText { get; set; }
    }
}
