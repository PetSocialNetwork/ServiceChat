namespace ServiceChat.Domain.Exceptions
{
    public class ChatNotFoundException : DomainException
    {
        public ChatNotFoundException(string? message) : base(message)
        {
        }

        public ChatNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
