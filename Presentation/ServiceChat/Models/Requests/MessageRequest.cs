using System.ComponentModel.DataAnnotations;

namespace ServiceChat.WebApi.Models.Requests
{
    public class MessageRequest : BySearchRequest
    {
        [Required]
        public Guid ChatId { get; set; }
    }
}
