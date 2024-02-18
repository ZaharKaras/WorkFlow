using AutoMapper;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.CardUsers.GetAssignees
{
	public class GetAssigneesHandler : IRequestHandler<GetAssigneesQuery, IEnumerable<Guid>>
	{
		private readonly ICardUserRepository _cardUserRepository;
		private readonly IMapper _mapper;

		public GetAssigneesHandler(ICardUserRepository cardUserRepository, IMapper mapper)
		{
			_cardUserRepository = cardUserRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<Guid>> Handle(GetAssigneesQuery request, CancellationToken cancellationToken)
		{
			var result = _mapper.Map<IEnumerable<Guid>>(
				await _cardUserRepository.GetByCardIdAsync(request.cardId, cancellationToken));

			return result;
		}
	}
}
