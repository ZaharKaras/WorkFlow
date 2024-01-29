using MediatR;

namespace Board.Application.Cards.AddAssignee
{
	public record AddAssigneeCommand(Guid cardId, Guid userId) : IRequest;
}
