using MediatR;

namespace RSS.Application.Feeds.Create;

public record CreateFeedCommand(string uri, Guid UserId) : IRequest;
