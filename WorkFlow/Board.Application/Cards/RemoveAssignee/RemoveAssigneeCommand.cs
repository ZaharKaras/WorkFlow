using MediatR;

namespace Board.Application.Cards.RemoveAssignee
{
	public record RemoveAssigneeCommand(Guid cardId, Guid userId) : IRequest;
}
