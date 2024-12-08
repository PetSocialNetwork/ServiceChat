#pragma warning disable CS8618 
using System.ComponentModel.DataAnnotations;

namespace ServiceChat.WebApi.Models.Requests
{
    public class UpdateMessageRequest
    {
        [Required]
        public Guid Id { get; init; }
        [Required]
        public string MessageText { get; set; }
    }
}
