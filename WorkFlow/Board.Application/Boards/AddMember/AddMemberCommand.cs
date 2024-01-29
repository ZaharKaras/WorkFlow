using MediatR;

namespace Board.Application.Boards.AddMember
{
	public record AddMemberCommand(Guid boardId, Guid userId) : IRequest;
}
