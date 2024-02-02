using Board.Core.Interfaces;

namespace Board.Infrastructure.Repositories
{
	public class BoardRepository
		: GenericRepository<Core.Entities.Board>, IBoardRepository
	{
		public BoardRepository(AppDbContext dbContext) 
			: base(dbContext) { }

		public Task<IEnumerable<Core.Entities.Board>> GetByUserIdAsync(Guid userId, CancellationToken token = default)
		{
			var boardIds = _context.BoardsUsers
				.Where(bu => bu.UserId == userId)
				.Select(bu => bu.BoardId)
				.ToList();

			var boards = _context.Boards
				.Where(board => boardIds.Contains(board.Id))
				.AsEnumerable();

			return Task.FromResult(boards);
		}
	}
}
