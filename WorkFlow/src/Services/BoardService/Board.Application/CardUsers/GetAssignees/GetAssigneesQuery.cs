using MediatR;

namespace Board.Application.CardUsers.GetAssignees
{
	public record GetAssigneesQuery(Guid cardId) : IRequest<IEnumerable<Guid>>;
}
