using FluentValidation;
using ServiceChat.WebApi.Models.Requests;

namespace ServiceChat.WebApi.Validators
{
    public class MessageRequestValidator : AbstractValidator<MessageRequest>
    {
        public MessageRequestValidator()
        {
            RuleFor(x => x.ChatId)
                .NotEmpty()
                .WithMessage("ChatId должен быть задан.")
                .NotEqual(Guid.Empty)
                .WithMessage("ChatId не может быть пустым GUID.");
        }
    }
}
