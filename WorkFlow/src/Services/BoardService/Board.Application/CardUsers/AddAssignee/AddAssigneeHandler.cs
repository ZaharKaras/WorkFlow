using Board.Core.Entities;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.CardUsers.AddAssignee
{
	public class AddAssigneeHandler : IRequestHandler<AddAssigneeCommand>
	{
		private readonly ICardUserRepository _cardUserRepository;

		public AddAssigneeHandler(ICardUserRepository cardUserRepository)
		{
			_cardUserRepository = cardUserRepository;
		}

		public async Task Handle(AddAssigneeCommand request, CancellationToken cancellationToken)
		{
			var cardUser = new CardUser(
				Guid.NewGuid(),
				request.cardId,
				request.userId);

			await _cardUserRepository.AddAsync(cardUser, cancellationToken);
		}
	}
}
