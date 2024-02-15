using Board.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repositories
{
	public class BoardRepository
		: GenericRepository<Core.Entities.Board>, IBoardRepository
	{
		public BoardRepository(AppDbContext dbContext) 
			: base(dbContext) { }

		public async Task<IEnumerable<Core.Entities.Board>> GetByUserIdAsync(Guid userId, CancellationToken token = default)
		{
			var boardIds = await _context.BoardsUsers
				.Where(boardUser => boardUser.UserId == userId)
				.Select(boardUser => boardUser.BoardId)
				.ToListAsync(token);

			var boards = await _context.Boards
				.Where(board => boardIds.Contains(board.Id))
				.ToListAsync(token);

			return boards;
		}
	}
}
