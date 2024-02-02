using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.CardUsers.GetAssignees
{
	public class GetAssigneesHandler : IRequestHandler<GetAssigneesQuery, IEnumerable<Guid>>
	{
		private readonly ICardUserRepository _cardUserRepository;

		public GetAssigneesHandler(ICardUserRepository cardUserRepository)
		{
			_cardUserRepository = cardUserRepository;
		}

		public async Task<IEnumerable<Guid>> Handle(GetAssigneesQuery request, CancellationToken cancellationToken)
		{
			return await _cardUserRepository.GetByCardIdAsync(request.cardId, cancellationToken);
		}
	}
}
