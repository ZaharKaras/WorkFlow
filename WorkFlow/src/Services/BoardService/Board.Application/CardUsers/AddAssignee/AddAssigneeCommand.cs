using MediatR;

namespace Board.Application.CardUsers.AddAssignee
{
	public record AddAssigneeCommand(Guid cardId, Guid userId) : IRequest;
}
