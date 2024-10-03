using FluentValidation;
using WebApplication1.ServerApp.Сore.Contracts;

namespace WebApplication1.ServerApp.Application.Validation
{
    public class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
    {
        public CreateEventRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(x => x.DateTime)
                .NotEmpty().WithMessage("DateTime is required.")
                .GreaterThan(DateTime.Now).WithMessage("Event date must be in the future.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.");
        }
    }
}
