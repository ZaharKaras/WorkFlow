using Board.Core.Exceptions.Cards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Cards.Update
{
	public class UpdateCardHandler : IRequestHandler<UpdateCardCommand>
	{
		private readonly ICardRepository _cardRepository;

		public UpdateCardHandler(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		public async Task Handle(UpdateCardCommand request, CancellationToken cancellationToken)
		{
			var card = await _cardRepository.GetByIdAsync(request.id);

			if (card is null)
			{
				throw new CardNotFoundException(request.id);
			}

			card.SetDescription(request.description);
			card.SetDeadLine(request.deadLine);
			card.ChangeStatus(request.status);
			card.UpdateTitle(request.title);

			await _cardRepository.UpdateAsync(card, cancellationToken);
			await _cardRepository.SaveChangesAsync(cancellationToken);
		}
	}
}
