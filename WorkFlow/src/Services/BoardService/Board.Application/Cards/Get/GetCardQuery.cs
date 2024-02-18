using Board.Application.Cards.DTOs;
using MediatR;

namespace Board.Application.Cards.Get
{
    public record GetCardQuery(Guid cardId) : IRequest<CardDTO>;
}
