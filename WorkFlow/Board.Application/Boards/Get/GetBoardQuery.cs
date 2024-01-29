using MediatR;

namespace Board.Application.Boards.Get
{
	public record GetBoardQuery(Guid boardId) : IRequest<BoardDTO>;
}
