using Board.Core.Entities;
using Board.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repositories
{
	public class CardUserRepository
		: GenericRepository<CardUser>, ICardUserRepository
	{
		public CardUserRepository(AppDbContext context) 
			: base(context) { }

		public async Task<IEnumerable<Guid>> GetByCardIdAsync(Guid cardId, CancellationToken token = default)
		{
			return await _context.CardUsers
				.Where(cardUser => cardUser.CardId == cardId)
				.Select(cardUser => cardUser.UserId)
				.ToListAsync(token);
		}
	}
}
