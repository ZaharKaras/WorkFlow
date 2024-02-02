using MediatR;

namespace Board.Application.BoardUsers.DeleteMember
{
	public record DeleteMemberCommand(Guid boardId, Guid userId) : IRequest;
}
