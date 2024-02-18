using FluentValidation;

namespace Board.Application.CardUsers.RemoveAssignee
{
	public class RemoveAssigneeCommandValidator : AbstractValidator<RemoveAssigneeCommand>
	{
		public RemoveAssigneeCommandValidator()
		{
			RuleFor(c => c.cardId).Empty().NotNull()
				.WithMessage("Card must not be empty");

			RuleFor(c => c.userId).Empty().NotNull()
				.WithMessage("User must not be empty");
		}
	}
}