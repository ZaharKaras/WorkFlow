using MediatR;

namespace Board.Application.Cards.Update
{
	public record UpdateCardCommand(
		Guid id,
		string title,
		string? description,
		CardStatus status,
		DateTime deadLine) 
		: IRequest;
}
