using System.ComponentModel.DataAnnotations;

namespace ServiceChat.WebApi.Models.Requests
{
    public class AddChatRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public List<Guid> FriendIds { get; set; } = [];
    }
}
