using Board.Core.Entities;

namespace Board.Core.Interfaces
{
	public interface IBoardUserRepository : IGenericRepository<BoardUser>
	{
		public Task<IEnumerable<Guid>> GetByBoardIdAsync(Guid boardId, CancellationToken token = default);
	}
}
