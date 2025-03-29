namespace ServiceChat.WebApi.Models.Responses
{
    public class LastMessageResponse
    {
        public Guid? Id { get; init; }
        public Guid? ChatId { get; init; }
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string? MessageText { get; set; }
        public DateTime? DateRecord { get; set; }
    }
}
