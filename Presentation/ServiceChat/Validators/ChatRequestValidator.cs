using FluentValidation;
using ServiceChat.WebApi.Models.Requests;

namespace ServiceChat.WebApi.Validators
{
    public class ChatRequestValidator : AbstractValidator<ChatRequest>
    {
        public ChatRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId должен быть задан.")
                .NotEqual(Guid.Empty)
                .WithMessage("UserId не может быть пустым GUID.");
        }
    }
}
