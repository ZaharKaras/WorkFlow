using Board.Core.Entities;
using Board.Core.Exceptions.Cards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Cards.AddAssignee
{
	public class AddAssigneeHandler : IRequestHandler<AddAssigneeCommand>
	{
		private readonly ICardRepository _cardRepository;

		public AddAssigneeHandler(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		public async Task Handle(AddAssigneeCommand request, CancellationToken cancellationToken)
		{
			var card = await _cardRepository.GetByIdAsync(request.cardId);

			if (card == null)
				throw new CardNotFoundException(request.cardId);

			if(card.AssigneesId.FirstOrDefault(request.userId) == request.userId)
				throw new AssigneeAlreadyExists(request.userId);

			card.AddAssignee(request.userId);

			await _cardRepository.UpdateAsync(card);
		}
	}
}
