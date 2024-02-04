using AutoMapper;
using Board.Core.Entities;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Cards.Create
{
	public class CreateCardHandler : IRequestHandler<CreateCardCommand>
	{
		private readonly ICardRepository _cardRepository;
		private readonly IMapper _mapper;

		public CreateCardHandler(ICardRepository cardRepository, IMapper mapper)
		{
			_cardRepository = cardRepository;
			_mapper = mapper;
		}

		public async Task Handle(CreateCardCommand request, CancellationToken cancellationToken)
		{
			var card = _mapper.Map<Card>(request);

			await _cardRepository.AddAsync(card, cancellationToken);
			await _cardRepository.SaveChangesAsync(cancellationToken);
		}
	}
}
