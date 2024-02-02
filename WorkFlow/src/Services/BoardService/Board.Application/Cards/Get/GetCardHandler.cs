using Board.Application.Boards;
using Board.Application.Cards.DTOs;
using Board.Core.Exceptions.Cards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Cards.Get
{
    public class GetCardHandler : IRequestHandler<GetCardQuery, CardDTO>
	{
		private readonly ICardRepository _cardRepository;

		public GetCardHandler(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		public async Task<CardDTO> Handle(GetCardQuery request, CancellationToken cancellationToken)
		{
			var card = await _cardRepository.GetByIdAsync(request.cardId);

			if (card == null)
				throw new CardNotFoundException(request.cardId);

			return new CardDTO(
				card.Id,
				card.Title,
				card.Description,
				card.BoardId,
				card.Status,
				card.DeadLine,
				card.AddedDate
			);
		}
	}
}
