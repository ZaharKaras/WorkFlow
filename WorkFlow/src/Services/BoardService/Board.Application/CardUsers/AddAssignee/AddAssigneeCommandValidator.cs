using FluentValidation;

namespace Board.Application.CardUsers.AddAssignee
{
	public class AddAssigneeCommandValidator : AbstractValidator<AddAssigneeCommand>
	{
		public AddAssigneeCommandValidator()
		{
			RuleFor(c => c.cardId).Empty().NotNull()
				.WithMessage("Card must not be empty");

			RuleFor(c => c.userId).Empty().NotNull()
				.WithMessage("User must not be empty");
		}
	}
}
