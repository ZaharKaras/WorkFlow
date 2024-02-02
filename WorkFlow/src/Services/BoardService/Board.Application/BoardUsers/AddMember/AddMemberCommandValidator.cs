using Board.Application.BoardUsers.DeleteMember;
using FluentValidation;

namespace Board.Application.BoardUsers.AddMember
{
	public class AddMemberCommandValidator : AbstractValidator<AddMemberCommand>
	{
		public AddMemberCommandValidator()
		{
			RuleFor(c => c.boardId).Empty().NotNull()
				.WithMessage("Board must not be empty");

			RuleFor(c => c.userId).Empty().NotNull()
				.WithMessage("User must not be empty");
		}
	}
}
