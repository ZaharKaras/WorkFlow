using Board.Application.Cards.Create;
using Board.Application.Cards.DTOs;
using Board.Application.Cards.Update;
using Board.Application.CardUsers.AddAssignee;
using Board.Application.CardUsers.RemoveAssignee;

namespace Board.Application.Services.Interfaces
{
	public interface ICardService
	{
		Task<IEnumerable<CardListDTO>> GetCardsByUserId(Guid userId, CancellationToken token);
		Task<CardDTO> GetCardById(Guid id, CancellationToken token);
		Task CreateCard(CreateCardCommand command, CancellationToken token);
		Task DeleteCard(Guid id, CancellationToken token);
		Task UpdateCard(UpdateCardCommand command, CancellationToken token);
		Task<IEnumerable<Guid>> GetAssigneesByCardId(Guid cardId, CancellationToken token);
		Task AddAssigneeToCard(AddAssigneeCommand command, CancellationToken token);
		Task RemoveAssigneeFromCard(RemoveAssigneeCommand command, CancellationToken token);
	}
}
