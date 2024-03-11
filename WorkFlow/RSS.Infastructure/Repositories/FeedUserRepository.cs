using RSS.Core.Entities;
using RSS.Core.Interfaces;

namespace RSS.Infastructure.Repositories
{
	public class FeedUserRepository : GenericRepository<FeedUser>, IFeedUserRepository
	{
		public FeedUserRepository(AppDbContext context)
			: base(context) { }
	}
}
