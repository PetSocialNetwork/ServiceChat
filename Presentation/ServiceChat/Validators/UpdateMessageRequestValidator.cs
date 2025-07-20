using FluentValidation;
using ServiceChat.WebApi.Models.Requests;

namespace ServiceChat.WebApi.Validators
{
    public class UpdateMessageRequestValidator : AbstractValidator<UpdateMessageRequest>
    {
        public UpdateMessageRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id должен быть задан.")
                .NotEqual(Guid.Empty)
                .WithMessage("Id не может быть пустым GUID.");

            RuleFor(x => x.MessageText)
                .NotEmpty()
                .WithMessage("MessageText не может быть пустым.");
        }
    }
}
