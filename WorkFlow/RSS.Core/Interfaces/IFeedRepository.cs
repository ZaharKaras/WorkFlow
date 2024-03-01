using RSS.Core.Entities;

namespace RSS.Core.Interfaces
{
	public interface IFeedRepository : IGenericRepository<Feed>
	{
		Task<IEnumerable<Feed>> GetByUserIdAsync(Guid userId, CancellationToken token = default);
	}
}
