using System.ComponentModel.DataAnnotations;

namespace ServiceChat.WebApi.Models.Requests
{
    public class PaginationRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Take { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Offset { get; set; }
    }
}
