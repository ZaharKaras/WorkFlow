using FluentValidation;

namespace Board.Application.Boards.Update
{
	public class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
	{
		public UpdateBoardCommandValidator()
		{
			RuleFor(c => c.boardName).MaximumLength(45)
				.WithMessage("The name must be no more than 45 characters long");

			RuleFor(c => c.boardName).NotEmpty()
				.WithMessage("The name must not be empty");

			RuleFor(c => c.ownerId).NotEmpty()
				.WithMessage("The owner id cannot be null");
		}
	}
}
