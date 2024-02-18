using AutoMapper;
using Board.Application.Cards.DTOs;
using Board.Core.Interfaces;
using MediatR;

namespace Board.Application.Cards.List
{
    public class ListCardHandler : IRequestHandler<ListCardQuery, IEnumerable<CardListDTO>>
	{
		private readonly ICardRepository _cardRepository;
		private readonly IMapper _mapper;

		public ListCardHandler(ICardRepository cardRepository, IMapper mapper)
		{
			_cardRepository = cardRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<CardListDTO>> Handle(ListCardQuery request, CancellationToken cancellationToken)
		{
			var cards = await _cardRepository.GetByBoardIdAsync(request.boardId);

			var cardDTOs = _mapper.Map<IEnumerable<CardListDTO>>(cards);

			return cardDTOs;
		}
	}
}
