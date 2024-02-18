using FluentValidation;

namespace Board.Application.Boards.Create
{
	public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
	{
		public CreateBoardCommandValidator()
		{
			RuleFor(c => c.boardName).MaximumLength(45)
				.WithMessage("The name must be no more than 45 characters long");

			RuleFor(c => c.boardName).NotEmpty()
				.WithMessage("The name must not be empty");
		}
	}
}
