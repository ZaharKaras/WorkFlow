using Board.Application.Cards.Create;
using Board.Application.Cards.Delete;
using Board.Application.Cards.DTOs;
using Board.Application.Cards.Get;
using Board.Application.Cards.List;
using Board.Application.Cards.Update;
using Board.Application.CardUsers.AddAssignee;
using Board.Application.CardUsers.GetAssignees;
using Board.Application.CardUsers.RemoveAssignee;
using Board.Application.Services.Interfaces;
using MediatR;

namespace Board.Application.Services
{
	public class CardService : ICardService
	{
		private readonly IMediator _mediator;

		public CardService(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IEnumerable<CardListDTO>> GetCardsByUserId(Guid userId, CancellationToken token)
		{
			var query = new ListCardQuery(userId);
			var result = await _mediator.Send(query, token);
			return result;
		}

		public async Task<CardDTO> GetCardById(Guid id, CancellationToken token)
		{
			var query = new GetCardQuery(id);
			var result = await _mediator.Send(query, token);

			return result;
		}

		public async Task CreateCard(CreateCardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);
		}

		public async Task DeleteCard(Guid id, CancellationToken token)
		{
			var command = new DeleteCardCommand(id);
			await _mediator.Send(command, token);
		}

		public async Task UpdateCard(UpdateCardCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);
		}

		public async Task<IEnumerable<Guid>> GetAssigneesByCardId(Guid cardId, CancellationToken token)
		{
			var query = new GetAssigneesQuery(cardId);
			var result = await _mediator.Send(query, token);

			return result;
		}

		public async Task AddAssigneeToCard(AddAssigneeCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);
		}

		public async Task RemoveAssigneeFromCard(RemoveAssigneeCommand command, CancellationToken token)
		{
			await _mediator.Send(command, token);
		}
	}
}
