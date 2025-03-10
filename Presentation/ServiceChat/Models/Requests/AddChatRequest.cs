using System.ComponentModel.DataAnnotations;

namespace ServiceChat.WebApi.Models.Requests
{
    public class AddChatRequest
    {
        [Required]
        public List<Guid> FriendIds { get; set; } = [];
    }
}
