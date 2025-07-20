using FluentValidation;
using ServiceChat.WebApi.Models.Requests;

namespace ServiceChat.WebApi.Validators
{
    public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
    {
        public PaginationRequestValidator()
        {
            RuleFor(s => s.Take)
                .NotEmpty()
                .WithMessage("Параметр take должен быть обязательно заполнен.")
                .InclusiveBetween(0, 1000)
                .WithMessage("Значение take должно быть в диапазоне от 0 до 1000.");

            RuleFor(s => s.Offset)
               .NotEmpty()
               .WithMessage("Параметр offset должен быть обязательно заполнен.")
               .GreaterThanOrEqualTo(0)
               .WithMessage("Значение offset не может быть отрицательным.");
        }
    }
}
