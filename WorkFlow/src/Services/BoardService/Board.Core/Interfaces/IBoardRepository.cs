using Board.Core.Entities;

namespace Board.Core.Interfaces
{
	public interface IBoardRepository : IGenericRepository<Entities.Board>
	{
		public Task<IEnumerable<Entities.Board>> GetByUserIdAsync(Guid userId, CancellationToken token = default);
	}
}
