using MediatR;

namespace Board.Application.Boards.Delete
{
	public record DeleteBoardCommand(Guid boardId) : IRequest;
}
