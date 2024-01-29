using MediatR;

namespace Board.Application.Cards.List
{
	public record ListCardQuery(Guid boardId) : IRequest<CardListDTO>;
}
