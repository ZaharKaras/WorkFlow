using Board.Application.Cards.DTOs;
using MediatR;

namespace Board.Application.Cards.List
{
    public record ListCardQuery(Guid boardId) : IRequest<IEnumerable<CardListDTO>>;
}
