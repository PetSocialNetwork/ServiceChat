using System.ComponentModel.DataAnnotations;

namespace ServiceChat.WebApi.Models.Requests
{
    public class ChatRequest : BySearchRequest
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
