using MediatR;

namespace Board.Application.Boards.DeleteMember
{
	public record DeleteMemberCommand(Guid boardId, Guid userId) : IRequest;
}
