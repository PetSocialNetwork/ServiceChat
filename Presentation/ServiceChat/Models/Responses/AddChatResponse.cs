#pragma warning disable CS8618  
using ServiceChat.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceChat.WebApi.Models.Responses
{
    public class AddChatResponse
    {
        [Required]
        public Guid Id { get; init; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public List<Guid> FriendIds { get; set; } 
        public virtual List<Message>? Messages { get; set; }
    }
}
