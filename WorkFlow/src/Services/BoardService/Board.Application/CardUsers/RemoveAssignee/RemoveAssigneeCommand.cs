using MediatR;

namespace Board.Application.CardUsers.RemoveAssignee
{
	public record RemoveAssigneeCommand(Guid cardId, Guid userId) : IRequest;
}
