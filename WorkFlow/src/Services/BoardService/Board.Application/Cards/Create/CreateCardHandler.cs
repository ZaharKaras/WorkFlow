using Board.Core.Entities;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Cards.Create
{
	public class CreateCardHandler : IRequestHandler<CreateCardCommand>
	{
		private readonly ICardRepository _cardRepository;

		public CreateCardHandler(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		public async Task Handle(CreateCardCommand request, CancellationToken cancellationToken)
		{
			var card = new Card(
				Guid.NewGuid(),
				request.title,
				request.boardId);

			await _cardRepository.AddAsync(card, cancellationToken);
		}
	}
}
