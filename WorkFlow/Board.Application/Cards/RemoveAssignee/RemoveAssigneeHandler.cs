using Board.Core.Exceptions.Cards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Cards.RemoveAssignee
{
	public class RemoveAssigneeHandler : IRequestHandler<RemoveAssigneeCommand>
	{
		private readonly ICardRepository _cardRepository;

		public RemoveAssigneeHandler(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		public async Task Handle(RemoveAssigneeCommand request, CancellationToken cancellationToken)
		{
			var card = await _cardRepository.GetByIdAsync(request.cardId);

			if (card == null)
				throw new CardNotFoundException(request.cardId);

			card.RemoveAssignee(request.userId);

			await _cardRepository.UpdateAsync(card);
		}
	}
}
