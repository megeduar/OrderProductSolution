using FluentValidation;

namespace Application.User.Commands.Login
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public class CreateLoginCommandValidator : AbstractValidator<AuthenticateCommand>
        {
            public CreateLoginCommandValidator()
            {
                RuleFor(v => v.Username)
                    .NotEmpty()
                    .WithMessage("Name is required.");

                RuleFor(v => v.Password)
                    .NotEmpty()
                    .WithMessage("Password is required.");
            }
        }
    }
}
