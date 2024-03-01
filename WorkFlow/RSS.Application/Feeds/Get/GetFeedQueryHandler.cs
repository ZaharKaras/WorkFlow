using AutoMapper;
using MediatR;
using RSS.Application.DTOs;
using RSS.Core.Entities;
using RSS.Core.Interfaces;

namespace RSS.Application.Feeds.Get
{
	public class GetFeedQueryHandler : IRequestHandler<GetFeedQuery, IEnumerable<ItemDTO>>
	{
		private readonly IFeedRepository _feedRepository;
		private readonly IParseService _parseService;
		private readonly IMapper _mapper;

		public GetFeedQueryHandler(
			IFeedRepository feedRepository, 
			IMapper mapper, 
			IParseService parseService)
		{
			_feedRepository = feedRepository;
			_mapper = mapper;
			_parseService = parseService;
		}

		public async Task<IEnumerable<ItemDTO>> Handle(GetFeedQuery request, CancellationToken cancellationToken)
		{
			var feed = await _feedRepository.GetByIdAsync(request.id, cancellationToken);

			var items = _mapper.Map<IEnumerable<Item>>(
				_parseService.ParseItemAsync(feed!.Link));

			var itemsDTO = _mapper.Map<IEnumerable<ItemDTO>>(items);

			return itemsDTO;
		}
	}
}
