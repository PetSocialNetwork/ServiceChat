#pragma warning disable CS8618 
using System.ComponentModel.DataAnnotations;

namespace ServiceChat.WebApi.Models.Requests
{
    public class AddMessageRequest
    {
        [Required]
        public Guid ChatId { get; init; }
        [Required]
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string MessageText { get; set; }
    }
}
