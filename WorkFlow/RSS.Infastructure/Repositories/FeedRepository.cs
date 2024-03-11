using Microsoft.EntityFrameworkCore;
using RSS.Core.Entities;
using RSS.Core.Interfaces;

namespace RSS.Infastructure.Repositories
{
	public class FeedRepository 
		: GenericRepository<Feed>, IFeedRepository
	{
		public FeedRepository(AppDbContext context) 
			: base(context) { }

		public async Task<IEnumerable<Feed>> GetByUserIdAsync(Guid userId, CancellationToken token = default)
		{
			var feedIds = await _context.FeedsUsers
				.Where(feedUser => feedUser.UserId == userId)
				.Select(feedUser => feedUser.FeedId)
				.ToListAsync(token);

			return await _context.Feeds
				.Where(card => feedIds.Contains(card.Id))
				.ToListAsync(token);
		}
	}
}
