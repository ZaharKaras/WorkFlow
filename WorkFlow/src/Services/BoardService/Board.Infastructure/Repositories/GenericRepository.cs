using Board.Core.Base;
using Board.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repositories
{
	public abstract class GenericRepository<T> : IGenericRepository<T> where T : Entity
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
			await _dbSet.AddAsync(entity);
		}

		public async Task DeleteAsync(Guid id, CancellationToken token = default)
		{
			var entity = await GetByIdAsync(id);
			_dbSet.Remove(entity!);
		}

		public async Task<T?> GetByIdAsync(Guid id, CancellationToken token = default)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task SaveChangesAsync(CancellationToken token = default)
		{
			await _context.SaveChangesAsync();
		}

		public Task UpdateAsync(T entity, CancellationToken token = default)
		{
			_dbSet.Update(entity);

			return Task.CompletedTask;
		}
	}
}
