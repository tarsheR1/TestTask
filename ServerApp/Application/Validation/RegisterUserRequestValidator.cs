using FluentValidation;
using WebApplication1.ServerApp.Сore.Contracts;

namespace WebApplication1.ServerApp.Application.Validation
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.firstname)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.lastname)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(x => x.birthdate)
                .NotEmpty().WithMessage("Birthdate is required.")
                .LessThan(DateTime.Now).WithMessage("Birthdate must be in the past.");

            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.")
                .Length(6, 100).WithMessage("Password must be between 6 and 100 characters.");
        }
    }

}
