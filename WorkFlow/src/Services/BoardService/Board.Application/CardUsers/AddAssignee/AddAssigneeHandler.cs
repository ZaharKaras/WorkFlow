using AutoMapper;
using Board.Core.Entities;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.CardUsers.AddAssignee
{
	public class AddAssigneeHandler : IRequestHandler<AddAssigneeCommand>
	{
		private readonly ICardUserRepository _cardUserRepository;
		private readonly IMapper _mapper;

		public AddAssigneeHandler(ICardUserRepository cardUserRepository, IMapper mapper)
		{
			_cardUserRepository = cardUserRepository;
			_mapper = mapper;
		}

		public async Task Handle(AddAssigneeCommand request, CancellationToken cancellationToken)
		{
			var cardUser = _mapper.Map<CardUser>(request);

			await _cardUserRepository.AddAsync(cardUser, cancellationToken);
		}
	}
}
