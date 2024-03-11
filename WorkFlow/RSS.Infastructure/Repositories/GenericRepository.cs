using Microsoft.EntityFrameworkCore;
using RSS.Core.Interfaces;

namespace RSS.Infastructure.Repositories
{
	public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly AppDbContext _context;
		internal DbSet<T> _dbSet;

		public GenericRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}
		public async Task AddAsync(T entity, CancellationToken token = default)
		{
			await _dbSet.AddAsync(entity, token);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await GetByIdAsync(id);
			_dbSet.Remove(entity!);
		}

		public async Task<T?> GetByIdAsync(Guid id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task SaveChangesAsync(CancellationToken token = default)
		{
			await _context.SaveChangesAsync(token);
		}
	}
}
