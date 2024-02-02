using Board.Application.Cards.DTOs;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Cards.List
{
    public class ListCardHandler : IRequestHandler<ListCardQuery, IEnumerable<CardListDTO>>
	{
		private readonly ICardRepository _cardRepository;

		public ListCardHandler(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		public async Task<IEnumerable<CardListDTO>> Handle(ListCardQuery request, CancellationToken cancellationToken)
		{
			var cards = await _cardRepository.GetByBoardIdAsync(request.boardId);

			var cardDTOs = cards.Select(card => new CardListDTO(
				card.Id,
				card.Title
			));

			return cardDTOs;
		}
	}
}
