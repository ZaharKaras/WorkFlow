using FluentValidation;
using Identity.API.DTOs;

namespace Identity.Infrastructure.Validators
{
    public class RegistrationValidator : AbstractValidator<RegistrationRequest>
    {
        public RegistrationValidator()
        {
			RuleFor(x => x.Name).NotEmpty().MaximumLength(40)
				.WithMessage("Incorrect naming.");

            RuleFor(x => x.Email)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Incorrect email.");

			RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
				   .MinimumLength(8).WithMessage("Your password length must be at least 8.")
				   .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
				   .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
				   .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
				   .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
				   .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");

			RuleFor(p => p.ConfirmPassword)
				.Equal(x => x.Password)
				.WithMessage("Password do not match");
		}
	}
}
