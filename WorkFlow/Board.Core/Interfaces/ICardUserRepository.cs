using Board.Core.Entities;

namespace Board.Core.Interfaces
{
	public interface ICardUserRepository : IGenericRepository<CardUser>
	{
		public Task<IEnumerable<Guid>> GetByCardIdAsync(Guid cardId, CancellationToken token = default);
	}
}
