namespace ServiceChat.WebApi.Models.Requests
{
    public class AddChatRequest
    {
        public List<Guid> FriendIds { get; set; } = [];
    }
}
