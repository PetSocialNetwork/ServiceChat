namespace ServiceChat.Domain.Exceptions
{
    public class ChatAlreadyExistsException : Exception
    {
        public ChatAlreadyExistsException(string? message) : base(message)
        {
        }

        public ChatAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
