#pragma warning disable CS8618  
using System.ComponentModel.DataAnnotations;

namespace ServiceChat.WebApi.Models.Responses
{
    public class MessageResponse
    {
        [Required]
        public Guid Id { get; init; }
        [Required]
        public Guid ChatId { get; init; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string MessageText { get; set; }
        [Required]
        public DateTime DateRecord { get; set; }
    }
}