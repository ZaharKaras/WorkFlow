using Board.Core.Exceptions.Cards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Cards.Delete
{
	public class DeleteCardHandler : IRequestHandler<DeleteCardCommand>
	{
		private readonly ICardRepository _cardRepository;

		public DeleteCardHandler(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		public async Task Handle(DeleteCardCommand request, CancellationToken cancellationToken)
		{
			var card = await _cardRepository.GetByIdAsync(request.cardId);

			if (card == null)
				throw new CardNotFoundException(request.cardId);

			await _cardRepository.DeleteAsync(request.cardId, cancellationToken);
		}
	}
}
