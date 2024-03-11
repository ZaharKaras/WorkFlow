using AutoMapper;
using MediatR;
using RSS.Application.DTOs;
using RSS.Application.Services.Interfaces;
using RSS.Core.Exceptions;
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
			var feed = await _feedRepository.GetByIdAsync(request.id);

			if (feed is null)
			{
				throw new FeedNotFoundException(request.id);
			}

			var items = _mapper.Map<IEnumerable<ItemDTO>>(
				_parseService.ParseItemAsync(feed!.Link));

			return items;
		}
	}
}
