using Board.Core.Exceptions.Cards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.CardUsers.RemoveAssignee
{
	public class RemoveAssigneeHandler : IRequestHandler<RemoveAssigneeCommand>
	{
		private readonly ICardUserRepository _cardUserRepository;

		public RemoveAssigneeHandler(ICardUserRepository cardUserRepository)
		{
			_cardUserRepository = cardUserRepository;
		}

		public async Task Handle(RemoveAssigneeCommand request, CancellationToken cancellationToken)
		{
			var cardUser = await _cardUserRepository.GetByIdAsync(request.cardId);

			if (cardUser is null)
			{
				throw new CardNotFoundException(request.cardId);
			}

			await _cardUserRepository.DeleteAsync(cardUser.Id, cancellationToken);
			await _cardUserRepository.SaveChangesAsync(cancellationToken);
		}
	}
}
