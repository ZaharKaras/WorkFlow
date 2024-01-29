using MediatR;

namespace Board.Application.Cards.Delete
{
	public record DeleteCardCommand(Guid cardId) : IRequest;
}
