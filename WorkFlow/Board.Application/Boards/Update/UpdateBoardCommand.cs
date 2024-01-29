using MediatR;

namespace Board.Application.Boards.Update
{
	public record UpdateBoardCommand(string boardName, Guid ownerId, List<Guid> membersId, Guid boardId) : IRequest;
}
