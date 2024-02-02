using FluentValidation;


namespace Board.Application.BoardUsers.DeleteMember
{
	public class DeleteMemberCommandValidator : AbstractValidator<DeleteMemberCommand>
	{
		public DeleteMemberCommandValidator()
		{
			RuleFor(c => c.boardId).Empty().NotNull()
				.WithMessage("Board must not be empty");

			RuleFor(c => c.userId).Empty().NotNull()
				.WithMessage("User must not be empty");
		}
	}
}
