using TestLetshare.Application.Features.Auth.Commands;
using FluentValidation;

namespace TestLetshare.Application.Features.Auth.Validators
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        public SignInValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username is required.")
                .EmailAddress().WithMessage("Invalid username format.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
