using Board.Core.Entities;
using Board.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repositories
{
	public class CardRepository
		: GenericRepository<Card>, ICardRepository
	{
		public CardRepository(AppDbContext context) 
			: base(context) { }

		public async Task<IEnumerable<Card>> GetByBoardIdAsync(Guid boardId, CancellationToken token = default)
		{
			return await _context.Cards
				.Where(card => card.BoardId == boardId)
				.ToListAsync(token);
		}

		public async Task<IEnumerable<Card>> GetByUserIdAsync(Guid userId, CancellationToken token = default)
		{
			var cardIds = await _context.CardUsers
				.Where(cardUser => cardUser.UserId == userId)
				.Select(cardUser => cardUser.CardId)
				.ToListAsync(token);

			return await _context.Cards
				.Where(card => cardIds.Contains(card.Id))
				.ToListAsync(token);
		}
	}
}
