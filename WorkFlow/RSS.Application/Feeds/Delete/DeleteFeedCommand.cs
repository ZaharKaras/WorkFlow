using MediatR;

namespace RSS.Application.Feeds.Delete;
public record DeleteFeedCommand(Guid id) : IRequest;
