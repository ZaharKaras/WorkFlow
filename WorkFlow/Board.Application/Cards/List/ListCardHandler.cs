using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Cards.List
{
	public class ListCardHandler : IRequestHandler<ListCardQuery, CardListDTO>
	{
		private readonly ICardRepository _cardRepository;

		public ListCardHandler(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		public async Task<CardListDTO> Handle(ListCardQuery request, CancellationToken cancellationToken)
		{
			var cards = await _cardRepository.GetByBoardIdAsync(request.boardId);

			return new CardListDTO(cards.Select(card => card.Title).ToList());
		}
	}
}
