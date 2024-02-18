using MediatR;

namespace Board.Application.BoardUsers.GetMembers
{
	public record GetMembersQuery(Guid boardId) : IRequest<IEnumerable<Guid>>;
}
