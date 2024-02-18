using FluentValidation;

namespace Board.Application.Cards.Create
{
	public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
	{
		public CreateCardCommandValidator()
		{
			RuleFor(c => c.title).MaximumLength(45)
				.WithMessage("The title must be no more than 45 characters long");

			RuleFor(c => c.title).NotEmpty()
				.WithMessage("The title must not be empty");
		}
	}
}
