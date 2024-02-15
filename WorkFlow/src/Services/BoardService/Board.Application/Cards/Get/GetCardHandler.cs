using AutoMapper;
using Board.Application.Boards;
using Board.Application.Cards.DTOs;
using Board.Core.Exceptions.Cards;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Cards.Get
{
    public class GetCardHandler : IRequestHandler<GetCardQuery, CardDTO>
	{
		private readonly ICardRepository _cardRepository;
		private readonly IMapper _mapper;

		public GetCardHandler(ICardRepository cardRepository, IMapper mapper)
		{
			_cardRepository = cardRepository;
			_mapper = mapper;
		}

		public async Task<CardDTO> Handle(GetCardQuery request, CancellationToken cancellationToken)
		{
			var card = await _cardRepository.GetByIdAsync(request.cardId);

			if (card is null)
			{
				throw new CardNotFoundException(request.cardId);
			}

			return _mapper.Map<CardDTO>(card);
		}
	}
}
