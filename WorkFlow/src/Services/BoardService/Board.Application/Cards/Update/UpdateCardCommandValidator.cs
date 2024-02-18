using FluentValidation;

namespace Board.Application.Cards.Update
{
	public class UpdateCardCommandValidator : AbstractValidator<UpdateCardCommand>
	{
		public UpdateCardCommandValidator()
		{
			RuleFor(c => c.title).MaximumLength(45)
				.WithMessage("The title must be no more than 45 characters long");

			RuleFor(c => c.title).NotEmpty()
				.WithMessage("The title must not be empty");

			RuleFor(c => c.description).MaximumLength(1000)
				.WithMessage("The description must be no more than 1000 characters long");

			RuleFor(c => c.deadLine).LessThan(DateTime.Now)
				.WithMessage("The dead line cannot be earlier than current date");
		}
	}
}
