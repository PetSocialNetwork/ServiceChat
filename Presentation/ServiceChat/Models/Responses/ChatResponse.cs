#pragma warning disable CS8618  
using System.ComponentModel.DataAnnotations;

namespace ServiceChat.WebApi.Models.Responses
{
    public class ChatResponse
    {
        [Required]
        public Guid Id { get; init; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public List<Guid> FriendIds { get; set; }
    }
}
