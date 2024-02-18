using Board.Core.Entities;
using Board.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repositories
{
	public class BoardUserRepository
		: GenericRepository<BoardUser>, IBoardUserRepository
	{
		public BoardUserRepository(AppDbContext context) 
			: base(context) { }

		public async Task<IEnumerable<Guid>> GetByBoardIdAsync(Guid boardId, CancellationToken token = default)
		{
			return await _context.BoardsUsers
				.Where(boardUser => boardUser.BoardId == boardId)
				.Select(boardUser => boardUser.UserId)
				.ToListAsync(token);
		}
	}
}
