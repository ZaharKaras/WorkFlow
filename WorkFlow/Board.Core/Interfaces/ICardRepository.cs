using Board.Core.Entities;

namespace Board.Core.Interfaces
{
	public interface ICardRepository : IGenericRepository<Card>
	{
		public Task<IEnumerable<Card>> GetByBoardIdAsync(Guid boardId, CancellationToken token = default);
		public Task<IEnumerable<Card>> GetByUserIdAsync(Guid userId, CancellationToken token = default);
	}
}
