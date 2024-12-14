namespace ServiceChat.WebApi.Models.Requests
{
    public class AddChatRequest
    {
        public List<Guid> UserIds { get; set; } = new List<Guid>();
    }
}
