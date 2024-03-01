using AutoMapper;
using MediatR;
using RSS.Core.Entities;
using RSS.Core.Interfaces;

namespace RSS.Application.Feeds.Create;

public class CreateFeedCommandHandler : IRequestHandler<CreateFeedCommand>
{
	private readonly IParseService _parser;
	private readonly IFeedRepository _feedRepository;
	private readonly IFeedUserRepository _feedUserRepository;
	private readonly IMapper _mapper;

	public CreateFeedCommandHandler(
		IParseService parser,
		IFeedRepository feedRepository,
		IMapper mapper,
		IFeedUserRepository feedUserRepository)
	{
		_parser = parser;
		_feedRepository = feedRepository;
		_mapper = mapper;
		_feedUserRepository = feedUserRepository;
	}

	public async Task Handle(CreateFeedCommand request, CancellationToken cancellationToken)
	{
		var synFeed = _parser.ParseFeedAsync(request.uri);

		var feed = _mapper.Map<Feed>(synFeed);

		await _feedRepository.AddAsync(feed, cancellationToken);
		await _feedUserRepository.AddAsync(new FeedUser(request.UserId, feed.Id));

		await _feedRepository.SaveChangesAsync(cancellationToken);
		await _feedUserRepository.SaveChangesAsync(cancellationToken);
	}
}