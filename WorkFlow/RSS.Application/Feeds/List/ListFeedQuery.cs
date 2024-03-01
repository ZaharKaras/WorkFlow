using MediatR;
using RSS.Application.DTOs;

namespace RSS.Application.Feeds.List;
public record ListFeedQuery(Guid userId) : IRequest<IEnumerable<FeedDTO>>;