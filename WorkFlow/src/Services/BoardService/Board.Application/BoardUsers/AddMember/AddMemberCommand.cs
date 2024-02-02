using MediatR;

namespace Board.Application.BoardUsers.AddMember
{
	public record AddMemberCommand(Guid boardId, Guid userId) : IRequest;
}
