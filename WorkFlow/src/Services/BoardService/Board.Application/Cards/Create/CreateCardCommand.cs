using MediatR;

namespace Board.Application.Cards.Create
{
	public record CreateCardCommand(string title, Guid boardId) : IRequest;

}
