using MediatR;

namespace Board.Application.Boards.Update
{
	public record UpdateBoardCommand(Guid id, string boardName, Guid ownerId) : IRequest;
}
