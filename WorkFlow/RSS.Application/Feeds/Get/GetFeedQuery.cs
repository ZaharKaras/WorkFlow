using MediatR;
using RSS.Application.DTOs;

namespace RSS.Application.Feeds.Get;
public record GetFeedQuery(Guid id) : IRequest<IEnumerable<ItemDTO>>;
