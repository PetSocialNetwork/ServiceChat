namespace ServiceChat.Domain.Exceptions
{
    public class MessageNotFoundException : DomainException
    {
        public MessageNotFoundException(string? message) : base(message)
        {
        }

        public MessageNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
