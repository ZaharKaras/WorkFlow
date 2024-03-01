using AutoMapper;
using MediatR;
using RSS.Application.DTOs;
using RSS.Core.Interfaces;

namespace RSS.Application.Feeds.List
{
	public class ListFeedQueryHandler : IRequestHandler<ListFeedQuery, IEnumerable<FeedDTO>>
	{
		private readonly IFeedRepository _feedRepository;
		private readonly IMapper _mapper;

		public ListFeedQueryHandler(IFeedRepository feedRepository, IMapper mapper)
		{
			_feedRepository = feedRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<FeedDTO>> Handle(ListFeedQuery request, CancellationToken cancellationToken)
		{
			var feeds = _mapper.Map<IEnumerable<FeedDTO>>(
				await _feedRepository.GetByUserIdAsync(request.userId));

			return feeds;
		}
	}
}
